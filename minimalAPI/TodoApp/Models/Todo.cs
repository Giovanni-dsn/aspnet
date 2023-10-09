public class Todo
{
    public Todo(string title)
    {
        Title = title;
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Done { get; set; } = false;
    public DateTime CreatedOn { get; set; } = DateTime.Now.Date;
}