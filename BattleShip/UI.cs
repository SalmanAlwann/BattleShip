using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pastel; // Pastel: A Module for the color of the game. 

namespace BattleShip
{

    internal class UI
    {
        // 0cebd1
        // FFFFFF

        // public variables

        #region publicVariables
        public static Game game = new Game();

        public static string name; // Name: Stands for the name of the player.
        public static int option; // Option: Get the option the user has choosed.
        DateTime now = DateTime.Now; // Now: Returns the current Date Time.
        public static int[] lengthShip = { 2, 2, 3, 4, 5 }; // here are the length of the ships in order.
        #endregion

        #region Banner
        public void BannerCredits(DateTime date, bool n) /*
                                                          * Date: Current date.
                                                          * N: check if the option is valid or not.
                                                          */
        {
            if (n) // if(n): means if the user input is correct it will print valid banner.
            {
                Console.WriteLine("\n██████".Pastel("0cebd1") + "╗  ".Pastel("FFFFFF") + "█████".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + " ████████".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + "████████".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + "     ███████".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + "███████".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + "  ██".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + "██████".Pastel("0cebd1") + "╗ ".Pastel("FFFFFF") + "                 __/___\n██".Pastel("0cebd1") + "╔══".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╔══".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╗╚══".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╔══╝╚══".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╔══╝".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║     ".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╔════╝".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╔════╝".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║  ".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╔══".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + "\t".Pastel("0cebd1") + "        _____/______|\n██████".Pastel("0cebd1") + "╔╝".Pastel("FFFFFF") + "███████".Pastel("0cebd1") + "║   ".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║      ".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║   ".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║     ".Pastel("FFFFFF") + "█████".Pastel("0cebd1") + "╗  ".Pastel("FFFFFF") + "███████".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + "███████".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "██████".Pastel("0cebd1") + "╔╝".Pastel("FFFFFF") + "   _______/_____\\_______\\_____\n██".Pastel("0cebd1") + "╔══".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╔══".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║   ".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║      ".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║   ".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "     ██".Pastel("0cebd1") + "╔══╝  ╚════".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╔══".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "╔═══╝".Pastel("FFFFFF") + "   \\              < < <       |\n██████".Pastel("0cebd1") + "╔╝".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║  ".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "   ██".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "      ██".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "   ███████".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + "███████".Pastel("0cebd1") + "╗".Pastel("FFFFFF") + "███████".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "  ██".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "██".Pastel("0cebd1") + "║".Pastel("FFFFFF") + "    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~".Pastel("0cebd1") + "\n╚═════╝ ╚═╝  ╚═╝   ╚═╝      ╚═╝   ╚══════╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝╚═╝\n".Pastel("#FFFFFF"));
                Console.Write(" " + "{ ".Pastel("0cebd1") + "Authors".Pastel("FFFFFF") + ":".Pastel("0cebd1") + " Salman Alwan".Pastel("FFFFFF") + ",".Pastel("0cebd1") + " Sheeren Hreish".Pastel("#FFFFFF") + " }".Pastel("0cebd1"));
                Console.Write("\t " + "{ ".Pastel("0cebd1") + $"{date}".Pastel("FFFFFF") + " }".Pastel("0cebd1"));
            }
            else // else: means if 'n' is false, print the invalid banner.
            {
                Console.WriteLine("\n██████".Pastel("9D0000") + "╗  ".Pastel("FFFFFF") + "█████".Pastel("9D0000") + "╗".Pastel("FFFFFF") + " ████████".Pastel("9D0000") + "╗".Pastel("FFFFFF") + "████████".Pastel("9D0000") + "╗".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╗".Pastel("FFFFFF") + "     ███████".Pastel("9D0000") + "╗".Pastel("FFFFFF") + "███████".Pastel("9D0000") + "╗".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╗".Pastel("FFFFFF") + "  ██".Pastel("9D0000") + "╗".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╗".Pastel("FFFFFF") + "██████".Pastel("9D0000") + "╗ ".Pastel("FFFFFF") + "\n██".Pastel("9D0000") + "╔══".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╗".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╔══".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╗╚══".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╔══╝╚══".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╔══╝".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║     ".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╔════╝".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╔════╝".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║  ".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╔══".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╗".Pastel("FFFFFF") + "\t".Pastel("9D0000") + "\n██████".Pastel("9D0000") + "╔╝".Pastel("FFFFFF") + "███████".Pastel("9D0000") + "║   ".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║      ".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║   ".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║     ".Pastel("FFFFFF") + "█████".Pastel("9D0000") + "╗  ".Pastel("FFFFFF") + "███████".Pastel("9D0000") + "╗".Pastel("FFFFFF") + "███████".Pastel("9D0000") + "║".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║".Pastel("FFFFFF") + "██████".Pastel("9D0000") + "╔╝".Pastel("FFFFFF") + "\n██".Pastel("9D0000") + "╔══".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╗".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╔══".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║   ".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║      ".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║   ".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║".Pastel("FFFFFF") + "     ██".Pastel("9D0000") + "╔══╝  ╚════".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╔══".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║".Pastel("FFFFFF") + "██".Pastel("9D0000") + "╔═══╝".Pastel("FFFFFF") + "\n██████".Pastel("9D0000") + "╔╝".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║  ".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║".Pastel("FFFFFF") + "   ██".Pastel("9D0000") + "║".Pastel("FFFFFF") + "      ██".Pastel("9D0000") + "║".Pastel("FFFFFF") + "   ███████".Pastel("9D0000") + "╗".Pastel("FFFFFF") + "███████".Pastel("9D0000") + "╗".Pastel("FFFFFF") + "███████".Pastel("9D0000") + "║".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║".Pastel("FFFFFF") + "  ██".Pastel("9D0000") + "║".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║".Pastel("FFFFFF") + "██".Pastel("9D0000") + "║".Pastel("FFFFFF") + "\n╚═════╝ ╚═╝  ╚═╝   ╚═╝      ╚═╝   ╚══════╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝╚═╝\n".Pastel("#FFFFFF"));
                Console.Write(" " + "{ ".Pastel("9D0000") + "Authors".Pastel("FFFFFF") + ":".Pastel("9D0000") + " Salman Alwan".Pastel("FFFFFF") + ",".Pastel("9D0000") + " Sheeren Hreish".Pastel("#FFFFFF") + " }".Pastel("9D0000"));
                Console.Write("\t " + "{ ".Pastel("9D0000") + $"{date}".Pastel("FFFFFF") + " }".Pastel("9D0000"));
            }
        }
        #endregion

