using SIRCADE.ApiCore.Controllers.Reports.Responses;

namespace SIRCADE.ApiCore.Controllers.Reports.Services;

public interface IExcelFilesService
{
    string Generate(string reportName, string labelTitle, IEnumerable<ReportInfoResponse> data);
}