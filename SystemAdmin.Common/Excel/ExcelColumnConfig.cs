using OfficeOpenXml.Style;

namespace SystemAdmin.Common.Excel
{
    /// <summary>
    /// Excel 列属性配置类
    /// </summary>
    public class ExcelColumnConfig
    {
        /// <summary>
        /// Excel 格式字符串
        /// </summary>
        public string Format { get; set; } = string.Empty;

        /// <summary>
        /// 可覆盖默认对齐
        /// </summary>
        public ExcelHorizontalAlignment? Alignment { get; set; }

        // 预设常用格式
        public static ExcelColumnConfig Text => new() { Format = "@" };
        public static ExcelColumnConfig Number => new() { Format = "#,##0" };
        public static ExcelColumnConfig Decimal => new() { Format = "#,##0.00" };
        public static ExcelColumnConfig Date => new() { Format = "yyyy/MM/dd" };
        public static ExcelColumnConfig DateTime => new() { Format = "yyyy/MM/dd HH:mm" };
        public static ExcelColumnConfig Percent => new() { Format = "0.00%" };
    }
}
