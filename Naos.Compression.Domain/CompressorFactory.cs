// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompressorFactory.cs" company="Naos">
//    Copyright (c) Naos 2017. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Compression.Domain
{
    using System;

    using Spritely.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Get the correct <see cref="ICompressAndDecompress" /> implementation based on the kind.
    /// </summary>
    public static class CompressorFactory
    {
        /// <summary>
        /// Builds the correct <see cref="ICompressAndDecompress" /> implementation based on the kind.
        /// </summary>
        /// <param name="compressionKind">Kind of compression.</param>
        /// <returns><see cref="ICompressAndDecompress" /> implementation based on the kind.</returns>
        public static ICompressAndDecompress BuildCompressor(CompressionKind compressionKind)
        {
            new { compressionKind }.Must().NotBeEqualTo(CompressionKind.Invalid).OrThrowFirstFailure();

            switch (compressionKind)
            {
                case CompressionKind.None: return new NullCompressor();
                case CompressionKind.DotNetZip: return new DotNetZipCompressor();
                default: throw new NotSupportedException(Invariant($"{nameof(CompressionKind)} value {compressionKind} is not currently supported."));
            }
        }
    }
}