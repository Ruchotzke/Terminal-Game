using System.Collections.Generic;
using terminalgame.computing.hardware.display;

namespace terminalgame.computing.hardware
{
    /// <summary>
    /// Some sort of monitor for displaying text.
    /// </summary>
    public class Monitor : IHwComponent
    {

        public Display DisplayManager;
        
        public float MaxPowerDraw()
        {
            return 0;
        }

        public float PollJoulesUsed()
        {
            return 0;
        }

        public string Category()
        {
            return "Interfaces:Monitor";
        }

        public string Name()
        {
            return "Monitor";
        }

        public List<string> Capabilities()
        {
            return new List<string>()
            {
                "Display:Text",
            };
        }
    }
}