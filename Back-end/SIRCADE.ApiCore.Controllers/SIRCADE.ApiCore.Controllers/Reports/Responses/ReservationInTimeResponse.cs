namespace SIRCADE.ApiCore.Controllers.Reports.Responses;

public record ReservationInTimeResponse(string State, IEnumerable<TypeQuantity> TypeQuantities);