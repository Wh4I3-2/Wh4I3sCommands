using Outpath_Modding.API.Mod;
using Outpath_Modding.GameConsole;
using Outpath_Modding.GameConsole.Components;
using System;
using Wh4I3sCommands.Commands;

namespace Wh4I3sCommands
{
    public class Mod : Mod<Config>
    {
        public override string Author { get; set; } = "Wh4I3";
        public override string Name { get; set; } = "Wh4I3's Commands";
        public override string Description { get; set; } = "A set of helpful commands for debugging and/or messing around.";
        public override Version Version { get; set; } = new Version(0, 0, 2);

        public static Mod Instance { get; private set; }

        public override void OnLoaded()
        {
            Logger.Debug($"Wh4I3's Commands Loaded");
            Instance = this;

            #region Command Registration
            CommandManager.AddCommand(new Help());
            CommandManager.AddCommand(new Give());
            CommandManager.AddCommand(new List());
            CommandManager.AddCommand(new Credits());
            #endregion

            base.OnLoaded();
        }
    }
}