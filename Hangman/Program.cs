/*  Hangman game
 * Author: Jonne Kaajalahti
 * Basic hangman game to be played in the console environment
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonneKaajalahti_hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            // Aloitus
            Console.Title = "C# Hangman";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Hangman --- by Jonne Kaajalahti");
            Random rng = new Random();
            Console.ResetColor();

        start:
            Console.WriteLine("Add words to be guessed and type 'next' to continue.");
            // add words to a list
            Console.ForegroundColor = ConsoleColor.White;
            List<string> wordList = new List<string>();
            string addedWord;

            // add words to be guessed
            while (true)
            {
                addedWord = Console.ReadLine().ToLower();
                if (addedWord != "next")
                {
                    wordList.Add(addedWord);
                }
                else
                {
                    goto here;
                }
            }
        here:
            Console.WriteLine("--------------------------");

            // make an array of the list
            string[] wordBank = wordList.ToArray();


            string wordToGuess = wordBank[rng.Next(0, wordBank.Length)]; // select the word in the game randomly
            string wordToGuessUpper = wordToGuess.ToUpper();

            // show the word lenght
            StringBuilder showPlayer = new StringBuilder(wordToGuess.Length);
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                showPlayer.Append('_');
            }

            // create lists for right and wrong answers
            List<char> rightGuesses = new List<char>();
            List<char> wrongGuesses = new List<char>();

            int lifes = 6; // amount of lifes
            bool win = false;
            int revealedLetters = 0;
            string inputtedChat;
            char guess;

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("THE WORD HAS {0} CHARACTERS .", wordToGuess.Length);
            Console.WriteLine("You have {0} tries left.", lifes);


            //  the game asks for characters until you run out of lifes.
            while (!win && lifes > 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Guess a character: ");

                inputtedChat = Console.ReadLine().ToUpper();
                guess = inputtedChat[0];

                if (rightGuesses.Contains(guess))
                {
                    Console.WriteLine("You've already guessed '{0}' and it was correct!", guess);
                    continue;
                }
                else if (wrongGuesses.Contains(guess))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You've already guessed '{0}' and it was wrong!", guess);
                    continue;
                }

                if (wordToGuessUpper.Contains(guess))
                {
                    rightGuesses.Add(guess);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine();
                    Console.WriteLine("Your guess was correct!");
                    for (int i = 0; i < wordToGuess.Length; i++)
                    {
                        if (wordToGuessUpper[i] == guess)
                        {
                            showPlayer[i] = wordToGuess[i];
                            revealedLetters++;
                        }
                    }

                    if (revealedLetters == wordToGuess.Length)
                    {
                        win = true;
                    }
                }
                //  adds the word to wrongGuesses and removes one life
                else
                {
                    wrongGuesses.Add(guess);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("the word does not have '{0}' in it.", guess);
                    lifes--;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("You have {0} tries left.", lifes);
                }

                Console.WriteLine(showPlayer.ToString());
            }

            //  determine the output for winning and losing.
            if (win)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("YOU WON!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("you lost, the correct word was '{0}'", wordToGuess);
            }
            Console.ResetColor();
            Console.WriteLine("Press 'ENTER' if you wish to play again." +
            "\nPress any other key to exit...");

            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Console.Clear();
                goto start;
            }
            else
            {
                Environment.Exit(-1);
            }
        }
    }
}