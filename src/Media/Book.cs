namespace Lab.Media; 

public class Book : Media
{
    public string Author { get; set; }
    public uint PageCount { get; set; }
    public string Publisher { get; set; }

    public Book() {
        Author = "Unassigned";
        PageCount = uint.MaxValue;
        Publisher = "Unassigned";
    }
    
    public override string Display()
    {
        return $"Id: {MediaId}\nTitle: {Title}\nAuthor: {Author}\nPages: {PageCount}\nPublisher: {Publisher}\nGenres: {string.Join(", ", Genres)}\n";
    }
}