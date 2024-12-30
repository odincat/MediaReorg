# MediaReorg
## Introduction
This tool reorganizes your music collection into the [directory structure](https://jellyfin.org/docs/general/server/media/music/) that [Jellyfin](https://jellyfin.org/) expects, based on the tags of your music files. It also renames the files like this: `{title} - {artist}.{extension}`. Additionally, when a file is processed, its MD5 hash is used to avoid unnecessary copying in the future (`processedTracks.txt`).

**Please ensure that all of your music files have the correct tags set, before using this.**

The CLI tool expects that all the files are in the same directory (not nested!).

Also, by default, only `.flac` files will be processed. If you want to process other file types, you can modify the `allowedFileExtensions` list in `Program.cs`.

## Usage
Assuming you are in the `MediaReorg` directory and have .NET 9 installed:

`dotnet run -- start <inputDir> <outputDir>`

## Example
### Input Directory
```
├── 03 - Everything But The Girl - Missing (Todd Terry Club Mix).flac
├── 04 - Baccara - Yes Sir, I can Boogie (Extended Mix feat. Michael Universal Version).flac
├── 11 - Alexandra Stan - Mr. Saxobeat (Maan Extended Version).flac
├── 5. Sophie Ellis-Bextor - Murder On The Dancefloor (Extended Album Version).flac
├── Go West - Pet Shop Boys.flac
├── Groovejet _If This Ain_t Love_ [feat_ Sophie Ellis-Bextor] - Spiller Dj.flac
├── Hold You (Clubb Mix 1) - ATB.flac
├── I_m Outta Love - Anastacia.flac
├── Living On My Own (Radio Mix) - Freddie Mercury.flac
└── Sing it back (Boris Dlugosch Musical Mix) (1999) - Moloko.flac
```

### Output Directory
```
.
├── ATB
│   └── Hold You
│       └── Hold You (Clubb Mix 1) - ATB.flac
├── Alexandra Stan
│   └── Saxobeats
│       └── Mr. Saxobeat (Maan Extended Version) - Alexandra Stan.flac
├── Anastacia
│   └── Ultimate Collection
│       └── I'm Outta Love - Anastacia.flac
├── Baccara
│   └── Maxi CD - Yes Sir, I Can Boogie '99 (LC 00316)
│       └── Yes Sir, I can Boogie (Extended Mix feat. Michael Universal Version) - Baccara.flac
├── Everything But The Girl
│   └── Missing (Remixes)
│       └── Missing (Todd Terry Club Mix) - Everything But The Girl.flac
├── Freddie Mercury
│   └── Best Of 93
│       └── Living On My Own (Radio Mix) - Freddie Mercury.flac
├── Moloko
│   └── Die Hit-Giganten - Best Of 90s - CD 1
│       └── Sing it back (Boris Dlugosch Musical Mix) (1999) - Moloko.flac
├── Pet Shop Boys
│   └── Best of 93 - CD 2
│       └── Go West - Pet Shop Boys.flac
├── Sophie Ellis-Bextor
│   └── Murder On The Dancefloor
│       └── Murder On The Dancefloor (Extended Album Version) - Sophie Ellis-Bextor.flac
├── Spiller Dj
│   └── Groovejet (If This Ain't Love) [feat. Sophie Ellis-Bextor]
│       └── Groovejet (If This Ain't Love) [feat. Sophie Ellis-Bextor] (Extended Vocal Mix) - Spiller Dj.flac
└── processedTracks.txt
```