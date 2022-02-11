using System.IO;
using ProtoBuf;
using Xunit;

namespace Byndyusoft.AspNetCore.Mvc.Formatters.Models
{
    [ProtoContract]
    public class SimpleModel
    {
        [ProtoMember(2)] public string Field = default!;

        [ProtoMember(1)] public int Property { get; set; }

        [ProtoMember(3)] public SeekOrigin Enum { get; set; }

        [ProtoMember(4)] public int? Nullable { get; set; }

        [ProtoMember(5)] public int[] Array { get; set; } = default!;

        public static SimpleModel Create()
        {
            return new SimpleModel
            {
                Property = 10,
                Enum = SeekOrigin.Current,
                Field = "string",
                Array = new[] { 1, 2 },
                Nullable = 100
            };
        }

        public void Verify()
        {
            var input = Create();

            Assert.Equal(input.Property, Property);
            Assert.Equal(input.Field, Field);
            Assert.Equal(input.Enum, Enum);
            Assert.Equal(input.Array, Array);
            Assert.Equal(input.Nullable, Nullable);
        }
    }
}