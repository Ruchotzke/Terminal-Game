namespace Terminal
{
    /// <summary>
    /// An abstraction of the OS running on this device.
    /// </summary>
    public class OS
    {
        public string Prompt = "$> ";
        public float WorkUnitsPerSecond = 100;
        
        /// <summary>
        /// Construct a new OS.
        /// </summary>
        public OS()
        {
            
        }

        /// <summary>
        /// Process a user command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string TakeCommand(string command)
        {
            string ret = command;
            
            /* Only return content if needed */
            if(ret != "") return command + "\n" + Prompt;
            return Prompt;
        }

        /// <summary>
        /// Get the delay caused by HW.
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        public float GetTimeForWorkUnits(float units)
        {
            return units / WorkUnitsPerSecond;
        }
    }
}