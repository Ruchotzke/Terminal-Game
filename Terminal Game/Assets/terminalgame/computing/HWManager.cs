using System.Collections.Generic;
using terminalgame.computing.hardware;
using terminalgame.computing.os;

namespace terminalgame.computing
{
    /// <summary>
    /// Manager for all hardware resources on a computer.
    /// </summary>
    public class HwManager
    {

        /// <summary>
        /// All components managed.
        /// </summary>
        public List<HwComponent> HwComponents;

        /// <summary>
        /// All storage hardware.
        /// </summary>
        public List<Storage> StorageCatalog;

        public List<Monitor> MonitorCatalog;

        /// <summary>
        /// All storage media containing bootable systems.
        /// </summary>
        private List<Storage> BootList;

        /// <summary>
        /// Construct a new manager.
        /// </summary>
        public HwManager()
        {
            HwComponents = new List<HwComponent>();
            
            /* Catalogs */
            StorageCatalog = new List<Storage>();
            BootList = new List<Storage>();
            MonitorCatalog = new List<Monitor>();
        }
        
        /// <summary>
        /// Initialize the hardware components of this device.
        /// Find a reference to an OS and return it to the computer.
        /// </summary>
        /// <returns>A handle to an OS present to boot into.</returns>
        public OS Initialize()
        {
            /* Catalogue all HW components, searching for an OS */
            foreach (var hw in HwComponents)
            {
                string category = hw.Category();
                
                if (category.StartsWith("Storage"))
                {
                    /* This is a storage medium */
                    Storage storage = (Storage)hw;
                    StorageCatalog.Add(storage);
                    
                    /* Check for an OS and add to the boot list */
                    if (storage.GetOS() != null) BootList.Add(storage);
                }
                else if (category.StartsWith("Interface"))
                {
                    /* This is a monitor */
                    MonitorCatalog.Add((Monitor)hw);
                }
            }
            
            /* Perform a power check to make sure that there is at least enough power for basic components */
            //TODO
            
            /* Return the OS first in the boot order, or null */
            if (BootList.Count != 0) return BootList[0].GetOS();
            return null;
        }
    }
}