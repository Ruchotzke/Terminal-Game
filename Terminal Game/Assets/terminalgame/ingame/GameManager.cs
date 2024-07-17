using terminalgame.computing;
using terminalgame.computing.hardware;
using terminalgame.computing.hardware.storage;
using terminalgame.computing.os;
using UnityEngine;

namespace terminalgame.ingame
{
    public class GameManager : MonoBehaviour
    {
        private Computer c;
    
        private void Start()
        {
            /* Generate a new computer and OS */
            c = new Computer();
            c.HwManager = new HwManager();
            HDD storage = new HDD();
            CPU cpu = new CPU();
            Memory ram = new Memory(4000);
            storage.BindOS(new OS());
            c.HwManager.HwComponents.Add(storage);
            c.HwManager.HwComponents.Add(cpu);
        
            /* Find and bind all monitors */
            foreach (var m in FindObjectsOfType<MonitorManager>())
            {
                c.HwManager.HwComponents.Add(m.Monitor);
            }
        
            /* Boot the computer */
            c.Boot();
        }

        private void Update()
        {
            c.Tick(Time.deltaTime);
        }
    }
}
