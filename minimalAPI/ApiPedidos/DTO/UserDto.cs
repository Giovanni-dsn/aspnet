namespace ApiPedidos.Dto;

public record UserRequestDto(string Email, string Password,
    string Phone, string Name);

public record UserResponseDto(string Email, string Phone, string? Name);

public record LoginRequestDto(string Email, string Password);