        // SetName: method used to set player username.
        public void SetName()
        {
            Console.Write("\n\n " + "[".Pastel("0cebd1") + "$".Pastel("FFFFFF") + "]".Pastel("0cebd1") + " Welcome".Pastel("FFFFFF") + ",".Pastel("0cebd1") + " Please Insert Your Name".Pastel("#FFFFFF") + ": ".Pastel("0cebd1"));
            name = Console.ReadLine();
            if (name == "") // if the user is invalid
            {
                Console.Clear(); // clear the screen
                BannerCredits(now, false); // print false banner.
                Console.Write("\n\n " + "{".Pastel("9D0000") + "#".Pastel("FFFFFF") + "}".Pastel("9D0000") + "You don't have".Pastel("FFFFFF") + $" name".Pastel("9D0000") + $"?, Please Press".Pastel("FFFFFF") + " Enter ".Pastel("9D0000") + "and enter your name. ");
                Console.ReadLine(); // wait for user input
                Console.Clear(); // clear the screen
                BannerCredits(now, true); // print true banner
                SetName(); // recall the setname function
            }
        }

        // method to print that we are generating the table.
        public void GenerateMessage()
        {
            Console.Clear(); // clear the screen
            BannerCredits(now, true); // print true banner.
            Console.Write("\n\n " + "[".Pastel("0cebd1") + "$".Pastel("FFFFFF") + "]".Pastel("0cebd1") + " Welcome".Pastel("FFFFFF") + $" {name}".Pastel("0cebd1") + ", Please wait, We are generating the table for you ".Pastel("#FFFFFF") + "... ".Pastel("0cebd1"));
        }

        // method that has the menu of the game with the options.
        public void Menu()
        {
            Console.Clear();
            BannerCredits(now, true);
            Console.WriteLine("\n\n " + "[".Pastel("0cebd1") + "1".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Start Game ".Pastel("FFFFFF"));
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "2".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Help menu ".Pastel("FFFFFF"));
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "3".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Exit ".Pastel("FFFFFF"));
            Console.Write("\n " + "{".Pastel("0cebd1") + "$".Pastel("FFFFFF") + "}".Pastel("0cebd1") + $" {name}".Pastel("0cebd1") + $", Choose option: ".Pastel("FFFFFF"));
            // Check if user input not in the correct format.
            try
            {
                option = int.Parse(Console.ReadLine()); // try to get valid option
            }
            // if not, it will print invalid banner, press enter to return back.
            catch (System.FormatException)
            {
                Console.Clear(); // clear the screem
                BannerCredits(now, false); // print invalid banner
                Console.Write("\n\n " + "{".Pastel("9D0000") + "#".Pastel("FFFFFF") + "}".Pastel("9D0000") + $" {name}".Pastel("9D0000") + $", You Choosed Invalid Option, Press ".Pastel("FFFFFF") + "Enter".Pastel("9D0000") + " To Return.");
                Console.ReadLine(); // wait for user input.
                Menu(); // recall menu function
            }
            UserReq(); // call userreq funtions to take the userrequest and handle it.
        }

        // userreq, to take the player request, and handle it as follows.
        public void UserReq()
        {
            // if he choosed option 1.
            if (option == 1)
            {
                game.Start(); // start the game
            }
            // if he choosed option 2.
            else if (option == 2)
            {
                // print the rules and help menu
                game.HelpMenu();
                // then reoption the menu.
                Menu();
            }
            // if he choosed to exit.
            else if (option == 3)
            {
                // then exit the game.
                Environment.Exit(0);

            }
            // else, means that he entered incorrect option
            else
            {
                Console.Clear(); // clear the screen
                BannerCredits(now, false); // print invalid banner
                Console.Write("\n\n " + "{".Pastel("9D0000") + "#".Pastel("FFFFFF") + "}".Pastel("9D0000") + $" {name}".Pastel("9D0000") + $", You Choosed Invalid Option, Press ".Pastel("FFFFFF") + "Enter".Pastel("9D0000") + " To Return.");
                Console.ReadLine(); // wait for user input.
                Menu(); // call menu function.
            }
        }

        // we will start with this function, it is the main function.
        public void MainFunc()
        {
            Console.Clear(); // clear the console.
            BannerCredits(now, true); // print true banner
            SetName(); // set the player username.

            GenerateMessage(); // print the 'generating table' message.
            Console.ReadLine(); // wait for user input.
            Menu(); // menu function to see what the user wants to do, whether start the game or read help menu.
        }
    }
}