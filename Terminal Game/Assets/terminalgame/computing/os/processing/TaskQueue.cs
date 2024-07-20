using System.Collections.Generic;

namespace terminalgame.computing.os.processing
{
    /// <summary>
    /// A queue for a series of tasks (processes).
    /// </summary>
    public class TaskQueue
    {
        /// <summary>
        /// All processes currently awaiting dependencies.
        /// </summary>
        public List<Process> Stalled;

        /// <summary>
        /// All tasks which are ready to be actively worked on.
        /// </summary>
        public List<Process> Ready;

        /// <summary>
        /// Construct a new task queue.
        /// </summary>
        public TaskQueue()
        {
            Stalled = new List<Process>();
            Ready = new List<Process>();
        }

        /// <summary>
        /// Enqueue a new process into this system.
        /// </summary>
        /// <param name="p"></param>
        public void EnqueueProcess(Process p)
        {
            /* Check dependencies and then place into proper array */
            if (p.Dependencies.Count > 0)
            {
                Stalled.Add(p);
            }
            else
            {
                Ready.Add(p);
            }
        }

        /// <summary>
        /// Callback used to check when a dependent task is completed, whether or not this task is now ready.
        /// </summary>
        /// <param name="p"></param>
        public void TryReadyProcess(Process p)
        {
            if (p.Dependencies.Count == 0)
            {
                Stalled.Remove(p);
                Ready.Add(p);
            }
        }

        
        /// <summary>
        /// Gather at most max processes from the queue.
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public (List<Process> processes, bool hasStalled) GetProcesses(int max)
        {
            List<Process> ret = new List<Process>();

            for (int i = 0; i < max && i < Ready.Count; i++)
            {
                ret.Add(Ready[i]);
            }

            return (ret, Stalled.Count > 0);
        }

        /// <summary>
        /// Remove a completed process from this queue.
        /// </summary>
        /// <param name="p"></param>
        public void RemoveCompletedProcess(Process p)
        {
            if (Ready.Contains(p)) Ready.Remove(p);
        }
    }
}