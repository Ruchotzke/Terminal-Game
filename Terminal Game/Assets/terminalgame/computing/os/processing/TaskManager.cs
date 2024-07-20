using System.Collections.Generic;
using System.Linq;
using terminalgame.computing.hardware;
using UnityEngine;

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
        public Dictionary<int, TaskQueue> Tasks;

        /// <summary>
        /// The counter used to increment the PID.
        /// </summary>
        private uint _pidctr = 1;
        
        public TaskManager()
        {
            Tasks = new Dictionary<int, TaskQueue>();
        }

        /// <summary>
        /// Enqueue a new task for this OS to work through.
        /// </summary>
        /// <param name="process">The process to enqueue.</param>
        /// <param name="priority">The completion priority of this process.,</param>
        public void EnqueueTask(Process process, int priority = 0)
        {
            if (!Tasks.Keys.Contains(priority))
            {
                Tasks[priority] = new TaskQueue();
            }

            /* Add the task to the appropriate bin */
            Tasks[priority].EnqueueProcess(process);
            process.PID = _pidctr++;
            
            /* If needed, tell any dependencies to remove themselves when done */
            foreach (var dependency in process.Dependencies)
            {
                dependency.OnConclude.Add(delegate { process.Dependencies.Remove(dependency);});    // Dependency done
                dependency.OnConclude.Add(delegate{ Tasks[priority].TryReadyProcess(process);});    // Attempt to ready
            }
            
            /* This task should clean itself up when done */
            process.OnConclude.Add(delegate { Tasks[priority].RemoveCompletedProcess(process); });
        }

        public void OnTick(float dt, HwManager resources)
        {
            /* Work through from high priority to low, working on tasks */
            int MAX_TASKS = 3;
            int tasksLeft = 3;
            int MAX_PRIORITY = 5;
            bool areStalledTasks = false;

            for (int priority = 0; priority < MAX_PRIORITY && tasksLeft > 0; priority++)
            {
                /* check if this priority level exists */
                if (!Tasks.Keys.Contains(priority)) continue;
                
                /* attempt to get tasks to work on */
                var taskResult = Tasks[priority].GetProcesses(tasksLeft);
                areStalledTasks |= taskResult.hasStalled;
                
                /* work through the gathered tasks */
                foreach (var proc in taskResult.processes)
                {
                    bool completed = ProcessTask(proc, resources, dt);
                    tasksLeft--;
                }
            }
            
            /* If MAX_TASKS == tasksLeft and there are stalled tasks, something is very wrong (those tasks can never complete) */
            if (MAX_TASKS == tasksLeft && areStalledTasks)
            {
                Debug.LogError("Stalled tasks while there are no remaining ready tasks. System is stalled.");
            }
        }

        /// <summary>
        /// Work on a given task.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="resources"></param>
        /// <param name="dt"></param>
        private bool ProcessTask(Process p, HwManager resources, float dt)
        {
            /* If this task is just started, initialize it */
            if (p.CurrentWork == 0.0f)
            {
                p.OnStart();
            }
                    
            /* Compute how much work should be applied */
            CPU hw = resources.CPUCatalog[0];
            float work = 1f;
            work *= p.Characterization.GetCharacterizationModifier(hw.Capabilities());
            bool done = p.ApplyWork(work * dt);
                    
            /* If the task was completed, clean it up */
            if (done)
            {
                foreach (var followup in p.OnConclude)
                {
                    followup();
                }

                return true;
            }

            return false;
        }
    }
}