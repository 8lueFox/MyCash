namespace MyCash.Wallets.Application.DTO;

public record UserDto(string UserPackage);

public record UserBusDto(Guid Id, string UserPackage);