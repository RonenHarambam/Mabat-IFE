
using System;
using IFE;

namespace DLLTester
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ClassInspector classInspector = new ClassInspector();
            classInspector.LoadGUI(typeof(IFE.UUTManager));
        }
    }
}