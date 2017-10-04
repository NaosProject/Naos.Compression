// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompressionKind.cs" company="Naos">
//    Copyright (c) Naos 2017. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Compression.Domain
{
    /// <summary>
    /// Kind of compression.
    /// </summary>
    public enum CompressionKind
    {
        /// <summary>
        /// Invalid default option.
        /// </summary>
        Invalid,

        /// <summary>
        /// No compression, pass through.
        /// </summary>
        None,

        /// <summary>
        /// Zip compression using <see cref="System.IO.Compression" /> logic.
        /// </summary>
        DotNetZip,
    }
}