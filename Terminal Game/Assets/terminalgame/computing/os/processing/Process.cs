namespace terminalgame.computing.os.processing
{
    /// <summary>
    /// A single process running on an OS.
    /// </summary>
    public class Process
    {
        /// <summary>
        /// Process ID (assigned by OS).
        /// </summary>
        public uint PID;

        /// <summary>
        /// A delegate representing the task to be worked on.
        /// </summary>
        public delegate bool Work(float wu);

        /// <summary>
        /// The callback used to work on this process.
        /// </summary>
        public Work WorkCallback;
    }
}