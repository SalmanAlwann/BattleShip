// Main DLL's
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pastel; // Pastel: A Module for the color of the game. 
using System.Runtime.InteropServices;

/* BattleShip:
 * Battle Ship is a guessing game based on two opponents.
 * The Program Generates Random Table.
 * And in the table there is hidden ships not seen.
 * The player have to guess the places of the ships.
 * But it gets more challenging when it comes to the rules!
 * if the player guesses 8 times in a row in the incorrect places.
 * he will be kicked out of the game.
 * and the rest of the stats will be given by how the player plays the games!
 */
namespace BattleShip
{
    // Calling Class 'UI' to import some functions!
    internal class Game : UI
    {

        // All the variables in the class.
        #region Variables
        /*
         * Here iam importing some 'dlls' to make the windows maximaze auto.
         */
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0; // Hide Cord of the CMD
        private const int MAXIMIZE = 3; // Max Cord of the CMD
        private const int MINIMIZE = 6; // Min Cord of the CMD
        private const int RESTORE = 9; // Restore Cord of the CMD

        public static int X, Y; // The cordionts of the table
        public static int[,] table = new int[10, 10]; // table1: the ships are shown here.
        public static string[,] tableRealTime = new string[10, 10]; // table2: the ships are hidden here.
        public static string[] Direction = { "Portrait", "Landscape" }; // Array: generate ship direction.
        public static string[] PortraitScape = { "Up", "Down" }; // Portrait Array, up or down.
        public static string[] LandScape = { "Right", "Left" }; // Landscape Array, left or right.
        public static int[] lengthShip = { 2, 2, 3, 4, 5 }; // here are the length of the ships in order.
        Random rand = new Random(); // import Random library
        //public static String[,] grid = new string[10, 10]; 
        DateTime now = DateTime.Now; // importing Datetime library
        public static int row; // row variable for the rows in the table
        public static int column; // columns variable for the column in the table
        public static int guesses = 0; // user guesses will be counted here.
        public static int hits = 0; // user hits will be counted here.
        public static int failed = 0; // user fails will be counted here.
        public int c, b, a = 0; // here is the ships letters, we will use that to check if the user has guessed whole ship.
        public int shipsHit = 0; // this variable for the ships that were hit in a row.
        public static int totalShipsHit = 0; // this variable for the ships that were hit in total.
        public static string rankPlayer = ""; // this variable to assign user rank.
        #endregion

        // Here is the Main Functions
        #region Main Functions.

