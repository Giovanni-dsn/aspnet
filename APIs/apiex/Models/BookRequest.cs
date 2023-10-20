using Models;
public record BookRequest (
    int authorId,
    string code,
    string title,
    string category
);