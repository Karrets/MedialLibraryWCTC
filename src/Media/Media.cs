namespace Lab.Media; 

public abstract class Media {
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
    public virtual string Display()
    {
        return $"Id: {MediaId}\nTitle: {Title}\nGenres: {string.Join(", ", Genres)}\n";
    }

    public override string ToString() {
        return $"{MediaId},{Title},{string.Join("|", Genres)}";
    }
}