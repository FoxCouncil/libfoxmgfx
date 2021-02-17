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

        private static void Main(string[] args)
        {
            var ver = new Version { Full = obs_get_version() };

            Console.WriteLine("Hello OBS Version: " + ver);
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