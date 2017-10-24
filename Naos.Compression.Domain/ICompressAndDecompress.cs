// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICompressAndDecompress.cs" company="Naos">
//    Copyright (c) Naos 2017. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Compression.Domain
{
    /// <summary>
    /// Interface to work with compression.
    /// </summary>
    public interface ICompressAndDecompress : ICompress, IDecompress
    {
    }

    /// <summary>
    /// Interface to expose the <see cref="CompressionKind" />.
    /// </summary>
    public interface IHaveCompressionKind
    {
        /// <summary>
        /// Gets the kind of compression supported.
        /// </summary>
        CompressionKind CompressionKind { get; }
    }

    /// <summary>
    /// Interface to compress.
    /// </summary>
    public interface ICompress : IHaveCompressionKind
    {
        /// <summary>
        /// Compresses the provided byte array.
        /// </summary>
        /// <param name="uncompressedBytes">Byte array to compress.</param>
        /// <returns>Compressed version of the supplied byte array.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:Identifiers should not contain type names", Justification = "I like this name...")]
        byte[] CompressBytes(byte[] uncompressedBytes);
    }

    /// <summary>
    /// Interface to decompress.
    /// </summary>
    public interface IDecompress : IHaveCompressionKind
    {
        /// <summary>
        /// Decompresses the provided byte array.
        /// </summary>
        /// <param name="compressedBytes">Byte array to decompress.</param>
        /// <returns>Decompressed version of the supplied byte array.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:Identifiers should not contain type names", Justification = "I like this name...")]
        byte[] DecompressBytes(byte[] compressedBytes);
    }

    /// <summary>
    /// Null implementation of <see cref="ICompressAndDecompress"/>.
    /// </summary>
    public class NullCompressor : ICompressAndDecompress
    {
        /// <inheritdoc cref="ICompressAndDecompress"/>
        public CompressionKind CompressionKind => CompressionKind.None;

        /// <inheritdoc cref="ICompressAndDecompress"/>
        public byte[] CompressBytes(byte[] uncompressedBytes)
        {
            return uncompressedBytes;
        }

        /// <inheritdoc cref="ICompressAndDecompress"/>
        public byte[] DecompressBytes(byte[] compressedBytes)
        {
            return compressedBytes;
        }
    }
}