#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Crypto.Parameters
{
    public class ECPublicKeyParameters
        : ECKeyParameters
    {
        public ECPublicKeyParameters(
            ECPoint q,
            ECDomainParameters parameters)
            : this("EC", q, parameters)
        {
        }

        [Obsolete("Use version with explicit 'algorithm' parameter")]
        public ECPublicKeyParameters(
            ECPoint q,
            DerObjectIdentifier publicKeyParamSet)
            : base("ECGOST3410", false, publicKeyParamSet)
        {
            if (q == null)
                throw new ArgumentNullException("q");

            this.Q = q.Normalize();
        }

        public ECPublicKeyParameters(
            string algorithm,
            ECPoint q,
            ECDomainParameters parameters)
            : base(algorithm, false, parameters)
        {
            if (q == null)
                throw new ArgumentNullException("q");

            this.Q = q.Normalize();
        }

        public ECPublicKeyParameters(
            string algorithm,
            ECPoint q,
            DerObjectIdentifier publicKeyParamSet)
            : base(algorithm, false, publicKeyParamSet)
        {
            if (q == null)
                throw new ArgumentNullException("q");

            this.Q = q.Normalize();
        }

        public ECPoint Q { get; }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;

            var other = obj as ECPublicKeyParameters;

            if (other == null)
                return false;

            return Equals(other);
        }

        protected bool Equals(
            ECPublicKeyParameters other)
        {
            return Q.Equals(other.Q) && base.Equals(other);
        }

        public override int GetHashCode()
        {
            return Q.GetHashCode() ^ base.GetHashCode();
        }
    }
}

#endif