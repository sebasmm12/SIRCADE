namespace SIRCADE.ApiCore.Controllers.Reports.Responses;

public record ReportInfoResponse(string Label, IEnumerable<TypeQuantity> TypeQuantities);