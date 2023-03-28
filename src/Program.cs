using Lab.Media;

namespace Lab;

using NLog;

internal abstract class Program {
    private static readonly string Path = Directory.GetCurrentDirectory() + "/nlog.conf.xml";
    private static readonly Logger Logger = LogManager.LoadConfiguration(Path).GetCurrentClassLogger();

    public static void Main(string[] args) {
        Logger.Info("Program started");

        string scrubbedFile = FileScrubber.ScrubMovies();
        Logger.Info(scrubbedFile);


        bool run = true;
        while(run) {
            Console.WriteLine("1) Add Movie\n" +
                              "2) Display All Movies\n" +
                              " ) Enter To Quit\n");
            string uInput = Console.ReadLine() ?? "_";

            switch(uInput) {
                case "1":
                    AddMovie();
                    break;
                case "2":
                    ReadMovies();
                    break;
                default:
                    run = false;
                    break;
            }
        }

        Logger.Info("Program ended.");
    }

    private static void ReadMovies() {
        using Stream s = new MemoryStream();
        FileScrubber.ScrubMovies(s);
        s.Position = 0;
        using StreamReader sr = new(s);

        while(!sr.EndOfStream) {
            Console.WriteLine(sr.ReadLine());
        }
    }

    private static void AddMovie() {
        using var sw = FileScrubber.WriteHead();

        ulong mediaId;

        string? input = null;
        while(!ulong.TryParse(input, out mediaId)) {
            Console.WriteLine("Enter a media id (numeric): ");
            input = Console.ReadLine();
        }

        Console.WriteLine("Enter a title: ");
        string title = Console.ReadLine() ?? "";
        
        Console.WriteLine("Enter a comma seperated list of genres: ");
        string[] genres = (Console.ReadLine() ?? "").Split()
            .AsParallel()
            .Select(i => i.Trim().Replace(",", null))
            .ToArray();
        
        Console.WriteLine("Enter a director: ");
        string director = Console.ReadLine() ?? "";

        input = null;
        int timespan;
        while(!int.TryParse(input, out timespan)) {
            Console.WriteLine("Enter a runtime in minutes (numeric): ");
            input = Console.ReadLine();
        }

        var runtime = new TimeSpan(0, timespan, 0);
        
        sw.WriteLine(new Movie {
            Director = director,
            Genres = genres.ToList(),
            MediaId = mediaId,
            RunningTime = runtime,
            Title = title
        });
    }
}