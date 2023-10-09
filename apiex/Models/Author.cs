namespace Models {
    public class Author {

        public Author(string name, int idade)
        {
            Name = name;
            Idade = idade;
        }
        public int Id {get; set;}
        public string Name { get; set; }
        public int Idade {get; set;}

    }
}