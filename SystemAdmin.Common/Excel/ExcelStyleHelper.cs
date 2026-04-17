using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.Drawing;

namespace SystemAdmin.Common.Excel
{
    public class ExcelStyleHelper
    {
        /// <summary>
        /// Excel表格数据填充-标准
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="dt"></param>
        /// <param name="headers"></param>
        /// <param name="enableFilter"></param>
        public static void ApplyStandardStyle(ExcelWorksheet ws, DataTable dt, Dictionary<string, string> headers, bool enableFilter, Dictionary<string, ExcelColumnConfig> columnConfigs)
        {
            if (ws == null || dt == null || headers == null || headers.Count == 0)
                return;

            int colIndex = 1;
            int colCount = headers.Count;

            // 1. 写列头
            foreach (var header in headers)
            {
                ws.Cells[1, colIndex].Value = header.Value;
                colIndex++;
            }

            // 2. 先套列格式（必须在写数据之前，Text格式 @ 才有效）
            if (columnConfigs != null)
            {
                colIndex = 1;
                foreach (var header in headers)
                {
                    if (columnConfigs.TryGetValue(header.Key, out var config)
                        && !string.IsNullOrEmpty(config.Format))
                    {
                        ws.Cells[2, colIndex, 1048576, colIndex]
                            .Style.Numberformat.Format = config.Format;
                    }
                    colIndex++;
                }
            }

            // 3. 写数据
            int rowIndex = 2;
            foreach (DataRow row in dt.Rows)
            {
                colIndex = 1;
                foreach (var header in headers)
                {
                    if (dt.Columns.Contains(header.Key))
                        ws.Cells[rowIndex, colIndex].Value = row[header.Key];
                    colIndex++;
                }
                rowIndex++;
            }

            int rowCount = dt.Rows.Count + 1;

            // 4. 表头样式
            var headerRange = ws.Cells[1, 1, 1, colCount];

            headerRange.Style.Font.Name = "微软雅黑";
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Font.Color.SetColor(Color.Black);
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);

            ws.Row(1).Height = 25;

            // 5. 数据对齐样式
            if (rowCount > 1)
            {
                colIndex = 1;
                foreach (var header in headers)
                {
                    var colRange = ws.Cells[2, colIndex, rowCount, colIndex];

                    colRange.Style.Font.Name = "微软雅黑";
                    colRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    // 有指定对齐就用指定的，否则预设居中
                    var alignment = columnConfigs != null
                        && columnConfigs.TryGetValue(header.Key, out var config)
                        && config.Alignment.HasValue
                            ? config.Alignment.Value
                            : ExcelHorizontalAlignment.Center;

                    colRange.Style.HorizontalAlignment = alignment;
                    colIndex++;
                }
            }

            // 6. 边框
            var allRange = ws.Cells[1, 1, rowCount, colCount];
            var border = allRange.Style.Border;

            border.Top.Style =
            border.Bottom.Style =
            border.Left.Style =
            border.Right.Style = ExcelBorderStyle.Thin;

            // 7. 冻结
            ws.View.FreezePanes(2, 1);

            // 8. 筛选
            if (enableFilter)
                headerRange.AutoFilter = true;

            // 9. 自动列宽
            if (ws.Dimension != null)
                ws.Cells[ws.Dimension.Address].AutoFitColumns();
        }
    }
}
