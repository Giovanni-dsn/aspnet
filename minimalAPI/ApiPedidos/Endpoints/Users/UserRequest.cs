namespace ApiPedidos.Endpoints.Users;

public record UserRequest(
    string email,
    string password,
    string phone
);