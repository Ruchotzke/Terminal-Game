

using System.Collections.Generic;
using terminalgame.computing.os.display;
using terminalgame.computing.os.processing;

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
            _primary.PrintLn("1");
        }

        private float timer = 1.0f;
        private bool off = true;
        
        /// <summary>
        /// Tick the time forward for the OS.
        /// </summary>
        /// <param name="dt"></param>
        public void Tick(float dt)
        {
            timer -= dt;
            if (timer <= 0.0f)
            {
                timer = .5f;

                if (off)
                {
                    _primary.SlideUpwards();
                }
                else
                {
                    _primary.PrintLn("===========================================================================");
                }
                off = !off;
            }
        }

        /// <summary>
        /// Enqueue a given task on the OS.
        /// Sorted based on task type.
        /// </summary>
        /// <param name="p">The process to enqueue.</param>
        /// <returns>True if scheduled, otherwise false.</returns>
        public bool EnqueueTask(Process p)
        {
            return true;
        }
    }
}