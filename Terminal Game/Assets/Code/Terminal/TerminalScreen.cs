using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Terminal
{
    
    /// <summary>
    /// The screen of a terminal - contains output from the computer.
    /// </summary>
    public class TerminalScreen : MonoBehaviour
    {

        /// <summary>
        /// The character used for the terminal cursor.
        /// </summary>
        public const char CURSOR_CHARACTER = '_';
        
        /// <summary>
        /// The output text object.
        /// </summary>
        public TextMeshProUGUI ScreenOutput;

        /// <summary>
        /// The amount of time the cursor takes to turn on and off.
        /// </summary>
        public float CursorFlashDelay = 1f;

        private string _terminalText = "";
        private float _cursorTimer = 0.0f;
        private bool _cursorEnabled = false;
        private int _cursorPosition;

        private void Start()
        {
            _terminalText = ScreenOutput.text;
            _cursorPosition = _terminalText.Length;
        }

        private void Update()
        {
            /* Get the input string */
            string input = Input.inputString;
            
            /* If left or right arrows are used, move the cursor */
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _cursorPosition = Mathf.Max(0, _cursorPosition - 1);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _cursorPosition = Mathf.Min(_terminalText.Length, _cursorPosition + 1);
            }

            /* Update the terminal text */
            if (input != "")
            {
                /* Add new items */
                foreach (char letter in input)
                {
                    if (letter is '\n' or '\r')
                    {
                        _terminalText = _terminalText.Insert(_cursorPosition, "\n");
                        _cursorPosition += 1;
                    }
                    else if (letter == '\b')
                    {
                        if (_terminalText.Length > 0)
                        {
                            _terminalText = _terminalText.Remove(_cursorPosition-1, 1);
                            _cursorPosition -= 1;
                        }
                    }
                    else
                    {
                        _terminalText = _terminalText.Insert(_cursorPosition, "" + letter);
                        _cursorPosition += 1;
                    }
                }
            }
            
            /* Update the cursor timer */
            _cursorTimer -= Time.deltaTime;
            if (_cursorTimer <= 0.0f)
            {
                _cursorEnabled = !_cursorEnabled;
                _cursorTimer = CursorFlashDelay;
            }
            
            /* Render the text to the screen, including the cursor as needed */
            if (_cursorEnabled)
            {
                ScreenOutput.text = InsertCursor(_cursorPosition, _terminalText);
            }
            else
            {
                ScreenOutput.text = _terminalText;
            }
        }

        /// <summary>
        /// Insert the cursor at the given position.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private string InsertCursor(int index, string content)
        {
            if (index == content.Length)
            {
                content += CURSOR_CHARACTER;
            }
            else
            {
                /* Add tags around the char being hovered */
                /* Insert closing first to make sure the index isn't altered */
                content = content.Insert(index + 1, "</u>");
                content = content.Insert(index, "<u>");
            }

            return content;
        }
    }
}

