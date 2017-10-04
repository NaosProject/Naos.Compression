// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DotNetZipCompressor.cs" company="Naos">
//    Copyright (c) Naos 2017. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Compression.Domain
{
    using System;
    using System.IO;
    using System.IO.Compression;

    using Spritely.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Build in dot net implementation of <see cref="ICompressAndDecompress"/>.  Implementation from: <a href="https://stackoverflow.com/questions/40909052/using-gzip-to-compress-decompress-an-array-of-bytes" />
    /// </summary>
    public class DotNetZipCompressor : ICompressAndDecompress
    {
        /// <inheritdoc cref="ICompressAndDecompress"/>
        public CompressionKind Kind => CompressionKind.DotNetZip;

        /// <inheritdoc cref="ICompressAndDecompress"/>
        byte[] ICompress.CompressBytes(byte[] uncompressedBytes)
        {
            new { uncompressedBytes }.Must().NotBeNull().OrThrow();

            return CompressBytes(uncompressedBytes);
        }

        /// <summary>
        /// Compresses the provided byte array.
        /// </summary>
        /// <param name="uncompressedBytes">Byte array to compress.</param>
        /// <returns>Compressed version of the supplied byte array.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "I like it this way.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "I like it this way.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:Identifiers should not contain type names", Justification = "I like this name...")]
        public static byte[] CompressBytes(byte[] uncompressedBytes)
        {
            new { uncompressedBytes }.Must().NotBeNull().OrThrow();

            byte[] compressedBytes = null;
            using (var compressedStream = new MemoryStream())
            {
                using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
                {
                    zipStream.Write(uncompressedBytes, 0, uncompressedBytes.Length);
                    zipStream.Close();
                    compressedBytes = compressedStream.ToArray();
                }
            }

            return compressedBytes;
        }

        /// <inheritdoc cref="ICompressAndDecompress"/>
        byte[] IDecompress.DecompressBytes(byte[] compressedBytes)
        {
            new { compressedBytes }.Must().NotBeNull().OrThrow();

            return DecompressBytes(compressedBytes);
        }

        /// <summary>
        /// Decompresses the provided byte array.
        /// </summary>
        /// <param name="compressedBytes">Byte array to decompress.</param>
        /// <returns>Decompressed version of the supplied byte array.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "I like it this way.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "I like it this way.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:Identifiers should not contain type names", Justification = "I like this name...")]
        public static byte[] DecompressBytes(byte[] compressedBytes)
        {
            new { compressedBytes }.Must().NotBeNull().OrThrow();

            byte[] decompressedBytes = null;

            using (var compressedStream = new MemoryStream(compressedBytes))
            {
                using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                {
                    using (var resultStream = new MemoryStream())
                    {
                        zipStream.CopyTo(resultStream);
                        decompressedBytes = resultStream.ToArray();
                    }
                }
            }

            return decompressedBytes;
        }
    }
}