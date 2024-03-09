using Outpath_Modding.API.Features.Item;
using Outpath_Modding.GameConsole.Components;

namespace Wh4I3sCommands.Commands
{
    public class List : ICommand
    {
        public string Command { get; set; } = "list";
        public string[] Abbreviate { get; set; } = new string[] { "lst" };
        public string Description { get; set; } = "List the Name and ID of an item or all items.\n\tlist <Name|ID|ALL>";

        public bool Execute(string[] args, out string reply)
        {
            if (args.Length <= 0) {
                reply = "Invalid syntax!";
                return false;
            }
            if (args[0] == "ALL")
            {
                reply = GetAll();
                return true;
            }
            if (int.Parse(args[0]).ToString() == args[0]) {
                reply = GetSpecificID(int.Parse(args[0]));
                if (reply == "")
                {
                    reply = "Invalid ID!";
                    return false;
                }
                return true;
            }
            
            reply = GetSpecificName(args[0]);
            if (reply == "")
            {
                reply = "Invalid Name!";
                return false;
            }
            return true;
        }

        private string GetAll()
        {
            string reply = "";
            for (int id = 0; id < ItemList.instance.itemList.Length; id++)
            {
                reply += $"\n\tID: {id}, Name: {ItemList.instance.itemList[id].itemName}, Type: {ItemList.instance.itemList[id].itemType}";
            }
            return reply;
        }

        private string GetSpecificID(int id)
        {
            if (id > ItemList.instance.itemList.Length || id < 0)
                return "";
            return $"ID: {id}, Name: {ItemList.instance.itemList[id].itemName}, Type: {ItemList.instance.itemList[id].itemType}";
        }

        private string GetSpecificName(string name)
        {
            for (int id = 0; id < ItemList.instance.itemList.Length; id++)
            {
                if (ItemList.instance.itemList[id].itemName.ToLower() == name.ToLower())
                {
                    return $"ID: {id}, Name: {ItemList.instance.itemList[id].itemName}, Type: {ItemList.instance.itemList[id].itemType}";
                }
            }
            return "";
        }
    }
}