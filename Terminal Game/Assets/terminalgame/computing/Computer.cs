using System.Collections;
using System.Collections.Generic;
using terminalgame.computing.os;
using UnityEngine;

namespace terminalgame.computing
{
    /// <summary>
    /// A computer is the container for all computing elements in the game.
    /// </summary>
    public class Computer
    {
        /// <summary>
        /// The OS managing this computer.
        /// </summary>
        public OS OperatingSystem;

        /// <summary>
        /// The hardware management system for this computer.
        /// </summary>
        public HwManager HwManager;

        /// <summary>
        /// Start the computer.
        /// </summary>
        public void Boot()
        {
            /* Initialize hardware components */
            OperatingSystem = HwManager.Initialize();
            
            /* Boot the OS if present, passing it a reference to the hardware to manage */
            if (OperatingSystem != null)
            {
                /* Boot normally */
                OperatingSystem.Boot(HwManager);
            }
            else
            {
                /* Hardware only */
                //TODO: Implement BIOS/HW check
            }
        }

        /// <summary>
        /// Tick time forward for this computer.
        /// </summary>
        /// <param name="dt">The amount of time which passed since the last tick.</param>
        public void Tick(float dt)
        {
            /* Tick each hardware component */
            foreach (var hw in HwManager.HwComponents)
            {
                hw.Tick(dt);
            }
            
            /* Tick the OS */
            OperatingSystem.Tick(dt);
        }
    }
}


