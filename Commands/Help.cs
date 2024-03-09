using Outpath_Modding.GameConsole.Components;
using System;

namespace Wh4I3sCommands.Commands
{
    public class Help : ICommand
    {
        public string Command { get; set; } = "help";
        public string[] Abbreviate { get; set; } = new string[] { "hlp" };
        public string Description { get; set; } = "Tells you the description of a command.\n\thelp <Command|ALL>";

        public bool Execute(string[] args, out string reply)
        {
            if (args.Length > 0)
            {
                if (args[0] == "ALL")
                {
                    reply = "";
                    for (int i = 0; i < CommandManager.Commands.Count; i++)
                    {
                        reply += $"\n\t{CommandManager.Commands[i].Command}: {CommandManager.Commands[i].Description}";
                    }
                    return true;
                }
                for (int i = 0; i < CommandManager.Commands.Count; i++)
                {
                    if (CommandManager.Commands[i].Command != args[0]) 
                        continue;
                    reply = $"\n\t{CommandManager.Commands[i].Command}: {CommandManager.Commands[i].Description}";
                    return true;
                }
                reply = "Invalid Command!";
                return false;
            }

            reply = "";
            for (int i = 0; i < CommandManager.Commands.Count; i++)
            {
                reply += $"\n\t{CommandManager.Commands[i].Command}: {CommandManager.Commands[i].Description}";
            }
            return true;
        }
    }
}