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
        /// How much work is needed to complete this task.
        /// </summary>
        public float TotalWork;

        /// <summary>
        /// How much work has been done so far.
        /// </summary>
        public float CurrentWork;

        public Process(float totalWork)
        {
            TotalWork = totalWork;
            CurrentWork = 0;
        }

        /// <summary>
        /// Work on this task, advancing it by workUnits units.
        /// </summary>
        /// <param name="workUnits">The amount of work to do.</param>
        /// <returns>True if completed, otherwise false.</returns>
        public bool ApplyWork(float workUnits)
        {
            CurrentWork += workUnits;
            
            OnUpdate(CurrentWork, TotalWork, workUnits);

            if (TotalWork <= CurrentWork)
            {
                OnConclude();
                return true;
            }

            return false;
        }


        /// <summary>
        /// A delegate used to update the processes.
        /// </summary>
        public delegate void OnUpdateProcess(float current, float needed, float dwu);

        /// <summary>
        /// The update callback.
        /// </summary>
        public OnUpdateProcess OnUpdate = (current, needed, dwu) => { };

        /// <summary>
        /// A delegate used to start the process.
        /// </summary>
        public delegate void OnStartProcess();

        /// <summary>
        /// A callback for when the process has been started.
        /// </summary>
        public OnStartProcess OnStart = () => { };
        
        /// <summary>
        /// A delegate used to conclude the process.
        /// </summary>
        public delegate void OnConcludeProcess();

        /// <summary>
        /// A callback for when the process has been completed.
        /// </summary>
        public OnConcludeProcess OnConclude = () => { };
    }
}