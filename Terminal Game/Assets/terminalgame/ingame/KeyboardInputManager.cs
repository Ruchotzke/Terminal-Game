using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace terminalgame.ingame
{
    /// <summary>
    /// The manager used to gather keyboard input for in-game keyboards.
    /// </summary>
    public class KeyboardInputManager : MonoBehaviour
    {
        /// <summary>
        /// The singleton of this manager.
        /// </summary>
        public static KeyboardInputManager Instance;

        /// <summary>
        /// A packet of keyboard input information for a given frame.
        /// </summary>
        public struct KeyboardPacket
        {
            /// <summary>
            /// The printable/character inputs from this frame.
            /// </summary>
            public string InputString;

            /// <summary>
            /// The horizontal direction (positive is right arrow).
            /// </summary>
            public int HorizontalDirection;

            /// <summary>
            /// The vertical direction (positive is up arrow).
            /// </summary>
            public int VerticalDirection;
        }

        private void Awake()
        {
            /* Apply the singleton */
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
        }

        private void Update()
        {
            
        }
    }

}
