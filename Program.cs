using System;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class Program
    {
        public static void Menu()
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to:");
            Console.WriteLine("[1] Add a new band");
            Console.WriteLine("[2] View all the bands");
            Console.WriteLine("[3] Add an album for a band");
            Console.WriteLine("[4] Add a song to an album");
            Console.WriteLine("[5] Let a band go (update isSigned to false)");
            Console.WriteLine("[6] Resign a band (update isSigned to true)");
            Console.WriteLine("[7] Prompt for a band name and view all their albums");
            Console.WriteLine("[8] View all albums ordered by ReleaseDate");
            Console.WriteLine("[9] View all bands that are signed");
            Console.WriteLine("[10] View all bands that are not signed");
            Console.WriteLine("[11] View all the songs");
            Console.WriteLine("[12] Quit the program");
        }
        static string ToTitleCase(string str)
        {
            var tokens = str.Split(new[] { " ", "-" }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                tokens[i] = token == token.ToUpper()
                    ? token
                    : token.Substring(0, 1).ToUpper() + token.Substring(1).ToLower();
            }

            return string.Join(" ", tokens);
        }
        static DateTime PromptForDateTime(string prompt)
        {
            Console.Write(prompt);
            DateTime userInput;
            var isThisGoodInput = DateTime.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that was not valid. I'm going to use a default date as your date.");
                return default(DateTime);
            }
        }

        static bool PromptForBool(string prompt)
        {
            Console.Write(prompt);
            bool userInput;
            var isThisGoodInput = bool.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that was not valid. I'm going to use false as your number.");
                return false;
            }

        }

        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();
            ToTitleCase(userInput);

            return userInput;
        }
        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that was not valid. I'm going to use 0 as your number.");
                return 0;
            }
        }


        static void Main(string[] args)
        {
            var context = new RhythmsGonnaGetYouContext();

            var keepGoing = true;
            while (keepGoing)
            {
                Menu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine();
                            Console.WriteLine("Adding a new band");
                            Console.WriteLine();

                            var newBand = new Band();

                            newBand.Name = PromptForString("What is the name of the band? ");
                            newBand.CountryOfOrigin = PromptForString("What country is that band from? ");
                            newBand.NumberOfMembers = PromptForInteger("How many members does that band have? ");
                            newBand.Website = PromptForString("What is the name of their website? ");
                            newBand.Style = PromptForString("What style of music does that band make? ");
                            newBand.IsSigned = PromptForBool("Is that band signed? True or False only ");
                            newBand.ContactName = PromptForString("What is the contact name for that band? ");
                            newBand.ContactPhoneNumber = PromptForString("What is that contact persons phone number? ");

                            context.Band.Add(newBand);
                            context.SaveChanges();
                            break;
                        }

                    case "2":
                        {
                            Console.WriteLine();
                            Console.WriteLine("Viewing all the bands");
                            Console.WriteLine();

                            var bandNames = context.Band.Include(Band => Band.Albums);
                            foreach (var band in bandNames)
                            {
                                Console.WriteLine($"There is a band named {band.Name}");
                            }

                            break;
                        }

                    case "3":
                        {
                            Console.WriteLine();
                            Console.WriteLine("Adding an album for a band");
                            Console.WriteLine();

                            var newAlbum = new Album();

                            newAlbum.Title = PromptForString("What is the name of the album? ");
                            newAlbum.IsExplicit = PromptForBool("Is the album explicit? True or False ");
                            newAlbum.ReleaseDate = PromptForDateTime("When was that album released? Format: yyyy-mm-dd ");
                            var bandNamePrompt = PromptForString("Which band made that album? ");
                            newAlbum.BandId = context.Band.Where(name => name.Name == bandNamePrompt).Select(band => band.Id).FirstOrDefault();

                            context.Album.Add(newAlbum);
                            context.SaveChanges();
                            break;
                        }

                    case "4":
                        {
                            Console.WriteLine();
                            Console.WriteLine("Adding a song to an album");
                            Console.WriteLine();

                            var newSong = new Song();

                            newSong.Title = PromptForString("What is the title of the song? ");
                            newSong.TrackNumber = PromptForInteger("What is the track number of that song? ");
                            newSong.Duration = PromptForString("What is the duration of that song? Format: mm:ss ");
                            var albumNamePrompt = PromptForString("Which album is that song on? ");
                            newSong.AlbumId = context.Album.Where(name => name.Title == albumNamePrompt).Select(album => album.Id).FirstOrDefault();

                            context.Song.Add(newSong);
                            context.SaveChanges();
                            break;
                        }

                    case "5":
                        {
                            Console.WriteLine();
                            Console.WriteLine("Letting a band go");
                            Console.WriteLine();

                            var whatBand = PromptForString("What band would you like to let go? ");
                            var letGoOfBand = context.Band.FirstOrDefault(band => band.Name == whatBand);
                            letGoOfBand.IsSigned = false;

                            context.SaveChanges();
                            break;
                        }

                    case "6":
                        {
                            Console.WriteLine();
                            Console.WriteLine("Signing a band");
                            Console.WriteLine();

                            var whatBand = PromptForString("What band would you like to sign? ");
                            var signBand = context.Band.FirstOrDefault(band => band.Name == whatBand);
                            signBand.IsSigned = true;

                            context.SaveChanges();
                            break;
                        }

                    case "7":
                        {

                            Console.WriteLine();
                            Console.WriteLine("Viewing a bands albums");
                            Console.WriteLine();

                            var whatBand = PromptForString("What band would you like to see albums from? ");
                            var albumsFromBand = context.Band.FirstOrDefault(band => band.Name == whatBand);
                            var albumName = context.Album.Include(album => album.Band).Where(album => album.Band == albumsFromBand);
                            foreach (var album in albumName)
                            {
                                Console.WriteLine(album.Title);
                            }

                            break;
                        }

                    case "8":
                        {
                            Console.WriteLine();
                            Console.WriteLine("Viewing all albums by release date");
                            Console.WriteLine();

                            var albumsByReleaseDate = context.Album.OrderBy(Album => Album.ReleaseDate);
                            foreach (var album in albumsByReleaseDate)
                            {
                                Console.WriteLine($"{album.Title} was released on {album.ReleaseDate.ToString("MM/dd/yyyy")}");
                            }

                            break;
                        }

                    case "9":
                        {
                            Console.WriteLine();
                            Console.WriteLine("Viewing all signed bands");
                            Console.WriteLine();

                            var signedBands = context.Band.Where(Band => Band.IsSigned == true);
                            foreach (var band in signedBands)
                            {
                                Console.WriteLine($"{band.Name} is a signed band");
                            }

                            break;
                        }

                    case "10":
                        {
                            Console.WriteLine();
                            Console.WriteLine("Viewing all unsigned bands");
                            Console.WriteLine();

                            var unsignedBands = context.Band.Where(Band => Band.IsSigned == false);
                            foreach (var band in unsignedBands)
                            {
                                Console.WriteLine($"{band.Name} is an unsigned band");
                            }

                            break;
                        }
                    case "11":
                        {
                            Console.WriteLine();
                            Console.WriteLine("Viewing all the songs");
                            Console.WriteLine();

                            var songNames = context.Song.Include(song => song.Album);
                            foreach (var song in songNames)
                            {
                                Console.WriteLine($"There is a song called {song.Title}");
                            }

                            break;
                        }

                    case "12":
                        Console.WriteLine();
                        Console.WriteLine("Goodbye!");
                        keepGoing = false;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid input");
                        Console.WriteLine();
                        break;
                }
            }
        }

    }
}
