using System.Net.Http;
using System.Net.Http.Json;
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
        private static readonly TypeModel TypeModel = RuntimeTypeModel.Default;

        protected override void ConfigureHttpClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(ProtoBufDefaults.MediaTypeHeader);
        }

        protected override void ConfigureMvc(IMvcCoreBuilder builder)
        {
            builder.AddProtoBufFormatters(options => { options.TypeModel = TypeModel; });
        }

        [Fact]
        public async Task NullObject()
        {
            // Act
            var response =
                await Client.PostAsProtoBufAsync<SimpleModel>("/protobuf-formatter/echo", null!, TypeModel);

            // Asert
            response.EnsureSuccessStatusCode();
            var model = await response.Content.ReadFromProtoBufAsync<SimpleModel>(TypeModel);
            Assert.Null(model);
        }

        [Fact]
        public async Task PrimitiveType()
        {
            // Act
            var response = await Client.PostAsProtoBufAsync("/protobuf-formatter/echo", 10, TypeModel);

            // Asert
            response.EnsureSuccessStatusCode();
            var model = await response.Content.ReadFromProtoBufAsync<int>(TypeModel);
            Assert.Equal(10, model);
        }

        [Fact]
        public async Task SimpleType()
        {
            // Arrange
            var simpleType = SimpleModel.Create();

            // Act
            var response =
                await Client.PostAsProtoBufAsync("/protobuf-formatter/echo", simpleType, TypeModel);

            // Asert
            response.EnsureSuccessStatusCode();
            var model = await response.Content.ReadFromProtoBufAsync<SimpleModel>(TypeModel);
            Assert.NotNull(model);
            model.Verify();
        }

        [Fact]
        public async Task MediaTypeFormat()
        {
            // Arrange
            var simpleType = SimpleModel.Create();
            var content = ProtoBufContent.Create(simpleType, TypeModel);

            // Act
            Client.DefaultRequestHeaders.Accept.Clear();
            var response = await Client.PostAsync("/protobuf-formatter/echo?format=protobuf", content).ConfigureAwait(true);

            // Asert
            response.EnsureSuccessStatusCode();
            var model = await response.Content.ReadFromProtoBufAsync<SimpleModel>(TypeModel);
            Assert.NotNull(model);
            model.Verify();
        }
    }
}