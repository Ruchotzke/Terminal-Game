namespace terminalgame.computing.hardware
{
    /// <summary>
    /// A basic CPU.
    /// </summary>
    public class CPU : HwComponent
    {
        
        
        public override float MaxPowerDraw()
        {
            return 65;
        }

        public override float PollJoulesUsed()
        {
            return 0;
        }

        public override string Category()
        {
            return "Computing:CPU";
        }

        public override string Name()
        {
            return "Intel 8086";
        }

        public override void Tick(float delta)
        {
            return;
        }
    }
}