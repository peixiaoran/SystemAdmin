using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace SystemAdmin.Common.Excel
{
    public class ExcelStyleHelper
    {
        /// <summary>
        /// 应用标准导出样式
        /// </summary>
        /// <param name="ws">Worksheet</param>
        /// <param name="headers">列头名称</param>
        /// <param name="rowCount">总行数（包含表头）</param>
        /// <param name="enableFilter">是否启用筛选</param>
        public static void ApplyStandardStyle(ExcelWorksheet ws, string[] headers, int rowCount, bool enableFilter = true)
        {
            int colCount = headers.Length;

            // 表头
            for (int i = 0; i < colCount; i++)
            {
                ws.Cells[1, i + 1].Value = headers[i];
            }

            using (var headerRange = ws.Cells[1, 1, 1, colCount])
            {
                headerRange.Style.Font.Name = "微软雅黑";
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Font.Color.SetColor(Color.Black);
                headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }

            ws.Row(1).Height = 25;

            // 数据区样式
            if (rowCount > 1)
            {
                using (var dataRange = ws.Cells[2, 1, rowCount, colCount])
                {
                    dataRange.Style.Font.Name = "微软雅黑";
                    dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    dataRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    dataRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    dataRange.Style.Fill.BackgroundColor.SetColor(Color.White);
                }
            }

            // 边框
            using (var borderRange = ws.Cells[1, 1, rowCount, colCount])
            {
                borderRange.Style.Border.Top.Style =
                borderRange.Style.Border.Bottom.Style =
                borderRange.Style.Border.Left.Style =
                borderRange.Style.Border.Right.Style =
                    ExcelBorderStyle.Thin;
            }

            // 其他通用设置
            ws.Cells[ws.Dimension.Address].AutoFitColumns();
            ws.View.FreezePanes(2, 1);

            if (enableFilter)
            {
                ws.Cells[1, 1, 1, colCount].AutoFilter = true;
            }
        }
    }
}
