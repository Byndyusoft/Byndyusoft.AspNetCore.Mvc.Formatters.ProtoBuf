using System;
using System.IO;
using System.Net.Http;
using ProtoBuf.Meta;

namespace Byndyusoft.AspNetCore.Mvc.Formatters
{
    public class StreamProtoBufHttpContent : StreamContent
    {
        public StreamProtoBufHttpContent() : this(new MemoryStream())
        {
        }

        private StreamProtoBufHttpContent(MemoryStream stream) : base(stream)
        {
            Stream = stream;
        }

        public MemoryStream Stream { get; }

        public void WriteObject<T>(T value, TypeModel typeModel)
        {
            if (value != null) typeModel.Serialize(Stream, value);
            Stream.Position = 0;
        }

        public void WriteObject(object value, TypeModel typeModel)
        {
            if (value != null) typeModel.Serialize(Stream, value);
            Stream.Position = 0;
        }

        public T ReadObject<T>(TypeModel typeModel)
        {
            if (Stream.Length == 0)
                return default!;

            Stream.Position = 0;
            return typeModel.Deserialize<T>(Stream);
        }
    }
}