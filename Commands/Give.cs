using Outpath_Modding.GameConsole;
using Outpath_Modding.GameConsole.Components;
using System;

namespace Wh4I3sCommands.Commands
{
    public class Give : ICommand
    {
        public string Command { get; set; } = "give";
        public string[] Abbreviate { get; set; } = new string[] { "giv" };
        public string Description { get; set; } = "Gives you an amount of a specified item.\n\tSyntax: give <Name | ID | ALL> <Amount=1>";

        public bool Execute(string[] args, out string reply)
        {
            if (args.Length <= 0)
            {
                reply = "Invalid syntax!";
                return false;
            }
            int amount = 1;
            if (args.Length > 1)
            {
                amount = int.Parse(args[1].Substring(0, args[1].Length - 1)) * (1 + 999 * Convert.ToInt32(args[1].ToLower().EndsWith("k"))) * (1 + 999999 * Convert.ToInt32(args[1].ToLower().EndsWith("m"))) * (1 + 999999999 * Convert.ToInt32(args[1].ToLower().EndsWith("b")));
            }
            if (args[0] == "ALL")
            {
                AddAll(amount);
                reply = $"Added {amount} of every item.";
                return true;
            }
            if (int.TryParse(args[0], out int id) == false)
            {
                ItemInfo item = AddName(args[0], amount);
                reply = $"Added {amount} of item: {item.itemName}";
                return true;
            }
            else
            {
                ItemInfo _item = AddID(id, amount);
                if (_item == null)
                {
                    reply = "Invalid ID!";
                    return false;
                }
                reply = $"Added {amount} of item: {_item.itemName}";
                return true;
            }
        }

        private void AddAll(int amount)
        {
            for (int id = 0; id < ItemList.instance.itemList.Length; id++)
            {
                InventoryManager.instance.AddItemToInv(ItemList.instance.itemList[id], amount);
            }
        }

        private ItemInfo AddID(int id, int amount)
        {
            if (id > ItemList.instance.itemList.Length || id < 0)
                return null;
            InventoryManager.instance.AddItemToInv(ItemList.instance.itemList[id], amount);
            return ItemList.instance.itemList[id];
        }

        private ItemInfo AddName(string name, int amount)
        {
            for (int id = 0; id < ItemList.instance.itemList.Length; id++)
            {
                if (ItemList.instance.itemList[id].itemName.ToLower().Replace(" ", "_") == name.ToLower())
                {
                    InventoryManager.instance.AddItemToInv(ItemList.instance.itemList[id], amount);
                    return ItemList.instance.itemList[id];
                }
            }
            return null;
        }
    }
}