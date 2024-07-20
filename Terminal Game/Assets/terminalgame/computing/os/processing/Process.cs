using System.Collections.Generic;

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
        /// A human-readable name for this process.
        /// </summary>
        public string Name;

        /// <summary>
        /// How much work is needed to complete this task.
        /// </summary>
        public float TotalWork;

        /// <summary>
        /// How much work has been done so far.
        /// </summary>
        public float CurrentWork;

        /// <summary>
        /// A list of dependencies associated with this process.
        /// </summary>
        public List<Process> Dependencies;

        /// <summary>
        /// The characterization of this process.
        /// </summary>
        public WorkloadCharacterization Characterization;

        public Process(float totalWork, WorkloadCharacterization workloadCharacterization, string name)
        {
            TotalWork = totalWork;
            Characterization = workloadCharacterization;
            Dependencies = new List<Process>();
            CurrentWork = 0;
            Name = name;
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
                foreach (var f in OnConclude)
                {
                    f();
                }
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
        public List<OnConcludeProcess> OnConclude = new List<OnConcludeProcess>();
    }
}