#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X509
{
	/**
     * The CRLNumber object.
     * <pre>
     *     CRLNumber::= Integer(0..MAX)
     * </pre>
     */
	public class CrlNumber
        : DerInteger
    {
        public CrlNumber(
            BigInteger number)
            : base(number)
        {
        }

        public BigInteger Number => PositiveValue;

        public override string ToString()
        {
            return "CRLNumber: " + Number;
        }
    }
}

#endif