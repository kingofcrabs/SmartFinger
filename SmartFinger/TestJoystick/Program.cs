using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace TestGameHandle
{
    class Program
    {
        static Joystick curJoystick;

        static void Main(string[] args)
        {
            // Initialize DirectInput
            var dirInput = new SharpDX.DirectInput.DirectInput();
            var typeJoystick = SharpDX.DirectInput.DeviceType.Gamepad;
            var allDevices = dirInput.GetDevices();
            bool isGetJoystick = false;
            
            foreach (var item in allDevices)
            {
                if (typeJoystick == item.Type)
                {
                    curJoystick = new SharpDX.DirectInput.Joystick(dirInput, item.InstanceGuid);
                    curJoystick.Acquire();
                    isGetJoystick = true;
                    Thread t1 = new Thread(joyListening);
                    t1.IsBackground = true;
                    t1.Start();
                }
            }
            if (!isGetJoystick)
            {
                Console.WriteLine("没有插入手柄");
                Console.WriteLine("Press any key to exit!");
            }
            Console.ReadKey();
        }

        private static void joyListening(object obj)
        {
            while (true)
            {
                var joys = curJoystick.GetCurrentState();
                
                var btns = joys.Buttons.ToList();
                var controllers = joys.PointOfViewControllers;
                if (controllers[0] == 27000)
                {
                    Console.WriteLine("Left");
                }
                else if (controllers[0] == 0)
                {
                    Console.WriteLine("Up");
                }
                else if (controllers[0] == 18000)
                {
                    Console.WriteLine("Down");
                }
                else if (controllers[0] == 9000)
                {
                    Console.WriteLine("Right");
                }
                Thread.Sleep(100);
                //if(btns.Contains(true))
                //    Console.WriteLine("true");
                //else
                //    Console.WriteLine("false");

            }  
        }
    }

    

}
