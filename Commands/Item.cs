using Outpath_Modding.GameConsole;
using Outpath_Modding.GameConsole.Components;
using System;

namespace Wh4I3sCommands.Commands
{
    public class Item : ICommand
    {
        public string Command { get; set; } = "item";
        public string[] Abbreviate { get; set; } = new string[] { "itm" };
        public string Description { get; set; } = "Adds, removes or sets an amount of an item. (Replace spaces in names with an underscore)\n\tSyntax: item [Add | Remove | Set] <Name | ID | ALL> <Amount=1>";

        public bool Execute(string[] args, out string reply)
        {
            if (args.Length <= 1)
            {
                reply = "Invalid syntax!";
                return false;
            }
            int amount = 1;
            if (args.Length > 2)
            {
                string amountString = args[2].ToLower();
                if (amountString.EndsWith("k") || amountString.EndsWith("m") || amountString.EndsWith("b"))
                {
                    amount = int.Parse(amountString.Substring(0, amountString.Length - 1));
                    amount *= 1 + 999 * Convert.ToInt32(amountString.EndsWith("k"));
                    amount *= 1 + 999999 * Convert.ToInt32(amountString.EndsWith("m"));
                    amount *= 1 + 999999999 * Convert.ToInt32(amountString.EndsWith("b"));
                }
                else
                    amount = int.Parse(args[2]);
            }
            if (args[1] == "ALL")
            {
                All(amount, args[0]);
                InventoryManager.instance.UpdateInvCategories();
                InventoryManager.instance.UpdateInv();
                switch (args[0].ToLower())
                {
                    case "add":
                        reply = $"Added {amount} of every item.";
                        break;
                    case "remove":
                        reply = $"Removed {amount} of every item.";
                        break;
                    case "set":
                        reply = $"Set {amount} of every item.";
                        break;
                    default:
                        reply = "";
                        return false;
                }
                return true;
            }
            if (int.TryParse(args[1], out int id) == false)
            {
                ItemInfo item = Name(args[1], amount, args[0]);
                InventoryManager.instance.UpdateInvCategories();
                InventoryManager.instance.UpdateInv();
                switch (args[0].ToLower())
                {
                    case "add":
                        reply = $"Added {amount} of item: {item.itemName}";
                        break;
                    case "remove":
                        reply = $"Removed {amount} of item: {item.itemName}";
                        break;
                    case "set":
                        reply = $"Set {amount} of item: {item.itemName}";
                        break;
                    default:
                        reply = "";
                        return false;
                }
                return true;
            }
            else
            {
                ItemInfo _item = ID(id, amount, args[0]);
                InventoryManager.instance.UpdateInvCategories();
                InventoryManager.instance.UpdateInv();
                if (_item == null)
                {
                    reply = "Invalid ID!";
                    return false;
                }
                switch (args[0].ToLower())
                {
                    case "add":
                        reply = $"Added {amount} of item: {_item.itemName}";
                        break;
                    case "remove":
                        reply = $"Removed {amount} of item: {_item.itemName}";
                        break;
                    case "set":
                        reply = $"Set {amount} of item: {_item.itemName}";
                        break;
                    default:
                        reply = "";
                        return false;
                }
                return true;
            }
        }

        private void All(int amount, string mode)
        {
            mode = mode.ToLower();
            switch (mode)
            {
                case "add":
                    for (int i = 0; i < ItemList.instance.itemList.Length; i++)
                    {
                        InventoryManager.instance.AddItemToInv(ItemList.instance.itemList[i], amount);
                    }
                    break;
                case "remove":
                    for (int i = 0; i < ItemList.instance.itemList.Length; i++)
                    {
                        InventoryManager.instance.RemoveItemFromInv(ItemList.instance.itemList[i], amount);
                    }
                    break;
                case "set":
                    for (int i = 0; i < ItemList.instance.itemList.Length; i++)
                    {
                        if (amount > 0)
                        {
                            InventoryManager.instance.RemoveItemFromInv_NoUpdate(ItemList.instance.itemList[i], 2147483647);
                            InventoryManager.instance.AddItemToInv(ItemList.instance.itemList[i], amount);
                        }
                        else
                        {
                            InventoryManager.instance.RemoveItemFromInv(ItemList.instance.itemList[i], 2147483647);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private ItemInfo ID(int id, int amount, string mode)
        {
            if (id > ItemList.instance.itemList.Length || id < 0)
                return null;

            mode = mode.ToLower();
            switch (mode)
            {
                case "add":
                    InventoryManager.instance.AddItemToInv(ItemList.instance.itemList[id], amount);
                    break;
                case "remove":
                    InventoryManager.instance.RemoveItemFromInv(ItemList.instance.itemList[id], amount);
                    break;
                case "set":
                    if (amount > 0)
                    {
                        InventoryManager.instance.RemoveItemFromInv_NoUpdate(ItemList.instance.itemList[id], 2147483647);
                        InventoryManager.instance.AddItemToInv(ItemList.instance.itemList[id], amount);
                    }
                    else
                    {
                        InventoryManager.instance.RemoveItemFromInv(ItemList.instance.itemList[id], 2147483647);
                    }
                    break;
                default:
                    break;
            }
            return ItemList.instance.itemList[id];
        }

        private ItemInfo Name(string name, int amount, string mode)
        {
            for (int id = 0; id < ItemList.instance.itemList.Length; id++)
            {
                if (ItemList.instance.itemList[id].itemName.ToLower().Replace(" ", "_") == name.ToLower())
                {
                    mode = mode.ToLower();
                    switch (mode)
                    {
                        case "add":
                            InventoryManager.instance.AddItemToInv(ItemList.instance.itemList[id], amount);
                            break;
                        case "remove":
                            InventoryManager.instance.RemoveItemFromInv(ItemList.instance.itemList[id], amount);
                            break;
                        case "set":
                            if (amount > 0)
                            {
                                InventoryManager.instance.RemoveItemFromInv_NoUpdate(ItemList.instance.itemList[id], 2147483647);
                                InventoryManager.instance.AddItemToInv(ItemList.instance.itemList[id], amount);
                            }
                            else
                            {
                                InventoryManager.instance.RemoveItemFromInv(ItemList.instance.itemList[id], 2147483647);
                            }
                            break;
                        default:
                            break;
                    }
                    return ItemList.instance.itemList[id];
                }
            }
            return null;
        }
    }
}