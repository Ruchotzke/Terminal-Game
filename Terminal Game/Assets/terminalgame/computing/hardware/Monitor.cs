using System.Collections.Generic;
using terminalgame.computing.os.display;
using terminalgame.ingame;

namespace terminalgame.computing.hardware
{
    /// <summary>
    /// Some sort of monitor for displaying text.
    /// </summary>
    public class Monitor : IHwComponent
    {
        /// <summary>
        /// The driver link back to the OS.
        /// </summary>
        public DisplayDriver OsLink;

        /// <summary>
        /// The link to the in-game display.
        /// </summary>
        public MonitorManager RealLink;
        
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

        public void Tick(float delta)
        {
            /* Grab the new screen from the driver */
            List<string> screen = OsLink.GetScreen();
            
            /* Now update the text string on the monitor */
            string text = "";
            foreach (string s in screen)
            {
                text += s + "<br>";
            }
            RealLink.UpdateText(text);
        }
    }
}