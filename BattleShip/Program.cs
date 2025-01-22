using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
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
    internal class Program
    {

        static void Main(string[] args)
        {
            Game game = new Game(); // import game class
            //game.FullScreen(); // full screen the cmd window
            //game.Start();
            UI uI = new UI(); // import ui.
            uI.MainFunc(); // call the MainFunc and start with it

        }
    }
}