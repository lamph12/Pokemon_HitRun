#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
    public class HeartbeatExtension
    {
        protected readonly byte mMode;

        public HeartbeatExtension(byte mode)
        {
            if (!HeartbeatMode.IsValid(mode))
                throw new ArgumentException("not a valid HeartbeatMode value", "mode");

            mMode = mode;
        }

        public virtual byte Mode => mMode;

        /**
         * Encode this {@link HeartbeatExtension} to a {@link Stream}.
         * 
         * @param output
         * the {@link Stream} to encode to.
         * @throws IOException
         */
        public virtual void Encode(Stream output)
        {
            TlsUtilities.WriteUint8(mMode, output);
        }

        /**
         * Parse a {@link HeartbeatExtension} from a {@link Stream}.
         * 
         * @param input
         * the {@link Stream} to parse from.
         * @return a {@link HeartbeatExtension} object.
         * @throws IOException
         */
        public static HeartbeatExtension Parse(Stream input)
        {
            var mode = TlsUtilities.ReadUint8(input);
            if (!HeartbeatMode.IsValid(mode))
                throw new TlsFatalAlert(AlertDescription.illegal_parameter);

            return new HeartbeatExtension(mode);
        }
    }
}

#endif