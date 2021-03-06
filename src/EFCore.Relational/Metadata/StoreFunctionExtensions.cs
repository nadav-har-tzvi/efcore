// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable enable

namespace Microsoft.EntityFrameworkCore.Metadata
{
    /// <summary>
    ///     Extension methods for <see cref="IStoreFunction" />.
    /// </summary>
    public static class StoreFunctionExtensions
    {
        /// <summary>
        ///     <para>
        ///         Creates a human-readable representation of the given metadata.
        ///     </para>
        ///     <para>
        ///         Warning: Do not rely on the format of the returned string.
        ///         It is designed for debugging only and may change arbitrarily between releases.
        ///     </para>
        /// </summary>
        /// <param name="function"> The metadata item. </param>
        /// <param name="options"> Options for generating the string. </param>
        /// <param name="indent"> The number of indent spaces to use before each new line. </param>
        /// <returns> A human-readable representation. </returns>
        public static string ToDebugString(
            [NotNull] this IStoreFunction function,
            MetadataDebugStringOptions options,
            int indent = 0)
        {
            var builder = new StringBuilder();
            var indentString = new string(' ', indent);

            builder
                .Append(indentString)
                .Append("StoreFunction: ");

            if (function.ReturnType != null)
            {
                builder.Append(function.ReturnType);
            }
            else
            {
                builder.Append(function.EntityTypeMappings.FirstOrDefault()?.EntityType.DisplayName() ?? "");
            }

            builder.Append(" ");

            if (function.Schema != null)
            {
                builder
                    .Append(function.Schema)
                    .Append(".");
            }

            builder.Append(function.Name);

            if (function.IsBuiltIn)
            {
                builder.Append(" IsBuiltIn");
            }

            if ((options & MetadataDebugStringOptions.SingleLine) == 0)
            {
                var parameters = function.Parameters.ToList();
                if (parameters.Count != 0)
                {
                    builder.AppendLine().Append(indentString).Append("  Parameters: ");
                    foreach (var parameter in parameters)
                    {
                        builder.AppendLine().Append(parameter.ToDebugString(options, indent + 4));
                    }
                }

                var mappings = function.EntityTypeMappings.ToList();
                if (mappings.Count != 0)
                {
                    builder.AppendLine().Append(indentString).Append("  EntityTypeMappings: ");
                    foreach (var mapping in mappings)
                    {
                        builder.AppendLine().Append(mapping.ToDebugString(options, indent + 4));
                    }
                }

                var columns = function.Columns.ToList();
                if (columns.Count != 0)
                {
                    builder.AppendLine().Append(indentString).Append("  Columns: ");
                    foreach (var column in columns)
                    {
                        builder.AppendLine().Append(column.ToDebugString(options, indent + 4));
                    }
                }

                if ((options & MetadataDebugStringOptions.IncludeAnnotations) != 0)
                {
                    builder.Append(function.AnnotationsToDebugString(indent: indent + 2));
                }
            }

            return builder.ToString();
        }
    }
}
