using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFE
{
    class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal static ushort CalculateChecksum(byte[] data)
        {
            int retVal = 0;

            foreach (byte dataByte in data)
            {
                retVal = retVal + dataByte;
            }
            return (ushort)retVal;
        }


    }
}
