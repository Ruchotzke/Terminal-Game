using System.Collections.Generic;
using terminalgame.computing.os.processing;
using UnityEngine;
using UnityEngine.UIElements;

namespace terminalgame.computing.os.display
{
    /// <summary>
    /// A class used to simulate a terminal display.
    /// </summary>
    public class DisplayDriver
    {
        /// <summary>
        /// The size of the terminal.
        /// </summary>
        public (int rows, int cols) Size = (21,80);

        public char[,] Screen;

        /// <summary>
        /// A temporary holder for data during processing.
        /// </summary>
        private char[,] _backBuffer;

        private OS _os;
        
        public DisplayDriver(OS parentOS)
        {
            _os = parentOS;
            
            /* Initialize and clear without OS intervention */
            Screen = new char[Size.rows, Size.cols];
            for (int r = 0; r < Size.rows; r++)
            {
                for (int c = 0; c < Size.cols; c++)
                {
                    Screen[r, c] = ' ';
                }
            }
        }

        /// <summary>
        /// Clear the screen.
        /// </summary>
        /// <param name="cls">The character to clear the screen with.</param>
        public void ClearScreen(char cls = ' ')
        {
            Process p = new Process(Size.rows * Size.cols / 100.0f);
            p.OnUpdate = (current, needed, dwu) =>
            {
                float i = 0.0f;
                for (int r = 0; r < Size.rows; r++)
                {
                    for (int c = 0; c < Size.cols && i < current; c++, i += 0.01f)
                    {
                        Screen[r, c] = cls;
                    }
                }
            };

            _os.EnqueueTask(p);
        }

        /// <summary>
        /// Slide all text upwards by a given number of rows, clearing the bottom rows with the supplied char.
        /// </summary>
        /// <param name="rows">The number of rows to slide upwards.</param>
        /// <param name="cls">The char to clear bottom rows with.</param>
        /// <param name="clearBot">True if the bottom needs to be cleared, false if left alone (more performant)</param>
        public void SlideUpwards(int rows = 1, char cls = ' ', bool clearBot = true)
        {
            if (!clearBot)
            {
                Process p = new Process((Size.rows - rows) * Size.cols * 0.0001f);
                p.OnStart = () =>
                {
                    _backBuffer = new char[Size.rows, Size.cols];
                    for (int r = 0; r < Size.rows; r++)
                    {
                        for (int c = 0; c < Size.cols; c++)
                        {
                            _backBuffer[r, c] = Screen[r, c];
                        }
                    }
                };
                p.OnUpdate = (current, needed, dwu) =>
                {
                    /* Slide upwards */
                    int i;
                    float curr = 0.0f;
                    for (i = 0; i < Size.rows - rows; i++)
                    {
                        for (int c = 0; c < Size.cols && curr < current; c++, curr += 0.0001f)
                        {
                            Screen[i, c] = _backBuffer[i + rows, c];
                        }
                    }
                };

                _os.EnqueueTask(p);
            }
            else
            {
                Process p = new Process(Size.rows * Size.cols * 0.0001f);
                p.OnStart = () =>
                {
                    _backBuffer = new char[Size.rows, Size.cols];
                    for (int r = 0; r < Size.rows; r++)
                    {
                        for (int c = 0; c < Size.cols; c++)
                        {
                            _backBuffer[r, c] = Screen[r, c];
                        }
                    }
                };
                p.OnUpdate = (current, needed, dwu) =>
                {
                    /* Slide upwards */
                    int i;
                    float curr = 0.0f;
                    for (i = 0; i < Size.rows - rows; i++)
                    {
                        for (int c = 0; c < Size.cols && curr < current; c++, curr += 0.0001f)
                        {
                            Screen[i, c] = _backBuffer[i + rows, c];
                        }
                    }

                    /* Clear remaining rows if requested */
                    for (; i < Size.rows; i++)
                    {
                        for (int c = 0; c < Size.cols && curr < current; c++, curr += 0.0001f)
                        {
                            Screen[i, c] = cls;
                        }
                    }
                };

                _os.EnqueueTask(p);
            }
        }

        /// <summary>
        /// Set the char at a given position.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="ch"></param>
        public void SetChar(int row, int col, char ch)
        {
            Process p = new Process(.01f);
            p.OnConclude = () =>
            {
                Screen[row, col] = ch;
            };
            _os.EnqueueTask(p);
        }

        /// <summary>
        /// Set the chars at a given position to a supplied string.
        /// If off the screen, the chars are clipped.
        /// </summary>
        /// <param name="row">The row to put the string in</param>
        /// <param name="col">The offset of the str.</param>
        /// <param name="str">The string to place. Will be clipped if off screen.</param>
        public void SetStr(int row, int col, string str)
        {
            Process p = new Process(str.Length * 0.01f);
            p.OnUpdate = (current, needed, dwu) =>
            {
                float curr = 0f;
                for (int c = col, i = 0; c < Size.cols && i < str.Length && curr < current; c++, i++, curr += 0.01f)
                {
                    Screen[row, c] = str[i];
                }
            };

            _os.EnqueueTask(p);
        }

        /// <summary>
        /// Print a new line to the bottom of the terminal.
        /// </summary>
        /// <param name="str">The string to print. Does NOT need newline termination.</param>
        public void PrintLn(string str)
        {
            str = str.TrimEnd();

            SlideUpwards();
            
            SetStr(Size.rows - 1, 0, str);
        }

        /// <summary>
        /// Get the contents of a given row as a string.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string GetLine(int row)
        {
            string ret = "";
            for (int c = 0; c < Size.cols; c++)
            {
                ret += Screen[row, c];
            }

            return ret.TrimEnd();
        }

        /// <summary>
        /// Get the contents of the screen as a list of strings.
        /// Row zero is at the top.
        /// </summary>
        /// <returns></returns>
        public List<string> GetScreen()
        {
            List<string> ret = new List<string>();
            for (int r = 0; r < Size.rows; r++)
            {
                ret.Add(GetLine(r));
            }

            return ret;
        }
    }
}