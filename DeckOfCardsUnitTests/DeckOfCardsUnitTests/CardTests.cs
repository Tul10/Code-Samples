using System;
using System.Collections.Generic;
using System.Linq;
using DeckOfCards;
using Xunit;

namespace DeckOfCardsUnitTests
{
    public class DeckTests
    {
        public class DeckConstructorShould
        {
            [Fact]
            public void Contain52Cards()
            {
                var cards = new Cards();
                Assert.Equal(52, cards.Deck.Count);
            }

            [Fact]
            public void ContainOnlyUniqueCards()
            {
                var cards = new Cards();
                Assert.Equal(52, cards.Deck.Distinct().ToList().Count);
            }

            [Fact]
            public void ContainOnlyFourSuits()
            {
                var cards = new Cards();
                Assert.Equal(4, cards.Deck.Select(card => card.Suit).Distinct().Count());
            }

            [Fact]
            public void ContainThirteenCardsForEachSuit()
            {
                var cards = new Cards();

                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    var cardsOfGivenSuit = cards.Deck.Where(card => card.Suit == suit).ToList();
                    Assert.Equal(13, cardsOfGivenSuit.Count);
                }
            }

            [Fact]
            public void ContainCardValuesForEachSuit()
            {
                var cards = new Cards();

                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    var cardsOfGivenSuit = cards.Deck.Where(card => card.Suit == suit).ToList();

                    foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                    {
                        var cardValueExists = cardsOfGivenSuit.SingleOrDefault(card => card.Value == value);
                        Assert.NotNull(cardValueExists);
                    }
                }
            }
        }

        public class ShuffleShould
        {
            [Fact]
            public void NotAddOrRemoveOrChangeCardsInDeck()
            {
                var cards = new Cards();
                var beforeShuffleList = new List<Card>(cards.Deck);
                cards.Shuffle();
                var afterShuffleList = new List<Card>(cards.Deck);

                beforeShuffleList = beforeShuffleList.OrderBy(card => card.Suit).ThenBy(card => card.Value).ToList();
                afterShuffleList = afterShuffleList.OrderBy(card => card.Suit).ThenBy(card => card.Value).ToList();

                Assert.True(beforeShuffleList.SequenceEqual(afterShuffleList));
            }

            [Fact]
            public void ChangeOrdering()
            {
                var cards = new Cards();
                var preShuffleOrder = new List<Card>(cards.Deck);
                cards.Shuffle();
                Assert.False(preShuffleOrder.SequenceEqual(cards.Deck));
            }

            [Fact]
            public void ChangeOrderingWhenCalledMultipleTimes()
            {
                var cards = new Cards();
                cards.Shuffle();
                var orderAfterFirstShuffle = new List<Card>(cards.Deck);
                cards.Shuffle();

                Assert.False(orderAfterFirstShuffle.SequenceEqual(cards.Deck));
            }
        }

        public class SortShould
        {
            [Fact]
            public void NotAddOrRemoveOrChangeCardsInDeck()
            {
                var cards = new Cards();
                var beforeSortList = new List<Card>(cards.Deck);
                cards.Sort();
                var afterSortList = new List<Card>(cards.Deck);

                beforeSortList = beforeSortList.OrderBy(card => card.Suit).ThenBy(card => card.Value).ToList();
                afterSortList = afterSortList.OrderBy(card => card.Suit).ThenBy(card => card.Value).ToList();

                Assert.True(beforeSortList.SequenceEqual(afterSortList));
            }

            //Sorted Order here means first by suit (Diamonds, clubs, hearts, spades) then by value (Ace to King)
            //as defined in CardValue and CardSuit enums
            [Fact]
            public void ReorderCardsToBeInSortedOrder()
            {
                var cards = new Cards();

                var sortedDeck = new List<Card>(cards.Deck.OrderBy(card => card.Suit).ThenBy(card => card.Value));

                var temp = cards.Deck[1];
                cards.Deck[1] = cards.Deck[2];
                cards.Deck[2] = temp;
                
                cards.Sort();
                
                Assert.True(cards.Deck.SequenceEqual(sortedDeck));
            }

            [Fact]
            public void OrderShouldNotChangeCallingSortMultipleTimes()
            {
                var cards = new Cards();

                var temp = cards.Deck[1];
                cards.Deck[1] = cards.Deck[2];
                cards.Deck[2] = temp;

                cards.Sort();

                var sortedDeck = new List<Card>(cards.Deck);

                cards.Sort();

                Assert.True(cards.Deck.SequenceEqual(sortedDeck));
            }
        }

        public class ViewShould
        {
            [Fact]
            public void NotAddOrRemoveOrChangeOrReorderCardsInDeck()
            {
                var cards = new Cards();
                var beforeViewList = new List<Card>(cards.Deck);
                cards.View();

                Assert.True(beforeViewList.SequenceEqual(cards.Deck));
            }
        }
    }
}
