using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace SystemAdmin.Common.Excel
{
    public class ExcelStyleHelper
    {
        /// <summary>
        /// Excel表格导出标准样式
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="headers"></param>
        /// <param name="rowCount"></param>
        /// <param name="enableFilter"></param>
        public static void ApplyStandardStyle(ExcelWorksheet ws, string[] headers, int rowCount, bool enableFilter = true)
        {
            if (ws == null || headers == null || headers.Length == 0)
                return;

            int colCount = headers.Length;

            // 1. 表头
            for (int i = 0; i < colCount; i++)
            {
                ws.Cells[1, i + 1].Value = headers[i];
            }

            var headerRange = ws.Cells[1, 1, 1, colCount];
            headerRange.Style.Font.Name = "微软雅黑";
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Font.Color.SetColor(Color.Black);
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);

            ws.Row(1).Height = 25;

            // 2. 数据区样式
            if (rowCount > 1)
            {
                var dataRange = ws.Cells[2, 1, rowCount, colCount];
                dataRange.Style.Font.Name = "微软雅黑";
                dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                dataRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }

            // 3. 边框
            var allRange = ws.Cells[1, 1, rowCount, colCount];
            var border = allRange.Style.Border;

            border.Top.Style =
            border.Bottom.Style =
            border.Left.Style =
            border.Right.Style = ExcelBorderStyle.Thin;

            // 4. 视图 & 筛选
            ws.View.FreezePanes(2, 1);

            if (enableFilter)
            {
                headerRange.AutoFilter = true;
            }

            // 5. 列宽
            if (ws.Dimension != null)
            {
                ws.Cells[ws.Dimension.Address].AutoFitColumns();
            }
        }
    }
}