        // ResetTable: Method used to reset the whole int table.
        public void ResetTable(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] = 0; // place 0 in the [i,j] cords
                }
            }
        }

        // ResetTableNew: Method used to reset the whole string table.
        public void ResetTableNew(string[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] = " "; // place " " in the [i,j] cords
                }
            }
        }

        // GenDirection: Method used to generate random Direction ['Portrait, 'Landscape'].
        public int GenDirection()
        {

            return rand.Next(Direction.Length); // random pick from array/
        }
        // GenRow: Method used to generate random int from 0-10.
        public int GenRow()
        {
            return rand.Next(0, 10);
        }
        // GenColumns: Method used to generate random int from 0-10.
        public int GenColumns()
        {
            return rand.Next(0, 10);
        }
        // GenDirectionScape: Method used to generate amd return random number from the array of the scapes.
        public int GenDirectionScape(string scape)
        {
            if (scape == "Portrait") // check if the parameter is portrait
            {
                return rand.Next(PortraitScape.Length);
            }
            else // else: is landscape.
            {
                return rand.Next(LandScape.Length);
            }
        }

        // Start the game with thius method.
        public void Start()
        {
            ResetTableNew(tableRealTime); // reset the table.
            for (int i = 0; i < lengthShip.Length; i++)
            {
                CheckTable(lengthShip[i], lengthShip[i]); // check the table, for example: no ships are connected.
            }

            UserInput(); // take user input from the player.
        }

        // UserInput: Method to take input from the user and check it.
        public void UserInput() 
        {
            //PrintTable();
            PrintTableNew(); // Print the table with hidden ships.
            while (totalShipsHit < 5) // While the user hasnt huiessed all the ships, keep running.
            {
                try // try this, if not break.
                {
                    // Take the X cords.
                    Console.Write("\n\n " + "[".Pastel("0cebd1") + "+".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Enter X cordinots: ".Pastel("FFFFFF"));
                    X = int.Parse(Console.ReadLine());
                    
                    // Take the Y cords.
                    Console.Write("\n\n " + "[".Pastel("0cebd1") + "+".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Enter Y cordinots: ".Pastel("FFFFFF"));
                    Y = int.Parse(Console.ReadLine());
                    
                    // If to check if the user input is correct.
                    if (X > 9 || X < 0 || Y > 9 || Y < 0)
                    {
                        UserInput();

                    }
                }
                // if not, try to catch the error, and rerun it.
                catch (System.FormatException)
                {
                    UserInput();
                }
                guesses++; // increase guesses.

                if (table[X, Y] == 0) // if user guess value is 0, that means no hit.
                {
                    tableRealTime[X, Y] = "*"; // change it with "*" in the new table.
                    hits = 0; // reset hits.
                    failed++; // increase failed.
                    shipsHit = 0; // reset ships hit in a row.
                }
                // if there is 2, hit.
                if (table[X, Y] == 2)
                {
                    tableRealTime[X, Y] = "d"; // change it with "d".
                    // check if he hit a whole ship.
                    if (tableRealTime[X + 1, Y] == "d" || tableRealTime[X - 1, Y] == "d" || tableRealTime[X, Y - 1] == "d" || tableRealTime[X, Y + 1] == "d")
                    {
                        totalShipsHit++; // increase total ships hit.
                        shipsHit++; // add ships hit in row.
                    }
                    hits++; // increase hits.
                    failed = 0; // reset failed in row.
                    table[X, Y] = 12; // replace it with 12.
                }
                // if there is 3, hit.
                if (table[X, Y] == 3)
                {
                    tableRealTime[X, Y] = "c"; // change it with "c".
                    hits++; // increase hits.
                    failed = 0; // reset failed in row.
                    table[X, Y] = 13; // replace it with 13.
                    c++; // increase c.
                    // check if he hit a whole ship.
                    if (c == 3) 
                    {
                        totalShipsHit++; // increase total ships hit.
                        shipsHit++; // add ships hit in row.
                    }
                }
                // if there is 4, hit.
                if (table[X, Y] == 4)
                {
                    tableRealTime[X, Y] = "b"; // change it with "b".
                    hits++; // increase hits.
                    failed = 0; // reset failed in row.
                    table[X, Y] = 14; // replace it with 14.
                    b++; // increase b.
                    // check if he hit a whole ship.
                    if (b == 4)
                    {
                        totalShipsHit++; // increase total ships hit.
                        shipsHit++; // add ships hit in row.
                    }
                }
                // if there is 5, hit.
                if (table[X, Y] == 5)
                {
                    tableRealTime[X, Y] = "a"; // change it with " a".
                    hits++; // increase hits.
                    failed = 0; // reset failed in row.
                    table[X, Y] = 15; // replace it with 15.
                    a++; // increase a.
                    // check if he hit a whole ship.
                    if (a == 5)
                    {
                        totalShipsHit++; // increase total ships hit.
                        shipsHit++; // add ships hit in row.
                    }
                }
                // print the new table with updated cords.
                PrintTableNew();
                // check status of the game.
                CheckStatus();

            }
            // end game when he finishes.
            EndGame();
        }


        // CheckStatus: method to end || give rank to the player.
        public void CheckStatus()
        {
            // if failed in row equal to 8.
            if (failed == 8)
            {
                EndGame(); // endgame.
            }
            // if he hitted 5 in row.
            if (hits == 5)
            {
                // give him rank professional
                rankPlayer = "professional";
                // print that he is professional
                Console.Write("\n\n " + "[".Pastel("0cebd1") + "$".Pastel("FFFFFF") + "]".Pastel("0cebd1") + " YOU ARE A PROFESSIONAL PLAYER, YOU HITS 5 TIMES IN A ROW, KEEP IT UP".Pastel("FFFFFF") + "!!".Pastel("0cebd1"));

            }
            // if he hitted 2 ships in row.
            if (shipsHit == 2)
            {
                // give him rank expert.
                rankPlayer = "expert";
            }
            // if none of above.
            else
            {
                // give him rank noob.
                rankPlayer = "noob";
            }

        }


        // EndGame: method used in the end of the match, it will print the stats of the match.
        public void EndGame()
        {
            Console.Clear(); // clear console.
            UI ui = new UI(); // new ui child
            ui.BannerCredits(now, true); // print banner from ui
            Console.WriteLine();
            // some words to make him feel good :)
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "-".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" The game is over, hope you like it".Pastel("FFFFFF") + "...".Pastel("0cebd1"));
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "-".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" In front of you your ranks throughout the game".Pastel("FFFFFF") + " :".Pastel("0cebd1"));
            // Player stats.
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "*".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Your guesses".Pastel("FFFFFF") + " :".Pastel("0cebd1") + guesses);
            Console.WriteLine(PlayerStats());
            Console.WriteLine();

            // exit the game.
            Environment.Exit(0); 
        }

        // PlayerStats: method to print player stats.
        public string PlayerStats()
        {
            // check if he is professional.
            if (rankPlayer == "professional")
            {
                // return professional message.
                return "\n " + "[".Pastel("0cebd1") + "*".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" You Are Proffesional, Good Job :) !!".Pastel("FFFFFF") + " :".Pastel("0cebd1");
            }
            // check if he is expert
            else if (rankPlayer == "expert")
            {
                // return expert message.
                return "\n " + "[".Pastel("0cebd1") + "*".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Dudeee!!!, Nice Work, You are EXPERT!!!!!!!".Pastel("FFFFFF") + " :".Pastel("0cebd1");
            }
            // if none of above
            else
            {
                // return noob message.
                return "\n " + "[".Pastel("0cebd1") + "*".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" You Still Beginner, Try Again With Your Best :)".Pastel("FFFFFF") + " :".Pastel("0cebd1");
            }
        }
        public void CheckTable(int lengthShip, int v)
        {

            int sum = 0;
            bool buildingShip = false;

            while (buildingShip == false)
            {
                column = GenColumns();
                row = GenRow();
                #region Corner
                if (row == 0 && column == 0)
                {
                    if (Direction[GenDirection()] == "Portrait")
                    {
                        sum = 0;
                        for (int i = 0; i < lengthShip; i++)
                        {
                            sum += table[i, column + 1];
                            sum += table[i, column];
                        }

                        sum += table[lengthShip, column];

                        if (sum == 0)
                        {
                            buildingShip = true;

                            for (int i = 0; i < lengthShip; i++)
                            {
                                table[i, 0] = v;
                            }
                        }
                    }

                    else
                    {
                        sum = 0;

                        for (int i = 0; i < lengthShip; i++)
                        {
                            sum += table[row + 1, i];
                            sum += table[row, i];
                        }

                        sum += table[row, lengthShip];


                        if (sum == 0)
                        {
                            buildingShip = true;
                            for (int i = 0; i < lengthShip; i++)
                            {
                                table[0, i] = v;
                            }
                        }

                    }
                }

                if (row == 0 && column == 9)
                {

                    if (Direction[GenDirection()] == "Portrait")
                    {
                        sum = 0;
                        for (int i = row; i < row + lengthShip; i++)
                        {
                            sum += table[i, column];
                            sum += table[i, column - 1];
                        }

                        if (sum == 0)
                        {
                            buildingShip = true;
                            for (int i = 0; i < lengthShip; i++)
                            {
                                table[i, 9] = v;
                            }
                        }

                    }
                    else
                    {
                        sum = 0;
                        for (int i = column; i > column - lengthShip; i--)
                        {
                            sum += table[row, i];
                            sum += table[row + 1, i];
                        }
                        sum += table[row, column - lengthShip];

                        if (sum == 0)
                        {
                            buildingShip = true;

                            for (int i = 9; i > 9 - lengthShip; i--)
                            {
                                table[0, i] = v;
                            }
                        }


                    }
                }
                if (row == 9 && column == 0)
                {
                    if (Direction[GenDirection()] == "Portrait")
                    {
                        sum = 0;
                        for (int i = row; i > row - lengthShip; i--)
                        {
                            sum += table[i, column];
                            sum += table[i, column + 1];
                        }
                        sum += table[row - lengthShip, column];

                        if (sum == 0)
                        {
                            buildingShip = true;

                            for (int i = 9; i > 9 - lengthShip; i--)
                            {
                                table[i, 0] = v;
                            }
                        }

                    }
                    else
                    {
                        sum = 0;

                        for (int i = column; i < column + lengthShip; i++)
                        {
                            sum += table[row, i];
                            sum += table[row - 1, i];
                        }

                        sum += table[row, lengthShip];

                        if (sum == 0)
                        {
                            buildingShip = true;

                            for (int i = 0; i < lengthShip; i++)
                            {
                                table[9, i] = v;
                            }
                        }

                    }
                }
                if (row == 9 && column == 9)
                {
                    if (Direction[GenDirection()] == "Portrait")
                    {
                        sum = 0;
                        for (int i = row; i > row - lengthShip; i--)
                        {
                            sum += table[i, column];
                            sum += table[i, column - 1];
                        }
                        sum += table[row - lengthShip, column];

                        if (sum == 0)
                        {
                            buildingShip = true;

                            for (int i = 9; i > 9 - lengthShip; i--)
                            {
                                table[i, 9] = v;
                            }
                        }

                    }
                    else
                    {
                        sum = 0;
                        for (int i = column; i > column - lengthShip; i--)
                        {
                            sum += table[row, i];
                            sum += table[row - 1, i];
                        }

                        sum += table[row, column - lengthShip];

                        if (sum == 0)
                        {
                            buildingShip = true;

                            for (int i = 9; i > 9 - lengthShip; i--)
                            {
                                table[9, i] = v;
                            }
                        }

                    }
                }
                #endregion


                if ((row > 0 && row < 9) && column == 0)
                {
                    if (Direction[GenDirection()] == "Portrait")
                    {
                        if (PortraitScape[GenDirectionScape("Portrait")] == "Up")
                        {

                            if (row - (lengthShip - 1) >= 0)
                            {
                                sum = 0;
                                for (int i = row; i > row - lengthShip; i--)
                                {
                                    sum += table[i, column];
                                    sum += table[i, column + 1];
                                }
                                sum += table[row + 1, column];
                                if ((row - lengthShip) + 1 > 0)
                                {
                                    sum += table[row - lengthShip, column];
                                }

                                if (sum == 0)
                                {
                                    buildingShip = true;

                                    for (int i = row; i > row - lengthShip; i--)
                                    {
                                        table[i, 0] = v;
                                    }
                                }

                            }
                        }
                        else
                        {


                            if (row + (lengthShip - 1) <= 9)
                            {
                                sum = 0;
                                for (int i = row; i < row + lengthShip; i++)
                                {
                                    sum += table[i, column];
                                    sum += table[i, column + 1];
                                }
                                sum += table[row - 1, column];
                                if ((row + lengthShip) - 1 < 9)
                                {
                                    sum += table[row + lengthShip, column];
                                }

                                if (sum == 0)
                                {
                                    buildingShip = true;

                                    for (int i = row; i < row + lengthShip; i++)
                                    {
                                        table[i, 0] = v;
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        sum = 0;

                        for (int i = column; i < column + lengthShip; i++)
                        {
                            sum += table[row, i];
                            sum += table[row + 1, i];
                            sum += table[row - 1, i];
                        }

                        sum += table[row, column + lengthShip];

                        if (sum == 0)
                        {
                            buildingShip = true;

                            for (int i = 0; i < lengthShip; i++)
                            {
                                table[row, i] = v;
                            }
                        }

                    }

                }



                if ((row > 0 && row < 9) && column == 9)
                {
                    if (Direction[GenDirection()] == "Portrait")
                    {
                        if (PortraitScape[GenDirectionScape("Portrait")] == "Up")
                        {
                            if (row - (lengthShip - 1) >= 0)
                            {
                                sum = 0;
                                for (int i = row; i > row - lengthShip; i--)
                                {
                                    sum += table[i, column];
                                    sum += table[i, column - 1];
                                }
                                sum += table[row + 1, column];
                                if ((row - lengthShip) + 1 > 0)
                                {
                                    sum += table[row - lengthShip, column];
                                }

                                if (sum == 0)
                                {
                                    buildingShip = true;

                                    for (int i = row; i > row - lengthShip; i--)
                                    {
                                        table[i, 9] = v;
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (row + (lengthShip - 1) <= 9)
                            {
                                sum = 0;
                                for (int i = row; i < row + lengthShip; i++)
                                {
                                    sum += table[i, column];
                                    sum += table[i, column - 1];
                                }
                                sum += table[row - 1, column];
                                if ((row + lengthShip) - 1 < 9)
                                {
                                    sum += table[row + lengthShip, column];
                                }

                                if (sum == 0)
                                {
                                    buildingShip = true;

                                    for (int i = row; i < row + lengthShip; i++)
                                    {
                                        table[i, 9] = v;
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        sum = 0;

                        for (int i = column; i > column - lengthShip; i--)
                        {
                            sum += table[row, i];
                            sum += table[row + 1, i];
                            sum += table[row - 1, i];
                        }

                        sum += table[row, column - lengthShip];

                        if (sum == 0)
                        {
                            buildingShip = true;

                            for (int i = 9; i > 9 - lengthShip; i--)
                            {
                                table[row, i] = v;
                            }
                        }

                    }

                }



                if ((column > 0 && column < 9) && row == 0)
                {
                    if (Direction[GenDirection()] == "Landscape")
                    {
                        if (LandScape[GenDirectionScape("Landscape")] == "Left")
                        {
                            if (column - (lengthShip - 1) >= 0)
                            {
                                sum = 0;

                                for (int i = column; i > column - lengthShip; i--)
                                {
                                    sum += table[row, i];
                                    sum += table[row + 1, i];
                                }
                                sum += table[row, column + 1];
                                if ((column - lengthShip) + 1 > 0)
                                {
                                    sum += table[row, column - lengthShip];
                                }

                                if (sum == 0)
                                {
                                    buildingShip = true;

                                    for (int i = column; i > column - lengthShip; i--)
                                    {
                                        table[0, i] = v;
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (column + (lengthShip - 1) <= 9)
                            {
                                sum = 0;

                                for (int i = column; i < column + lengthShip; i++)
                                {
                                    sum += table[row, i];
                                    sum += table[row + 1, i];
                                }
                                sum += table[row, column - 1];
                                if ((column + lengthShip) - 1 < 9)
                                {
                                    sum += table[row, column + lengthShip];
                                }

                                if (sum == 0)
                                {
                                    buildingShip = true;

                                    for (int i = column; i < column + lengthShip; i++)
                                    {
                                        table[0, i] = v;
                                    }
                                }

                            }
                        }

                    }
                    else
                    {
                        sum = 0;

                        for (int i = row; i < row + lengthShip; i++)
                        {
                            sum += table[i, column];
                            sum += table[i, column + 1];
                            sum += table[i, column - 1];
                        }

                        sum += table[lengthShip, column];

                        if (sum == 0)
                        {
                            buildingShip = true;

                            for (int i = 0; i < lengthShip; i++)
                            {
                                table[i, column] = v;
                            }
                        }

                    }

                }

                if ((column > 0 && column < 9) && row == 9)
                {
                    if (Direction[GenDirection()] == "Landscape")
                    {
                        if (LandScape[GenDirectionScape("Landscape")] == "Left")
                        {
                            if (column - (lengthShip - 1) >= 0)
                            {
                                sum = 0;

                                for (int i = column; i > column - lengthShip; i--)
                                {
                                    sum += table[row, i];
                                    sum += table[row - 1, i];
                                }
                                sum += table[row, column + 1];
                                if ((column - lengthShip) + 1 > 0)
                                {
                                    sum += table[row, column - lengthShip];
                                }

                                if (sum == 0)
                                {
                                    buildingShip = true;

                                    for (int i = column; i > column - lengthShip; i--)
                                    {
                                        table[9, i] = v;
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (column + (lengthShip - 1) <= 9)
                            {
                                sum = 0;

                                for (int i = column; i < column + lengthShip; i++)
                                {
                                    sum += table[row, i];
                                    sum += table[row - 1, i];
                                }
                                sum += table[row, column - 1];
                                if ((column + lengthShip) - 1 < 9)
                                {
                                    sum += table[row, column + lengthShip];
                                }

                                if (sum == 0)
                                {
                                    buildingShip = true;

                                    for (int i = column; i < column + lengthShip; i++)
                                    {
                                        table[9, i] = v;
                                    }
                                }

                            }
                        }

                    }
                    else
                    {
                        sum = 0;

                        for (int i = row; i > row - lengthShip; i--)
                        {
                            sum += table[i, column];
                            sum += table[i, column + 1];
                            sum += table[i, column - 1];
                        }

                        sum += table[9 - lengthShip, column];

                        if (sum == 0)
                        {
                            buildingShip = true;

                            for (int i = 9; i > 9 - lengthShip; i--)
                            {
                                table[i, column] = v;
                            }
                        }

                    }

                }
                if ((column > 0 && row > 0) && (column < 9 && row < 9))
                {
                    if (Direction[GenDirection()] == "Landscape")
                    {
                        if (LandScape[GenDirectionScape("Landscape")] == "Left")
                        {
                            if (column - (lengthShip - 1) >= 0)
                            {
                                sum = 0;
                                for (int i = column; i > column - lengthShip; i--)
                                {
                                    sum += table[row, i];
                                    sum += table[row + 1, i];
                                    sum += table[row - 1, i];
                                }
                                sum += table[row, column + 1];

                                if ((column - lengthShip) + 1 > 0)
                                {
                                    sum += table[row, column - lengthShip];
                                }

                                if (sum == 0)
                                {
                                    buildingShip = true;

                                    for (int i = column; i > column - lengthShip; i--)
                                    {
                                        table[row, i] = v;
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (column + (lengthShip - 1) <= 9)
                            {
                                sum = 0;
                                for (int i = column; i < column + lengthShip; i++)
                                {
                                    sum += table[row, i];
                                    sum += table[row + 1, i];
                                    sum += table[row - 1, i];
                                }
                                sum += table[row, column - 1];

                                if ((column + lengthShip) - 1 < 9)
                                {
                                    sum += table[row, column + lengthShip];
                                }

                                if (sum == 0)
                                {
                                    buildingShip = true;

                                    for (int i = column; i < column + lengthShip; i++)
                                    {
                                        table[row, i] = v;
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        if (PortraitScape[GenDirectionScape("Portrait")] == "Up")
                        {
                            if (row - (lengthShip - 1) >= 0)
                            {
                                sum = 0;
                                for (int i = row; i > row - lengthShip; i--)
                                {
                                    sum += table[i, column];
                                    sum += table[i, column + 1];
                                    sum += table[i, column - 1];
                                }
                                sum += table[row + 1, column];

                                if ((row - lengthShip) + 1 > 0)
                                {
                                    sum += table[row - lengthShip, column];
                                }

                                if (sum == 0)
                                {
                                    buildingShip = true;

                                    for (int i = row; i > row - lengthShip; i--)
                                    {
                                        table[i, column] = v;
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (row + (lengthShip - 1) <= 9)
                            {
                                sum = 0;
                                for (int i = row; i < row + lengthShip; i++)
                                {
                                    sum += table[i, column];
                                    sum += table[i, column + 1];
                                    sum += table[i, column - 1];
                                }
                                sum += table[row - 1, column];

                                if ((row + lengthShip) - 1 < 9)
                                {
                                    sum += table[row + lengthShip, column];
                                }

                                if (sum == 0)
                                {
                                    buildingShip = true;

                                    for (int i = row; i < row + lengthShip; i++)
                                    {
                                        table[i, column] = v;
                                    }
                                }

                            }
                        }
                    }
                }

            }

        }
        #endregion

        // FullScreen: method used to maximize the cmd window :)
        /*public void FullScreen()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);
        }*/

        // HelpMenu: this method includes all the info and rules of the game
        public void HelpMenu()
        {
            Console.Clear(); // clear console
            BannerCredits(now, true); // print banner.
            Console.WriteLine("\n\n " + "[".Pastel("0cebd1") + "+".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" The rules are quite simple: ".Pastel("FFFFFF"));
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "-".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" The Game Will Position The Ships In Random Format: The Position of the ships are completely randomized by the game.".Pastel("FFFFFF"));
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "-".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Guess the position of the ships: ".Pastel("FFFFFF"));
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "1".Pastel("FFFFFF") + "] {".Pastel("0cebd1") + $" If the player guesses more than 8 times, and didnt hit any ships, he failed. ".Pastel("FFFFFF") + "}".Pastel("0cebd1"));
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "2".Pastel("FFFFFF") + "] {".Pastel("0cebd1") + $" If the player guesses 5 times in a row in the correct positions, he will be given 'professional' rank. ".Pastel("FFFFFF") + "}".Pastel("0cebd1"));
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "3".Pastel("FFFFFF") + "] {".Pastel("0cebd1") + $" If the player guesses 2 ships in a row, he will be given 'expert' rank. ".Pastel("FFFFFF") + "}".Pastel("0cebd1"));
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "4".Pastel("FFFFFF") + "] {".Pastel("0cebd1") + $" Else, the player will be given 'noob' rank. ".Pastel("FFFFFF") + "}".Pastel("0cebd1"));
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "-".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Tips for the game: ".Pastel("FFFFFF"));
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "1".Pastel("FFFFFF") + "] {".Pastel("0cebd1") + $" Fire at the center of the board.Statistically, you are more likely to hit a ship if you aim for the center of the board, so start there ".Pastel("FFFFFF") + "}".Pastel("0cebd1"));
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "2".Pastel("FFFFFF") + "] {".Pastel("0cebd1") + $" Move away when you have two misses in the same segment. If you strike out twice when firing, try firing into a different segment of the board. ".Pastel("FFFFFF") + "}".Pastel("0cebd1"));
            Console.WriteLine("\n " + "[".Pastel("0cebd1") + "3".Pastel("FFFFFF") + "] {".Pastel("0cebd1") + $" This means once you hit a ship you need to make your target the surrounding area. No matter what type of a square it is fire on all the areas to get the next target. ".Pastel("FFFFFF") + "}".Pastel("0cebd1"));
            Console.Write("\n " + "{".Pastel("0cebd1") + "$".Pastel("FFFFFF") + "}".Pastel("0cebd1") + $" {name}".Pastel("0cebd1") + $", Press Enter To Return: ".Pastel("FFFFFF"));
            Console.ReadLine(); // wait for user input
        }

        /*public void PrintTable()
        {

            Console.Clear();
            UI ui = new UI();
            ui.BannerCredits(now, true);
            Console.WriteLine("\n\n " + "[".Pastel("0cebd1") + "D|D".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Destroyers\t".Pastel("FFFFFF") + "[".Pastel("0cebd1") + "D|D".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Destroyers\t".Pastel("FFFFFF") + "[".Pastel("0cebd1") + "C|C|C".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Cruiser\t\t".Pastel("FFFFFF") + "[".Pastel("0cebd1") + "B|B|B|B".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Battleship\t".Pastel("FFFFFF") + "[".Pastel("0cebd1") + "A|A|A|A|A".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Aircraft\t".Pastel("FFFFFF"));
            Console.WriteLine("\n Guesses: " + guesses);
            Console.WriteLine("");

            Console.WriteLine("  |".Pastel("0cebd1") + "1 2 3 4 5 6 7 8 9 10".Pastel("FFFFFF"));

            Console.WriteLine("--+---------------------".Pastel("0cebd1"));

            for (int k = 0; k < 10; k++)
            {

                Console.Write((k + 1));
                if (k == 9)
                {
                    Console.Write("|".Pastel("0cebd1"));
                }
                else
                {
                    Console.Write(" |".Pastel("0cebd1"));
                }

                for (int j = 0; j < 10; j++)
                {
                    Console.Write(table[k, j]);



                    Console.Write(" ");


                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }*/
        
        // PrintTableNew: method used to print the table.
        public void PrintTableNew()
        {
            //PrintTable();
            Console.Clear(); // clear console
            UI ui = new UI(); // ui child
            ui.BannerCredits(now, true); // print banner
            // info of the ships
            Console.WriteLine("\n\n " + "[".Pastel("0cebd1") + "D|D".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Destroyers\t".Pastel("FFFFFF") + "[".Pastel("0cebd1") + "D|D".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Destroyers\t".Pastel("FFFFFF") + "[".Pastel("0cebd1") + "C|C|C".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Cruiser\t\t".Pastel("FFFFFF") + "[".Pastel("0cebd1") + "B|B|B|B".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Battleship\t".Pastel("FFFFFF") + "[".Pastel("0cebd1") + "A|A|A|A|A".Pastel("FFFFFF") + "]".Pastel("0cebd1") + $" Aircraft\t".Pastel("FFFFFF"));
            // player stats during the game.
            Console.WriteLine("\n Guesses: " + guesses);
            Console.WriteLine("\n hits: " + hits);
            Console.WriteLine("\n ships hits: " + totalShipsHit);
            Console.WriteLine("\n ships hit row: " + shipsHit);

            Console.WriteLine("");

            // here print the table.
            Console.WriteLine("  |".Pastel("0cebd1") + "0 1 2 3 4 5 6 7 8 9".Pastel("FFFFFF"));

            Console.WriteLine("--+---------------------".Pastel("0cebd1"));

            for (int k = 0; k < 10; k++)
            {

                Console.Write((k));
                if (k == 9)
                {
                    Console.Write(" |".Pastel("0cebd1"));
                }
                else
                {
                    Console.Write(" |".Pastel("0cebd1"));
                }

                for (int j = 0; j < 10; j++)
                {
                    Console.Write(tableRealTime[k, j]); // print the value in k,j cords



                    Console.Write(" "); // print space


                }
                Console.WriteLine(); // print new line
            }

            Console.WriteLine(); // print new line

        }
    }
}