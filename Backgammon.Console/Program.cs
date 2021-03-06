﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backgammon.Model;
using Fixtures;

namespace Backgammon.ConsoleUI
{
    public class Program
    {
        static Board Board{ get; set; }
        static Dice Dice { get; set; }
        static void Main(string[] args)
        {
            Dice = new Dice();
            Console.WriteLine("Welcome to the best backgammon game on the internet");
            Console.WriteLine("The game will start soon");
            Console.Write("Please pick a colour White or Black :");
            var ColourPicker = Console.ReadLine();
            Console.WriteLine("The Board is now initializing good luck :)");
            BoardRender();
            Console.WriteLine("White to move first ");

            do
            {
                Move(Globals.Gcolour);
                Console.ReadKey();
            } while (Board.Locations[50].Number<15 | Board.Locations[51].Number<15);
            if (Board.Locations[50].Number == 15) 
            {
                Console.WriteLine("Congratulations Black has won the game!!!");
            }
            else
            {
                Console.WriteLine("Congratulations White has won the game!!!");
            }
            Console.ReadKey();


        }

       

        
        public static void BoardRender()
        {
            Board = new Board(Dice); 
            StringBuilder topPiece = new StringBuilder();
            StringBuilder bottomPiece = new StringBuilder();
            topPiece.Append("-----------------");
            Console.WriteLine(topPiece.ToString());
            int count = 23;
            int dots = 7;
            
            for (int i = 0; i < 12; i++)
            {
                if (i==6)
                {
                    Console.WriteLine("|---------------|" + "   Pieces taken by the other player; Black pieces =" + Board.Locations[40].Number + " White pieces =" + Board.Locations[41].Number);// +"Pieces taken off by the player; Black pieces =" + Board.Locations[26].Number + " White pieces =" + Board.Locations[27].Number);
                }
                StringBuilder piece = new StringBuilder();
                piece.Append("|");
                var zeroOrOne = Board.Locations[i].Colour;
                var zeroOrOneC = Board.Locations[count].Colour;
                if (zeroOrOne == Colours.White)
                {
                    for (int a = 0; a < Board.Locations[i].Number; a++)
                    {
                        piece.Append("0");
                    }
                }
                else if (zeroOrOne == Colours.Black)
                {
                    for (int a = 0; a < Board.Locations[i].Number; a++)
                    {
                        piece.Append("1");
                    }
                }
                for (int c = 0; c < dots - Board.Locations[i].Number; c++)
                {
                    piece.Append(".");
                }
                piece.Append("|");
                for (int c = 0; c < dots - Board.Locations[count].Number; c++)
                {
                    piece.Append(".");
                }
                if (zeroOrOneC == Colours.White)
                {
                    for (int a = 0; a < Board.Locations[count].Number; a++)
                    {
                        piece.Append("0");
                    }
                }
                else if (zeroOrOneC == Colours.Black)
                {
                    for (int a = 0; a < Board.Locations[count].Number; a++)
                    {
                        piece.Append("1");
                    }
                }
                piece.Append("|");
                count = count - 1;
                Console.WriteLine(piece.ToString());
            }
            bottomPiece.Append("-----------------");
            Console.WriteLine(bottomPiece.ToString());

        }
        public static void Move(Colours colour)
        {
            //iteration with the user only
            //Must know which colour it is to roll dice.
            Console.WriteLine("It is " + colour + " turn");
            Console.Write("Press D to roll the Dice :");
            Console.ReadKey();
            Console.WriteLine();
            var roll1 = Dice.Throw();
            var roll2 = Dice.Throw();
            Console.WriteLine("You rolled a "+roll1+" and a "+roll2);
            //output dice           
            if (Board.ValidMoves(colour, roll1).Count == 0 & Board.ValidMoves(colour,roll2).Count == 0)
            {
                Console.WriteLine("No valid moves available");
                return;
            }
            if (roll1 == roll2)
            {
                Console.WriteLine("You rolled a double! This means that you get 4 throws");

                MoveOneDice(colour, roll1);
                MoveOneDice(colour, roll2);
                MoveOneDice(colour, roll1);
                MoveOneDice(colour, roll2);
                if (colour == Colours.White)
                {
                    Globals.Gcolour = Colours.Black;
                }
                else
                {
                    Globals.Gcolour = Colours.White;
                }
                return;
            }
            Console.WriteLine("Select the dice that you would like to use first roll number 1 or 2 :");
            var dicePicker = Console.ReadLine();           
            if (dicePicker == "1")
            {
                MoveOneDice(colour, roll1);
                MoveOneDice(colour, roll2);
            }
            else
            {
                MoveOneDice(colour, roll2);
                MoveOneDice(colour, roll1);
            }
            if (colour == Colours.White)
            {
                Globals.Gcolour = Colours.Black;
            }
            else
            {
                Globals.Gcolour = Colours.White;
            }


        }

