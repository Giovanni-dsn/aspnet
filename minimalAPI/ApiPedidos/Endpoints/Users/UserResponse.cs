namespace ApiPedidos.Endpoints.Users;

public record UserResponse(
    string Email,
    string Phone,
    string? Name
);