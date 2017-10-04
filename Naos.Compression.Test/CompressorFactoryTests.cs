// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompressorFactoryTests.cs" company="Naos">
//    Copyright (c) Naos 2017. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Compression.Test
{
    using System;

    using FluentAssertions;

    using Naos.Compression.Domain;

    using Xunit;

    public static class CompressorFactoryTests
    {
        [Fact]
        public static void BuildCompressor___Invalid_kind___Throws()
        {
            // Arrange
            var kind = CompressionKind.Invalid;
            Action action = () => CompressorFactory.BuildCompressor(kind);

            // Act
            var exception = Record.Exception(action);

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().Be("Value must not be equal to Invalid.\r\nParameter name: compressionKind");
        }

        [Fact]
        public static void BuildCompressor___None_kind___NullCompressor()
        {
            // Arrange
            var kind = CompressionKind.None;
            var expectedType = typeof(NullCompressor);

            // Act
            var actual = CompressorFactory.BuildCompressor(kind);

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeOfType(expectedType);
            actual.Kind.Should().Be(kind);
        }

        [Fact]
        public static void BuildCompressor___DotNetZip_kind___DotNetZipCompressor()
        {
            // Arrange
            var kind = CompressionKind.DotNetZip;
            var expectedType = typeof(DotNetZipCompressor);

            // Act
            var actual = CompressorFactory.BuildCompressor(kind);

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeOfType(expectedType);
            actual.Kind.Should().Be(kind);
        }
    }
}
