using System;
using System.Runtime.InteropServices;

namespace SandboxApp.utils
{
    internal class ConstCharPtrMarshaler : ICustomMarshaler
    {
        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return Marshal.PtrToStringAnsi(pNativeData);
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            return IntPtr.Zero;
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
        }

        public void CleanUpManagedData(object ManagedObj)
        {
        }

        public int GetNativeDataSize()
        {
            return IntPtr.Size;
        }

        private static readonly ConstCharPtrMarshaler instance = new ConstCharPtrMarshaler();

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return instance;
        }
    }
}