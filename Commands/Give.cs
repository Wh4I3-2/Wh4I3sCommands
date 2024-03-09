using Outpath_Modding.GameConsole.Components;

namespace Wh4I3sCommands.Commands
{
    public class Give : ICommand
    {
        public string Command { get; set; } = "give";
        public string[] Abbreviate { get; set; } = new string[] { "giv" };
        public string Description { get; set; } = "Gives you an amount of a specified item.\n\tgive <Name|ID|ALL> <Amount=1>";

        public bool Execute(string[] args, out string reply)
        {
            if (args.Length <= 0)
            {
                reply = "Invalid syntax!";
                return false;
            }
            int amount = 1;
            if (args.Length > 1) {
                amount = int.Parse(args[1]);
            }
            if (args[0] == "ALL")
            {
                AddAll(amount);
                reply = $"Added {amount} of every item.";
                return true;
            }
            if (int.Parse(args[0]).ToString() == args[0])
            {
                ItemInfo _item = AddID(int.Parse(args[0]), amount);
                if (_item == null)
                {
                    reply = "Invalid ID!";
                    return false;
                }
                reply = $"Added {amount} of item: {_item.itemName}";
                return true;
            }

            ItemInfo item = AddName(args[0], amount);
            if (item == null)
            {
                reply = "Invalid Name!";
                return false;
            }
            reply = $"Added {amount} of item: {item.itemName}";
            return true;
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
                if (ItemList.instance.itemList[id].itemName.ToLower() == name.ToLower())
                {
                    InventoryManager.instance.AddItemToInv(ItemList.instance.itemList[id], amount);
                    return ItemList.instance.itemList[id];
                }
            }
            return null;
        }
    }
}