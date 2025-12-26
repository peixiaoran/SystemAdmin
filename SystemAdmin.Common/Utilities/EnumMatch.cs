using System;
using System.Collections.Generic;
using System.Text;

namespace SystemAdmin.Common.Utilities
{
    /// <summary>
    /// 枚举字符串匹配工具类
    ///
    /// 设计目标：
    /// 1. 数据库存 string，代码中使用 enum / [Flags] enum
    /// 2. 统一 string → enum 的解析逻辑
    /// 3. 避免误用 C# 自带的 Contains / LINQ Contains
    /// 4. 语义清晰，工程安全
    /// </summary>
    public static class EnumMatchExtensions
    {
        #region 基础解析

        /// <summary>
        /// 将字符串解析为枚举（支持 [Flags]，支持 "A,B" / "A, B"）
        /// </summary>
        public static bool TryParseEnum<TEnum>(this string? value, out TEnum result)
            where TEnum : struct, Enum
        {
            result = default;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            // Flags 枚举：支持逗号分隔
            if (IsFlagsEnum<TEnum>())
            {
                var parts = value.Split(
                    ',',
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                long combined = 0;

                foreach (var part in parts)
                {
                    if (!Enum.TryParse<TEnum>(part, true, out var flag))
                        return false;

                    combined |= Convert.ToInt64(flag);
                }

                result = (TEnum)Enum.ToObject(typeof(TEnum), combined);
                return true;
            }

            // 普通枚举
            return Enum.TryParse(value.Trim(), true, out result);
        }

        #endregion

        #region 普通枚举（非 Flags）

        /// <summary>
        /// 普通枚举的完全匹配（适用于非 [Flags] 枚举）
        /// 示例： "End".MatchEnum(StepType.End)
        /// </summary>
        public static bool MatchEnum<TEnum>(this string? value, TEnum expected)
            where TEnum : struct, Enum
        {
            if (!value.TryParseEnum<TEnum>(out var actual))
                return false;

            return EqualityComparer<TEnum>.Default.Equals(actual, expected);
        }

        #endregion

        #region Flags 枚举判断

        /// <summary>
        /// Flags 枚举：是否包含指定的单个标志位
        /// 示例： "Org,User".HasFlagValue(StepAssignmentOp.Org)
        /// </summary>
        public static bool HasFlagValue<TEnum>(this string? value, TEnum flag)
            where TEnum : struct, Enum
        {
            EnsureFlagsEnum<TEnum>();

            if (!value.TryParseEnum<TEnum>(out var actual))
                return false;

            var a = Convert.ToInt64(actual);
            var f = Convert.ToInt64(flag);

            return (a & f) != 0;
        }

        /// <summary>
        /// Flags 枚举：是否同时包含所有指定标志位
        /// 示例： "Org,User".HasAllFlagValues(Org | User)
        /// </summary>
        public static bool HasAllFlagValues<TEnum>(this string? value, TEnum flags)
            where TEnum : struct, Enum
        {
            EnsureFlagsEnum<TEnum>();

            if (!value.TryParseEnum<TEnum>(out var actual))
                return false;

            var a = Convert.ToInt64(actual);
            var f = Convert.ToInt64(flags);

            return (a & f) == f;
        }

        /// <summary>
        /// Flags 枚举：是否命中任意一个标志位
        /// 示例： "DeptUser".HasAnyFlagValue(DeptUser | Rule)
        /// </summary>
        public static bool HasAnyFlagValue<TEnum>(this string? value, TEnum flags)
            where TEnum : struct, Enum
        {
            EnsureFlagsEnum<TEnum>();

            if (!value.TryParseEnum<TEnum>(out var actual))
                return false;

            var a = Convert.ToInt64(actual);
            var f = Convert.ToInt64(flags);

            return (a & f) != 0;
        }

        /// <summary>
        /// Flags 枚举：是否与指定标志位完全一致（不多不少）
        /// 示例： "Org,User".ExactFlagMatch(Org | User)
        /// </summary>
        public static bool ExactFlagMatch<TEnum>(this string? value, TEnum flags)
            where TEnum : struct, Enum
        {
            EnsureFlagsEnum<TEnum>();

            if (!value.TryParseEnum<TEnum>(out var actual))
                return false;

            return Convert.ToInt64(actual) == Convert.ToInt64(flags);
        }

        #endregion

        #region 私有辅助方法

        private static bool IsFlagsEnum<TEnum>()
            where TEnum : struct, Enum
        {
            return typeof(TEnum).IsDefined(typeof(FlagsAttribute), false);
        }

        private static void EnsureFlagsEnum<TEnum>()
            where TEnum : struct, Enum
        {
            if (!IsFlagsEnum<TEnum>())
                throw new InvalidOperationException(
                    $"{typeof(TEnum).Name} 不是 [Flags] 枚举，请使用 MatchEnum 方法。");
        }

        #endregion
    }
}
