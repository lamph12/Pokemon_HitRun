#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
    /**
     * RFC 3546 3.6
     */
    public class OcspStatusRequest
    {
        protected readonly X509Extensions mRequestExtensions;
        protected readonly IList mResponderIDList;

        /**
         * @param responderIDList
         * an {@link IList} of {@link ResponderID}, specifying the list of trusted OCSP
         * responders. An empty list has the special meaning that the responders are
         * implicitly known to the server - e.g., by prior arrangement.
         * @param requestExtensions
         * OCSP request extensions. A null value means that there are no extensions.
         */
        public OcspStatusRequest(IList responderIDList, X509Extensions requestExtensions)
        {
            mResponderIDList = responderIDList;
            mRequestExtensions = requestExtensions;
        }

        /**
         * @return an {@link IList} of {@link ResponderID}
         */
        public virtual IList ResponderIDList => mResponderIDList;

        /**
         * @return OCSP request extensions
         */
        public virtual X509Extensions RequestExtensions => mRequestExtensions;

        /**
         * Encode this {@link OcspStatusRequest} to a {@link Stream}.
         * 
         * @param output
         * the {@link Stream} to encode to.
         * @throws IOException
         */
        public virtual void Encode(Stream output)
        {
            if (mResponderIDList == null || mResponderIDList.Count < 1)
            {
                TlsUtilities.WriteUint16(0, output);
            }
            else
            {
                var buf = new MemoryStream();
                for (var i = 0; i < mResponderIDList.Count; ++i)
                {
                    var responderID = (ResponderID)mResponderIDList[i];
                    var derEncoding = responderID.GetEncoded(Asn1Encodable.Der);
                    TlsUtilities.WriteOpaque16(derEncoding, buf);
                }

                TlsUtilities.CheckUint16(buf.Length);
                TlsUtilities.WriteUint16((int)buf.Length, output);
                buf.WriteTo(output);
            }

            if (mRequestExtensions == null)
            {
                TlsUtilities.WriteUint16(0, output);
            }
            else
            {
                var derEncoding = mRequestExtensions.GetEncoded(Asn1Encodable.Der);
                TlsUtilities.CheckUint16(derEncoding.Length);
                TlsUtilities.WriteUint16(derEncoding.Length, output);
                output.Write(derEncoding, 0, derEncoding.Length);
            }
        }

        /**
         * Parse a {@link OcspStatusRequest} from a {@link Stream}.
         * 
         * @param input
         * the {@link Stream} to parse from.
         * @return an {@link OcspStatusRequest} object.
         * @throws IOException
         */
        public static OcspStatusRequest Parse(Stream input)
        {
            var responderIDList = Platform.CreateArrayList();
            {
                var length = TlsUtilities.ReadUint16(input);
                if (length > 0)
                {
                    var data = TlsUtilities.ReadFully(length, input);
                    var buf = new MemoryStream(data, false);
                    do
                    {
                        var derEncoding = TlsUtilities.ReadOpaque16(buf);
                        var responderID = ResponderID.GetInstance(TlsUtilities.ReadDerObject(derEncoding));
                        responderIDList.Add(responderID);
                    } while (buf.Position < buf.Length);
                }
            }

            X509Extensions requestExtensions = null;
            {
                var length = TlsUtilities.ReadUint16(input);
                if (length > 0)
                {
                    var derEncoding = TlsUtilities.ReadFully(length, input);
                    requestExtensions = X509Extensions.GetInstance(TlsUtilities.ReadDerObject(derEncoding));
                }
            }

            return new OcspStatusRequest(responderIDList, requestExtensions);
        }
    }
}

#endif