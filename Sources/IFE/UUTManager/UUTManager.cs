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
    /// Bigendian protocol
    /// </summary>
    public class UUTManager
    {
        const ushort SYNC = 0xEB6B;

        BRICK32 _commManager;
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

        /// <summary>
        /// Gets the device handle by index
        /// </summary>
        /// <param name="deviceHandle"></param>
        /// <param name="index">Position in the array</param>
        public void OpenDevice(int index = 0)
        {
            _commManager.OpenDevice(index);
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
        private void ReadMessage(int channelNumber,Enums.CardId cardId, byte fpga, byte startAddress, ushort numberOfBytesToRead,out byte[] receivedBytes)
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


    }
}
