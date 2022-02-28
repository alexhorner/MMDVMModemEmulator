using System;
using System.Collections.Generic;

namespace MMDVMProtocol
{
    public class Frame
    {
        public ushort Length = 0;
        public byte Command = 0x00;
        public ushort DataLength => Convert.ToUInt16(Data.Count);
        public List<byte> Data = new();

        public byte[] ToPacket()
        {
            List<byte> finalPacket = new()
            {
                ModemConstants.FrameStart,
                Convert.ToByte(Length),
                Command
            };
            
            finalPacket.AddRange(Data);

            return finalPacket.ToArray();
        }
    }
}