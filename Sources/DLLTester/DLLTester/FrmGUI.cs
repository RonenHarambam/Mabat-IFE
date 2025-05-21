using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DLLTester
{
    public partial class s : Form
    {
        private ClassInspector _classInspector;

        /// <summary>
        /// Load GUI
        /// </summary>
        /// <param name="classInspector"></param>
        public s(ClassInspector classInspector)
        {
            InitializeComponent();
            _classInspector = classInspector;
            this.KeyPreview = true; // Form intercepts key events first
            this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);
        }

        private void FrmGUI_Load(object sender, EventArgs e)
        {
            LoadClassFunctions();
            CbFunctionList_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// Load all the class functions and put them in the combo box
        /// </summary>
        private void LoadClassFunctions()
        {
            List<MethodInfo> methods = _classInspector.FunctionList;

            foreach (MethodInfo method in methods)
            {
                cbFunctionList.Items.Add(method.Name);
            }

            if (cbFunctionList.Items.Count > 0)
                cbFunctionList.SelectedIndex = 0;

            // Handle selection change
            cbFunctionList.SelectedIndexChanged += CbFunctionList_SelectedIndexChanged;
        }

        /// <summary>
        /// Handles when use select different function from the combo box
        /// Shows all the parameters for the function, name, and type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbFunctionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get method based on user selection and than get parameters of that method
            MethodInfo method = GetMethodSelection();
            var parameters = method.GetParameters();

            // Clear the flow layout panel and add parameters to the controller
            flpParameters.Controls.Clear();

            foreach (var param in parameters)
            {
                Control control;
                if(param.ParameterType.BaseType == typeof (Enum))
                {
                    control = new UserControlEnumParametersField();
                    ((UserControlEnumParametersField)control).SetParameterName(param.Name);
                    ((UserControlEnumParametersField)control).SetParameterType(param.ParameterType.ToString());

                    foreach (var enumValue in Enum.GetNames(param.ParameterType))
                    {
                        ((UserControlEnumParametersField)control).GetComboBox.Items.Add(enumValue);
                    }
                }
                else
                {
                    control = new UserControlParametersField();
                    ((UserControlParametersField)control).SetParameterName(param.Name);
                    ((UserControlParametersField)control).SetParameterType(param.ParameterType.ToString());
                }

                flpParameters.Controls.Add(control);
            }
        }

        private void BtnExecuteFunction_Click(object sender, EventArgs e)
        {
            MethodInfo function = GetMethodSelection();
            string[] parametersArray = GetParametersValues();
            Object response = _classInspector.InvokeMethod(function, parametersArray);
            lblResponse.Text = response?.ToString();
        }

        /// <summary>
        /// Get from the flow layout panel all the parameters
        /// </summary>
        /// <returns></returns>
        private string[] GetParametersValues()
        {
            List<string> parametersValueList = new List<string>();

            // Iterate through each UserControlParametersField in the FlowLayoutPanel
            foreach (Control control in flpParameters.Controls)
            {
                string val = null;
                if (control is UserControlEnumParametersField enumField)
                {
                    val = enumField.GetParameterValue();
                }
                else if (control is UserControlParametersField paramField)
                {
                    val = paramField.GetParameterValue();
                }

                if(val != string.Empty)
                {
                    parametersValueList.Add(val);
                }
            }

            // Return the collected parameter values as an array of strings
            return parametersValueList.ToArray();
        }

        /// <summary>
        /// Return the method selected by user
        /// </summary>
        /// <returns></returns>
        private MethodInfo GetMethodSelection()
        {
            // Get the method from the list by its name
            string methodName = cbFunctionList.SelectedItem.ToString();
            var method = _classInspector.FunctionList.FirstOrDefault(m => m.Name == methodName);

            return method;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Trigger on Enter key
            {
                BtnExecuteFunction_Click(sender, EventArgs.Empty);
                e.Handled = true; // Prevent further processing by focused control
            }
        }
    }
}