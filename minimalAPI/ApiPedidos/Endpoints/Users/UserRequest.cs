namespace ApiPedidos.Endpoints.Users;

public record UserRequest(
    string Email,
    string Password,
    string Phone,
    string Name
);