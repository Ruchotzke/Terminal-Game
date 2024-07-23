using System;
using UnityEngine;

namespace terminalgame.managers
{
    /// <summary>
    /// The manager for timesteps.
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
        /// <summary>
        /// The singleton reference to the time manager.
        /// </summary>
        public static TimeManager Instance;

        private void Awake()
        {
            /* Handle singleton */
            if (Instance != null) Destroy(gameObject);
            Instance = this;
        }
    }
}