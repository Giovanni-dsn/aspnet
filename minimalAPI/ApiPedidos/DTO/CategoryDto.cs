namespace ApiPedidos.Dto;

public class CategoryResponseDto
{
    public Guid Id { get; set; }
    public string? Name;
    public bool Active;
};

public record CategoryRequestDto(string Name, bool Active);