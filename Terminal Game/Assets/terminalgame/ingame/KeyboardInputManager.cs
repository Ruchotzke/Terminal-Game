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

            /// <summary>
            /// Add (merge) two keyboard packets.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static KeyboardPacket operator +(KeyboardPacket a, KeyboardPacket b)
            {
                KeyboardPacket o = new KeyboardPacket();

                o.InputString = a.InputString + b.InputString;

                o.HorizontalDirection = a.HorizontalDirection + b.HorizontalDirection;
                o.VerticalDirection = a.VerticalDirection = b.VerticalDirection;

                return o;
            }
        }

        /// <summary>
        /// A subscriber function for keyboard updates.
        /// </summary>
        public delegate void KeyboardUpdateSubscriber(KeyboardPacket key);

        /// <summary>
        /// All listeners for keyboard updates.
        /// </summary>
        public List<KeyboardUpdateSubscriber> Listeners;
        
        private void Awake()
        {
            /* Apply the singleton */
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;

            Listeners = new List<KeyboardUpdateSubscriber>();
        }

        private void Update()
        {
            /* Generate a new object */
            KeyboardPacket curr = new KeyboardPacket();
            curr.InputString = Input.inputString;
            curr.HorizontalDirection = 0;
            curr.HorizontalDirection += (Input.GetKeyDown(KeyCode.RightArrow) ? 1 : 0);
            curr.HorizontalDirection -= (Input.GetKeyDown(KeyCode.LeftArrow) ? 1 : 0);
            curr.VerticalDirection = 0;
            curr.VerticalDirection += (Input.GetKeyDown(KeyCode.UpArrow) ? 1 : 0);
            curr.VerticalDirection -= (Input.GetKeyDown(KeyCode.DownArrow) ? 1 : 0);
            
            /* Notify all subscribers */
            foreach (var sub in Listeners)
            {
                sub(curr);
            }
        }
    }

}
