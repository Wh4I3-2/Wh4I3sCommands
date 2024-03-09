using Outpath_Modding.GameConsole.Components;
using System;

namespace Wh4I3sCommands.Commands
{
    public class Credits : ICommand
    {
        public string Command { get; set; } = "credits";
        public string[] Abbreviate { get; set; } = new string[] { "crdt" };
        public string Description { get; set; } = "Change credits.\n\tcredits <+|-|*|/|=> <Value=1>";

        public bool Execute(string[] args, out string reply)
        {
            if (args.Length <= 0) {
                reply = "Invalid syntax!";
                return false;
            }

            float value = 1.0f;
            if (args.Length > 1)
            {
                value = float.Parse(args[1]);
            }

            switch (args[0]) {
                case "+":
                    PlayerGarden.instance.credits += (int)Math.Round(value);
                    reply = $"Added {(int)Math.Round(value)} Credits; new value is {PlayerGarden.instance.credits}";
                    break;
                case "-":
                    PlayerGarden.instance.credits -= (int)Math.Round(value);
                    reply = $"Removed {(int)Math.Round(value)} Credits; new value is {PlayerGarden.instance.credits}";
                    break;
                case "*":
                    PlayerGarden.instance.credits = (int)Math.Round(PlayerGarden.instance.credits * value);
                    reply = $"Multiplied Credits by {value}; new value is {PlayerGarden.instance.credits}";
                    break;
                case "/":
                    PlayerGarden.instance.credits = (int)Math.Round(PlayerGarden.instance.credits / value);
                    reply = $"Divided Credits by {value}; new value is {PlayerGarden.instance.credits}";
                    break;
                case "=": 
                    PlayerGarden.instance.credits = (int)Math.Round(value);
                    reply = $"Set Credits to {PlayerGarden.instance.credits}";
                    break;
                default:
                    reply = $"Invalid syntax!";
                    return false;
            }
            PlayerGarden.instance.UpdateCreditsText();
            PlayerGarden.instance.cellsAnim.Play("CellCounter_Pop", -1, 0f);
            PlayerGarden.instance.creditsParticles.Play();
            return true;
        }
    }
}