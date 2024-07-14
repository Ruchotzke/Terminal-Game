using System;
using terminalgame.computing.hardware;
using TMPro;
using UnityEngine;

namespace terminalgame.ingame
{
    public class MonitorManager : MonoBehaviour
    {

        public Monitor Monitor;

        public TextMeshProUGUI Screen;
        
        private void Awake()
        {
            Monitor = new Monitor();
            Monitor.RealLink = this;
        }

        /// <summary>
        /// Update the text on the screen.
        /// </summary>
        /// <param name="m"></param>
        public void UpdateText(string text)
        {
            Screen.text = text;
        }
    }
}
