using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace terminalgame.computing
{
    /// <summary>
    /// A computer is the container for all computing elements in the game.
    /// </summary>
    public class Computer
    {
        /// <summary>
        /// The interfaces used to interact with this computer.
        /// </summary>
        public List<Interface> Interfaces;

        /// <summary>
        /// The OS managing this computer.
        /// </summary>
        public OS OperatingSystem;

        /// <summary>
        /// The hardware managment system for this computer.
        /// </summary>
        public HWManager HwManager;
    }
}


