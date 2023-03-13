namespace Lab.Media; 

public class Album : Media
{
    public string Artist { get; set; }
    public string RecordLabel { get; set; }

    public Album() {
        Artist = "Unassigned";
        RecordLabel = "Unassigned";
    }

    public override string Display()
    {
        return $"Id: {MediaId}\nTitle: {Title}\nArtist: {Artist}\nLabel: {RecordLabel}\nGenres: {string.Join(", ", Genres)}\n";
    }
}