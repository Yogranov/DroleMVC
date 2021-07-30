using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Threading;
using System.Diagnostics;
using System.IO.Ports;

namespace Drole.Controllers
{
    public class LedController : Controller
    {

        [DllImport("user32.dll")]
        static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        static extern bool CloseWindow(IntPtr hWnd);

        [DllImport("user32")]
        private static extern bool SetForegroundWindow(IntPtr hwnd);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;

        private static void LockDoor(bool lockIt)
        {
            using (SerialPort serialPort1 = new SerialPort())
            {
                serialPort1.PortName = "COM7"; //set the port name you see in arduino IDE
                serialPort1.BaudRate = 9600;   //set the Baud you see in arduino IDE

                serialPort1.Open();

                Thread.Sleep(5);
                if (serialPort1.IsOpen)
                {
                    //Console.WriteLine(lockIt ? "l" : "u");
                    serialPort1.WriteLine(lockIt ? "u" : "l");
                    serialPort1.Close();
                }
            }
        }

        // GET: Led
        public ActionResult Index() {
            LockDoor(false);
            return View();
        }
    }
}