        public static void MoveOneDice(Colours colour, int roll)
        {
            var repeater = "notValid";
            do
            {
                int pieceNumber = 0;
                Console.WriteLine("If your piece has been taken you piece will come in on the dice you chose press 0 to continue in this case");
                var availableMoves = Board.ValidLocationsPiecesCanGo(colour);
                Console.WriteLine("Select a piece from the Board to move. E.G. press 1 to move the pieces in location 1 :");
                pieceNumber = Convert.ToInt32(Console.ReadLine());
                if (Board.executeMove(Globals.Gcolour, pieceNumber, roll) == "Valid")
                {
                    repeater = "Valid";                   
                }
                else if (Board.executeMove(Globals.Gcolour, pieceNumber, roll) == "No available moves")
                {
                    Console.WriteLine("No valid move available skipping turn");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid input press enter to restart selection process");
                    Console.ReadKey();
                }
            } while (repeater == "notValid");
            BoardOutputter();



        }
        public static void BoardOutputter()
        {
            StringBuilder topPiece = new StringBuilder();
            StringBuilder bottomPiece = new StringBuilder();
            topPiece.Append("-----------------");
            Console.WriteLine(topPiece.ToString());
            int count = 23;
            int dots = 7;
            for (int i = 0; i < 12; i++)
            {
                if (i == 6)
                {
                    Console.WriteLine("|---------------|" + "          Pieces that have been taken by the other player; Black pieces =" + Board.Locations[40].Number + " White pieces =" + Board.Locations[41].Number +" B:"+ Board.Locations[50].Number+" W:"+Board.Locations[51].Number);
                }
                StringBuilder piece = new StringBuilder();
                piece.Append("|");
                var zeroOrOne = Board.Locations[i].Colour;
                var zeroOrOneC = Board.Locations[count].Colour;
                if (zeroOrOne == Colours.White)
                {
                    for (int a = 0; a < Board.Locations[i].Number; a++)
                    {
                        piece.Append("0");
                    }
                }
                else if (zeroOrOne == Colours.Black)
                {
                    for (int a = 0; a < Board.Locations[i].Number; a++)
                    {
                        piece.Append("1");
                    }
                }
                for (int c = 0; c < dots - Board.Locations[i].Number; c++)
                {
                    piece.Append(".");
                }
                piece.Append("|");
                for (int c = 0; c < dots - Board.Locations[count].Number; c++)
                {
                    piece.Append(".");
                }
                if (zeroOrOneC == Colours.White)
                {
                    for (int a = 0; a < Board.Locations[count].Number; a++)
                    {
                        piece.Append("0");
                    }
                }
                else if (zeroOrOneC == Colours.Black)
                {
                    for (int a = 0; a < Board.Locations[count].Number; a++)
                    {
                        piece.Append("1");
                    }
                }
                piece.Append("|");
                count = count - 1;
                Console.WriteLine(piece.ToString());
            }
            bottomPiece.Append("-----------------");
            Console.WriteLine(bottomPiece.ToString());
        }
              
    }
}
