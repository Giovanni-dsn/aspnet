using Data;
using Microsoft.EntityFrameworkCore;

namespace Models {
    public class Book {

        public Book(int authorId, string code, string title, string category)
        {
            AuthorId = authorId;
            Code = code;
            Title = title;
            Category = category;
        }
        public int Id { get; set; }

        public int AuthorId {get; set;}
        public string Code {get; set;}
        public string Title {get; set;}

        public Author? Autor {get; set;}
        public string Category {get; set;}

    }
}