using System.Collections.Generic;

namespace terminalgame.computing.hardware
{
    /// <summary>
    /// A general interface for hardware.
    /// </summary>
    public interface IHwComponent
    {
        /// <summary>
        /// Get the power draw (or supply) of this component.
        /// </summary>
        /// <returns>A float wattage representing maximal use.</returns>
        public float MaxPowerDraw();
        
        /// <summary>
        /// Get the wattage consumed since the last poll to simulate dynamic power draw.
        /// </summary>
        /// <returns>The number of joules of power used since the last poll. Should be below MaxPowerDraw().</returns>
        public float PollJoulesUsed();
        
        /// <summary>
        /// Return the general category this component fits into.
        /// </summary>
        /// <returns>A string name for the category.</returns>
        public string Category();

        /// <summary>
        /// The name of this component.
        /// </summary>
        /// <returns>A string containing the name of ths component.</returns>
        public string Name();

        /// <summary>
        /// Get the capabilities of this component.
        /// </summary>
        /// <returns>An array of capability strings.</returns>
        public List<string> Capabilities();
    }
}