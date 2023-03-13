using Lab.Media;

namespace Lab;

using NLog;

internal abstract class Program {
    private static readonly string Path = Directory.GetCurrentDirectory() + "\\nlog.config";
    private static readonly Logger Logger = LogManager.LoadConfiguration(Path).GetCurrentClassLogger();

    public static void Main(string[] args) {
        Logger.Info("Program started");

        var movie = new Movie {
            MediaId = 123,
            Title = "Greatest Movie Ever, The (2023)",
            Genres = {"Comedy", "Romance"}
        };

        Console.WriteLine(movie);
        
        Logger.Info("Program ended.");
    }
}