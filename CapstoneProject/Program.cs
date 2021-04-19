using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject
{
    public enum Alphabet
    {
        NONE,
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
    // Last Modified: 4/18/2021                                                                             *
    //                                                                                                      *
    // ******************************************************************************************************
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.CursorVisible = true;
            bool isDone;

            DisplayWelcomeScreen();
            do
            {
                List<string> phrase = null;
                phrase = DisplayGetPhraseFromUser();
                DisplayStartGame(phrase);
                DisplayClosingScreen();
                isDone = DisplayAskToPlayAgain();
            } while (isDone);
        }

        /// <summary>
        /// ask user to play again
        /// </summary>
        /// <returns></returns>
        static bool DisplayAskToPlayAgain()
        {
            string userResponse;
            bool isDone = true;
            bool validResponse;
            DisplayScreenHeader("Play Again?");
            Console.WriteLine();
            Console.WriteLine("Do you want to play again? [yes or no]");
            do
            {
                validResponse = true;
                userResponse = Console.ReadLine().ToLower();
                if (userResponse == "yes")
                {
                    isDone = false;
                }
                else if (userResponse == "no")
                {
                    isDone = true;
                }
                else
                {
                    Console.WriteLine("\tPlease enter Yes or No");
                    validResponse = false;
                }
            } while (!validResponse);

            DisplayContinuePrompt();

            return isDone;
        }

        /// <summary>
        /// Start Game Display
        /// </summary>
        /// <param name="phrase"></param>
        static void DisplayStartGame(List<string> phrase)
        {
            bool finishedGame = false;
            int mistakes = 0;
            List<string> guesses = new List<string>();

            //
            // repeat guessing until they guessed the word, or they lost
            //
            do
            {
                DisplayScreenHeader("Start Game");

                //
                // Check to see if they lost
                //
                if (mistakes == 6)
                {
                    Console.Clear();
                    DisplayScreenHeader("Start Game");
                    
                    finishedGame = true;
                    Console.WriteLine();
                    Console.WriteLine("\t\tLooks like you lost this time.");
                    Console.WriteLine();
                    Console.Write("\t\tThe Word/Phrase was: ");
                    foreach (string character in phrase)
                    {
                        Console.Write($"{character}");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                }

                //
                // Blank Spaces and letters guessed
                //
                finishedGame = DisplayBlankSpacesAndCorrectGuesses(guesses, phrase, finishedGame);

                //
                // Draw Hangman
                //
                DisplayDrawHangman(mistakes);

                //
                // Repeat Letters Guessed
                //
                DisplayRepeatLettersGuessed(guesses);

                //
                // Check to see if they won
                //
                if (finishedGame)
                {
                    break;
                }

                //
                // get guess from user and validate it
                //
                guesses = DisplayValidateUserGuess(guesses);

                //
                // Check if letter guessed is in the word/phrase
                //
                mistakes = DisplayCheckGuessForMistakes(guesses, phrase);

                DisplayContinuePrompt();
                Console.Clear();

            } while (!finishedGame);

            if (mistakes != 6)
            {
                Console.WriteLine();
                Console.WriteLine("\t\tCongratulations! You Won The Game!");
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Repeat Letters Guessed Back To User
        /// </summary>
        /// <param name="guesses"></param>
        static void DisplayRepeatLettersGuessed(List<string> guesses)
        {
            Console.WriteLine();
            Console.Write("\tLetters Guessed: ");
            foreach (string guess in guesses)
            {
                Console.Write(guess + " | ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Write the Phrase with blanks and letters guest
        /// </summary>
        /// <param name="guesses"></param>
        /// <param name="phrase"></param>
        /// <returns></returns>
        static bool DisplayBlankSpacesAndCorrectGuesses(List<string> guesses, List<string> phrase, bool finishedGame)
        {
            int strikes = 0;
            string letterGuess;
            int lettersWrong = phrase.Count();

            Console.Write("\t");
            foreach (string letter in phrase)
            {
                foreach (string guess in guesses)
                {
                    if (guess != letter)
                    {
                        strikes++;
                    }
                    else
                    {
                        break;
                    }
                }
                letterGuess = letter;

                if (letter == " ")
                {
                    Console.Write(" ");
                    lettersWrong--;
                }
                else if (strikes == guesses.Count())
                {
                    Console.Write("-");
                }
                else if (strikes < guesses.Count())
                {
                    Console.Write(letterGuess);
                    lettersWrong--;
                }
                strikes = 0;
            }
            Console.WriteLine();

            if (lettersWrong == 0)
            {
                finishedGame = true;
            }

            return finishedGame;
        }

        /// <summary>
        /// Get guess from user and validate it.
        /// </summary>
        /// <returns></returns>
        static List<string> DisplayValidateUserGuess(List<string> guesses)
        {
            string userResponse;
            Alphabet letter;
            bool validGuess;
            int strikes = guesses.Count();

            do
            {
                validGuess = true;
                Console.Write("\tGuess Letter: ");
                userResponse = Console.ReadLine().ToUpper();
                if (int.TryParse(userResponse, out int integerGuess))
                {
                    validGuess = false;
                    Console.WriteLine("\tPlease enter a valid guess");
                }
                else if (Enum.TryParse(userResponse, out letter))
                {
                    if (guesses.Count() == 0)
                    {
                        guesses.Add(letter.ToString());
                    }
                    else
                    {
                        foreach (string guess in guesses)
                        {
                            if (guess != letter.ToString())
                            {
                                strikes--;
                            }
                        }

                        if (strikes == 0)
                        {
                            guesses.Add(letter.ToString());
                        }
                        else
                        {
                            Console.WriteLine("\tLetter has already been guessed. Please guess another letter");
                        }
                    }
                }
                else
                {
                    validGuess = false;
                    Console.WriteLine("\tPlease enter a valid guess");
                }
            } while (!validGuess);

            return guesses;
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
            int badCharacter;
            List<string> phrase = new List<string>();
            DisplayScreenHeader("Word/Phrase");

            Console.WriteLine();
            Console.WriteLine("\tEnter a word or phrase. ");
            Console.WriteLine("\tNote: Characters other then letters will be automatically removed.");
            Console.WriteLine();
            Console.Write("\tWord/Phrase: ");
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
                if (Enum.TryParse(character.ToString(), out Alphabet letter) && !int.TryParse(character.ToString(), out badCharacter))
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
            Console.ForegroundColor = ConsoleColor.DarkBlue;
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
            Console.ForegroundColor = ConsoleColor.DarkBlue;
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
            Console.ForegroundColor = ConsoleColor.DarkBlue;
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
            Console.ForegroundColor = ConsoleColor.DarkBlue;
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
            Console.ForegroundColor = ConsoleColor.DarkBlue;
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
            Console.ForegroundColor = ConsoleColor.DarkBlue;
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
            Console.ForegroundColor = ConsoleColor.DarkBlue;
        }

        #endregion
    }
}
