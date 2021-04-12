using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject
{
    public enum Alphabet
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z
    }
    // ******************************************************************************************************
    //                                                                                                      *
    // Title: Capstone Project                                                                              *
    // Description: Hangman Program                                                                         *
    // Application Type: Console                                                                            *
    // Author: Cameron Carlson                                                                              *
    // Dated Created: 4/11/2021                                                                             *
    // Last Modified: 4/12/2021                                                                             *
    //                                                                                                      *
    // ******************************************************************************************************
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Gray;

            DisplayWelcomeScreen();
            List<string> phrase = null;
            List<string> guess = null;
            phrase = DisplayGetPhraseFromUser();
            DisplayStartGame(phrase);
            DisplayClosingScreen();
        }

        static void DisplayStartGame(List<string> phrase)
        {
            string userRepsonse;
            bool validGuess;
            bool finishedGame = false;
            int mistakes;
            Alphabet letter;
            List<string> guesses = new List<string>();
            DisplayScreenHeader("Start Game");

            //
            // repeat guessing until they guessed the word, or they lost
            //
            do
            {
                //
                // get guess from user and validate it
                //
                do
                {
                    validGuess = true;
                    Console.Write("\tGuess Letter: ");
                    userRepsonse = Console.ReadLine().ToUpper();
                    if (int.TryParse(userRepsonse, out int integerGuess))
                    {
                        validGuess = false;
                        Console.WriteLine("\tPlease enter a valid guess");
                    }
                    else if (Enum.TryParse(userRepsonse, out letter))
                    {
                        guesses.Add(letter.ToString());
                    }
                    else
                    {
                        validGuess = false;
                        Console.WriteLine("\tPlease enter a valid guess");
                    }
                } while (!validGuess);

                mistakes = DisplayCheckGuessForMistakes(guesses, phrase);

                //
                // Check to see if the game is finished
                //
                if (mistakes == 6)
                {
                    finishedGame = true;
                    Console.WriteLine();
                    Console.WriteLine("Looks like you lost this time.");
                    Console.Write("The Word/Phrase was: ");
                    foreach (string character in phrase)
                    {
                        Console.Write($"{character}");
                    }
                }

            } while (!finishedGame);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Check users guess
        /// </summary>
        /// <param name="guesses"></param>
        /// <param name="phrase"></param>
        /// <returns></returns>
        static int DisplayCheckGuessForMistakes(List<string> guesses, List<string> phrase)
        {
            int mistakes = 0;
            int strikes = 0;
            foreach (string guess in guesses)
            {
                foreach (string letter in phrase)
                {
                    if (guess != letter)
                    {
                        strikes++;
                    }
                }
                if (strikes == phrase.Count)
                {
                    mistakes++;
                }
                strikes = 0;
            }

            DisplayDrawHangman(mistakes);

            return mistakes;
        }

        /// <summary>
        /// Draw the hangman picture
        /// </summary>
        /// <param name="mistakes"></param>
        static void DisplayDrawHangman(int mistakes)
        {
            switch (mistakes)
            {
                case 0:
                    DisplayHangman0Mistakes();
                    break;

                case 1:
                    DisplayHangman1Mistakes();
                    break;

                case 2:
                    DisplayHangman2Mistakes();
                    break;

                case 3:
                    DisplayHangman3Mistakes();
                    break;

                case 4:
                    DisplayHangman4Mistakes();
                    break;

                case 5:
                    DisplayHangman5Mistakes();
                    break;

                case 6:
                    DisplayHangman6Mistakes();
                    break;

                default:

                    break;
            }
        }

        /// <summary>
        /// Get Word or Phrase From User
        /// </summary>
        /// <returns></returns>
        static List<string> DisplayGetPhraseFromUser()
        {
            string userResponse;
            bool validResponse;
            List<string> phrase = new List<string>();
            DisplayScreenHeader("Word/Phrase");

            Console.WriteLine();
            Console.Write("\tEnter a word or phrase: ");
            do
            {
                validResponse = true;
                userResponse = Console.ReadLine().ToUpper();
                if (userResponse == "")
                {
                    validResponse = false;
                }
            } while (!validResponse);
            char[] characters = userResponse.ToCharArray();
            foreach (char character in characters)
            {
                if (Enum.TryParse(character.ToString(), out Alphabet letter))
                {
                    phrase.Add(letter.ToString());
                }
                else if (character.ToString() == " ")
                {
                    phrase.Add(character.ToString());
                }
            }

            DisplayContinuePrompt();

            return phrase;
        }



        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tHangman");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using our Hangman Application!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion

        #region HangemanPictures
        static void DisplayHangman0Mistakes()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("         _________                 ");
            Console.WriteLine("        |         |         ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("       ___________|______        ");

            DisplayContinuePrompt();
        }

        static void DisplayHangman1Mistakes()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("         _________                 ");
            Console.WriteLine("        |         |         ");
            Console.WriteLine("       ( )        |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("       ___________|______        ");

            DisplayContinuePrompt();
        }

        static void DisplayHangman2Mistakes()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("         _________                 ");
            Console.WriteLine("        |         |         ");
            Console.WriteLine("       ( )        |        ");
            Console.WriteLine("        |         |        ");
            Console.WriteLine("        |         |        ");
            Console.WriteLine("        |         |        ");
            Console.WriteLine("        |         |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("       ___________|______        ");

            DisplayContinuePrompt();
        }

        static void DisplayHangman3Mistakes()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("         _________                 ");
            Console.WriteLine("        |         |         ");
            Console.WriteLine("       ( )        |        ");
            Console.WriteLine("        |         |        ");
            Console.WriteLine("       /|         |        ");
            Console.WriteLine("      / |         |        ");
            Console.WriteLine("        |         |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("       ___________|______        ");

            DisplayContinuePrompt();
        }

        static void DisplayHangman4Mistakes()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("         _________                 ");
            Console.WriteLine("        |         |         ");
            Console.WriteLine("       ( )        |        ");
            Console.WriteLine("        |         |        ");
            Console.WriteLine("       /|\\        |        ");
            Console.WriteLine("      / | \\       |        ");
            Console.WriteLine("        |         |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("       ___________|______        ");

            DisplayContinuePrompt();
        }

        static void DisplayHangman5Mistakes()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("         _________                 ");
            Console.WriteLine("        |         |         ");
            Console.WriteLine("       ( )        |        ");
            Console.WriteLine("        |         |        ");
            Console.WriteLine("       /|\\        |        ");
            Console.WriteLine("      / | \\       |        ");
            Console.WriteLine("        |         |        ");
            Console.WriteLine("       /          |        ");
            Console.WriteLine("      /           |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("       ___________|______        ");

            DisplayContinuePrompt();
        }

        static void DisplayHangman6Mistakes()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("         _________                 ");
            Console.WriteLine("        |         |         ");
            Console.WriteLine("       ( )        |        ");
            Console.WriteLine("        |         |        ");
            Console.WriteLine("       /|\\        |        ");
            Console.WriteLine("      / | \\       |        ");
            Console.WriteLine("        |         |        ");
            Console.WriteLine("       / \\        |        ");
            Console.WriteLine("      /   \\       |        ");
            Console.WriteLine("                  |        ");
            Console.WriteLine("       ___________|______        ");

            DisplayContinuePrompt();
        }

        #endregion
    }
}
