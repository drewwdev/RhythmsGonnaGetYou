using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class Program
    {
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
                Console.WriteLine("[11] Quit the program");
                var choice = Console.ReadLine();

                if (choice == "1")
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

                }
                else
                if (choice == "2")
                {
                    Console.WriteLine();
                    Console.WriteLine("Viewing all the bands");
                    Console.WriteLine();

                    var bandNames = context.Band.Include(Band => Band.Albums);
                    foreach (var band in bandNames)
                    {
                        Console.WriteLine($"There is a band named {band.Name}");
                    }
                }
                else
                if (choice == "3")
                {
                    Console.WriteLine();
                    Console.WriteLine("Adding an album for a band");
                    Console.WriteLine();

                }
                else
                if (choice == "4")
                {
                    Console.WriteLine();
                    Console.WriteLine("Adding a song to an album");
                    Console.WriteLine();

                }
                else
                if (choice == "5")
                {
                    Console.WriteLine();
                    Console.WriteLine("Letting a band go");
                    Console.WriteLine();

                }
                else
                if (choice == "6")
                {
                    Console.WriteLine();
                    Console.WriteLine("Signing a band");
                    Console.WriteLine();

                }
                else
                if (choice == "7")
                {
                    Console.WriteLine();
                    Console.WriteLine("Viewing a bands albums");
                    Console.WriteLine();

                }
                else
                if (choice == "8")
                {
                    Console.WriteLine();
                    Console.WriteLine("Viewing all albums by release date");
                    Console.WriteLine();

                    var albumsByReleaseDate = context.Album.OrderBy(Band => Band.ReleaseDate);
                    foreach (var album in albumsByReleaseDate)
                    {
                        Console.WriteLine($"{album.Title} was released on {album.ReleaseDate.ToString("MM/dd/yyyy")}");
                    }
                }
                else
                if (choice == "9")
                {
                    Console.WriteLine();
                    Console.WriteLine("Viewing all signed bands");
                    Console.WriteLine();

                    var signedBands = context.Band.Where(Band => Band.IsSigned == true);
                    foreach (var band in signedBands)
                    {
                        Console.WriteLine($"{band.Name} is a signed band");
                    }
                }
                else
                if (choice == "10")
                {
                    Console.WriteLine();
                    Console.WriteLine("Viewing all unsigned bands");
                    Console.WriteLine();

                    var signedBands = context.Band.Where(Band => Band.IsSigned == false);
                    foreach (var band in signedBands)
                    {
                        Console.WriteLine($"{band.Name} is an unsigned band");
                    }
                }
                else
                if (choice == "11")
                {
                    Console.WriteLine();
                    Console.WriteLine("Goodbye!");
                    keepGoing = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid input");
                    Console.WriteLine();
                }
            }
        }
    }
}
