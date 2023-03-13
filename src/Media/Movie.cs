namespace Lab.Media;

public class Movie : Media {
    public string Director {get; set;}
    public TimeSpan RunningTime {get; set;}

    public Movie() {
        Director = "Unassigned";
        RunningTime = new TimeSpan(-1);
    }

    public override string Display() {
        return
            $"Id: {MediaId}\nTitle: {Title}\nDirector: {Director}\nRun time: {RunningTime}\nGenres: {string.Join(", ", Genres)}\n";
    }
}