using Outpath_Modding.API.Config;

namespace Wh4I3sCommands
{
    public class Config : IConfig
    {
        public bool Enable { get; set; } = true;

        public int TestInt { get; set; } = 10;
    }
}