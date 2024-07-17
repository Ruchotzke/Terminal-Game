using System.Collections.Generic;

namespace terminalgame.computing.hardware
{
    /// <summary>
    /// An implementation of RAM.
    /// </summary>
    public class Memory : HwComponent
    {

        /// <summary>
        /// Generate a new memory module.
        /// </summary>
        /// <param name="amount">The amount of RAM (in KB)</param>
        public Memory(float amount)
        {
            _capabilities = new Dictionary<string, float>();
            _capabilities.Add("memory", amount);
        }
        
        public override float MaxPowerDraw()
        {
            return 5.0f;
        }

        public override float PollJoulesUsed()
        {
            return 0.0f;
        }

        public override string Category()
        {
            return "Memory";
        }

        public override string Name()
        {
            return "RAM DDR1";
        }

        public override void Tick(float delta)
        {
            return;
        }
    }
}