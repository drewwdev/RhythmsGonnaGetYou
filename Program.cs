using System;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new RhythmsGonnaGetYouContext();
            var AlbumsWithBands = context.Album.Include(Album => Album.Band);

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

                }
                else
                if (choice == "2")
                {
                    Console.WriteLine();
                    Console.WriteLine("Viewing all the bands");

                }
                else
                if (choice == "3")
                {
                    Console.WriteLine();
                    Console.WriteLine("Adding an album for a band");

                }
                else
                if (choice == "4")
                {
                    Console.WriteLine();
                    Console.WriteLine("Adding a song to an album");

                }
                else
                if (choice == "5")
                {
                    Console.WriteLine();
                    Console.WriteLine("Letting a band go");

                }
                else
                if (choice == "6")
                {
                    Console.WriteLine();
                    Console.WriteLine("Signing a band");

                }
                else
                if (choice == "7")
                {
                    Console.WriteLine();
                    Console.WriteLine("Viewing a bands albums");

                }
                else
                if (choice == "8")
                {
                    Console.WriteLine();
                    Console.WriteLine("Viewing all albums by release date");

                }
                else
                if (choice == "9")
                {
                    Console.WriteLine();
                    Console.WriteLine("Viewing all signed bands");

                }
                else
                if (choice == "10")
                {
                    Console.WriteLine();
                    Console.WriteLine("Viewing all unsigned bands");

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
                }
            }
        }
    }
}
