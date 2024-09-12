using ClosedXML.Excel;
using SIRCADE.ApiCore.Controllers.Reports.Responses;

namespace SIRCADE.ApiCore.Controllers.Reports.Services.Imp;

public class ExcelFilesService : IExcelFilesService
{
    public string Generate(string reportName, string labelTitle, IEnumerable<ReportInfoResponse> data)
    {
        using var workbook = new XLWorkbook();

        var sheet = workbook.Worksheets.Add(reportName);

        SetTitles(sheet, labelTitle, data);

        SetData(sheet, data);

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return Convert.ToBase64String(stream.ToArray());
    }


    private static void SetTitles(IXLWorksheet sheet, string labelTitle, IEnumerable<ReportInfoResponse> data)
    {
        sheet.Cell(1, 1).Value = labelTitle;

        var typeQuantities = data
                                .First()
                                .TypeQuantities
                                .ToList();

        for (var i = 1; i <= typeQuantities.Count; i++)
        {
            sheet.Cell(1, i + 1).Value = typeQuantities.ElementAt(i).Name;
        }
    }

    private static void SetData(IXLWorksheet sheet, IEnumerable<ReportInfoResponse> data)
    {
        var row = 2;

        foreach (var reportInfoResponse in data)
        {
            sheet.Cell(row, 1).Value = reportInfoResponse.Label;

            for (var i = 0; i < reportInfoResponse.TypeQuantities.Count(); i++)
            {
                sheet.Cell(row, i + 2).Value = reportInfoResponse.TypeQuantities.ElementAt(i).Quantity;
            }

            row++;
        }
    }
}