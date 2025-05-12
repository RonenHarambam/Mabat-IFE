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
    }
}
