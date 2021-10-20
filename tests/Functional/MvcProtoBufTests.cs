using System.Net.Http;
using System.Net.Http.ProtoBuf;
using System.Threading.Tasks;
using Byndyusoft.AspNetCore.Mvc.Formatters.Models;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Meta;
using Xunit;

namespace Byndyusoft.AspNetCore.Mvc.Formatters.Functional
{
    public class MvcProtoBufTests : MvcTestFixture
    {
        private readonly TypeModel _typeModel;

        public MvcProtoBufTests()
        {
            _typeModel = RuntimeTypeModel.Default;
        }

        protected override void ConfigureHttpClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(ProtoBufDefaults.MediaTypeHeader);
        }

        protected override void ConfigureMvc(IMvcCoreBuilder builder)
        {
            builder.AddProtoBufFormatters(options => { options.TypeModel = _typeModel; });
        }

        [Fact]
        public async Task NullObject()
        {
            // Act
            var response =
                await Client.PostAsProtoBufAsync<SimpleModel>("/protobuf-formatter/echo", null, _typeModel);

            // Asert
            response.EnsureSuccessStatusCode();
            var model = await response.Content.ReadFromProtoBufAsync<SimpleModel>(_typeModel);
            Assert.Null(model);
        }

        [Fact]
        public async Task PrimitiveType()
        {
            // Act
            var response = await Client.PostAsProtoBufAsync("/protobuf-formatter/echo", 10, _typeModel);

            // Asert
            response.EnsureSuccessStatusCode();
            var model = await response.Content.ReadFromProtoBufAsync<int>(_typeModel);
            Assert.Equal(10, model);
        }

        [Fact]
        public async Task SimpleType()
        {
            // Arrange
            var simpleType = SimpleModel.Create();

            // Act
            var response =
                await Client.PostAsProtoBufAsync("/protobuf-formatter/echo", simpleType, _typeModel);

            // Asert
            response.EnsureSuccessStatusCode();
            var model = await response.Content.ReadFromProtoBufAsync<SimpleModel>(_typeModel);
            Assert.NotNull(model);
            model.Verify();
        }

        [Fact]
        public async Task MediaTypeFormat()
        {
            // Arrange
            var simpleType = SimpleModel.Create();
            var content = ProtoBufContent.Create(simpleType, _typeModel);

            // Act
            Client.DefaultRequestHeaders.Accept.Clear();
            var response = await Client.PostAsync("/protobuf-formatter/echo?format=protobuf", content);

            // Asert
            response.EnsureSuccessStatusCode();
            var model = await response.Content.ReadFromProtoBufAsync<SimpleModel>(_typeModel);
            Assert.NotNull(model);
            model.Verify();
        }
    }
}