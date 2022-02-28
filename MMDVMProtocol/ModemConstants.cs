namespace MMDVMProtocol
{
    public static class ModemConstants
    {
        public const byte FrameStart = 0xE0;

        public const byte GetVersion = 0x00;
        public const byte GetStatus = 0x01;
        public const byte SetConfig = 0x02;
        public const byte SetMode = 0x03;
        public const byte SetFreq = 0x04;

        public const byte SendCwid = 0x0A;

        public const byte DstarHeader = 0x10;
        public const byte DstarData = 0x11;
        public const byte DstarLost = 0x12;
        public const byte DstarEot = 0x13;

        public const byte DmrData1 = 0x18;
        public const byte DmrLost1 = 0x19;
        public const byte DmrData2 = 0x1A;
        public const byte DmrLost2 = 0x1B;
        public const byte DmrShortlc = 0x1C;
        public const byte DmrStart = 0x1D;
        public const byte DmrAbort = 0x1E;

        public const byte YsfData = 0x20;
        public const byte YsfLost = 0x21;

        public const byte P25Hdr = 0x30;
        public const byte P25Ldu = 0x31;
        public const byte P25Lost = 0x32;

        public const byte NxdnData = 0x40;
        public const byte NxdnLost = 0x41;

        public const byte M17LinkSetup = 0x45;
        public const byte M17Stream = 0x46;
        public const byte M17Packet = 0x47;
        public const byte M17Lost = 0x48;
        public const byte M17Eot = 0x49;

        public const byte PocsagData = 0x50;

        public const byte Ax25Data = 0x55;

        public const byte FmParams1 = 0x60;
        public const byte FmParams2 = 0x61;
        public const byte FmParams3 = 0x62;
        public const byte FmParams4 = 0x63;
        public const byte FmData = 0x65;
        public const byte FmControl = 0x66;
        public const byte FmEot = 0x67;

        public const byte Ack = 0x70;
        public const byte Nak = 0x7F;

        public const byte SerialData = 0x80;

        public const byte Transparent = 0x90;
        public const byte QsoInfo = 0x91;

        public const byte Debug1 = 0xF1;
        public const byte Debug2 = 0xF2;
        public const byte Debug3 = 0xF3;
        public const byte Debug4 = 0xF4;
        public const byte Debug5 = 0xF5;
        public const byte DebugDump = 0xFA;

        public const uint MaxResponses = 30;

        public const uint BufferLength = 2000;

        public const byte Cap1Dstar = 0x01;
        public const byte Cap1Dmr = 0x02;
        public const byte Cap1Ysf = 0x04;
        public const byte Cap1P25 = 0x08;
        public const byte Cap1Nxdn = 0x10;
        public const byte Cap1M17 = 0x20;
        public const byte Cap1Fm = 0x40;
        public const byte Cap2Pocsag = 0x01;
        public const byte Cap2Ax25 = 0x02;
    }
}