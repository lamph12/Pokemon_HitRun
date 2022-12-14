#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
namespace Org.BouncyCastle.Asn1
{
    /**
     * class for breaking up an Oid into it's component tokens, ala
     * java.util.StringTokenizer. We need this class as some of the
     * lightweight Java environment don't support classes like
     * StringTokenizer.
     */
    public class OidTokenizer
    {
        private int index;
        private readonly string oid;

        public OidTokenizer(
            string oid)
        {
            this.oid = oid;
        }

        public bool HasMoreTokens => index != -1;

        public string NextToken()
        {
            if (index == -1) return null;

            var end = oid.IndexOf('.', index);
            if (end == -1)
            {
                var lastToken = oid.Substring(index);
                index = -1;
                return lastToken;
            }

            var nextToken = oid.Substring(index, end - index);
            index = end + 1;
            return nextToken;
        }
    }
}

#endif