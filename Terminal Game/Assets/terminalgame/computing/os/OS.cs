﻿

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

        /// <summary>
        /// The primary monitor being used.
        /// </summary>
        private DisplayDriver _primary;

        /// <summary>
        /// The hardware running underneath this OS.
        /// </summary>
        private HwManager _hardware;

        /// <summary>
        /// The task scheduler for this OS.
        /// </summary>
        private TaskManager _taskManager;
        
        #region OS_State

        /// <summary>
        /// The row the cursor is currently in.
        /// </summary>
        private int _cursorRow;
        
        /// <summary>
        /// The column the cursor is currently in.
        /// </summary>
        private int _cursorCol;

        /// <summary>
        /// The character to print the cursor with.
        /// </summary>
        private char _cursorChar = '\u2588';

        /// <summary>
        /// Whether the cursor is being printed or not
        /// </summary>
        private bool _cursorOn = true;

        /// <summary>
        /// The time left until a cursor blink.
        /// </summary>
        private float _cursorTimer = 0.0f;

        /// <summary>
        /// How long the cursor should stay on/off.
        /// </summary>
        private float _cursorBlinkDelay = 0.75f;

        #endregion
        
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
            _primary.PrintLn("BOOT sequence initiated");
            _primary.PrintLn("Cataloguing Hardware Resources...................................done.");
            _primary.PrintLn("Loading Modules..................................................done.");
            _primary.PrintLn("Initializing Task Scheduler......................................done.");
            _primary.PrintLn("BOOT sequence completed in 0.19s");
            _primary.SlideUpwards();
            
            /* Initialize the cursor */
            _cursorRow = _primary.Size.rows - 1;
            _cursorCol = 0;
            
            /* Print the prompt */
            _cursorCol = PrintPrompt();
            _primary.SetChar(_cursorRow, _cursorCol, '\u2588');
        }
        
        /// <summary>
        /// Tick the time forward for the OS.
        /// </summary>
        /// <param name="dt"></param>
        public void Tick(float dt)
        {
            /* Update the processes */
            _taskManager.OnTick(dt, _hardware);
            
            /* Update the cursor */
            _cursorTimer -= dt;
            if (_cursorTimer <= 0.0f)
            {
                _cursorTimer = _cursorBlinkDelay;
                _cursorOn = !_cursorOn;
                _primary.SetChar(_cursorRow, _cursorCol, _cursorOn ? _cursorChar : ' ');
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
            _taskManager.EnqueueTask(p);
            return true;
        }

        /// <summary>
        /// Print the OS prompt to the user.
        /// </summary>
        /// <returns>The new cursor column.</returns>
        private int PrintPrompt()
        {
            string prompt = "$> ";
            return _primary.PrintLn(prompt) + 1; //to account for setstr() trimming
        }
    }
}