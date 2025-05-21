using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLLTester
{
    public partial class UserControlEnumParametersField : UserControl
    {

        public ComboBox GetComboBox
        {
            private set => cbEnumValues = value;
            get => cbEnumValues;
        }

        public UserControlEnumParametersField()
        {
            InitializeComponent();
        }
    }
}