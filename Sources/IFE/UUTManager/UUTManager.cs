using DeviceManager.BRICK32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OTools;

namespace IFE
{
    /// <summary>
    /// Big endian protocol
    /// </summary>
    public class UUTManager
    {
        const ushort SYNC = 0xEB6B;

        BRICK32 _commManager=new BRICK32();

        ConversionsBase _conversions = new  ConversionsBigEndian();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private ushort CalculateChecksum(byte[] data)
        {
            int retVal=0;

            foreach(byte dataByte in data)
            {
                retVal = retVal + dataByte;
            }
            return (ushort)retVal;
        }


        Dictionary<byte, ushort> _config1CommAddressToAddressOffset;
        Dictionary<byte, ushort> _config2CommAddressToAddressOffset;
        Dictionary<byte, ushort> _config3CommAddressToAddressOffset;
        Dictionary<byte, ushort> _uartStatusCommStatusToAddressOffset;

        private void Init()
        {
            _config1CommAddressToAddressOffset = new Dictionary<byte, ushort>();
            _config2CommAddressToAddressOffset = new Dictionary<byte, ushort>();
            _config3CommAddressToAddressOffset = new Dictionary<byte, ushort>();
            _uartStatusCommStatusToAddressOffset = new Dictionary<byte, ushort>();

            _config1CommAddressToAddressOffset.Add(40, 0x9);
            _config1CommAddressToAddressOffset.Add(39, 0xC);
            _config1CommAddressToAddressOffset.Add(38, 0xF);
            _config1CommAddressToAddressOffset.Add(37, 0x12);
            _config1CommAddressToAddressOffset.Add(36, 0x15);
            _config1CommAddressToAddressOffset.Add(35, 0x18);
            _config1CommAddressToAddressOffset.Add(34, 0x1B);
            _config1CommAddressToAddressOffset.Add(33, 0x1E);
            _config1CommAddressToAddressOffset.Add(32, 0x21);
            _config1CommAddressToAddressOffset.Add(31, 0x24);
            _config1CommAddressToAddressOffset.Add(30, 0x27);
            _config1CommAddressToAddressOffset.Add(29, 0x2A);
            _config1CommAddressToAddressOffset.Add(28, 0x2D);
            _config1CommAddressToAddressOffset.Add(27, 0x30);
            _config1CommAddressToAddressOffset.Add(26, 0x33);
            _config1CommAddressToAddressOffset.Add(25, 0x36);
            _config1CommAddressToAddressOffset.Add(24, 0x39);
            _config1CommAddressToAddressOffset.Add(23, 0x3C);
            _config1CommAddressToAddressOffset.Add(22, 0x3F);
            _config1CommAddressToAddressOffset.Add(21, 0x42);
            _config1CommAddressToAddressOffset.Add(20, 0x45);
            _config1CommAddressToAddressOffset.Add(19, 0x48);
            _config1CommAddressToAddressOffset.Add(18, 0x4B);
            _config1CommAddressToAddressOffset.Add(17, 0x4E);
            _config1CommAddressToAddressOffset.Add(16, 0x51);
            _config1CommAddressToAddressOffset.Add(15, 0x54);
            _config1CommAddressToAddressOffset.Add(14, 0x57);
            _config1CommAddressToAddressOffset.Add(13, 0x5A);
            _config1CommAddressToAddressOffset.Add(12, 0x5D);
            _config1CommAddressToAddressOffset.Add(11, 0x60);
            _config1CommAddressToAddressOffset.Add(10, 0x63);
            _config1CommAddressToAddressOffset.Add(9, 0x66);
            _config1CommAddressToAddressOffset.Add(8, 0x69);
            _config1CommAddressToAddressOffset.Add(7, 0x6C);
            _config1CommAddressToAddressOffset.Add(6, 0x6F);
            _config1CommAddressToAddressOffset.Add(5, 0x72);
            _config1CommAddressToAddressOffset.Add(4, 0x75);
            _config1CommAddressToAddressOffset.Add(3, 0x78);
            _config1CommAddressToAddressOffset.Add(2, 0x7B);
            _config1CommAddressToAddressOffset.Add(1, 0x7E);

            _config2CommAddressToAddressOffset.Add(40, 0xA);
            _config2CommAddressToAddressOffset.Add(39, 0xD);
            _config2CommAddressToAddressOffset.Add(38, 0x10);
            _config2CommAddressToAddressOffset.Add(37, 0x13);
            _config2CommAddressToAddressOffset.Add(36, 0x16);
            _config2CommAddressToAddressOffset.Add(35, 0x19);
            _config2CommAddressToAddressOffset.Add(34, 0x1C);
            _config2CommAddressToAddressOffset.Add(33, 0x1F);
            _config2CommAddressToAddressOffset.Add(32, 0x22);
            _config2CommAddressToAddressOffset.Add(31, 0x25);
            _config2CommAddressToAddressOffset.Add(30, 0x28);
            _config2CommAddressToAddressOffset.Add(29, 0x2B);
            _config2CommAddressToAddressOffset.Add(28, 0x2E);
            _config2CommAddressToAddressOffset.Add(27, 0x31);
            _config2CommAddressToAddressOffset.Add(26, 0x34);
            _config2CommAddressToAddressOffset.Add(25, 0x37);
            _config2CommAddressToAddressOffset.Add(24, 0x3A);
            _config2CommAddressToAddressOffset.Add(23, 0x3D);
            _config2CommAddressToAddressOffset.Add(22, 0x40);
            _config2CommAddressToAddressOffset.Add(21, 0x43);
            _config2CommAddressToAddressOffset.Add(20, 0x46);
            _config2CommAddressToAddressOffset.Add(19, 0x49);
            _config2CommAddressToAddressOffset.Add(18, 0x4C);
            _config2CommAddressToAddressOffset.Add(17, 0x4F);
            _config2CommAddressToAddressOffset.Add(16, 0x52);
            _config2CommAddressToAddressOffset.Add(15, 0x55);
            _config2CommAddressToAddressOffset.Add(14, 0x58);
            _config2CommAddressToAddressOffset.Add(13, 0x5B);
            _config2CommAddressToAddressOffset.Add(12, 0x5E);
            _config2CommAddressToAddressOffset.Add(11, 0x61);
            _config2CommAddressToAddressOffset.Add(10, 0x64);
            _config2CommAddressToAddressOffset.Add(9, 0x67);
            _config2CommAddressToAddressOffset.Add(8, 0x6A);
            _config2CommAddressToAddressOffset.Add(7, 0x6D);
            _config2CommAddressToAddressOffset.Add(6, 0x70);
            _config2CommAddressToAddressOffset.Add(5, 0x73);
            _config2CommAddressToAddressOffset.Add(4, 0x76);
            _config2CommAddressToAddressOffset.Add(3, 0x79);
            _config2CommAddressToAddressOffset.Add(2, 0x7C);
            _config2CommAddressToAddressOffset.Add(1, 0x7F);

            _config3CommAddressToAddressOffset.Add(40, 0xB);
            _config3CommAddressToAddressOffset.Add(39, 0xE);
            _config3CommAddressToAddressOffset.Add(38, 0x11);
            _config3CommAddressToAddressOffset.Add(37, 0x14);
            _config3CommAddressToAddressOffset.Add(36, 0x17);
            _config3CommAddressToAddressOffset.Add(35, 0x1A);
            _config3CommAddressToAddressOffset.Add(34, 0x1D);
            _config3CommAddressToAddressOffset.Add(33, 0x20);
            _config3CommAddressToAddressOffset.Add(32, 0x23);
            _config3CommAddressToAddressOffset.Add(31, 0x26);
            _config3CommAddressToAddressOffset.Add(30, 0x29);
            _config3CommAddressToAddressOffset.Add(29, 0x2C);
            _config3CommAddressToAddressOffset.Add(28, 0x2F);
            _config3CommAddressToAddressOffset.Add(27, 0x32);
            _config3CommAddressToAddressOffset.Add(26, 0x35);
            _config3CommAddressToAddressOffset.Add(25, 0x38);
            _config3CommAddressToAddressOffset.Add(24, 0x3B);
            _config3CommAddressToAddressOffset.Add(23, 0x3E);
            _config3CommAddressToAddressOffset.Add(22, 0x41);
            _config3CommAddressToAddressOffset.Add(21, 0x44);
            _config3CommAddressToAddressOffset.Add(20, 0x47);
            _config3CommAddressToAddressOffset.Add(19, 0x4A);
            _config3CommAddressToAddressOffset.Add(18, 0x4D);
            _config3CommAddressToAddressOffset.Add(17, 0x50);
            _config3CommAddressToAddressOffset.Add(16, 0x53);
            _config3CommAddressToAddressOffset.Add(15, 0x56);
            _config3CommAddressToAddressOffset.Add(14, 0x59);
            _config3CommAddressToAddressOffset.Add(13, 0x5C);
            _config3CommAddressToAddressOffset.Add(12, 0x5F);
            _config3CommAddressToAddressOffset.Add(11, 0x62);
            _config3CommAddressToAddressOffset.Add(10, 0x65);
            _config3CommAddressToAddressOffset.Add(9, 0x68);
            _config3CommAddressToAddressOffset.Add(8, 0x6B);
            _config3CommAddressToAddressOffset.Add(7, 0x6E);
            _config3CommAddressToAddressOffset.Add(6, 0x71);
            _config3CommAddressToAddressOffset.Add(5, 0x74);
            _config3CommAddressToAddressOffset.Add(4, 0x77);
            _config3CommAddressToAddressOffset.Add(3, 0x7A);
            _config3CommAddressToAddressOffset.Add(2, 0x7D);
            _config3CommAddressToAddressOffset.Add(1, 0x80);

            _uartStatusCommStatusToAddressOffset.Add(40, 0x8E);
            _uartStatusCommStatusToAddressOffset.Add(39, 0x8F);
            _uartStatusCommStatusToAddressOffset.Add(38, 0x90);
            _uartStatusCommStatusToAddressOffset.Add(37, 0x91);
            _uartStatusCommStatusToAddressOffset.Add(36, 0x92);
            _uartStatusCommStatusToAddressOffset.Add(35, 0x93);
            _uartStatusCommStatusToAddressOffset.Add(34, 0x94);
            _uartStatusCommStatusToAddressOffset.Add(33, 0x95);
            _uartStatusCommStatusToAddressOffset.Add(32, 0x96);
            _uartStatusCommStatusToAddressOffset.Add(31, 0x97);
            _uartStatusCommStatusToAddressOffset.Add(30, 0x98);
            _uartStatusCommStatusToAddressOffset.Add(29, 0x99);
            _uartStatusCommStatusToAddressOffset.Add(28, 0x9A);
            _uartStatusCommStatusToAddressOffset.Add(27, 0x9B);
            _uartStatusCommStatusToAddressOffset.Add(26, 0x9C);
            _uartStatusCommStatusToAddressOffset.Add(25, 0x9D);
            _uartStatusCommStatusToAddressOffset.Add(24, 0x9E);
            _uartStatusCommStatusToAddressOffset.Add(23, 0x9F);
            _uartStatusCommStatusToAddressOffset.Add(22, 0xA0);
            _uartStatusCommStatusToAddressOffset.Add(21, 0xA1);
            _uartStatusCommStatusToAddressOffset.Add(20, 0xA2);
            _uartStatusCommStatusToAddressOffset.Add(19, 0xA3);
            _uartStatusCommStatusToAddressOffset.Add(18, 0xA4);
            _uartStatusCommStatusToAddressOffset.Add(17, 0xA5);
            _uartStatusCommStatusToAddressOffset.Add(16, 0xA6);
            _uartStatusCommStatusToAddressOffset.Add(15, 0xA7);
            _uartStatusCommStatusToAddressOffset.Add(14, 0xA8);
            _uartStatusCommStatusToAddressOffset.Add(13, 0xA9);
            _uartStatusCommStatusToAddressOffset.Add(12, 0xAA);
            _uartStatusCommStatusToAddressOffset.Add(11, 0xAB);
            _uartStatusCommStatusToAddressOffset.Add(10, 0xAC);
            _uartStatusCommStatusToAddressOffset.Add(9, 0xAD);
            _uartStatusCommStatusToAddressOffset.Add(8, 0xAE);
            _uartStatusCommStatusToAddressOffset.Add(7, 0xAF);
            _uartStatusCommStatusToAddressOffset.Add(6, 0xB0);
            _uartStatusCommStatusToAddressOffset.Add(5, 0xB1);
            _uartStatusCommStatusToAddressOffset.Add(4, 0xB2);
            _uartStatusCommStatusToAddressOffset.Add(3, 0xB3);
            _uartStatusCommStatusToAddressOffset.Add(2, 0xB4);
            _uartStatusCommStatusToAddressOffset.Add(1, 0xB5);
        }

