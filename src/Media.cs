namespace Lab; 

public class Media {
    // public properties
    public ulong MediaId { get; set; }
    public string Title { get; set; }
    public List<string> Genres { get; set; }

    // constructor
    public Media()
    {
        MediaId = ulong.MaxValue;
        Title = "Unassigned";
        Genres = new();
    }

    // public method
    public string Display()
    {
        return $"Id: {MediaId}\nTitle: {Title}\nGenres: {string.Join(", ", Genres)}\n";
    }
}