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
        
    }
}