﻿using System.Collections.Generic;

namespace terminalgame.computing.hardware.storage
{
    
    /// <summary>
    /// A simple hard drive.
    /// </summary>
    public class HDD : Storage
    {
        /// <summary>
        /// Construct a new hard drive.
        /// </summary>
        public HDD()
        {
            _capabilities = new Dictionary<string, float>();
            _capabilities.Add("storage", 10.0f);
        }
        
        public override float MaxPowerDraw()
        {
            return -25;
        }

        public override float PollJoulesUsed()
        {
            throw new System.NotImplementedException();
        }

        public override string Name()
        {
            return "IBM 3380 Hard Disk";
        }

        public override uint GetTotalStorageKBytes()
        {
            return 500000;
        }

        public override uint GetUsedStorageKBytes()
        {
            //TODO: Update
            return 0;
        }

        public override string Category()
        {
            return base.Category() + ":HDD";
        }
    }
}