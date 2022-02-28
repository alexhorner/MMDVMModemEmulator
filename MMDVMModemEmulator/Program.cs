using System;
using System.IO.Ports;
using System.Threading.Tasks;
using MMDVMProtocol;

namespace MMDVMModemEmulator
{
    // https://cloud.dstar.su/files/G4KLX/MMDVM/MMDVM%20Specification%2020150922.pdf
    class Program
    {
        static void Main(string[] args)
        {
            SerialPort port = new SerialPort(args.Length <= 0 ? "COM7" : args[0], 115200);
            port.Open();

            FrameProcessor frameProcessor = new();
            frameProcessor.Start();

            _ = Task.Run(() =>
            {
                while (true)
                {
                    while (port.BytesToRead <= 0)
                    {
                        //wait
                    }

                    byte inb = (byte)port.ReadByte();
                    
                    Console.WriteLine($"B: 0x{BitConverter.ToString(new []{ inb })}");
                    
                    frameProcessor.Insert(inb);
                }
            });

            while (true)
            {
                Frame incomingFrame = frameProcessor.DequeueFrame();
                
                Console.WriteLine($"Received Frame: 0x{BitConverter.ToString(new []{ incomingFrame.Command })}");

                Frame outgoingFrame = null;

                switch (incomingFrame.Command)
                {
                    case ModemConstants.GetVersion:
                        Console.WriteLine(">> Get Version");
                        outgoingFrame = FrameUtils.CreateGetVersionResponseFrame("Hello");
                        break;
                    
                    case ModemConstants.GetStatus:
                        Console.WriteLine(">> Get Status");
                        outgoingFrame = FrameUtils.CreateEmptyStatusFrame();
                        break;
                }

                if (outgoingFrame != null)
                {
                    byte[] packet = outgoingFrame.ToPacket();
                    
                    port.Write(packet, 0, packet.Length);
                }
            }
        }
    }
}