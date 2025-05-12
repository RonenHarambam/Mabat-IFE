using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace DeviceManager.BRICK32
{
    public class BRICK32

    {
        #region DLL IMPORT

        public struct STAR_CFG_MK2_BASE_TRANSMIT_CLOCK
        {
            public UInt16 multiplier;
            public UInt16 divisor;
        }

        //[DllImport("star_conf_api_pxi.dll", CallingConvention = CallingConvention.StdCall)]
        //public static extern int CFG_PXI_setTransmitClock(uint device, char linkNum, STAR_CFG_MK2_BASE_TRANSMIT_CLOCK clockRateParams);
        [DllImport("star_conf_api_pxi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int CFG_PXI_setTransmitClock(uint device, byte linkNum, STAR_CFG_MK2_BASE_TRANSMIT_CLOCK clockRateParams);

        [DllImport(@"star-api.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr STAR_getApiVersion();

        [DllImport(@"star-api.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void STAR_destroyVersionInfo(IntPtr pVersionInfo);

        [DllImport(@"star-api.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr STAR_getDeviceList(out uint count);

        [DllImport(@"star-api.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr STAR_getDeviceSerialNumber(uint deviceHandle);

        [DllImport(@"star-api.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr STAR_destroyString(IntPtr str);

        [DllImport(@"star-api.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern uint STAR_openChannelToLocalDevice(uint device, STAR_CHANNEL_DIRECTION direction, IntPtr channelNumber, int isQueued);

        [DllImport(@"star-api.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe private static extern STAR_TRANSFER_STATUS STAR_receivePacket(uint channelId, byte[] pPacketData, ref uint pPacketLength, out STAR_EOP_TYPE pEopType, int timeout);

        [DllImport(@"star-api.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern STAR_TRANSFER_STATUS STAR_transmitPacket(uint channelId, byte[] pPacketData, uint pPacketLength, STAR_EOP_TYPE pEopType, int timeout);

        [DllImport(@"star-api.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern STAR_TRANSFER_STATUS STAR_closeChannel(uint channelId);

        //[DllImport(@"star-api.dll", CallingConvention = CallingConvention.Cdecl)]
        //private static extern IntPtr STAR_createRxOperation(int itemCount, STAR_RECEIVE_MASK mask);

        //[DllImport(@"star-api.dll", CallingConvention = CallingConvention.Cdecl)]
        //private static extern int STAR_disposeTransferOperation(IntPtr starTransferOperation);

        //[DllImport(@"star-api.dll", CallingConvention = CallingConvention.Cdecl)]
        //private static extern int STAR_submitTransferOperation(uint channelId, STAR_TRANSFER_OPERATION starTransferOperation);
        #endregion


        /// <summary>
        /// Key   - channel index
        /// Valeu - channel handle
        /// </summary>
        Dictionary<int, uint> _chIndexToChId = new Dictionary<int, uint>();

        /// <summary>
        /// (200Mbits)*multiplier/division
        /// </summary>
        /// <param name="deviceHandle"></param>
        /// <param name="_Port">1-2</param>
        /// <param name="divisor"></param>
        /// <param name="multiplier"></param>
        /// <returns></returns>
        //public void SetTransmitClock(char _Port, ushort divisor, ushort multiplier)
        public void SetTransmitClock(byte port, ushort divisor, ushort multiplier)
        {
            int status = 0;
            STAR_CFG_MK2_BASE_TRANSMIT_CLOCK Clock_Cfg = new STAR_CFG_MK2_BASE_TRANSMIT_CLOCK();
            Clock_Cfg.divisor = divisor;
            Clock_Cfg.multiplier = multiplier;
            status = CFG_PXI_setTransmitClock(_deviceHandle, port, Clock_Cfg);
            if(status!=1)
            {
                throw new Exception("Cannot set transmit clock");
            }
        }

        /// <summary>
        /// Gets the Version of STAR-API.
        /// </summary>
        public void GetApiVersion(out STAR_VERSION_INFO versionInfo)
        {
            IntPtr p = STAR_getApiVersion();

            versionInfo = (STAR_VERSION_INFO)Marshal.PtrToStructure(p, typeof(STAR_VERSION_INFO));

            STAR_destroyVersionInfo(p); // Frees a version information structure previously created by a call to STAR_getApiVersion()
        }

        /// <summary>
        /// Gets the serial number (unique alphanumeric identifier) of the device identified by a given identifier
        /// </summary>
        /// <param name="deviceHandle"></param>
        /// <param name="deviceSerial"></param>
        public void GetDeviceSerial(uint deviceHandle, out string deviceSerial)
        {
            IntPtr ptrSN = STAR_getDeviceSerialNumber(deviceHandle);

            deviceSerial = Marshal.PtrToStringAnsi(ptrSN);

            STAR_destroyString(ptrSN); // Destroy a string returned by STAR-API
        }

        uint _deviceHandle=0;

        /// <summary>
        /// Gets the device handle by index
        /// </summary>
        /// <param name="deviceHandle"></param>
        /// <param name="index">Position in the array</param>
        public void OpenDevice(int index = 0)
        {
            uint[] devices = GetDeviceList();

            if (devices.Length <= index)
            {
                throw new Exception("Index out of range..");
            }

            _deviceHandle = devices[index];
        }



        /// <summary>
        /// Gets an array of identifiers for all devices present for all drivers
        /// </summary>
        /// <returns></returns>
        public uint[] GetDeviceList()
        {
             uint count;
            IntPtr ptrDevices = STAR_getDeviceList(out count);

            uint[] devices = new uint[count];

            if (ptrDevices == IntPtr.Zero)
            {
                return devices; // Empty array..
            }

            Helper.Copy<uint>(ptrDevices, devices, 0, (int)count);

            return devices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceHandle">from GetDeviceId</param>
        /// <param name="channelNumber">1-31</param>
        /// <param name="channelId"></param>
        /// <param name="channelDirection"></param>
        public void OpenChannel( int channelNumber, STAR_CHANNEL_DIRECTION channelDirection = STAR_CHANNEL_DIRECTION.STAR_CHANNEL_DIRECTION_INOUT)
        {
            IntPtr ptrChannelNumber = new IntPtr(channelNumber);
            var channelId = STAR_openChannelToLocalDevice(_deviceHandle, channelDirection, ptrChannelNumber, 1);
            _chIndexToChId.Add(channelNumber, channelId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelNumber">from OpenChannel</param>
        public STAR_TRANSFER_STATUS CloseChannel(int channelNumber,bool clearIndexTchannel)
        {
            if (_chIndexToChId.ContainsKey(channelNumber))
            {
                var result= STAR_closeChannel(_chIndexToChId[channelNumber]);
                if (clearIndexTchannel)
                {
                    _chIndexToChId.Remove(channelNumber);
                }
                return result;
            }
            else
            {
                return STAR_TRANSFER_STATUS.STAR_TRANSFER_STATUS_COMPLETE;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void CloseAllChannels()
        {
            foreach(var kvp in _chIndexToChId)
            {
                CloseChannel(kvp.Key,false);
            }
            _chIndexToChId.Clear();
        }

        const int MaxPacketLength = 65535;



        /// <summary>
        /// Receive a single packet on a previously opened channel in to the buffer specified.
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <param name="receiveBufferBytes"></param>
        /// <param name="timeOut">The maximum time in milliseconds to wait for the packet to be received, or -1 to wait indefinitely</param>
        public void ReceivePacket (int channelNumber, out byte[] receiveBufferBytes, int timeOut = -1)
        {

            /* Define receive buffer of maximum packet length */
            byte[] receiveBuffer = new byte[MaxPacketLength];

 

            /* Initialise receive buffer length to size of receive buffer array */
            uint receiveBufferLength = MaxPacketLength;

            /* Define eop type */
            STAR_EOP_TYPE eopType;

            /* Receive packet on selected channel */
            STAR_TRANSFER_STATUS status = STAR_receivePacket(_chIndexToChId[channelNumber], receiveBuffer, ref receiveBufferLength, out eopType, timeOut);

            if (status != STAR_TRANSFER_STATUS.STAR_TRANSFER_STATUS_COMPLETE)
            {
                throw new Exception("Cannot receive packet. Details:" + status.ToString());
            }

            receiveBufferBytes = new byte[receiveBufferLength];

            for (int i = 0; i < receiveBufferLength; i++)
            {
                receiveBufferBytes[i] = receiveBuffer[i];
            }
        }

        /// <summary>
        /// Transmit a single packet on a previously opened channel from the buffer specified.
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <param name="packet">Packet to transmit</param>
        /// <param name="timeOut">The maximum time in milliseconds to wait for the packet to be transmitted, or -1 to wait indefinitely</param>
        public void TransmitPacket(int channelNumber, byte[] packet, int timeOut = -1, STAR_EOP_TYPE eopType = STAR_EOP_TYPE.STAR_EOP_TYPE_EOP)
        {
            var status = STAR_transmitPacket(_chIndexToChId[channelNumber], packet, (uint)packet.Length, eopType, timeOut);
            if(status!= STAR_TRANSFER_STATUS.STAR_TRANSFER_STATUS_COMPLETE)
            {
                throw new Exception("Cannot send packet. Details:" + status.ToString());
            }
        }

        ///// <summary>
        ///// Creates a receive operation that can then be submitted.
        ///// </summary>
        ///// <param name="itemCount">The maximum number of stream items to receive (the size of an individual stream item is not limited). This can be -1 to receive an unlimited number of items</param>
        ///// <param name="mask">Bitmask with flags set for the type of traffic one wishes to receive</param>
        ///// <returns></returns>
        //public STAR_TRANSFER_OPERATION CreateRxOperation(int itemCount, STAR_RECEIVE_MASK mask)
        //{
        //    IntPtr ptr = STAR_createRxOperation(itemCount, mask);

        //    STAR_TRANSFER_OPERATION op = (STAR_TRANSFER_OPERATION)Marshal.PtrToStructure(ptr, typeof(STAR_TRANSFER_OPERATION));

        //    STAR_disposeTransferOperation(ptr);

        //    return op;
        //}

        ///// <summary>
        ///// Submits a single transfer operation.
        ///// Once a transfer operation has completed, it can be resubmitted.
        ///// This can be useful for receiving a number of packets in a loop, or transmitting the same 
        ///// packet repeatedly.Note that if an operation is resubmitted before it has completed, 
        ///// the original operation will be cancelled and then the new one submitted.
        ///// </summary>
        ///// <param name="channelId">Channel to submit operation on</param>
        ///// <param name="transferOp"></param>
        ///// <returns>1 if operation was successfully submitted, else 0</returns>
        //public int SubmitTransferOperation(uint channelId, STAR_TRANSFER_OPERATION transferOp)
        //{
        //    return STAR_submitTransferOperation(channelId, transferOp);
        //}
    }
}
