

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

        /// <summary>
        /// Input management for this OS.
        /// </summary>
        private InputManager _inputManager;

        /// <summary>
        /// The string used for prompting the user.
        /// </summary>
        private string _promptString = "$> ";
        
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
        private char _cursorCharacter = '\u2588';

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

        /// <summary>
        /// Whether or not the OS is accepting input.
        /// </summary>
        private bool _acceptingInput = true;

        /// <summary>
        /// The current input string to the OS (anything following the prompt)
        /// </summary>
        private string _currentInput = "";

        /// <summary>
        /// How many chars were changed in the last tick (used to make screen updates reasonable/efficient).
        /// </summary>
        private int _numCharsChanged = 0;
        
        #endregion
        
        /// <summary>
        /// Boot the operating system.
        /// </summary>
        /// <param name="resources">A reference to available hardware resources.</param>
        public void Boot(HwManager resources)
        {
            _hardware = resources;
            _taskManager = new TaskManager();
            _inputManager = new InputManager();
            
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
            PrintPromptAndInput();
        }
        
        /// <summary>
        /// Tick the time forward for the OS.
        /// </summary>
        /// <param name="dt"></param>
        public void Tick(float dt)
        {
            /* Update the processes */
            _taskManager.OnTick(dt, _hardware);
            
            /* Handle any input */
            HandleInput();
            
            /* Print the current view */
            if (_numCharsChanged > 0 && _acceptingInput)
            {
                PrintPromptAndInput();
                _numCharsChanged = 0;
            }
            
            /* Update the cursor */
            _cursorTimer -= dt;
            if (_cursorTimer <= 0.0f)
            {
                _cursorTimer = _cursorBlinkDelay;
                _cursorOn = !_cursorOn;
                _numCharsChanged += 1;
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
        /// Process any input in the input stream.
        /// </summary>
        private void HandleInput()
        {
            bool cursorHasMoved = false;
            
            /* First move the cursor position */
            int horizMove = _inputManager.HorizontalCursorPoll();
            if (horizMove != 0)
            {
                /* Move the cursor */
                _cursorCol = Mathf.Clamp(_cursorCol + horizMove, 0, _currentInput.Length);
                _numCharsChanged += 2;
                cursorHasMoved = true;
            }
            
            /* Attempt to print all characters while input is accepted */
            while (_acceptingInput)
            {
                char? next = _inputManager.GetNextChar();

                if (next == null) break;

                if (next.Value is '\n' or '\r')
                {
                    /* Newline/enter pressed */
                    _acceptingInput = false;
                    _primary.SetChar(_cursorRow, _cursorCol + _promptString.Length, ' ');
                    HandleCommand(_currentInput);
                    _currentInput = "";
                    _cursorCol = 0;
                }
                else if (next.Value == '\b')
                {
                    /* Backspace */
                    if (_cursorCol == 0) continue;
                    _currentInput = _currentInput.Remove(_cursorCol - 1, 1);
                    _cursorCol -= 1;
                    _numCharsChanged += 1;
                    cursorHasMoved = true;
                }
                else
                {
                    /* Any other character */
                    if (_cursorCol == _primary.Size.cols - 1)
                    {
                        /* maintain a char at the end of the line */
                        //TODO
                        continue;
                    }

                    _currentInput = _currentInput.Insert(_cursorCol, "" + next.Value);
                    _cursorCol += 1;
                    _numCharsChanged += 1;
                    cursorHasMoved = true;
                }
            }
            
            /* Reset the cursor so its visible */
            if (cursorHasMoved)
            {
                _cursorTimer = _cursorBlinkDelay;
                _cursorOn = true;
            }
        }

        /// <summary>
        /// Print the OS prompt along with the input/cursor currently used.
        /// </summary>
        private void PrintPromptAndInput()
        {
            if (!_cursorOn)
            {
                _primary.SetStr(_cursorRow, 0, _promptString + _currentInput + " ", false);
            }
            else
            {
                string newstr = _promptString + _currentInput.Substring(0, _cursorCol) + _cursorCharacter;
                if (_cursorCol < _currentInput.Length - 1) newstr += _currentInput.Substring(_cursorCol + 1);
                else newstr += " ";
                _primary.SetStr(_cursorRow, 0, newstr, false);
            }
        }

        /// <summary>
        /// Handle the command offered to the OS.
        /// </summary>
        /// <param name="command">the command to process</param>
        private void HandleCommand(string command)
        {
            /* First split the command into argv */
            string[] split = command.Split();
            
            /* Match based on the first element (the command) */
            switch (split[0])
            {
                case "echo":
                    /* Echo: print out whatever else was supplied */
                    string merge = "";
                    if (split.Length > 1)
                    {
                        for (int i = 1; i < split.Length; i++)
                        {
                            merge += split[i] + " ";
                        }
                    }

                    _primary.PrintLn(merge);
                    _primary.PrintLn("", () => _acceptingInput = true);
                    break;
                default:
                    _primary.PrintLn(split[0] + " is not a recognized command.");
                    _primary.PrintLn("", () => _acceptingInput = true);
                    break;
            }
        }
    }
}