using System.Collections.Generic;
using System.Linq;
using terminalgame.computing.hardware;

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

        /// <summary>
        /// The counter used to increment the PID.
        /// </summary>
        private uint _pidctr = 1;
        
        public TaskManager()
        {
            Tasks = new Dictionary<int, List<Process>>();
        }

        /// <summary>
        /// Enqueue a new task for this OS to work through.
        /// </summary>
        /// <param name="process"></param>
        public void EnqueueTask(Process process, int priority = 0)
        {
            if (!Tasks.Keys.Contains(priority))
            {
                Tasks[priority] = new List<Process>();
            }

            Tasks[priority].Add(process);
            process.PID = _pidctr++;
        }

        public void OnTick(float dt, HwManager resources)
        {
            if (Tasks.Keys.Count > 0)
            {
                if (Tasks[0].Count > 0)
                {
                    /* Get a reference to the next task to tackle */
                    Process curr = Tasks[0][0];
                    
                    /* If this task is just started, initialize it */
                    if (curr.CurrentWork == 0.0f)
                    {
                        curr.OnStart();
                    }
                    
                    /* Compute how much work should be applied */
                    CPU hw = resources.CPUCatalog[0];
                    float work = 1f;
                    work *= curr.Characterization.GetCharacterizationModifier(hw.Capabilities());
                    bool done = curr.ApplyWork(work * dt);
                    
                    /* If the task was completed, clean it up */
                    if (done)
                    {
                        Tasks[0].RemoveAt(0);
                    }
                }
            }
        }
    }
}