#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
    public class DesParameters
        : KeyParameter
    {
        /*
        * DES Key Length in bytes.
        */
        public const int DesKeyLength = 8;

        /*
        * Table of weak and semi-weak keys taken from Schneier pp281
        */
        private const int N_DES_WEAK_KEYS = 16;

        private static readonly byte[] DES_weak_keys =
        {
            /* weak keys */
            0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01,
            0x1f, 0x1f, 0x1f, 0x1f, 0x0e, 0x0e, 0x0e, 0x0e,
            0xe0, 0xe0, 0xe0, 0xe0, 0xf1, 0xf1, 0xf1, 0xf1,
            0xfe, 0xfe, 0xfe, 0xfe, 0xfe, 0xfe, 0xfe, 0xfe,

            /* semi-weak keys */
            0x01, 0xfe, 0x01, 0xfe, 0x01, 0xfe, 0x01, 0xfe,
            0x1f, 0xe0, 0x1f, 0xe0, 0x0e, 0xf1, 0x0e, 0xf1,
            0x01, 0xe0, 0x01, 0xe0, 0x01, 0xf1, 0x01, 0xf1,
            0x1f, 0xfe, 0x1f, 0xfe, 0x0e, 0xfe, 0x0e, 0xfe,
            0x01, 0x1f, 0x01, 0x1f, 0x01, 0x0e, 0x01, 0x0e,
            0xe0, 0xfe, 0xe0, 0xfe, 0xf1, 0xfe, 0xf1, 0xfe,
            0xfe, 0x01, 0xfe, 0x01, 0xfe, 0x01, 0xfe, 0x01,
            0xe0, 0x1f, 0xe0, 0x1f, 0xf1, 0x0e, 0xf1, 0x0e,
            0xe0, 0x01, 0xe0, 0x01, 0xf1, 0x01, 0xf1, 0x01,
            0xfe, 0x1f, 0xfe, 0x1f, 0xfe, 0x0e, 0xfe, 0x0e,
            0x1f, 0x01, 0x1f, 0x01, 0x0e, 0x01, 0x0e, 0x01,
            0xfe, 0xe0, 0xfe, 0xe0, 0xfe, 0xf1, 0xfe, 0xf1
        };

        public DesParameters(
            byte[] key)
            : base(key)
        {
            if (IsWeakKey(key))
                throw new ArgumentException("attempt to create weak DES key");
        }

        public DesParameters(
            byte[] key,
            int keyOff,
            int keyLen)
            : base(key, keyOff, keyLen)
        {
            if (IsWeakKey(key, keyOff))
                throw new ArgumentException("attempt to create weak DES key");
        }

        /**
         * DES has 16 weak keys.  This method will check
         * if the given DES key material is weak or semi-weak.
         * Key material that is too short is regarded as weak.
         * <p>
         *     See
         *     <a href="http://www.counterpane.com/applied.html">
         *         "Applied
         *         Cryptography"
         *     </a>
         *     by Bruce Schneier for more information.
         * </p>
         * @return true if the given DES key material is weak or semi-weak,
         * false otherwise.
         */
        public static bool IsWeakKey(
            byte[] key,
            int offset)
        {
            if (key.Length - offset < DesKeyLength)
                throw new ArgumentException("key material too short.");

            //nextkey:
            for (var i = 0; i < N_DES_WEAK_KEYS; i++)
            {
                var unmatch = false;
                for (var j = 0; j < DesKeyLength; j++)
                    if (key[j + offset] != DES_weak_keys[i * DesKeyLength + j])
                    {
                        //continue nextkey;
                        unmatch = true;
                        break;
                    }

                if (!unmatch) return true;
            }

            return false;
        }

        public static bool IsWeakKey(
            byte[] key)
        {
            return IsWeakKey(key, 0);
        }

        public static byte SetOddParity(byte b)
        {
            var parity = b ^ 1U;
            parity ^= parity >> 4;
            parity ^= parity >> 2;
            parity ^= parity >> 1;
            parity &= 1U;

            return (byte)(b ^ parity);
        }

        /**
         * DES Keys use the LSB as the odd parity bit.  This can
         * be used to check for corrupt keys.
         * 
         * @param bytes the byte array to set the parity on.
         */
        public static void SetOddParity(byte[] bytes)
        {
            for (var i = 0; i < bytes.Length; i++) bytes[i] = SetOddParity(bytes[i]);
        }

        public static void SetOddParity(byte[] bytes, int off, int len)
        {
            for (var i = 0; i < len; i++) bytes[off + i] = SetOddParity(bytes[off + i]);
        }
    }
}

#endif