        /// <summary>
        /// Gets the device handle by index
        /// </summary>
        /// <param name="deviceHandle"></param>
        /// <param name="index">Position in the array</param>
        public void OpenDevice(int index = 0)
        {
            _commManager.OpenDevice(index);
            Init();
        }

        /// <param name="channelNumber">1-31</param>
        /// <param name="channelId"></param>
        /// <param name="channelDirection"></param>
        public void OpenChannel(int channelNumber, STAR_CHANNEL_DIRECTION channelDirection = STAR_CHANNEL_DIRECTION.STAR_CHANNEL_DIRECTION_INOUT)
        {
            _commManager.OpenChannel(channelNumber, channelDirection);
        }

        /// <summary>
        /// (200Mbits)*multiplier/division
        /// </summary>
        /// <param name="deviceHandle"></param>
        /// <param name="_Port">1-2</param>
        /// <param name="divisor"></param>
        /// <param name="multiplier"></param>
        /// <returns></returns>
        public void SetTransmitClock(byte port, ushort divisor, ushort multiplier)
        {
            _commManager.SetTransmitClock(port, divisor, multiplier);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CloseAllChannels()
        {
            _commManager.CloseAllChannels();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="fpga"></param>
        /// <param name="startAddress"></param>
        /// <param name=""></param>
        public void WriteMessage(int channelNumber,Enums.CardId cardId, byte fpga,ushort startAddress,byte[] data)
        {
            byte[] tmp;
            List<byte> bufferToSend = new List<byte>();

            //Sync
            tmp=_conversions.UShortToBytes(SYNC);
            bufferToSend.AddRange(tmp);

            //Card ID + FPGA
            fpga = (byte)(fpga | (byte)cardId);
            bufferToSend.Add(fpga);

            //Address
            startAddress = (ushort)(startAddress | (ushort)(1 << 15)); //Add write BIT 
            tmp = _conversions.UShortToBytes(startAddress);
            bufferToSend.AddRange(tmp);

            //Data length
            ushort dataLength = (ushort)data.Length;
            tmp = _conversions.UShortToBytes(dataLength);
            bufferToSend.AddRange(tmp);

            //Data
            bufferToSend.AddRange(data);

            //Checksum
            ushort checksum=CalculateChecksum(data);
            tmp = _conversions.UShortToBytes(checksum);
            bufferToSend.AddRange(tmp);

            _commManager.TransmitPacket(channelNumber, bufferToSend.ToArray());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <param name="cardId"></param>
        /// <param name="fpga"></param>
        /// <param name="startAddress"></param>
        /// <param name="numberOfBytesToRead"></param>
        /// <param name="receivedBytes"></param>
        private void ReadMessage(int channelNumber,Enums.CardId cardId, byte fpga, ushort startAddress, ushort numberOfBytesToRead,out byte[] receivedBytes)
        {
            byte[] tmp;
            List<byte> bufferToSend = new List<byte>();

            //Sync
            tmp = _conversions.UShortToBytes(SYNC);
            bufferToSend.AddRange(tmp);

            //Card ID + FPGA
            fpga = (byte)(fpga | (byte)cardId);
            bufferToSend.Add(fpga);

            //Address (read)
            tmp = _conversions.UShortToBytes(startAddress);
            bufferToSend.AddRange(tmp);

            //Data length
            tmp = _conversions.UShortToBytes(numberOfBytesToRead);
            bufferToSend.AddRange(tmp);

            //Send
            _commManager.TransmitPacket(channelNumber, bufferToSend.ToArray());

            //Receive
            receivedBytes = new byte[numberOfBytesToRead];
            _commManager.ReceivePacket(channelNumber, out receivedBytes);

        }

        int _channelNumber;
        Enums.CardId _cardId;
        byte _fpga;

        public void Configure(int channelNumber, Enums.CardId cardId,byte fpga)
        {
            _channelNumber = channelNumber;
            _cardId = cardId;
            _fpga = fpga;
        }

        public void GetFpgaVersion(out byte fpgaVersion)
        {
            ReadMessage(_channelNumber, _cardId, _fpga, 0x0, 1, out byte[] receivedBytes);
            fpgaVersion = receivedBytes[0];
        }

        public void GetFpgaYear(out byte fpgaYear)
        {
            ReadMessage(_channelNumber, _cardId, _fpga, 0x1, 1, out byte[] receivedBytes);
            fpgaYear = receivedBytes[0];
        }

        public void GetFpgaMonth(out byte fpgaMonth)
        {
            ReadMessage(_channelNumber, _cardId, _fpga, 0x2, 1, out byte[] receivedBytes);
            fpgaMonth = receivedBytes[0];
        }

        public void GetFpgaDay(out byte fpgaDay)
        {
            ReadMessage(_channelNumber, _cardId, _fpga, 0x3, 1, out byte[] receivedBytes);
            fpgaDay = receivedBytes[0];
        }

        #region ResetUART
        public void SetResetUART40_33(Enums.ResetOper rstUart33, Enums.ResetOper rstUart34, Enums.ResetOper rstUart35, Enums.ResetOper rstUart36,
                                     Enums.ResetOper rstUart37, Enums.ResetOper rstUart38, Enums.ResetOper rstUart39, Enums.ResetOper rstUart40)
        {

            WriteMessageBits(0x4, rstUart33, rstUart34, rstUart35, rstUart36, rstUart37, rstUart38, rstUart39, rstUart40);
        }


        public void SetResetUART32_25(Enums.ResetOper rstUart25, Enums.ResetOper rstUart26, Enums.ResetOper rstUart27, Enums.ResetOper rstUart28,
                             Enums.ResetOper rstUart29, Enums.ResetOper rstUart30, Enums.ResetOper rstUart31, Enums.ResetOper rstUart32)
        {

            WriteMessageBits(0x5, rstUart25, rstUart26, rstUart27, rstUart28, rstUart29, rstUart30, rstUart31, rstUart32);
        }

        public void SetResetUART24_17(Enums.ResetOper rstUart17, Enums.ResetOper rstUart18, Enums.ResetOper rstUart19, Enums.ResetOper rstUart20,
                                   Enums.ResetOper rstUart21, Enums.ResetOper rstUart22, Enums.ResetOper rstUart23, Enums.ResetOper rstUart24)
        {
            WriteMessageBits(0x6,rstUart17, rstUart18, rstUart19, rstUart20, rstUart21, rstUart22, rstUart23, rstUart24);
        }

        public void SetResetUART16_9(Enums.ResetOper rstUart9, Enums.ResetOper rstUart10, Enums.ResetOper rstUart11, Enums.ResetOper rstUart12,
                          Enums.ResetOper rstUart13, Enums.ResetOper rstUart14, Enums.ResetOper rstUart15, Enums.ResetOper rstUart16)
        {
            WriteMessageBits(0x6, rstUart9, rstUart10, rstUart11, rstUart12, rstUart13, rstUart14, rstUart15, rstUart16);
        }

        public void SetResetUART8_1(Enums.ResetOper rstUart1, Enums.ResetOper rstUart2, Enums.ResetOper rstUart3, Enums.ResetOper rstUart4,
                         Enums.ResetOper rstUart5, Enums.ResetOper rstUart6, Enums.ResetOper rstUart7, Enums.ResetOper rstUart8)
        {
            WriteMessageBits(0x6, rstUart1, rstUart2, rstUart3, rstUart4, rstUart5, rstUart6, rstUart7, rstUart8);
        }
        #endregion

        #region  BroadcastUART
        public void SetBroadcastUART40_33(Enums.EnableDisable en33, Enums.EnableDisable en34, Enums.EnableDisable en35, Enums.EnableDisable en36,
                              Enums.EnableDisable en37, Enums.EnableDisable en38, Enums.EnableDisable en39, Enums.EnableDisable en40)
        {

            WriteMessageBits(0x81, en33, en34, en35, en36, en37, en38, en39, en40);
        }

        public void SetBroadcastUART32_25(Enums.EnableDisable en25, Enums.EnableDisable en26, Enums.EnableDisable en27, Enums.EnableDisable en28,
                               Enums.EnableDisable en29, Enums.EnableDisable en30, Enums.EnableDisable en31, Enums.EnableDisable en32)
        {
            WriteMessageBits(0x82, en25, en26, en27, en28, en29, en30, en31, en32);
        }

        public void SetBroadcastUART24_17(Enums.EnableDisable en17, Enums.EnableDisable en18, Enums.EnableDisable en19, Enums.EnableDisable en20,
                               Enums.EnableDisable en21, Enums.EnableDisable en22, Enums.EnableDisable en23, Enums.EnableDisable en24)
        {
            WriteMessageBits(0x83, en17, en18, en19, en20, en21, en22, en23, en24);
        }

        public void SetBroadcastUART16_9(Enums.EnableDisable en9, Enums.EnableDisable en10, Enums.EnableDisable en11, Enums.EnableDisable en12,
                              Enums.EnableDisable en13, Enums.EnableDisable en14, Enums.EnableDisable en15, Enums.EnableDisable en16)
        {
            WriteMessageBits(0x84, en9, en10, en11, en12, en13, en14, en15, en16);
        }

        public void SetBroadcastUART8_1(Enums.EnableDisable en1, Enums.EnableDisable en2, Enums.EnableDisable en3, Enums.EnableDisable en4,
                             Enums.EnableDisable en5, Enums.EnableDisable en6, Enums.EnableDisable en7, Enums.EnableDisable en8)
        {
            WriteMessageBits(0x85, en1, en2, en3, en4, en5, en6, en7, en8);
        }
        #endregion

        public void SetBroadcastSPW3_4(Enums.EnableDisable bcSpw3, Enums.EnableDisable bcSpw4)
        {
            Enums.EnableDisable reserved = Enums.EnableDisable.Disable;
            WriteMessageBits(0x86, bcSpw3, bcSpw4, reserved, reserved, reserved, reserved, reserved, reserved);
        }

        #region UartConfig
        public void SetUartConfig1(byte port,byte timeOut)
        {
            WriteUartConfig1(_config1CommAddressToAddressOffset[port], timeOut);
        }

        public void SetUartConfig2(byte port, Enums.Parity parity, Enums.TwoOne stopBit, Enums.BaudRate baudRate)
        {
            WriteUartConfig2(_config2CommAddressToAddressOffset[port], parity,stopBit,baudRate);
        }

        public void SetUartConfig3(byte port, Enums.EnableDisable uartEnable, Enums.TxEnable txEnable, byte dataLength = 8)
        {
            WriteUartConfig3(_config3CommAddressToAddressOffset[port], uartEnable, txEnable, dataLength);
        }

        public void SetSPW3_Config(byte spwDataRate)
        {
            byte[] data2Send = { spwDataRate };
            WriteMessage(_channelNumber, _cardId, _fpga, 0x87, data2Send);
        }

        private void WriteUartConfig1(ushort startAddress, byte timeOut)
        {
            byte[] data2Send = { timeOut };
            WriteMessage(_channelNumber, _cardId, _fpga, startAddress, data2Send);
        }

        private void WriteUartConfig2(ushort startAddress, Enums.Parity parity, Enums.TwoOne stopBit, Enums.BaudRate baudRate)
        {
            byte byteSend = Convert.ToByte(Convert.ToByte(parity) | Convert.ToByte(stopBit) << 2 | Convert.ToByte(baudRate) << 4);
            byte[] data2Send = { byteSend };
            WriteMessage(_channelNumber, _cardId, _fpga, startAddress, data2Send);
        }

        private void WriteUartConfig3(ushort startAddress, Enums.EnableDisable uartEnable, Enums.TxEnable txEnable, byte dataLength = 8)
        {
            byte byteSend = Convert.ToByte(Convert.ToByte(uartEnable) | Convert.ToByte(txEnable) << 1 | (dataLength) << 2);
            byte[] data2Send = { byteSend };
            WriteMessage(_channelNumber, _cardId, _fpga, startAddress, data2Send);
        }

        #endregion

        public void SetSPW4_Config(byte spwDataRate)
        {
            byte[] data2Send = { spwDataRate };
            WriteMessage(_channelNumber, _cardId, _fpga, 0x88, data2Send);
        }

        public void SetGPIOsLedsControl(Enums.HighLow gpOut0, Enums.HighLow gpOut1, Enums.HighLow gpOut2,
                                     Enums.HighLow led0, Enums.HighLow led1, Enums.HighLow led2)
        {
            byte byte2Send = Convert.ToByte(Convert.ToByte(gpOut0) | Convert.ToByte(gpOut1) << 1 | Convert.ToByte(gpOut1) << 2 | Convert.ToByte(led0) << 3 | Convert.ToByte(led1) << 5 | Convert.ToByte(led2) << 6);
            byte[] data2Send = { byte2Send };
            WriteMessage(_channelNumber, _cardId, _fpga, 0x89, data2Send);
        }

        public void SetNackDisable(Enums.EnableDisable nackDisable)
        {
            byte[] data2Send = { Convert.ToByte(nackDisable) };
            WriteMessage(_channelNumber, _cardId, _fpga, 0x8A, data2Send);
        }

        public void GetGpioStaus(out Enums.HighLow gpIn0, out Enums.HighLow gpIn1, out Enums.HighLow gpIn2)
        {
            ReadMessage(_channelNumber, _cardId, _fpga, 0x8B, 1, out byte[] receivedBytes);
            gpIn0 = (Enums.HighLow)_conversions.GetBit(receivedBytes[0], 0);
            gpIn1 = (Enums.HighLow)_conversions.GetBit(receivedBytes[0], 1);
            gpIn2 = (Enums.HighLow)_conversions.GetBit(receivedBytes[0], 2);
        }

        public void GetCtrlSPWsErrors(out Enums.ErrorOK spwMainHeaderError, out Enums.ErrorOK spwMainCsError, 
                                      out Enums.ErrorOK spwMainChannelError, out Enums.ErrorOK spwReduChannelError,
                                      out Enums.ErrorOK spwReduHeaderError, out Enums.ErrorOK spwReduCsError)
        {
            ReadMessage(_channelNumber, _cardId, _fpga, 0x8C, 1, out byte[] receivedBytes);
            spwMainHeaderError = (Enums.ErrorOK)_conversions.GetBit(receivedBytes[0], 0);
            spwMainCsError = (Enums.ErrorOK)_conversions.GetBit(receivedBytes[0], 1);
            spwMainChannelError = (Enums.ErrorOK)_conversions.GetBit(receivedBytes[0], 2);
            spwReduChannelError = (Enums.ErrorOK)_conversions.GetBit(receivedBytes[0], 3);
            spwReduHeaderError = (Enums.ErrorOK)_conversions.GetBit(receivedBytes[0], 4);
            spwReduCsError = (Enums.ErrorOK)_conversions.GetBit(receivedBytes[0], 5);
        }

        public void GetCtrlSPWsErrors(out Enums.ErrorOK spwEx3eep, out Enums.ErrorOK spwEx4eep)
        {
            ReadMessage(_channelNumber, _cardId, _fpga, 0x8D, 1, out byte[] receivedBytes);
            spwEx3eep = (Enums.ErrorOK)_conversions.GetBit(receivedBytes[0], 0);
            spwEx4eep = (Enums.ErrorOK)_conversions.GetBit(receivedBytes[0], 1);
        }

        public void GetUartStatus(byte port ,out Enums.EmptyFull rxFifoEmpty, out Enums.EmptyFull rxFifoFull,
                             out Enums.EmptyFull txFifoEmpty, out Enums.EmptyFull txFIFOFull, out Enums.ErrorOK parityError,
                             out Enums.ErrorOK overrunError, out Enums.ErrorOK frameError, out Enums.ErrorOK rbrkError)
        {
            ushort startAddress;
            startAddress = _uartStatusCommStatusToAddressOffset[port];
            ReadMessage(_channelNumber, _cardId, _fpga, startAddress, 1, out byte[] receivedBytes);
            rxFifoEmpty = (Enums.EmptyFull)_conversions.GetBit(receivedBytes[0], 0);
            rxFifoFull = (Enums.EmptyFull)_conversions.GetBit(receivedBytes[0], 1);
            txFifoEmpty = (Enums.EmptyFull)_conversions.GetBit(receivedBytes[0], 2);
            txFIFOFull = (Enums.EmptyFull)_conversions.GetBit(receivedBytes[0], 3);
            parityError = (Enums.ErrorOK)_conversions.GetBit(receivedBytes[0], 4);
            overrunError = (Enums.ErrorOK)_conversions.GetBit(receivedBytes[0], 5);
            frameError = (Enums.ErrorOK)_conversions.GetBit(receivedBytes[0], 6);
            rbrkError = (Enums.ErrorOK)_conversions.GetBit(receivedBytes[0], 7);
        }

        private static T[] PackValuesToArray<T>(T item1, T item2, T item3, T item4, T item5, T item6, T item7, T item8)
        {
            return new T[]
            {
                item1, item2, item3, item4,
                item5, item6, item7, item8
            };
        }

        private void WriteMessageBits<T>(byte startAddress, T item1, T item2, T item3, T item4, T item5, T item6, T item7, T item8)
        {
            byte value=0;

            var bits=PackValuesToArray(item1, item2, item3, item4, item5, item6, item7, item8);
            for (int i=0;i<bits.Length;i++)
            {
                value = _conversions.SetBits(value,  Convert.ToByte(bits[i]), i);
            }
            byte[] data2Send = { value };
            WriteMessage(_channelNumber, _cardId, _fpga, startAddress, data2Send);
        }

    }
}
