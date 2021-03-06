﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompressorFactory.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Compression.Domain
{
    using System;

    using OBeautifulCode.Validation.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Get the correct <see cref="ICompressAndDecompress" /> implementation based on the kind.
    /// </summary>
    public class CompressorFactory : ICompressorFactory
    {
        private static readonly CompressorFactory InternalInstance = new CompressorFactory();

        /// <summary>
        /// Gets the singleton entry point to the code.
        /// </summary>
        public static ICompressorFactory Instance => InternalInstance;

        private CompressorFactory()
        {
            /* no-op to make sure this can only be accessed via instance property */
        }

        /// <inheritdoc />
        public ICompressAndDecompress BuildCompressor(CompressionKind compressionKind)
        {
            new { compressionKind }.Must().NotBeEqualTo(CompressionKind.Invalid);

            switch (compressionKind)
            {
                case CompressionKind.None: return new NullCompressor();
                case CompressionKind.DotNetZip: return new DotNetZipCompressor();
                default: throw new NotSupportedException(Invariant($"{nameof(CompressionKind)} value {compressionKind} is not currently supported."));
            }
        }
    }

    /// <summary>
    /// Abstract factory interface for building compressors.
    /// </summary>
    public interface ICompressorFactory
    {
        /// <summary>
        /// Builds the correct <see cref="ICompressAndDecompress" /> implementation based on the kind.
        /// </summary>
        /// <param name="compressionKind">Kind of compression.</param>
        /// <returns><see cref="ICompressAndDecompress" /> implementation based on the kind.</returns>
        ICompressAndDecompress BuildCompressor(CompressionKind compressionKind);
    }
}