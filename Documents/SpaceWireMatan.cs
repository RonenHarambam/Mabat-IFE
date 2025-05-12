using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

/// <summary>
/// Implementation of spacewire interface.
/// Use this interface when connecting to the devices that are connected to the system.
/// </summary>
public class SpaceWire
{

    #region DLL imports
    [DllImport("star-api.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern void STAR_runConfigService();

    [DllImport("star-api.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr STAR_getDeviceList(ref int count);

    [DllImport("star-api.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr STAR_getDeviceSerialNumber(int deviceId);

    [DllImport("star-api.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr STAR_getDeviceName(int deviceId);

    [DllImport("star-api.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int STAR_openChannelToLocalDevice(int device, STAR_CHANNEL_DIRECTION direction, byte channelNumber, int isQueued);

    [DllImport("star-api.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int STAR_closeChannel(int channelId);
    
    [DllImport("star-api.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int STAR_resetDevice(int deviceId);
    
    [DllImport("star-api.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern STAR_TRANSFER_STATUS STAR_transmitPacket(int channelId, byte[] pPacketData, UInt32 packetLength, STAR_EOP_TYPE eopType, int timeout);

    [DllImport("star-api.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern STAR_TRANSFER_STATUS STAR_receivePacket(uint channelId, byte[] pPacketData, ref UInt32 pPacketLength, out STAR_EOP_TYPE pEopType, int timeout);

    [DllImport("star-api.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int STAR_getDeviceChannels(int deviceId);

    [DllImport("star_conf_api_pxi.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int CFG_PXI_setTransmitClock(int deviceID, char linkNum, STAR_CFG_MK2_BASE_TRANSMIT_CLOCK clockRateParams);

    [DllImport("star_conf_api_router.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int CFG_ROUTER_setSpaceWireLinkStatus(int deviceID, char linkNum, STAR_CFG_SPW_LINK_STATUS linkStatus);

    [DllImport("star_conf_api_router.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int CFG_PXI_enableInterfaceModeOnPort(int deviceID, char portNum);

    [DllImport("star_conf_api_generic.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int CFG_enableInterfaceMode(int deviceID);

    [DllImport("star_conf_api_generic.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int CFG_disableInterfaceMode(int deviceID);

    [DllImport("star_conf_api_generic.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int CFG_setRouterGlobalSettings(int deviceID, STAR_CFG_ROUTER_GLOBAL_STATE pState);

    [DllImport("star_conf_api_generic.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int CFG_getRouterGlobalSettings(int deviceID, ref STAR_CFG_ROUTER_GLOBAL_STATE pState);

    [DllImport("star_conf_api_generic.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int CFG_getPortRoutingAddress(int deviceID, byte portNum, ref byte pAddress);

    [DllImport("star_conf_api_generic.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int CFG_setPortRoutingAddress(int deviceID, byte portNum, byte pAddress);



    //[DllImport("star-api.dll", CallingConvention = CallingConvention.StdCall)]
     // int STAR_API_CC CFG_ROUTER_getExternalPortStatus(PORT_STATUS_CONTROL portStatusControl,
     //                                                         _Out_ STAR_CFG_EXTERNAL_PORT_STATUS* status);

    /*
     * int STAR_API_CC CFG_getPortRoutingAddress(STAR_DEVICE_ID deviceID, U8 portNum, _Out_ U8* pAddress);
     * int STAR_API_CC CFG_setPortRoutingAddress(STAR_DEVICE_ID deviceID, U8 portNum, U8 address); 
     * [DllImport("star-api.dll", CallingConvention = CallingConvention.StdCall)]
     * int STAR_API_CC CFG_ROUTER_getExternalPortStatus(PORT_STATUS_CONTROL portStatusControl,
                                                              _Out_ STAR_CFG_EXTERNAL_PORT_STATUS* status);
     * 
    */

    #endregion DLL imports

    public enum STAR_CFG_TIMEOUT_MODE
    {
        STAR_CFG_TIMEOUT_MODE_BLOCKING,
        STAR_CFG_TIMEOUT_MODE_WATCHDOG

    };

    public enum STAR_CFG_PORT_TIMEOUT
    {
        /** 60-80us */
        STAR_CFG_PORT_TIMEOUT_100US     = 0x0,
        /** ~1.3ms */
        STAR_CFG_PORT_TIMEOUT_1MS       = 0x1,
        /** ~10ms */
        STAR_CFG_PORT_TIMEOUT_10MS      = 0x2,
        /** ~82ms */
        STAR_CFG_PORT_TIMEOUT_100MS     = 0x3,
        /** ~1.3s */
        STAR_CFG_PORT_TIMEOUT_1S        = 0x4
    };

    public struct STAR_CFG_ROUTER_GLOBAL_STATE
    {
        public STAR_CFG_TIMEOUT_MODE timeoutMode;
        public STAR_CFG_PORT_TIMEOUT timeoutPeriod;
        public char disableOnSilence;
        public char startOnRequest;
        public char enableSelfAddressing;
    };


    /// <summary>
    /// Channel configuration
    /// </summary>
    public enum STAR_CHANNEL_DIRECTION : int
    {
        STAR_CHANNEL_DIRECTION_IN = 1,
        STAR_CHANNEL_DIRECTION_OUT = 2,
        STAR_CHANNEL_DIRECTION_INOUT = STAR_CHANNEL_DIRECTION_IN | STAR_CHANNEL_DIRECTION_OUT
    };

    public struct STAR_CFG_MK2_BASE_TRANSMIT_CLOCK
    {
        public UInt16 multiplier;
        public UInt16 divisor;
    }

    public enum STAR_CFG_SPW_LINK_STATE
    {
        STAR_CFG_SPW_LINK_STATE_ERROR_RESET,    /** Error Reset */
        STAR_CFG_SPW_LINK_STATE_ERROR_WAIT,     /** Error Wait */
        STAR_CFG_SPW_LINK_STATE_READY,          /** Ready */
        STAR_CFG_SPW_LINK_STATE_STARTED,        /** Started */
        STAR_CFG_SPW_LINK_STATE_CONNECTING,     /** Connecting */
        STAR_CFG_SPW_LINK_STATE_RUN,            /** Run */
        STAR_CFG_SPW_LINK_STATE_INVALID         /** Invalid value */
    };

    public struct STAR_CFG_SPW_LINK_STATUS
    {
        public char triState;
        public char disable;
        public char start;
        public char autoStart;
        public char running;
        public STAR_CFG_SPW_LINK_STATE linkState;
    };


    /// <summary>
    /// Error code for the transmit/receive operations
    /// </summary>
    public enum STAR_TRANSFER_STATUS : int
    {
        STAR_TRANSFER_STATUS_NOT_STARTED,
        STAR_TRANSFER_STATUS_STARTED,
        STAR_TRANSFER_STATUS_COMPLETE,
        STAR_TRANSFER_STATUS_CANCELLED,
        STAR_TRANSFER_STATUS_ERROR
    }

    /// <summary>
    /// end-op-packet types
    /// </summary>
    public enum STAR_EOP_TYPE : int
    {
        STAR_EOP_TYPE_INVALID,
        STAR_EOP_TYPE_EOP,
        STAR_EOP_TYPE_EEP,
        STAR_EOP_TYPE_NONE,
    }

    public enum STAR_CFG_PORT_TYPE
    {
    /** Configuration port */
    STAR_CFG_PORT_TYPE_CONFIGURATION,
    /** SpaceWire Link port */
    STAR_CFG_PORT_TYPE_LINK,
    /** External port*/
    STAR_CFG_PORT_TYPE_EXTERNAL,
    /** Invalid value. This should never occur*/
    STAR_CFG_PORT_TYPE_INVALID
    }

    /// <summary>
    /// a data structure for holding specific information about each device found in the system
    /// </summary>
    public struct DevParameters
    {
        public int ID;
        public int[] ChannelsIDs;
        public string Name;
        public string SerialNumber;
    };

    /// <summary>
    /// An array of devices containing all the inforamtion about each device in the system
    /// </summary>
    public DevParameters[] AllTheDevices;
    public int deviceCount;

    public SpaceWire()
    {

    }

    /// <summary>
    /// Initialized all of the connected spw devices. At the end of the initialization process,
    /// The <AllTheDevices> will contain each of the devices information.
    /// </summary>
    /// <returns></returns>
    public bool Init()
    {
        int deviceCount = 0;
        IntPtr devList = STAR_getDeviceList(ref deviceCount);
        if (deviceCount <= 0)
            return false;

        AllTheDevices = new DevParameters[deviceCount];
        STAR_resetDevice(0);
        STAR_resetDevice(1);
        int[] deviceList = new int[deviceCount];
        Marshal.Copy(devList, deviceList, 0, deviceCount);
        for (int i = 0; i < deviceCount; i++)
        {
            AllTheDevices[i].ID = deviceList[i];
            AllTheDevices[i].Name = GetDeviceName(AllTheDevices[i].ID);
            AllTheDevices[i].SerialNumber = GetSerialNumber(AllTheDevices[i].ID);
            AllTheDevices[i].ChannelsIDs = new int[GetNumbersOfChannelsOnDevice(AllTheDevices[i].ID)];
        }

        return true;
    }
        
    /// <summary>
    /// Returns the device name, given the device's id
    /// </summary>
    /// <param name="devID"></param>
    /// <returns></returns>
    private string GetDeviceName(int devID)
    {
        IntPtr t = STAR_getDeviceName(devID);
        return Marshal.PtrToStringAnsi(t);
    }

    /// <summary>
    /// Returns the device serial number, given the device's id
    /// </summary>
    /// <param name="devID"></param>
    /// <returns></returns>
    private string GetSerialNumber(int devID)
    {
        IntPtr t = STAR_getDeviceSerialNumber(devID);
        return Marshal.PtrToStringAnsi(t);
    }

    /// <summary>
    /// Returns the how many channels are on this device, given the device's id
    /// </summary>
    /// <param name="devID"></param>
    /// <returns></returns>
    private int GetNumbersOfChannelsOnDevice(int devID)
    {
        int ChannelMask = STAR_getDeviceChannels(devID);
        int UsableChannelsMask = (ChannelMask >> 1); // Step over channel 0
        int numChannels = 0;
        int isChanAvailable;
        for (int bit = 0; bit < 32; bit++)
        {
            int mask = (int)Math.Pow(2, bit);
            isChanAvailable = (UsableChannelsMask & mask) >> bit;
            numChannels += isChanAvailable;
        }

        return numChannels;
    }

    public enum SW_ERRORS
    {
        SW_ERRORS_SUCCESS = 0,
        SW_ERRORS_DEVICE_INDEX_ERROR = -1,
        SW_ERRORS_CHANNEL_INDEX_ERROR = -2,
        SW_ERRORS_OPEN_CHANNEL_ERROR = -3,
        SW_ERRORS_TRANSMIT_ERROR = -4,
        SW_ERRORS_RECEIVE_ERROR = -4,
    }

    /// <summary>
    /// Opens the channel for sending and/or receiving
    /// </summary>
    /// <param name="deviceIndex">0 based index of the device from the AllTheDevices<> </param>
    /// <param name="channelIndex">0 based index of the channel</param>
    /// <param name="direction">IN, OUT or IN and OUT</param>
    /// <param name="isQueued">isQueued</param>
    /// <returns>error type</returns>
    public SW_ERRORS OpenChannel(int deviceIndex, int ChannelIntefaceNumber, STAR_CHANNEL_DIRECTION direction, int isQueued = 1)
    {
        if (deviceIndex < 0 || deviceIndex >= AllTheDevices.Length) return SW_ERRORS.SW_ERRORS_DEVICE_INDEX_ERROR;
        if (ChannelIntefaceNumber < 0 || ChannelIntefaceNumber >= AllTheDevices[deviceIndex].ChannelsIDs.Length) return SW_ERRORS.SW_ERRORS_CHANNEL_INDEX_ERROR;
        if (AllTheDevices[deviceIndex].ChannelsIDs[ChannelIntefaceNumber] != 0) return SW_ERRORS.SW_ERRORS_SUCCESS;
        AllTheDevices[deviceIndex].ChannelsIDs[ChannelIntefaceNumber] = STAR_openChannelToLocalDevice(AllTheDevices[deviceIndex].ID, direction, (byte)(ChannelIntefaceNumber + 1), isQueued);
        if (AllTheDevices[deviceIndex].ChannelsIDs[ChannelIntefaceNumber] == 0)
            return SW_ERRORS.SW_ERRORS_OPEN_CHANNEL_ERROR;
        return SW_ERRORS.SW_ERRORS_SUCCESS;
    }

    /// <summary>
    /// Closes the channel
    /// </summary>
    /// <param name="deviceIndex">0 based index of the device from the AllTheDevices<> </param>
    /// <param name="channelIndex">0 based index of the channel</param>
    public void CloseChannel(int deviceIndex, int ChannelIntefaceNumber)
    {
        STAR_closeChannel(AllTheDevices[deviceIndex].ChannelsIDs[ChannelIntefaceNumber]);
        AllTheDevices[deviceIndex].ChannelsIDs[ChannelIntefaceNumber] = 0;
    }

    /// <summary>
    /// Sends data on the spw channel
    /// </summary>
    /// <param name="deviceIndex">0 based index of the device from the AllTheDevices<> </param>
    /// <param name="channelIndex">0 based index of the channel</param>
    /// <param name="Data">the data to be transmited</param>
    /// <param name="TX_NumberOfBytes">how many bytes to transmit</param>
    /// <returns>error type</returns>
    public SW_ERRORS Transmit(int deviceIndex, int ChannelIntefaceNumber, byte[] Data, int TX_NumberOfBytes)
    {
        STAR_TRANSFER_STATUS status = STAR_transmitPacket(AllTheDevices[deviceIndex].ChannelsIDs[ChannelIntefaceNumber], Data, (uint)Data.Length, STAR_EOP_TYPE.STAR_EOP_TYPE_EOP, 20);
        if (status != STAR_TRANSFER_STATUS.STAR_TRANSFER_STATUS_COMPLETE)
            return SW_ERRORS.SW_ERRORS_TRANSMIT_ERROR;
        return SW_ERRORS.SW_ERRORS_SUCCESS;
    }

    public SW_ERRORS Transmit(int deviceIndex, int ChannelIntefaceNumber, int RouterNumber, byte[] Data, int TX_NumberOfBytes)
    {
        byte[] DataToSend = new byte[Data.Length + 1];
        string Hex = RouterNumber.ToString("X");
        DataToSend[0] = (byte)int.Parse(Hex, System.Globalization.NumberStyles.HexNumber);  //(byte)RouterNumber;
        Buffer.BlockCopy(Data, 0, DataToSend, 1, Data.Length);
        STAR_TRANSFER_STATUS status = STAR_transmitPacket(AllTheDevices[deviceIndex].ChannelsIDs[ChannelIntefaceNumber], DataToSend, (uint)DataToSend.Length, STAR_EOP_TYPE.STAR_EOP_TYPE_EOP, 20);
        // Adapt to brick MK2
        //STAR_TRANSFER_STATUS status = STAR_transmitPacket(AllTheDevices[deviceIndex].ChannelsIDs[ChannelIntefaceNumber], Data, (uint)Data.Length, STAR_EOP_TYPE.STAR_EOP_TYPE_EOP, 20);
        if (status != STAR_TRANSFER_STATUS.STAR_TRANSFER_STATUS_COMPLETE)
            return SW_ERRORS.SW_ERRORS_TRANSMIT_ERROR;
        return SW_ERRORS.SW_ERRORS_SUCCESS;
    }

    /// <summary>
    /// Reads data from a spw channel
    /// </summary>
    /// <param name="deviceIndex">0 based index of the device from the AllTheDevices<> </param>
    /// <param name="channelIndex">0 based index of the channel</param>
    /// <param name="Data"></param>
    /// <param name="NumRxBytes">how many bytes were received</param>
    /// <param name="eop"></param>
    /// <param name="timeout">read operation timeout</param>
    /// <returns></returns>
    public SW_ERRORS Receive(int deviceIndex, int ChannelIntefaceNumber, ref byte[] Data, ref uint NumRxBytes, ref STAR_EOP_TYPE eop, int timeout)
    {
        byte[] RxBuf = new byte[65535];
        byte[] TempRxBuf = new byte[65535];
        int indexInBuf = 0;
        NumRxBytes = (uint)RxBuf.Length;
        //STAR_TRANSFER_STATUS status = STAR_TRANSFER_STATUS.STAR_TRANSFER_STATUS_COMPLETE;
        //int numbytes = 1;
        while (NumRxBytes != 0)
        {
            STAR_receivePacket((uint)AllTheDevices[deviceIndex].ChannelsIDs[ChannelIntefaceNumber], RxBuf, ref NumRxBytes, out eop, 10);
            if (NumRxBytes > 0 && indexInBuf + NumRxBytes < TempRxBuf.Length)
            {
                Buffer.BlockCopy(RxBuf, 0, TempRxBuf, indexInBuf, (int)NumRxBytes);
                indexInBuf += (int)NumRxBytes;
            }
            
        }

        if (indexInBuf < 1)
        {
            return SW_ERRORS.SW_ERRORS_SUCCESS;
        }

        //if (status != STAR_TRANSFER_STATUS.STAR_TRANSFER_STATUS_COMPLETE)
        //    return SW_ERRORS.SW_ERRORS_TRANSMIT_ERROR;
        Data = new byte[indexInBuf];
        NumRxBytes = (uint)Data.Length;
        Buffer.BlockCopy(TempRxBuf, 0, Data, 0, indexInBuf);

        return SW_ERRORS.SW_ERRORS_SUCCESS;
    }

    public bool Loopback(int TX_deviceIndex, int TX_ChannelIntefaceNumber, int Tx_RouterNumber, byte[] TX_Data, int TX_NumBytes,
        int RX_deviceIndex, int RX_ChannelIntefaceNumber,int Rx_RouterNumber,
        int NumberOfIterations,
        ref int NumTxPackets, ref int NumRxPackets)
    {
        SW_ERRORS status;
        int tx_counter = 0;
        int rx_counter = 0;
        byte[] RX_Data = new byte[2048];
        uint RX_NumBytes;
        int b, errors;
        byte[] DataToSend = new byte[TX_Data.Length + 1];
        //DataToSend[0] = (byte)Tx_Port;
        string Hex = Rx_RouterNumber.ToString("X");
        DataToSend[0] = (byte)int.Parse(Hex, System.Globalization.NumberStyles.HexNumber); 
        //DataToSend[0] = (byte)Rx_RouterNumber;
        Buffer.BlockCopy(TX_Data, 0, DataToSend, 1, TX_Data.Length);

        //status = OpenChannel(TX_deviceIndex, TX_ChannelIntefaceNumber, STAR_CHANNEL_DIRECTION.STAR_CHANNEL_DIRECTION_OUT, 1);
        //if (status != SW_ERRORS.SW_ERRORS_SUCCESS)
        //{
        //    CloseChannel(TX_deviceIndex, TX_ChannelIntefaceNumber);
        //    return false;
        //}

        //status = OpenChannel(RX_deviceIndex, RX_ChannelIntefaceNumber, STAR_CHANNEL_DIRECTION.STAR_CHANNEL_DIRECTION_IN, 1);
        //if (status != SW_ERRORS.SW_ERRORS_SUCCESS)
        //{
        //    CloseChannel(TX_deviceIndex, TX_ChannelIntefaceNumber);
        //    CloseChannel(RX_deviceIndex, RX_ChannelIntefaceNumber);
        //    return false;
        //}
                
        STAR_EOP_TYPE eop = STAR_EOP_TYPE.STAR_EOP_TYPE_NONE;
        for (int i = 0; i < NumberOfIterations; i++ )
        {
            status = Transmit(TX_deviceIndex, TX_ChannelIntefaceNumber, Tx_RouterNumber, DataToSend, DataToSend.Length);
            if (status == SW_ERRORS.SW_ERRORS_SUCCESS)
            {
                tx_counter++;
                RX_NumBytes = 2048;
                status = Receive(RX_deviceIndex, RX_ChannelIntefaceNumber, ref RX_Data, ref RX_NumBytes, ref eop, 30);
                if (status == SW_ERRORS.SW_ERRORS_SUCCESS)
                {
                    errors = 0;
                    if (RX_NumBytes == TX_NumBytes)
                    {
                        for ( b = 0; b < TX_NumBytes; b++ )
                        {
                            if (TX_Data[b] != RX_Data[b])
                                errors++;
                        }

                        if (errors == 0)
                            rx_counter++;
                    }
                            
                }
            }
        }

        //CloseChannel(TX_deviceIndex, TX_ChannelIntefaceNumber);
        //CloseChannel(RX_deviceIndex, RX_ChannelIntefaceNumber);

        NumTxPackets = tx_counter;
        NumRxPackets = rx_counter;
        return true;
    }

    public int Cfg_TransmitClock(int DeviceIndex, char _Port, ushort divisor, ushort multiplier)
    {
        int Status = 0;
        STAR_CFG_MK2_BASE_TRANSMIT_CLOCK Clock_Cfg = new STAR_CFG_MK2_BASE_TRANSMIT_CLOCK();
        Clock_Cfg.divisor = divisor;
        Clock_Cfg.multiplier = multiplier;
        Status = CFG_PXI_setTransmitClock(AllTheDevices[DeviceIndex].ID, _Port, Clock_Cfg);
        return Status;
    }

    public int DisableTriState(int DeviceIndex, char _Port)
    {
        STAR_CFG_SPW_LINK_STATUS LinkStatus = new STAR_CFG_SPW_LINK_STATUS();
        int Status = 0;

        LinkStatus.autoStart = (char)1;
        LinkStatus.disable = (char)0;
        LinkStatus.linkState = STAR_CFG_SPW_LINK_STATE.STAR_CFG_SPW_LINK_STATE_CONNECTING;
        LinkStatus.running = (char)0;
        LinkStatus.start = (char)0;
        LinkStatus.triState = (char)0;

        Status = CFG_ROUTER_setSpaceWireLinkStatus(AllTheDevices[DeviceIndex].ID, _Port, LinkStatus);
        return (Status);
    }

   public int EnableInterfaceMode(int DeviceIndex)
    {
        int Status = 0;
        Status = CFG_enableInterfaceMode(AllTheDevices[DeviceIndex].ID);
        byte Test = 0;
        //Status = CFG_getPortRoutingAddress(1, 10, ref Test);
        //Status = CFG_setPortRoutingAddress(AllTheDevices[DeviceIndex].ID, (byte)10, (byte)14);
        //Status = CFG_setPortRoutingAddress(AllTheDevices[DeviceIndex].ID, (byte)14, (byte)10);
        return (Status);
    }

    public int DisableInterfaceMode(int DeviceIndex)
    {
        int Status = 0;
        Status = CFG_disableInterfaceMode(AllTheDevices[DeviceIndex].ID);
        return (Status);
    }

    public int SetRouterGlobalSettings(int DeviceIndex)
    {
        STAR_CFG_ROUTER_GLOBAL_STATE gState = new STAR_CFG_ROUTER_GLOBAL_STATE();
        gState.disableOnSilence = (char)0;
        gState.enableSelfAddressing = (char)1;
        gState.startOnRequest = (char)1;
        gState.timeoutMode = STAR_CFG_TIMEOUT_MODE.STAR_CFG_TIMEOUT_MODE_WATCHDOG;
        gState.timeoutPeriod = STAR_CFG_PORT_TIMEOUT.STAR_CFG_PORT_TIMEOUT_1MS;
        int Status = 0;
        Status = CFG_setRouterGlobalSettings(AllTheDevices[DeviceIndex].ID, gState);

        return Status;
    }

    public int GetRouterGlobalSettings(int DeviceIndex)
    {
        STAR_CFG_ROUTER_GLOBAL_STATE gState = new STAR_CFG_ROUTER_GLOBAL_STATE();     
        int Status = 0;

        Status = CFG_getRouterGlobalSettings(AllTheDevices[DeviceIndex].ID, ref gState);

        return Status;
    }

    public int SetPortRoutingAddress(int DeviceIndex, int SPW_Port, int SPW_Channel)
    {
        int Status = 0;
        int Channel = SPW_Channel + 13;
        Status = CFG_setPortRoutingAddress(AllTheDevices[DeviceIndex].ID, (byte)SPW_Port, (byte)Channel);
        Status = CFG_setPortRoutingAddress(AllTheDevices[DeviceIndex].ID, (byte)Channel, (byte)SPW_Port);
        return Status;
    }




}
