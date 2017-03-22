using System;
using System.Collections.Generic;
using System.Linq;

namespace DeckOfCards
{
    public class Cards
    {
        public List<Card> Deck { get; set; }

        public Cards()
        {
            Deck = new List<Card>();

            //Create a standard deck of cards with four suits of 13 cards each
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    Deck.Add(new Card(suit, value));
                }
            }
        }

        public void Sort()
        {
            //Sorts by Suit then card value
            Deck = Deck.OrderBy(x => x.Suit).ThenBy(x => x.Value).ToList();
        }

        public void Shuffle()
        {
            //This is not the best way to do a random number generation, and is not thread safe.  However, in this usage context 
            //it is unlikely to cause a problem.
            var rnd = new Random();

            for (var x = 0; x < Deck.Count; x++)
            {
                Swap(x, rnd.Next(x, Deck.Count));
            }
        }

        public void View()
        {
            //Print out cards ordered from top of deck to bottom
            Deck.ForEach(card => Console.WriteLine($"{card.Value.ToString()} \t| {card.Suit.ToString()}"));
        }

        private void Swap(int i, int j)
        {
            var temp = Deck[i];
            Deck[i] = Deck[j];
            Deck[j] = temp;
        }
    }
}