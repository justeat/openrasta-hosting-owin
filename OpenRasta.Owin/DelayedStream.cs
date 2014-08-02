using System.IO;
using System.Linq;
using OpenRasta.Collections;

namespace OpenRasta.Owin
{
    public class DelayedStream : Stream
    {
        private readonly Stream _baseStream;
        private readonly MemoryStream _delayedStream;
        private byte[] _bytes;

        public DelayedStream(Stream baseStream)
        {
            _baseStream = baseStream;
            _delayedStream = new MemoryStream();
        }

        public override bool CanRead
        {
            get { return _delayedStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _delayedStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _delayedStream.CanWrite; }
        }

        public override long Length
        {
            get { return _delayedStream.Length; }
        }

        public override long Position
        {
            get { return _delayedStream.Position; }
            set { _delayedStream.Position = value; }
        }

        public override void Flush()
        {
            _baseStream.Write(_bytes, 0, _bytes.Count());
            _baseStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _delayedStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _delayedStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _delayedStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_bytes == null)
            {
                _bytes = buffer;
            }
            else
            {
                _bytes.AddRange(buffer);
            }
            _delayedStream.Write(buffer, offset, count);
        }
    }
}