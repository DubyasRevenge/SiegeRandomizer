using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;

class Program
{
  //This program randomizes 7 Rainbow Six Siege operators in the console to choose for you
  //Potential improvement include randomized primary and secondary weapon as well as gadgets
  //Loadout randomizing would be a secondary selection, otherwise 7 random loadouts take up way too much of the terminal

  static List<string> attackers = new List<string>
  {
    "sledge", "thatcher", "thermite", "ash", "glaz", "fuze", "twitch", "montagne",
    "iq", "blitz", "buck", "blackbeard", "capitao", "hibana", "jackal", "ying", "zofia", "dokkaebi", "finka", "lion",
    "maverick", "nomad", "gridlock", "nokk", "amaru", "kali", "iana", "ace", "zero", "flores", "osa", "sens", "grim",
    "brava", "ram", "deimos", "striker", "rauora", "striker"
  };

  static List<string> defenders = new List<string>
  {
    "smoke", "mute", "castle", "pulse", "doc", "rook", "kapkan", "tachanka",
    "jager", "bandit", "frost", "valkyrie", "caveira", "echo", "mira", "lesion", "ela", "vigil", "alibi", "maestro",
    "clash", "kaid", "mozzie", "warden", "goyo", "wamai", "oryx", "melusi", "aruni", "thunderbird", "thorn", "azami",
    "solis", "fenrir", "tubarao", "skopos", "sentry"
  };

  private static List<string> defenseBans = new List<string>();
  private static List<string> attackBans = new List<string>();
  

  static void Main()
  {
    Console.WriteLine("---------- Welcome to the operator randomizer ----------");  
    Console.WriteLine("-------------------- Type your side --------------------");
    Console.WriteLine("--------- Or type 'Exit' to close the program ----------");
    Console.WriteLine("--- You can also type 'Reset' to restart the program ---");

    int loop = 0;
    do
    {
      for (int i = 0; i < 9; i++)
      {
        Console.WriteLine("Select Side");
        string startSide = Console.ReadLine()?.Trim().ToLower();

        if (startSide == "reset")
        {
          attackBans.Clear();
          defenseBans.Clear();
          Console.WriteLine("New game started");
        }
        else if (startSide == "atk")
        {
          //Console.WriteLine("Enter the next banned attacker");
          DisplayBannedOperators(attackers, attackBans, "Attackers");
          DisplayRandomOperators(attackers, "Attackers");
        }
        else if (startSide == "def")
        {
          //Console.WriteLine("Enter the next banned defender");
          DisplayBannedOperators(defenders, defenseBans, "Defenders");
          DisplayRandomOperators(defenders, "Defenders");
        }
        else if (startSide == "exit")
        {
          loop++;
          break;
        }
        else
        {
          Console.WriteLine("Invalid input. Please try again.");
        }
      }
    } while (loop < 1);



    static void DisplayRandomOperators(List<string> operators, string side)
    {
      var random = new Random();
      var selected = operators.OrderBy(_ => random.Next()).Take(7).ToList();

      Console.WriteLine($"\n{side}:");
      foreach (var op in selected)
      {
        Console.WriteLine($"- {op}");
      }
    }

    static void DisplayBannedOperators(List<string> operators, List<string> bannedList, string side)
    {
      Console.WriteLine($"Enter a(n) {side} operator to ban (or type 'none' to skip ban):");
      string input = Console.ReadLine()?.Trim().ToLower();

      // Check if input is "none" to skip the ban
      if (input == "none")
      {
        Console.WriteLine("No operators were banned this round.");
        return; // Exit the function if 'none' is entered
      }

      // If the input is invalid, prompt again until a valid one is entered
      if (!operators.Contains(input) || bannedList.Contains(input))
      {
        Console.WriteLine("Invalid input or operator already banned. Please try again.");
        DisplayBannedOperators(operators, bannedList, side); // Recursively call the function to retry
        return; // Return from the current function after retrying
      }

      // If the input is valid, ban the operator and remove it from the selection pool
      bannedList.Add(input);
      operators.Remove(input);
      Console.WriteLine($"{input} has been banned and removed from {side}.");
    }
  }
}