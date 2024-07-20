using terminalgame.ingame;
using UnityEngine;

namespace terminalgame.computing.os
{
    /// <summary>
    /// Input management for the OS.
    /// </summary>
    public class InputManager
    {

        /// <summary>
        /// The aggregate of all buffered keyboard strokes since the last poll.
        /// </summary>
        private KeyboardInputManager.KeyboardPacket _aggregate;
        
        /// <summary>
        /// Generate a new input manager.
        /// </summary>
        public InputManager()
        {
            KeyboardInputManager.Instance.Listeners.Add(UpdateInput);

            _aggregate = new KeyboardInputManager.KeyboardPacket();
        }

        /// <summary>
        /// A callback used to update keyboard input.
        /// </summary>
        /// <param name="packet"></param>
        public void UpdateInput(KeyboardInputManager.KeyboardPacket packet)
        {
            _aggregate += packet;
        }

        /// <summary>
        /// Poll the horizontal change in cursor.
        /// </summary>
        /// <returns></returns>
        public int HorizontalCursorPoll()
        {
            int tmp = _aggregate.HorizontalDirection;
            _aggregate.HorizontalDirection = 0;

            return tmp;
        }
        
        /// <summary>
        /// Poll the vertical change in cursor.
        /// </summary>
        /// <returns></returns>
        public int VerticalCursorPoll()
        {
            int tmp = _aggregate.VerticalDirection;
            _aggregate.VerticalDirection = 0;

            return tmp;
        }

        /// <summary>
        /// Pop the next char off of the input string.
        /// </summary>
        /// <returns></returns>
        public char? GetNextChar()
        {
            if (_aggregate.InputString.Length == 0) return null;
            
            char first = _aggregate.InputString[0];
            _aggregate.InputString = _aggregate.InputString.Substring(1);

            return first;
        }

        /// <summary>
        /// Pop a string of length maxChars off of the input string.
        /// </summary>
        /// <param name="maxChars"></param>
        /// <returns></returns>
        public string GetNextChars(int maxChars)
        {
            if (_aggregate.InputString.Length == 0) return "";
            
            int numPlaces = Mathf.Min(maxChars, _aggregate.InputString.Length);
            string first = _aggregate.InputString.Substring(0, numPlaces);
            _aggregate.InputString = _aggregate.InputString.Substring(numPlaces);
            return first;
        }
    }
}