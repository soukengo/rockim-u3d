using System;

namespace RockIM.Sdk.Internal.V1.Infra.Socket
{
    public abstract class BigEndian
    {
        public static byte[] FromUint8(byte s)
        {
            return IfReverse(BitConverter.GetBytes(s));
        }

        public static byte[] FromUint16(ushort s)
        {
            return IfReverse(BitConverter.GetBytes(s));
        }

        public static byte[] FromUint32(uint s)
        {
            return IfReverse(BitConverter.GetBytes(s));
        }

        public static byte[] FromUint64(ulong s)
        {
            return IfReverse(BitConverter.GetBytes(s));
        }

        public static byte ToUInt8(byte[] bytes)
        {
            return IfReverse(bytes)[0];
        }

        public static ushort ToUInt16(byte[] bytes)
        {
            return BitConverter.ToUInt16(IfReverse(bytes));
        }

        public static uint ToUInt32(byte[] bytes)
        {
            return BitConverter.ToUInt32(IfReverse(bytes));
        }

        public static ulong ToUInt64(byte[] bytes)
        {
            return BitConverter.ToUInt64(IfReverse(bytes));
        }

        private static byte[] IfReverse(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }
    }
}