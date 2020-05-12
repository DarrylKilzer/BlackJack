using System;
using System.Collections.Generic;

namespace blackjack.Models
{
  public class Game
  {
    public List<string> Deck { get; set; }
    public List<string> Hand { get; set; }
    public int HandValue { get; set; }
    public void Setup()
    {
      List<string> newDeck = BuildDeck();
      Deck = ShuffleDeck(newDeck);
      Hand = new List<string>();
      Draw();
      Draw();
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

    internal void EvaluateHand()
    {
      HandValue = 0;
      int aces = 0;
      foreach (string card in Hand)
      {
        int cardValue = GetCardValue(card.Split(" ")[0]);
        if (cardValue == 1)
        {
          aces++;
        }
        else
        {
          HandValue += cardValue;
        }
      }
      //add aces if less than 21
      if (aces > 0 && HandValue + 11 <= 21 && aces < 2)
      {
        aces--;
        HandValue += 11;
      }
      HandValue += aces;
    }

    internal void WinOrLose()
    {
      System.Console.WriteLine(HandValue);
    }

    private int GetCardValue(string val)
    {
      int output = 0;
      switch (val.ToLower())
      {
        case "2":
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
      Draw();
    }

    private void Draw()
    {
      Hand.Add(Deck[0]);
      Deck.RemoveAt(0);
    }

    internal void Stand()
    {
      System.Console.WriteLine("You hold tight.");
    }

    internal void PrintHand()
    {
      System.Console.Write("Hand:");
      foreach (string card in Hand)
      {
        System.Console.Write($" {card} ");
      }
      System.Console.WriteLine();
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