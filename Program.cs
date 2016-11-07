using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AionBotAlpha
{
    class Program
    {
        /*
        class Player
        {
            public float xCoord
            {
                get
                {
                    float _xCoord = 
                }
            }                    
        }
        */

        static public MemoryEngine processHook = new MemoryEngine();

        static void Main(string[] args)
        {
            List<Process> aionProcs = getAionProcs();
            for (int i = 0; i < aionProcs.Count; i++)
            {
                processHook.Hook(aionProcs[i].Id);
                string _name = processHook.ReadStringUnicode(processHook.GetModuleAddress("Game.dll") + 0x122141C, 32);
                Console.WriteLine("(" + i + ") " + _name + " PID:" + aionProcs[i].Id);

            }
            int _select = Convert.ToInt32(Console.ReadKey().KeyChar) - 48; //Converting char to int ( 0 id is 48, 1 is 49, etc)
            if (_select <= aionProcs.Count)
            {
                processHook.Hook(aionProcs[_select].Id);
            }
            else
            {
                Console.WriteLine("Input a number in range please!");
            }
            Console.ReadKey();
        }

        static private List<Process> getAionProcs()
        {
            List<Process> _aionList = new List<Process>();
            _aionList.AddRange(Process.GetProcessesByName("aion"));
            _aionList.AddRange(Process.GetProcessesByName("aion.bin"));
            return _aionList;
        }
    }
}
