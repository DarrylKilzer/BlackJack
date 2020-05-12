using System;
using blackjack.Models;

namespace blackjack
{
  public class App
  {
    public Game Game { get; private set; }
    public bool Playing { get; private set; }
    public void Run()
    {
      Console.Clear();
      System.Console.WriteLine("Welcome to Black Jack!");
      Game = new Game();
      Game.Setup();
      Playing = true;

      while (Playing)
      {
        if (GetUserInput())
        {
          continue;
        }
        Game.EvaluateHands();
        Playing = Game.WinOrLose();
      }
    }

    private bool GetUserInput()
    {
      Game.PrintHand(true);
      System.Console.WriteLine("What would you like to do? Stand / Hit / Quit");
      bool cont = false;
      switch (Console.ReadLine().ToLower())
      {
        case "quit":
        case "q":
          System.Console.WriteLine("Thanks for playing!");
          Playing = false;
          break;
        case "stand":
          Game.Stand();
          break;
        case "hit":
          Game.Hit();
          cont = true;
          break;
        default:
          System.Console.WriteLine("You must \"Stand\", \"Hit\", or \"Quit\"");
          break;
      }
      return cont;
    }
  }
}