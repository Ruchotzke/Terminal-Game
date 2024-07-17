using System.Collections.Generic;

namespace terminalgame.computing.os.processing
{
    /// <summary>
    /// The characterization of a processes needs.
    /// </summary>
    public class WorkloadCharacterization
    {
        /// <summary>
        /// How much IO factors into this processes operation.
        /// </summary>
        public float IO;

        /// <summary>
        /// The memory requirements of this process.
        /// </summary>
        public float Memory;

        /// <summary>
        /// The maximum number of threads allocable to ths process.
        /// </summary>
        public int MaxThreads;

        /// <summary>
        /// The exponential penalty applied to this process as more threads are added.
        /// </summary>
        public float ThreadPenalty = 0.96f;

        /// <summary>
        /// Characterization of any other needs this process may require.
        /// </summary>
        public Dictionary<string, float> AdditionalNeeds;

        /// <summary>
        /// Generate a new characterization.
        /// </summary>
        public WorkloadCharacterization(float io, float mem)
        {
            IO = io;
            Memory = mem;
            AdditionalNeeds = new Dictionary<string, float>();
        }

        /// <summary>
        /// Using the suppled hardware characteristics, determine the modifier on work speed.
        /// </summary>
        /// <param name="hwCapable">The capabilities of the hardware.</param>
        /// <returns></returns>
        public float GetCharacterizationModifier(Dictionary<string, float> hwCapable)
        {
            float modifier = 1.0f;

            foreach (var c in AdditionalNeeds.Keys)
            {
                if (hwCapable.TryGetValue(c, out var capabilty))
                {
                    modifier *= capabilty / AdditionalNeeds[c];
                }
                else
                {
                    return 0.0f;    //impossible on this HW
                }
            }
            
            return modifier;
        }

        /// <summary>
        /// Generate a characterization used for text rendering.
        /// </summary>
        /// <returns></returns>
        public static WorkloadCharacterization TextRendering()
        {
            WorkloadCharacterization w = new WorkloadCharacterization(0.2f, 0.01f);
            w.AdditionalNeeds.Add("computing", 1f);
            w.AdditionalNeeds.Add("graphics", 2f);

            return w;
        }
    }
}