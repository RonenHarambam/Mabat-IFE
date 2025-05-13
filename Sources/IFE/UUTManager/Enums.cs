using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFE
{
    public class Enums
    {
        public enum CardId:byte
        {
            Primary=0,
            Redundant=1
        }

        public enum Errors:byte
        {
            OK,
            Error=1
        }

        public enum OnOff:byte
        {
            Off=0,
            On=1
        }

        public enum OnBlinking:byte
        {
            Blinking=0,
            LedOn=1
        }

        public enum FullEmpty : byte
        {
            Empty = 0x0,
            Full = 0x1
        }


        public enum EmptyFull : byte
        {
            Full = 0x0,
            Empty = 0x1
        }

        public enum TwoOne : byte
        {
            One = 0x0,
            Two = 0x1
        }

        public enum LowHigh : byte
        {
            Low = 0x0,
            High = 0x1
        }

        public enum TxEnable : byte
        {
            Auto = 0x0,
            AlwaysOn = 0x1
        }

        public enum Parity : byte
        {
            None = 0x0,
            Odd = 0x1,
            Even = 0x2,
            NA = 0x3
        }

        public enum SpwRate : byte
        {
            Mbps9_6 = 0x0,
            Mbps12 = 0x1,
            Mbps16 = 0x2,
            Mbps24 = 0x3
        }

        public enum BaudRate : byte
        {
            Rate9600 = 0x0,
            Rate14400 = 0x1,
            Rate19200 = 0x2,
            Rate28800 = 0x3,
            Rate38400 = 0x4,
            Rate57600 = 0x5,
            Rate115200 = 0x6,
            Rate230400 = 0x7,
            Rate460800 = 0x8,
            Rate921600 = 0x9
        }

        public enum ResetOper:byte
        {
            Oper=1,
            Reset=1
        }

        public enum EnableDisable:byte
        {
            Disable=0,
            Enable=1
        }

        public enum DisableEnable : byte
        {
            Enable = 0,
            Disable = 1
        }
    }
}
