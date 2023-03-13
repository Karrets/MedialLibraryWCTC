namespace Lab.Media;

using NLog;

public class MovieFile {
    // public property
    public string filePath {get; set;}
    public List<Movie> Movies {get; set;}

    private static readonly Logger logger = LogManager
        .LoadConfiguration(Directory.GetCurrentDirectory() + "/nlog.conf.xml").GetCurrentClassLogger();

    // constructor is a special method that is invoked
    // when an instance of a class is created
    public MovieFile(string movieFilePath) {
        filePath = movieFilePath;
        Movies = new List<Movie>();

        // to populate the list with data, read from the data file
        try {
            var sr = new StreamReader(filePath);
            while(!sr.EndOfStream) {
                // create instance of Movie class
                var movie = new Movie();
                string line = sr.ReadLine();
                // first look for quote(") in string
                // this indicates a comma(,) in movie title
                int idx = line.IndexOf('"');
                if(idx == -1) {
                    // no quote = no comma in movie title
                    // movie details are separated with comma(,)
                    string[] movieDetails = line.Split(',');
                    movie.MediaId = UInt64.Parse(movieDetails[0]);
                    movie.Title = movieDetails[1];
                    movie.Genres = movieDetails[2].Split('|').ToList();
                    movie.Director = movieDetails[3];
                    movie.RunningTime = TimeSpan.Parse(movieDetails[4]);
                } else {
                    // quote = comma or quotes in movie title
                    // extract the movieId
                    movie.MediaId = UInt64.Parse(line.Substring(0, idx - 1));
                    // remove movieId and first comma from string
                    line = line.Substring(idx);
                    // find the last quote
                    idx = line.LastIndexOf('"');
                    // extract title
                    movie.Title = line.Substring(0, idx + 1);
                    // remove title and next comma from the string
                    line = line.Substring(idx + 2);
                    // split the remaining string based on commas
                    string[] details = line.Split(',');
                    // the first item in the array should be genres 
                    movie.Genres = details[0].Split('|').ToList();
                    // if there is another item in the array it should be director
                    movie.Director = details[1];
                    // if there is another item in the array it should be run time
                    movie.RunningTime = TimeSpan.Parse(details[2]);
                }

                Movies.Add(movie);
            }

            // close file when done
            sr.Close();
            logger.Info("Movies in file {Count}", Movies.Count);
        } catch(Exception ex) {
            logger.Error(ex.Message);
        }
    }

    // public method
    public bool isUniqueTitle(string title) {
        if(Movies.ConvertAll(m => m.Title.ToLower()).Contains(title.ToLower())) {
            logger.Info("Duplicate movie title {Title}", title);
            return false;
        }

        return true;
    }

    public void AddMovie(Movie movie) {
        try {
            // first generate movie id
            movie.MediaId = Movies.Max(m => m.MediaId) + 1;
            // if title contains a comma, wrap it in quotes
            string title = movie.Title.IndexOf(',') != -1 || movie.Title.IndexOf('"') != -1
                ? $"\"{movie.Title}\""
                : movie.Title;
            var sw = new StreamWriter(filePath, true);
            // write movie data to file
            sw.WriteLine(
                $"{movie.MediaId},{title},{string.Join("|", movie.Genres)},{movie.Director},{movie.RunningTime}");
            sw.Close();
            // add movie details to List
            Movies.Add(movie);
            // log transaction
            logger.Info("Media id {Id} added", movie.MediaId);
        } catch(Exception ex) {
            logger.Error(ex.Message);
        }
    }
}