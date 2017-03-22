using System;

namespace DeckOfCards
{
    class GameOfCards
    {
        public static void Main(string[] args)
        {
            var deck = new Cards();

            deck.Shuffle();
            Console.WriteLine("\n--------------------------Shuffled Deck--------------------------\n");
            deck.View();

            deck.Sort();
            Console.WriteLine("\n--------------------------Sorted Deck----------------------------\n");
            deck.View();

            Console.ReadLine();
        }
    }
}