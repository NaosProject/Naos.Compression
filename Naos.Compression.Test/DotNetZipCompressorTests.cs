// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DotNetZipCompressorTests.cs" company="Naos">
//    Copyright (c) Naos 2017. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Compression.Test
{
    using System;
    using System.Text;

    using FakeItEasy;

    using FluentAssertions;

    using Naos.Compression.Domain;

    using Xunit;

    public static class DotNetZipCompressorTests
    {
        [Fact]
        public static void CompressBytes___With_null_bytes___Throws()
        {
            // Arrange
            ICompressAndDecompress compressor = new DotNetZipCompressor();
            Action action = () => compressor.CompressBytes(null);

            // Act
            var exception = Record.Exception(action);

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<ArgumentNullException>();
            exception.Message.Should().Be("Parameter 'uncompressedBytes' is null.");
        }

        [Fact]
        public static void DecompressBytes___With_null_bytes___Throws()
        {
            // Arrange
            ICompressAndDecompress compressor = new DotNetZipCompressor();
            Action action = () => compressor.DecompressBytes(null);

            // Act
            var exception = Record.Exception(action);

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<ArgumentNullException>();
            exception.Message.Should().Be("Parameter 'compressedBytes' is null.");
        }

        [Fact]
        public static void RoundtripCompressDecompress___With_zero_bytes___Works()
        {
            // Arrange
            var expected = new byte[0];

            // Act
            var compressed = DotNetZipCompressor.CompressBytes(expected);
            var actual = DotNetZipCompressor.DecompressBytes(compressed);

            // Assert
            actual.Should().Equal(expected);
        }

        [Fact]
        public static void RoundtripCompressDecompress___With_some_bytes___Works()
        {
            // Arrange
            var expected = Encoding.UTF32.GetBytes(A.Dummy<string>());

            // Act
            var compressed = DotNetZipCompressor.CompressBytes(expected);
            var actual = DotNetZipCompressor.DecompressBytes(compressed);

            // Assert
            actual.Should().Equal(expected);
        }
    }
}
