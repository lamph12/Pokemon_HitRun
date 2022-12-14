#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Macs
{
    /**
	* implementation of GOST 28147-89 MAC
	*/
    public class Gost28147Mac : IMac
    {
        private const int blockSize = 8;
        private const int macSize = 4;
        private byte[] buf;
        private int bufOff;
        private bool firstStep = true;
        private readonly byte[] mac;

        //
        // This is default S-box - E_A.
        private readonly byte[] S =
        {
            0x9, 0x6, 0x3, 0x2, 0x8, 0xB, 0x1, 0x7, 0xA, 0x4, 0xE, 0xF, 0xC, 0x0, 0xD, 0x5,
            0x3, 0x7, 0xE, 0x9, 0x8, 0xA, 0xF, 0x0, 0x5, 0x2, 0x6, 0xC, 0xB, 0x4, 0xD, 0x1,
            0xE, 0x4, 0x6, 0x2, 0xB, 0x3, 0xD, 0x8, 0xC, 0xF, 0x5, 0xA, 0x0, 0x7, 0x1, 0x9,
            0xE, 0x7, 0xA, 0xC, 0xD, 0x1, 0x3, 0x9, 0x0, 0x2, 0xB, 0x4, 0xF, 0x8, 0x5, 0x6,
            0xB, 0x5, 0x1, 0x9, 0x8, 0xD, 0xF, 0x0, 0xE, 0x4, 0x2, 0x3, 0xC, 0x7, 0xA, 0x6,
            0x3, 0xA, 0xD, 0xC, 0x1, 0x2, 0x0, 0xB, 0x7, 0x5, 0x9, 0x4, 0x8, 0xF, 0xE, 0x6,
            0x1, 0xD, 0x2, 0x9, 0x7, 0xA, 0x6, 0x0, 0x8, 0xC, 0x4, 0x5, 0xF, 0x3, 0xB, 0xE,
            0xB, 0xA, 0xF, 0x5, 0x0, 0xC, 0xE, 0x8, 0x6, 0x2, 0x3, 0x9, 0x1, 0x7, 0xD, 0x4
        };

        private int[] workingKey;

        public Gost28147Mac()
        {
            mac = new byte[blockSize];
            buf = new byte[blockSize];
            bufOff = 0;
        }

        public void Init(
            ICipherParameters parameters)
        {
            Reset();
            buf = new byte[blockSize];
            if (parameters is ParametersWithSBox)
            {
                var param = (ParametersWithSBox)parameters;

                //
                // Set the S-Box
                //
                param.GetSBox().CopyTo(S, 0);

                //
                // set key if there is one
                //
                if (param.Parameters != null)
                    workingKey = generateWorkingKey(((KeyParameter)param.Parameters).GetKey());
            }
            else if (parameters is KeyParameter)
            {
                workingKey = generateWorkingKey(((KeyParameter)parameters).GetKey());
            }
            else
            {
                throw new ArgumentException("invalid parameter passed to Gost28147 init - "
                                            + Platform.GetTypeName(parameters));
            }
        }

        public string AlgorithmName => "Gost28147Mac";

        public int GetMacSize()
        {
            return macSize;
        }

        public void Update(
            byte input)
        {
            if (bufOff == buf.Length)
            {
                var sumbuf = new byte[buf.Length];
                Array.Copy(buf, 0, sumbuf, 0, mac.Length);

                if (firstStep)
                    firstStep = false;
                else
                    sumbuf = CM5func(buf, 0, mac);

                gost28147MacFunc(workingKey, sumbuf, 0, mac, 0);
                bufOff = 0;
            }

            buf[bufOff++] = input;
        }

        public void BlockUpdate(
            byte[] input,
            int inOff,
            int len)
        {
            if (len < 0)
                throw new ArgumentException("Can't have a negative input length!");

            var gapLen = blockSize - bufOff;

            if (len > gapLen)
            {
                Array.Copy(input, inOff, buf, bufOff, gapLen);

                var sumbuf = new byte[buf.Length];
                Array.Copy(buf, 0, sumbuf, 0, mac.Length);

                if (firstStep)
                    firstStep = false;
                else
                    sumbuf = CM5func(buf, 0, mac);

                gost28147MacFunc(workingKey, sumbuf, 0, mac, 0);

                bufOff = 0;
                len -= gapLen;
                inOff += gapLen;

                while (len > blockSize)
                {
                    sumbuf = CM5func(input, inOff, mac);
                    gost28147MacFunc(workingKey, sumbuf, 0, mac, 0);

                    len -= blockSize;
                    inOff += blockSize;
                }
            }

            Array.Copy(input, inOff, buf, bufOff, len);

            bufOff += len;
        }

        public int DoFinal(
            byte[] output,
            int outOff)
        {
            //padding with zero
            while (bufOff < blockSize) buf[bufOff++] = 0;

            var sumbuf = new byte[buf.Length];
            Array.Copy(buf, 0, sumbuf, 0, mac.Length);

            if (firstStep)
                firstStep = false;
            else
                sumbuf = CM5func(buf, 0, mac);

            gost28147MacFunc(workingKey, sumbuf, 0, mac, 0);

            Array.Copy(mac, mac.Length / 2 - macSize, output, outOff, macSize);

            Reset();

            return macSize;
        }

        public void Reset()
        {
            // Clear the buffer.
            Array.Clear(buf, 0, buf.Length);
            bufOff = 0;

            firstStep = true;
        }

        private static int[] generateWorkingKey(
            byte[] userKey)
        {
            if (userKey.Length != 32)
                throw new ArgumentException("Key length invalid. Key needs to be 32 byte - 256 bit!!!");

            var key = new int[8];
            for (var i = 0; i != 8; i++) key[i] = bytesToint(userKey, i * 4);

            return key;
        }

        private int gost28147_mainStep(int n1, int key)
        {
            var cm = key + n1; // CM1

            // S-box replacing

            var om = S[0 + ((cm >> (0 * 4)) & 0xF)] << (0 * 4);
            om += S[16 + ((cm >> (1 * 4)) & 0xF)] << (1 * 4);
            om += S[32 + ((cm >> (2 * 4)) & 0xF)] << (2 * 4);
            om += S[48 + ((cm >> (3 * 4)) & 0xF)] << (3 * 4);
            om += S[64 + ((cm >> (4 * 4)) & 0xF)] << (4 * 4);
            om += S[80 + ((cm >> (5 * 4)) & 0xF)] << (5 * 4);
            om += S[96 + ((cm >> (6 * 4)) & 0xF)] << (6 * 4);
            om += S[112 + ((cm >> (7 * 4)) & 0xF)] << (7 * 4);

//			return om << 11 | om >>> (32-11); // 11-leftshift
            var omLeft = om << 11;
            var omRight = (int)((uint)om >> (32 - 11)); // Note: Casts required to get unsigned bit rotation

            return omLeft | omRight;
        }

        private void gost28147MacFunc(
            int[] workingKey,
            byte[] input,
            int inOff,
            byte[] output,
            int outOff)
        {
            int N1, N2, tmp; //tmp -> for saving N1
            N1 = bytesToint(input, inOff);
            N2 = bytesToint(input, inOff + 4);

            for (var k = 0; k < 2; k++) // 1-16 steps
            for (var j = 0; j < 8; j++)
            {
                tmp = N1;
                N1 = N2 ^ gost28147_mainStep(N1, workingKey[j]); // CM2
                N2 = tmp;
            }

            intTobytes(N1, output, outOff);
            intTobytes(N2, output, outOff + 4);
        }

        //array of bytes to type int
        private static int bytesToint(
            byte[] input,
            int inOff)
        {
            return (int)((input[inOff + 3] << 24) & 0xff000000) + ((input[inOff + 2] << 16) & 0xff0000)
                                                                + ((input[inOff + 1] << 8) & 0xff00) +
                                                                (input[inOff] & 0xff);
        }

        //int to array of bytes
        private static void intTobytes(
            int num,
            byte[] output,
            int outOff)
        {
            output[outOff + 3] = (byte)(num >> 24);
            output[outOff + 2] = (byte)(num >> 16);
            output[outOff + 1] = (byte)(num >> 8);
            output[outOff] = (byte)num;
        }

        private static byte[] CM5func(
            byte[] buf,
            int bufOff,
            byte[] mac)
        {
            var sum = new byte[buf.Length - bufOff];

            Array.Copy(buf, bufOff, sum, 0, mac.Length);

            for (var i = 0; i != mac.Length; i++) sum[i] = (byte)(sum[i] ^ mac[i]);

            return sum;
        }
    }
}

#endif