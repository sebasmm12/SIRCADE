namespace SIRCADE.ApiCore.Controllers.Reports.Responses;

public record FrequentlyUserByReservationResponse(
    string UserName,
    IEnumerable<TypeQuantity> SportFieldTypeQuantities);