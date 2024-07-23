using TMPro;
using UnityEngine;

namespace terminalgame.hardware
{
    /// <summary>
    /// A monitor for displaying text.
    /// </summary>
    public class Monitor : MonoBehaviour
    {
        /// <summary>
        /// The text output for this monitor.
        /// </summary>
        public TextMeshProUGUI TextOutput;

        /// <summary>
        /// The output size for this monitor.
        /// </summary>
        public Vector2Int OutputSize;
    }
}