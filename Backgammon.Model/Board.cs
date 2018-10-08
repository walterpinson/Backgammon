﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.Model
{
    public class Board
    {
        public Dice Dice { get; set; }
        public Dictionary<int, Location> Locations { get; set; }
        public Board(Dice dice)
        {
            Dice = dice;
            Locations = new Dictionary<int, Location>();
            //Creation of the board alocating the location to a name.
            Locations[0] = new Location(2, Colours.White);
            Locations[1] = new Location(0, Colours.Empty);
            Locations[2] = new Location(0, Colours.Empty);
            Locations[3] = new Location(0, Colours.Empty);
            Locations[4] = new Location(0, Colours.Empty);
            Locations[5] = new Location(5, Colours.Black);
            Locations[6] = new Location(0, Colours.Empty);
            Locations[7] = new Location(3, Colours.Black);
            Locations[8] = new Location(0, Colours.Empty);
            Locations[9] = new Location(0, Colours.Empty);
            Locations[10] = new Location(0, Colours.Empty);
            Locations[11] = new Location(5, Colours.White);
            Locations[12] = new Location(5, Colours.Black);
            Locations[13] = new Location(0, Colours.Empty);
            Locations[14] = new Location(0, Colours.Empty);
            Locations[15] = new Location(0, Colours.Empty);
            Locations[16] = new Location(3, Colours.White);
            Locations[17] = new Location(0, Colours.Empty);
            Locations[18] = new Location(5, Colours.White);
            Locations[19] = new Location(0, Colours.Empty);
            Locations[20] = new Location(0, Colours.Empty);
            Locations[21] = new Location(0, Colours.Empty);
            Locations[22] = new Location(0, Colours.Empty);
            Locations[23] = new Location(2, Colours.Black);
            Locations[24] = new Location(0, Colours.Empty);//where exposed pieces go(black)
            Locations[25] = new Location(0, Colours.Empty);//where exposed pieces go(white)
            Locations[26] = new Location(0, Colours.Empty);//Where black pieces are taken off the board in end game
            Locations[27] = new Location(0, Colours.Empty);//Where white pieces are taken off the board in end game
        }

        public bool EndGameChecker(Colours colour)//Checks for the condtions of end game for black pieces.
        {
            var adder = 0;
            if (colour == Colours.Black)
            {
                for (int i = 0; i <= 5; i++)
                {
                    if (Locations[i].Colour == Colours.White)
                    {
                        adder = adder + 0;
                    }
                    else
                    {
                        adder = adder + Locations[i].Number;
                    }
                }
                if (adder == 15)//minus the location of the pieces where they are taken off as he might be taken 
                {
                    return true;
                }
                else
                { return false; };
            }
            else
            {
                for (int i = 18; i <= 23; i++)
                {
                    if (Locations[i].Colour == Colours.Black)
                    {
                        adder = adder + 0;
                    }
                    else
                    {
                        adder = adder + Locations[i].Number;
                    }
                }
                if (adder == 15)
                {
                    return true;
                }
                else
                { return false; };
            }

        }

        public List<int> ValidMoves(Colours colour)
        {
            return Locations.Where(kvp => kvp.Value.Colour == colour || kvp.Value.Number <= 1).Select(kvp => kvp.Key).ToList();
        }
        public List<int> ValidMovesWhenTakenWhite(Colours colour)
        {
            return Locations.Where(kvp => kvp.Value.Colour == colour || kvp.Value.Number <= 1).Select(kvp => kvp.Key).Take(5).ToList();
        }
        public List<int> ValidMovesWhenTakenBlack(Colours colour)
        {
            return Locations.Where(kvp => kvp.Value.Colour == colour || kvp.Value.Number <= 1).Select(kvp => kvp.Key).Skip(17).Take(6).ToList();
        }
        public void executeMove(Colours colour, int piecelocation, int diceValue)
        {
            //roll dice
            //select piece to move(must be a valid piece white cannot move black
            //show valid moves
            //move piece number shown on dice
            //add to the location the new piece
            //use index of key of dictionary to add onto pieces  
            //output board to the user 
            //cant output to the console in model so that is the problem with this get the move implemented tommorow.
            var availableMoves = ValidMoves(colour);
            var availableMovesTakenWhite = ValidMovesWhenTakenWhite(colour);
            var availableMovesTakenBlack = ValidMovesWhenTakenBlack(colour);
            var minimumBlack = CalculateMinimumBlack();
            var maximumWhite = CalculateMaximumWhite();

            if (piecelocation - diceValue < 0 & piecelocation - diceValue == minimumBlack & colour == Colours.Black & EndGameChecker(Colours.Black) == true)
            {
                var fromLocation = Locations[piecelocation];
                fromLocation.RemoveOnePiece();
                var toLocation = Locations[26];
                toLocation.AddOnePiece(Colours.Black);
                return;
            }
            else if (piecelocation +diceValue >=24 & piecelocation + diceValue == maximumWhite & colour == Colours.White & EndGameChecker(Colours.White) == true)
            {
                var fromLocation = Locations[piecelocation];
                fromLocation.RemoveOnePiece();
                var toLocation = Locations[27];
                toLocation.AddOnePiece(Colours.White);
                return;
            }
            if (colour == Colours.White & Locations[25].Number >= 1 & availableMovesTakenWhite.Contains(diceValue - 1))   //valid moves for the colour white are between the the numbers 0-5 and this is the only place where you can come back into if you have been taken.
            {
                var fromLocation = Locations[25];
                fromLocation.RemoveOnePiece();
                var toLocation = Locations[diceValue - 1];
                if (colour == Colours.White & Locations[diceValue - 1].Number == 1 & Locations[diceValue - 1].Colour != colour)
                {
                    toLocation.RemoveOnePiece();
                    Locations[24].AddOnePiece(Colours.Black);
                    toLocation.AddOnePiece(colour);
                    return;
                }
                else
                {
                    toLocation = Locations[diceValue - 1];
                    toLocation.AddOnePiece(colour);
                    return;
                }
            }
            else if (colour == Colours.Black & Locations[24].Number >= 1 & availableMovesTakenBlack.Contains(23 - diceValue + 1))   //valid moves for the colour white are between the the numbers 0-5 and this is the only place where you can come back into if you have been taken.
            {
                var fromLocation = Locations[24];
                fromLocation.RemoveOnePiece();
                var toLocation = Locations[23 - diceValue + 1];
                if (colour == Colours.Black & Locations[23 - diceValue + 1].Number == 1 & Locations[23 - diceValue + 1].Colour != colour)
                {
                    toLocation.RemoveOnePiece();
                    Locations[25].AddOnePiece(Colours.White);
                    toLocation.AddOnePiece(colour);
                    return;
                }
                else
                {
                    toLocation = Locations[23 - diceValue + 1];
                    toLocation.AddOnePiece(colour);
                    return;
                }
            }
            if (colour == Colours.White & availableMoves.Contains(piecelocation + diceValue) == false)
            {
                return;
            }
            if (colour == Colours.Black & availableMoves.Contains(piecelocation - diceValue) == false)
            {
                return;
            }
            if (Locations[piecelocation].Colour != colour)
            {
                return;
            }
            if (colour == Colours.White & availableMoves.Contains(piecelocation + diceValue))//used when exposed
            {
                //this will mean the not gcoulor will be added to a location out of board only to be brought in when it can with the help of avilable exposed moves.
                var fromLocation = Locations[piecelocation];
                fromLocation.RemoveOnePiece();

                var toLocation = Locations[piecelocation + diceValue];
                if (colour == Colours.White & Locations[piecelocation + diceValue].Number == 1 & Locations[piecelocation + diceValue].Colour != colour)
                {
                    toLocation.RemoveOnePiece();
                    Locations[24].AddOnePiece(Colours.Black);
                    toLocation.AddOnePiece(colour);
                    return;
                }
                else
                {
                    toLocation = Locations[piecelocation + diceValue];
                    toLocation.AddOnePiece(colour);
                    return;
                }

            }
            else if (colour == Colours.Black & availableMoves.Contains(piecelocation - diceValue))//used when exposed
            {
                //this will mean the not gcoulor will be added to a location out of board only to be brought in when it can with the help of avilable exposed moves.
                var fromLocation = Locations[piecelocation];
                fromLocation.RemoveOnePiece();
                var toLocation = Locations[piecelocation - diceValue];

                if (colour == Colours.Black & Locations[piecelocation - diceValue].Number == 1 & Locations[piecelocation - diceValue].Colour != colour)
                {
                    toLocation.RemoveOnePiece();
                    Locations[25].AddOnePiece(Colours.White);
                    toLocation.AddOnePiece(colour);
                    return;
                }
                else
                {
                    toLocation = Locations[piecelocation - diceValue];
                    toLocation.AddOnePiece(colour);
                    return;
                }

            }
            else
            {
                return;
            }


        }
        public int CalculateMinimumBlack()
        {
            int minimum = -1;
            int count = 0;
            do
            {
                if (Locations[5 - count].Number == 0)
                {
                    minimum = minimum - 1;
                    count = count + 1;
                }
                else
                {
                    return minimum;
                }

            } while (!(Locations[5 - (count - 1)].Number >0));
            return minimum;
        }
        public int CalculateMaximumWhite()
        {
            int maximum = 24;
            int count = 0;
            do
            {
                if (Locations[18 +count].Number==0)
                {
                    maximum = maximum + 1;
                    count = count + 1;
                }
                else
                {
                    return maximum; 
                }

            } while (!(Locations[18 - (count - 1)].Number > 0));
            return maximum;

        }

    }
}