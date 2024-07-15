

using System.Collections.Generic;
using terminalgame.computing.os.display;
using terminalgame.computing.os.processing;
using UnityEngine;

namespace terminalgame.computing.os
{

    /// <summary>
    /// The OS is responsible for driving both the hardware and interface.
    /// </summary>
    public class OS
    {
        /// <summary>
        /// The installation size of this OS.
        /// </summary>
        public uint InstallSize = 8000;

        private DisplayDriver _primary;

        /// <summary>
        /// The hardware running underneath this OS.
        /// </summary>
        private HwManager _hardware;

        private TaskManager _taskManager;
        
        /// <summary>
        /// Boot the operating system.
        /// </summary>
        /// <param name="resources">A reference to available hardware resources.</param>
        public void Boot(HwManager resources)
        {
            _hardware = resources;
            _taskManager = new TaskManager();
            
            /* Ensure the required hardware components are present */
            // TODO
            
            /* Generate a new display driver for each display */
            // TODO, support multiple
            _primary = new DisplayDriver(this);
            foreach (var mon in _hardware.MonitorCatalog)
            {
                mon.OsLink = _primary;
            }
            
            /* Print some test material to the display */
            _primary.PrintLn("00001 101 010 101 0100101 010 10 1010 1010 0101010 101 0101 010 010 01");
            _primary.SlideUpwards();
            _primary.PrintLn("00001 101 010 101 0100101 010 10 1010 1010 0101010 101 0101 010 010 01");
            _primary.PrintLn("00001 101 010 101 0100101 010 10 1010 1010 0101010 101 0101 010 010 01");
            _primary.PrintLn("00001 101 010 101 0100101 010 10 1010 1010 0101010 101 0101 010 010 01");
            _primary.PrintLn("00001 101 010 101 0100101 010 10 1010 1010 0101010 101 0101 010 010 01");
            _primary.PrintLn("00001 101 010 101 0100101 010 10 1010 1010 0101010 101 0101 010 010 01");
            _primary.PrintLn("00001 101 010 101 0100101 010 10 1010 1010 0101010 101 0101 010 010 01");
            _primary.PrintLn("00001 101 010 101 0100101 010 10 1010 1010 0101010 101 0101 010 010 01");
            _primary.PrintLn("00001 101 010 101 0100101 010 10 1010 1010 0101010 101 0101 010 010 01");
            _primary.PrintLn("00001 101 010 101 0100101 010 10 1010 1010 0101010 101 0101 010 010 01");
            _primary.PrintLn("00001 101 010 101 0100101 010 10 1010 1010 0101010 101 0101 010 010 01");
        }
        
        /// <summary>
        /// Tick the time forward for the OS.
        /// </summary>
        /// <param name="dt"></param>
        public void Tick(float dt)
        {
            /* Update the processes */
            _taskManager.OnTick(dt);
        }

        /// <summary>
        /// Enqueue a given task on the OS.
        /// Sorted based on task type.
        /// </summary>
        /// <param name="p">The process to enqueue.</param>
        /// <returns>True if scheduled, otherwise false.</returns>
        public bool EnqueueTask(Process p)
        {
            _taskManager.EnqueueTask(p);
            return true;
        }
    }
}