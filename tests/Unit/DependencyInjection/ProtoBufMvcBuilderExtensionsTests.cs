using System;
using System.Net.Http.ProtoBuf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Byndyusoft.AspNetCore.Mvc.Formatters.Unit.DependencyInjection
{
    public class ProtoBufMvcBuilderExtensionsTests
    {
        private readonly IMvcBuilder _mvcBuilder;
        private readonly IMvcCoreBuilder _mvcCoreBuilder;
        private readonly IServiceCollection _serviceCollection;

        public ProtoBufMvcBuilderExtensionsTests()
        {
            _serviceCollection = new ServiceCollection().AddLogging();
            _mvcBuilder = _serviceCollection.AddMvc();
            _mvcCoreBuilder = _serviceCollection.AddMvcCore();
        }

        [Fact]
        public void AddProtoBufFormatters_IMvcBuilder_NullBuilder_ThrowsException()
        {
            // Arrange
            var builder = null as IMvcBuilder;

            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            var exception = Assert.Throws<ArgumentNullException>(() => builder.AddProtoBufFormatters());

            // Assert
            Assert.Equal("builder", exception.ParamName);
        }

        [Fact]
        public void AddProtoBufFormatters_IMvcBuilder()
        {
            // Act
            _mvcBuilder.AddProtoBufFormatters();

            // Assert
            var mvcOptions = _serviceCollection.BuildServiceProvider().GetService<IOptions<MvcOptions>>().Value;
            Assert.Single(mvcOptions.InputFormatters, x => x.GetType() == typeof(ProtoBufInputFormatter));
            Assert.Single(mvcOptions.OutputFormatters, x => x.GetType() == typeof(ProtoBufOutputFormatter));

            var mapping =
                mvcOptions.FormatterMappings.GetMediaTypeMappingForFormat(ProtoBufDefaults.MediaTypeFormat);
            Assert.NotNull(mapping);
            Assert.Equal(ProtoBufDefaults.MediaType, mapping);
        }

        [Fact]
        public void AddProtoBufFormatters_IMvcBuilder_Options()
        {
            // Act
            _mvcBuilder.AddProtoBufFormatters(msgpack =>
            {
                msgpack.SupportedMediaTypes.Clear();
                msgpack.SupportedMediaTypes.Add("application/mediatype");
                msgpack.MediaTypeFormat = "format";
            });
            var mvcOptions = _serviceCollection.BuildServiceProvider().GetService<IOptions<MvcOptions>>().Value;

            // Assert
            var inputFormatter = (ProtoBufInputFormatter) Assert.Single(mvcOptions.InputFormatters,
                x => x.GetType() == typeof(ProtoBufInputFormatter));
            Assert.Equal(inputFormatter!.SupportedMediaTypes, new[] {"application/mediatype"});

            var outputFormatter = (ProtoBufOutputFormatter) Assert.Single(mvcOptions.OutputFormatters,
                x => x.GetType() == typeof(ProtoBufOutputFormatter));
            Assert.Equal(outputFormatter!.SupportedMediaTypes, new[] {"application/mediatype"});

            var mapping = mvcOptions.FormatterMappings.GetMediaTypeMappingForFormat("format");
            Assert.NotNull(mapping);
            Assert.Equal("application/mediatype", mapping);
        }

        [Fact]
        public void AddProtoBufFormatters_IMvcBuilder_Options_NullBuilder_ThrowsException()
        {
            // Arrange
            var builder = null as IMvcBuilder;

            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            var exception =
                Assert.Throws<ArgumentNullException>(() => builder.AddProtoBufFormatters(options => { }));

            // Assert
            Assert.Equal("builder", exception.ParamName);
        }

        [Fact]
        public void AddProtoBufFormatters_IMvcBuilder_Options_NullOptions_ThrowsException()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => _mvcBuilder.AddProtoBufFormatters(null));

            // Assert
            Assert.Equal("setupAction", exception.ParamName);
        }

        [Fact]
        public void AddProtoBufFormatters_IMvcCoreBuilder_NullBuilder_ThrowsException()
        {
            // Arrange
            var builder = null as IMvcCoreBuilder;

            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            var exception = Assert.Throws<ArgumentNullException>(() => builder.AddProtoBufFormatters());

            // Assert
            Assert.Equal("builder", exception.ParamName);
        }

        [Fact]
        public void AddProtoBufFormatters_IMvcCoreBuilder()
        {
            // Act
            _mvcCoreBuilder.AddProtoBufFormatters();

            // Assert
            var mvcOptions = _serviceCollection.BuildServiceProvider().GetService<IOptions<MvcOptions>>().Value;
            Assert.Single(mvcOptions.InputFormatters, x => x.GetType() == typeof(ProtoBufInputFormatter));
            Assert.Single(mvcOptions.OutputFormatters, x => x.GetType() == typeof(ProtoBufOutputFormatter));

            var mapping =
                mvcOptions.FormatterMappings.GetMediaTypeMappingForFormat(ProtoBufDefaults.MediaTypeFormat);
            Assert.NotNull(mapping);
            Assert.Equal(ProtoBufDefaults.MediaType, mapping);
        }

        [Fact]
        public void AddProtoBufFormatters_IMvcCoreBuilder_Options()
        {
            // Act
            _mvcCoreBuilder.AddProtoBufFormatters(msgpack =>
            {
                msgpack.SupportedMediaTypes.Clear();
                msgpack.SupportedMediaTypes.Add("application/mediatype");
                msgpack.MediaTypeFormat = "format";
            });
            var mvcOptions = _serviceCollection.BuildServiceProvider().GetService<IOptions<MvcOptions>>().Value;

            // Assert
            var inputFormatter = (ProtoBufInputFormatter) Assert.Single(mvcOptions.InputFormatters,
                x => x.GetType() == typeof(ProtoBufInputFormatter));
            Assert.Equal(inputFormatter!.SupportedMediaTypes, new[] {"application/mediatype"});

            var outputFormatter = (ProtoBufOutputFormatter) Assert.Single(mvcOptions.OutputFormatters,
                x => x.GetType() == typeof(ProtoBufOutputFormatter));
            Assert.Equal(outputFormatter!.SupportedMediaTypes, new[] {"application/mediatype"});

            var mapping = mvcOptions.FormatterMappings.GetMediaTypeMappingForFormat("format");
            Assert.NotNull(mapping);
            Assert.Equal("application/mediatype", mapping);
        }

        [Fact]
        public void AddProtoBufFormatters_IMvcCoreBuilder_Options_NullBuilder_ThrowsException()
        {
            // Arrange
            var builder = null as IMvcCoreBuilder;

            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            var exception =
                Assert.Throws<ArgumentNullException>(() => builder.AddProtoBufFormatters(options => { }));

            // Assert
            Assert.Equal("builder", exception.ParamName);
        }

        [Fact]
        public void AddProtoBufFormatters_IMvcCoreBuilder_Options_NullOptions_ThrowsException()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => _mvcCoreBuilder.AddProtoBufFormatters(null));

            // Assert
            Assert.Equal("setupAction", exception.ParamName);
        }
    }
}