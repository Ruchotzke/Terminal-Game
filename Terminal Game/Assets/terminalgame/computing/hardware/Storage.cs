using System.Collections.Generic;
using terminalgame.computing.os;

namespace terminalgame.computing.hardware
{
    /// <summary>
    /// A general storage component.
    /// </summary>
    public abstract class Storage: IHwComponent
    {

        /// <summary>
        /// The OS contained within this storage medium.
        /// </summary>
        public OS BoundOS;
        
        public abstract float MaxPowerDraw();

        public abstract float PollJoulesUsed();

        public virtual string Category()
        {
            return "Storage";
        }

        public abstract string Name();

        public virtual List<string> Capabilities()
        {
            List<string> ret = new List<string>();
            
            ret.Add("persistent storage");

            return ret;
        }

        public void Tick(float delta)
        {
            return;
        }

        /// <summary>
        /// Get the amount of storage possible with this storage medium in Kilobytes.
        /// </summary>
        /// <returns>A uint characterization representing kilobytes possible.</returns>
        public abstract uint GetTotalStorageKBytes();
        
        /// <summary>
        /// Get the amount of storage used with this storage medium in Kilobytes.
        /// </summary>
        /// <returns>A uint characterization representing kilobytes used.</returns>
        public abstract uint GetUsedStorageKBytes();

        /// <summary>
        /// If possible, return the OS present on this storage medium.
        /// </summary>
        /// <returns>The OS if found, otherwise null.</returns>
        public OS GetOS()
        {
            return BoundOS;
        }

        /// <summary>
        /// Bind this storage medium to an existing operating system.
        /// </summary>
        /// <returns>true if successful, otherwise false.</returns>
        public bool BindOS(OS os)
        {
            /* Make sure there is room on this disk for the OS. */
            if (GetTotalStorageKBytes() - GetUsedStorageKBytes() < os.InstallSize)
            {
                return false;
            }
            
            /* Install the OS, removing a size from the disk */
            BoundOS = os;
            return true;
        }
    }
}