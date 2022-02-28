using System;
using System.Collections.Generic;
using System.Text;

namespace MMDVMProtocol
{
    public static class FrameUtils
    {
        public static Frame CreateAckFrame(byte command) => new()
        {
            Length = 4,
            Command = ModemConstants.Ack,
            Data = new List<byte>
            {
                command
            }
        };
        
        public static Frame CreateNackFrame(byte command, byte reason) => new()
        {
            Length = 5,
            Command = ModemConstants.Nak,
            Data = new List<byte>
            {
                command,
                reason
            }
        };
        
        public static Frame CreateGetVersionRequestFrame() => new()
        {
            Length = 3,
            Command = ModemConstants.GetVersion
        };
        
        public static Frame CreateGetVersionResponseFrame(string description)
        {
            if (description.Length + 4 > 255) throw new ArgumentException("Description is too long", nameof(description));

            List<byte> descriptionBytes = new()
            {
                0x01
            };
            
            descriptionBytes.AddRange(Encoding.ASCII.GetBytes(description));
            
            return new()
            {
                Length = Convert.ToUInt16(description.Length + 4),
                Command = ModemConstants.GetVersion,
                Data = descriptionBytes
            };
        }
        
        public static Frame CreateEmptyStatusFrame() => new()
        {
            Length = 10,
            Command = ModemConstants.GetStatus,
            Data = new List<byte>
            {
                0x00, //Enabled Modes
                0x00, //State
                0x00, //Flags
                0x00, //D-Star Buffer,
                0x00, //DMR TS1 Buffer,
                0x00, //DMR TS2 Buffer,
                0x00  //Fusion Buffer
            }
        };
    }
}