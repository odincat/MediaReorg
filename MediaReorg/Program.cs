using Cocona;
using MediaReorg;

var builder = CoconaApp.CreateBuilder();
var app = builder.Build();

app.AddCommand("start", ([Argument] string inputDir, [Argument] string outputDir) => {
    string[] allowedFileExtensions = [".flac"];

    if (!Directory.Exists(inputDir)) {
        Console.WriteLine("Input directory does not exist.");
        return;
    }

    if (!Directory.Exists(outputDir)) {
        Console.WriteLine("Output directory does not exist.");
        return;
    }

    var files = Directory.GetFiles(inputDir);

    var processedTracks = new List<string>();

    if (File.Exists(outputDir + "/processedTracks.txt")) {
        processedTracks = [.. File.ReadAllLines(outputDir + "/processedTracks.txt")];
    }

    foreach (var file in files) {
        string extension = Path.GetExtension(file);
        var hash = Lib.CalculateMD5(file);

        if (!allowedFileExtensions.Contains(extension)) {
            Console.WriteLine($"Skipping {file} as it is not a valid file type (see allowedFileExtension variable).");
            continue;
        }

        if (processedTracks.Contains(hash)) {
            Console.WriteLine($"Skipping {file} as it has already been processed.");
            continue;
        }

        using var tagContext = TagLib.File.Create(file);

        if (tagContext == null) {
            Console.WriteLine($"Skipping {file} as it does not have any tags.");
            continue;
        }

        string? artist = tagContext.Tag.FirstPerformer;
        string? album = tagContext.Tag.Album;

        string? title = tagContext.Tag.Title;

        if (artist == null || album == null) {
            Console.WriteLine($"Skipping {file} as it does not have an artist or album tag.");
            continue;
        }

        string artistDir = Path.Combine(outputDir, artist);
        string albumDir = Path.Combine(artistDir, album);

        if (!Directory.Exists(artistDir)) {
            Directory.CreateDirectory(artistDir);
        }

        if (!Directory.Exists(albumDir)) {
            Directory.CreateDirectory(albumDir);
        }

        string newFileName = $"{title} - {artist}{extension}";

        if (File.Exists(Path.Combine(albumDir, newFileName))) {
            Console.WriteLine($"Skipping {file} as it already exists in the output directory.");
            continue;
        }

        File.Copy(file, Path.Combine(albumDir, newFileName));

        processedTracks.Add(hash);

        Console.WriteLine($"Successfully processed {file}");
    }

    File.WriteAllLines(Path.Combine(outputDir, "processedTracks.txt"), processedTracks);
});

app.Run();