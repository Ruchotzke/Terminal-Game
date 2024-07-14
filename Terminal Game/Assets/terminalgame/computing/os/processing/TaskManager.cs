using System.Collections.Generic;
using System.Linq;

namespace terminalgame.computing.os.processing
{
    /// <summary>
    /// The manager for scheduling/task updates for an OS.
    /// </summary>
    public class TaskManager
    {
        /// <summary>
        /// The tasks being evaluated, sorted by priority.
        /// </summary>
        public Dictionary<int, List<Process>> Tasks;

        public TaskManager()
        {
            Tasks = new Dictionary<int, List<Process>>();
        }

        /// <summary>
        /// Enqueue a new task for this OS to work through.
        /// </summary>
        /// <param name="process"></param>
        public void EnqueueTask(Process process, int priority = 1)
        {
            if (!Tasks.Keys.Contains(priority))
            {
                Tasks[priority] = new List<Process>();
            }

            Tasks[priority].Add(process);
        }
    }
}