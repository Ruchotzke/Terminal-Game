﻿using System.Collections.Generic;

namespace terminalgame.computing.hardware
{
    /// <summary>
    /// A general interface for hardware.
    /// </summary>
    public abstract class HwComponent
    {

        protected Dictionary<string, float> _capabilities;
        
        /// <summary>
        /// Get the power draw (or supply) of this component.
        /// </summary>
        /// <returns>A float wattage representing maximal use.</returns>
        public abstract float MaxPowerDraw();
        
        /// <summary>
        /// Get the wattage consumed since the last poll to simulate dynamic power draw.
        /// </summary>
        /// <returns>The number of joules of power used since the last poll. Should be below MaxPowerDraw().</returns>
        public abstract float PollJoulesUsed();
        
        /// <summary>
        /// Return the general category this component fits into.
        /// </summary>
        /// <returns>A string name for the category.</returns>
        public abstract string Category();

        /// <summary>
        /// The name of this component.
        /// </summary>
        /// <returns>A string containing the name of ths component.</returns>
        public abstract string Name();

        /// <summary>
        /// Get the capabilities of this component.
        /// </summary>
        /// <returns>An array of capability strings and power levels.</returns>
        public Dictionary<string, float> Capabilities()
        {
            return _capabilities;
        }

        /// <summary>
        /// Tick this components simulation forward a given amount of time.
        /// </summary>
        /// <param name="delta"></param>
        public abstract void Tick(float delta);
    }
}