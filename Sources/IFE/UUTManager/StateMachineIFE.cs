using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OTools;

namespace IFE
{
    class StateMachineIFE : OTools.StateMachineBase
    {
        
        internal const ushort SYNC = 0xEB6B;
        internal const byte SYNC1 = 0xEB;
        internal const byte SYNC2 = 0x6B;

        ushort _exepctedDataLength = 0;
        ushort _exepectedChecksum = 0;

        List<byte> _data = new List<byte>();

        protected override DelState InitState => WaitForSync1;

        private void WaitForSync1(byte input)
        {
            if (input == SYNC1)
            {
                SetState(WaitForSync2);
            }
        }

        private void WaitForSync2(byte input)
        {
            if (input == SYNC2)
            {
                SetState(WaitForCardIdAndFPGA);
            }
            else
            {
                Init();
            }
        }

        private void WaitForCardIdAndFPGA(byte input)
        {
            m_message.Add(input);
            SetState(WaitForStartAddressMSB);
        }

        private void WaitForStartAddressMSB(byte input)
        {
            m_message.Add(input);
            SetState(WaitForStartAddressLSB);
        }

        private void WaitForStartAddressLSB(byte input)
        {
            m_message.Add(input);
            _exepctedDataLength = 0;
            SetState(WaitForDataLengthMSB);
        }

        private void WaitForDataLengthMSB(byte input)
        {
            _exepctedDataLength = (ushort)(input << 8);
            m_message.Add(input);
            SetState(WaitForDataLengthLSB);
        }

        private void WaitForDataLengthLSB(byte input)
        {
            _exepctedDataLength = (ushort)(_exepctedDataLength | input);
            SetState(ReceievingData);
            _data.Clear();
        }

        private void ReceievingData(byte input)
        {
            m_message.Add(input);
            _data.Add(input);
            if(_data.Count== _exepctedDataLength)
            {
                SetState(WaitForChecksumMSB);
            }
        }

        private void WaitForChecksumMSB(byte input)
        {
            _exepectedChecksum = (ushort)(input << 8);
            SetState(WaitForChecksumLSB);
        }

        private void WaitForChecksumLSB(byte input)
        {
            _exepectedChecksum = (ushort)(_exepectedChecksum | input);
            if(Helper.CalculateChecksum(_data.ToArray())== _exepectedChecksum)
            {
                OnNewData(new NewDataArg(0, m_message.ToArray()));
            }
            Init();
        }
    }
}

