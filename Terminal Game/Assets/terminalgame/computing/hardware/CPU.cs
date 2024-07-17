using System.Collections.Generic;

namespace terminalgame.computing.hardware
{
    /// <summary>
    /// A basic CPU.
    /// </summary>
    public class CPU : HwComponent
    {

        /// <summary>
        /// Construct a new CPU.
        /// </summary>
        public CPU()
        {
            /* Generate the capabilities for this CPU */
            _capabilities = new Dictionary<string, float>();
            _capabilities.Add("computing", 5f);
            _capabilities.Add("graphics", 2f);
        }
        
        public override float MaxPowerDraw()
        {
            return 65;
        }

        public override float PollJoulesUsed()
        {
            return 0;
        }

        public override string Category()
        {
            return "Computing:CPU";
        }

        public override string Name()
        {
            return "Intel 8086";
        }

        public override void Tick(float delta)
        {
            return;
        }
    }
}