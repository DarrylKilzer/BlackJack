using System;
using System.Collections.Generic;

namespace blackjack.Models
{
  public class Game
  {
    public List<string> Deck { get; set; }
    public List<string> Hand { get; set; }
    public List<string> DealerHand { get; set; }
    public int HandValue { get; set; }
    public int DealerHandValue { get; set; }
    public void Setup()
    {
      List<string> newDeck = BuildDeck();
      Deck = ShuffleDeck(newDeck);
      Hand = new List<string>();
      DealerHand = new List<string>();
      Draw(DealerHand);
      Draw(DealerHand);
      Draw(Hand);
      Draw(Hand);
    }

    private List<string> BuildDeck()
    {
      List<string> suits = new List<string> { "Diamonds ", "Clubs ", "Hearts ", "Spades " };
      List<string> values = new List<string> { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
      List<string> cards = new List<string>();

      foreach (string suit in suits)
      {
        foreach (string value in values)
        {
          cards.Add($"{value} of {suit}");
        }
      }
      return cards;
    }

    internal int EvaluateHand(List<string> hand)
    {
      int handValue = 0;
      int aces = 0;
      foreach (string card in hand)
      {
        int cardValue = GetCardValue(card.Split(" ")[0]);
        if (cardValue == 1)
        {
          aces++;
        }
        else
        {
          handValue += cardValue;
        }
      }
      if (aces > 0 && handValue + 11 <= 21 && aces < 2)
      {
        aces--;
        handValue += 11;
      }
      handValue += aces;
      return handValue;
    }

    internal bool WinOrLose()
    {
      PrintHand(false);
      System.Console.WriteLine("\nYour hand is worth " + HandValue + "\nThe Dealer hand is worth " + DealerHandValue);
      if ((DealerHandValue < HandValue && HandValue <= 21) || DealerHandValue > 21)
      {
        System.Console.WriteLine("You won!");
      }
      else
      {
        System.Console.WriteLine("You lost!");
      }
      System.Console.WriteLine("Do you want to play again? Y/N");
      if (Console.ReadLine().ToLower() == "y")
      {
        Setup();
        return true;
      }
      else
      {
        return false;
      }
    }

    private int GetCardValue(string val)
    {
      int output = 0;
      switch (val.ToLower())
      {
        case "2":
        case "3":
        case "4":
        case "5":
        case "6":
        case "7":
        case "8":
        case "9":
          int.TryParse(val, out output);
          break;
        case "10":
        case "king":
        case "queen":
        case "jack":
          output = 10;
          break;
        case "ace":
          output = 1;
          break;
      }
      return output;
    }

    internal void Hit()
    {
      System.Console.WriteLine("You draw a card.");
      Draw(Hand);
      DealerPlay();
    }

    private void Draw(List<string> hand)
    {
      hand.Add(Deck[0]);
      Deck.RemoveAt(0);
    }

    internal void EvaluateHands()
    {
      DealerHandValue = EvaluateHand(DealerHand);
      HandValue = EvaluateHand(Hand);
    }
    internal void Stand()
    {
      System.Console.WriteLine("You hold tight.");
      EvaluateHands();
      DealerPlay();
    }

    private void DealerPlay()
    {
      EvaluateHands();
      if (DealerHandValue < 17)
      {
        Draw(DealerHand);
        DealerHandValue = EvaluateHand(DealerHand);
      }
    }
    internal void PrintHand(bool limited)
    {
      System.Console.Write("\nHand:");
      foreach (string card in Hand)
      {
        System.Console.Write($" {card} ");
      }
      if (limited)
      {
        System.Console.WriteLine();
        System.Console.Write("\nDealer hand:");
        System.Console.Write($" {DealerHand[0]}\n\n");
      }
      else
      {
        System.Console.Write("\nDealer Hand:");
        foreach (string card in DealerHand)
        {
          System.Console.Write($" {card} ");
        }
      }
    }

    private List<string> ShuffleDeck(List<string> deck)
    {
      Random random = new Random();
      for (int n = deck.Count - 1; n > 0; --n)
      {
        int k = random.Next(n + 1);
        string card = deck[n];
        deck[n] = deck[k];
        deck[k] = card;
      }
      return deck;
    }

  }
}