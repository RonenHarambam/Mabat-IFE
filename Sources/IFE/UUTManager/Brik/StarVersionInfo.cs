using System.Runtime.InteropServices;

namespace DeviceManager.BRICK32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct STAR_VERSION_INFO
    {
        /** The name of the module. */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Name;

        /** The author of the module. */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Author;

        /** The major version number of this module. */
        public ushort Major;

        /** The minor version number of this module. */
        public ushort Minor;

        /** The edit number of this module. */
        public ushort Edit;

        /** The patch number of this module. Edit will be 0 if patch is non zero */
        public ushort Patch;
    };
}
