#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
    public class RandomDsaKCalculator
        : IDsaKCalculator
    {
        private BigInteger q;
        private SecureRandom random;

        public virtual bool IsDeterministic => false;

        public virtual void Init(BigInteger n, SecureRandom random)
        {
            q = n;
            this.random = random;
        }

        public virtual void Init(BigInteger n, BigInteger d, byte[] message)
        {
            throw new InvalidOperationException("Operation not supported");
        }

        public virtual BigInteger NextK()
        {
            var qBitLength = q.BitLength;

            BigInteger k;
            do
            {
                k = new BigInteger(qBitLength, random);
            } while (k.SignValue < 1 || k.CompareTo(q) >= 0);

            return k;
        }
    }
}

#endif