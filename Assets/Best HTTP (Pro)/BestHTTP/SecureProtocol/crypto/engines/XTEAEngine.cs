#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
    /**
	* An XTEA engine.
	*/
    public class XteaEngine
        : IBlockCipher
    {
        private const int
            rounds = 32,
            block_size = 8,
//			key_size	= 16,
            delta = unchecked((int)0x9E3779B9);

        private bool _initialised, _forEncryption;

        /*
        * the expanded key array of 4 subkeys
        */
        private readonly uint[] _S = new uint[4];

        private readonly uint[] _sum0 = new uint[32];

        private readonly uint[] _sum1 = new uint[32];

        /**
         * Create an instance of the TEA encryption algorithm
         * and set some defaults
         */
        public XteaEngine()
        {
            _initialised = false;
        }

        public virtual string AlgorithmName => "XTEA";

        public virtual bool IsPartialBlockOkay => false;

        public virtual int GetBlockSize()
        {
            return block_size;
        }

        /**
         * initialise
         * 
         * @param forEncryption whether or not we are for encryption.
         * @param params the parameters required to set up the cipher.
         * @exception ArgumentException if the params argument is
         * inappropriate.
         */
        public virtual void Init(
            bool forEncryption,
            ICipherParameters parameters)
        {
            if (!(parameters is KeyParameter))
                throw new ArgumentException("invalid parameter passed to TEA init - "
                                            + Platform.GetTypeName(parameters));

            _forEncryption = forEncryption;
            _initialised = true;

            var p = (KeyParameter)parameters;

            setKey(p.GetKey());
        }

        public virtual int ProcessBlock(
            byte[] inBytes,
            int inOff,
            byte[] outBytes,
            int outOff)
        {
            if (!_initialised)
                throw new InvalidOperationException(AlgorithmName + " not initialised");

            Check.DataLength(inBytes, inOff, block_size, "input buffer too short");
            Check.OutputLength(outBytes, outOff, block_size, "output buffer too short");

            return _forEncryption
                ? encryptBlock(inBytes, inOff, outBytes, outOff)
                : decryptBlock(inBytes, inOff, outBytes, outOff);
        }

        public virtual void Reset()
        {
        }

        /**
         * Re-key the cipher.
         * 
         * @param  key  the key to be used
         */
        private void setKey(
            byte[] key)
        {
            int i, j;
            for (i = j = 0; i < 4; i++, j += 4) _S[i] = Pack.BE_To_UInt32(key, j);

            for (i = j = 0; i < rounds; i++)
            {
                _sum0[i] = (uint)j + _S[j & 3];
                j += delta;
                _sum1[i] = (uint)j + _S[(j >> 11) & 3];
            }
        }

        private int encryptBlock(
            byte[] inBytes,
            int inOff,
            byte[] outBytes,
            int outOff)
        {
            // Pack bytes into integers
            var v0 = Pack.BE_To_UInt32(inBytes, inOff);
            var v1 = Pack.BE_To_UInt32(inBytes, inOff + 4);

            for (var i = 0; i < rounds; i++)
            {
                v0 += (((v1 << 4) ^ (v1 >> 5)) + v1) ^ _sum0[i];
                v1 += (((v0 << 4) ^ (v0 >> 5)) + v0) ^ _sum1[i];
            }

            Pack.UInt32_To_BE(v0, outBytes, outOff);
            Pack.UInt32_To_BE(v1, outBytes, outOff + 4);

            return block_size;
        }

        private int decryptBlock(
            byte[] inBytes,
            int inOff,
            byte[] outBytes,
            int outOff)
        {
            // Pack bytes into integers
            var v0 = Pack.BE_To_UInt32(inBytes, inOff);
            var v1 = Pack.BE_To_UInt32(inBytes, inOff + 4);

            for (var i = rounds - 1; i >= 0; i--)
            {
                v1 -= (((v0 << 4) ^ (v0 >> 5)) + v0) ^ _sum1[i];
                v0 -= (((v1 << 4) ^ (v1 >> 5)) + v1) ^ _sum0[i];
            }

            Pack.UInt32_To_BE(v0, outBytes, outOff);
            Pack.UInt32_To_BE(v1, outBytes, outOff + 4);

            return block_size;
        }
    }
}

#endif