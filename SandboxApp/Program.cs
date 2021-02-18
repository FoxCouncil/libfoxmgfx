using LibFoxObsCs;
using System;
using System.Runtime.InteropServices;
using uint32_t = System.UInt32;

namespace SandboxApp
{
    internal class Program
    {
        public const string LIBRARY = "obs";
        public const CallingConvention importCall = CallingConvention.Cdecl;
        public const CharSet importCharSet = CharSet.Ansi;

        [DllImport(LIBRARY, CallingConvention = importCall)]
        public static extern uint32_t obs_get_version();

        [DllImport(LIBRARY, CallingConvention = importCall, CharSet = importCharSet)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstCharPtrMarshaler))]
        public static extern string obs_get_version_string();

        private static void Main(string[] args)
        {
            var ver = new Version { Full = obs_get_version() };
            var helloStr = "Hello OBS Version: " + ver;
            var helloStr2 = "Hello OBS Version: " + obs_get_version_string();

            Console.WriteLine(helloStr);
            Console.WriteLine(helloStr2);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Version
    {
        [FieldOffset(0)]
        public UInt32 Full;

        [FieldOffset(3)]
        public byte Major;

        [FieldOffset(2)]
        public byte Minor;

        [FieldOffset(0)]
        public UInt16 Patch;

        public override string ToString()
        {
            return Major.ToString() + "." + Minor.ToString() + "." + Patch.ToString();
        }
    }
}