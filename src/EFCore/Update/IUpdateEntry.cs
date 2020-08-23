// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Microsoft.EntityFrameworkCore.Update
{
    /// <summary>
    ///     <para>
    ///         The information passed to a database provider to save changes to an entity to the database.
    ///     </para>
    ///     <para>
    ///         This interface is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public interface IUpdateEntry
    {
        /// <summary>
        ///     Sets the original value of the given property.
        /// </summary>
        /// <param name="property"> The property to set. </param>
        /// <param name="value"> The value to set. </param>
        void SetOriginalValue([NotNull] IProperty property, [CanBeNull] object value);

        /// <summary>
        ///     Marks the given property as modified.
        /// </summary>
        /// <param name="property"> The property to mark as modified. </param>
        void SetPropertyModified([NotNull] IProperty property);

        /// <summary>
        ///     The type of entity to be saved to the database.
        /// </summary>
        IEntityType EntityType { get; }

        /// <summary>
        ///     The state of the entity to be saved.
        /// </summary>
        EntityState EntityState { get; set; }

        /// <summary>
        ///     The other entry that has the same key values, if one exists.
        /// </summary>
        IUpdateEntry SharedIdentityEntry { get; }

        /// <summary>
        ///     Gets a value indicating if the specified property is modified. If true, the current value assigned
        ///     to the property should be saved to the database.
        /// </summary>
        /// <param name="property"> The property to be checked. </param>
        /// <returns> <see langword="true" /> if the property is modified, otherwise <see langword="false" />. </returns>
        bool IsModified([NotNull] IProperty property);

        /// <summary>
        ///     Gets a value indicating if the specified property has a temporary value.
        /// </summary>
        /// <param name="property"> The property to be checked. </param>
        /// <returns> <see langword="true" /> if the property has a temporary value, otherwise <see langword="false" />. </returns>
        bool HasTemporaryValue([NotNull] IProperty property);

        /// <summary>
        ///     Gets a value indicating if the specified property should have a value generated by the database.
        /// </summary>
        /// <param name="property"> The property to be checked. </param>
        /// <returns> <see langword="true" /> if the property should have a value generated by the database, otherwise <see langword="false" />. </returns>
        bool IsStoreGenerated([NotNull] IProperty property);

        /// <summary>
        ///     Gets the value assigned to the property.
        /// </summary>
        /// <param name="propertyBase"> The property to get the value for. </param>
        /// <returns> The value for the property. </returns>
        object GetCurrentValue([NotNull] IPropertyBase propertyBase);

        /// <summary>
        ///     Gets the value assigned to the property when it was retrieved from the database.
        /// </summary>
        /// <param name="propertyBase"> The property to get the value for. </param>
        /// <returns> The value for the property. </returns>
        object GetOriginalValue([NotNull] IPropertyBase propertyBase);

        /// <summary>
        ///     Gets the value assigned to the property.
        /// </summary>
        /// <param name="propertyBase"> The property to get the value for. </param>
        /// <typeparam name="TProperty"> The type of the property. </typeparam>
        /// <returns> The value for the property. </returns>
        TProperty GetCurrentValue<TProperty>([NotNull] IPropertyBase propertyBase);

        /// <summary>
        ///     Gets the value assigned to the property when it was retrieved from the database.
        /// </summary>
        /// <param name="property"> The property to get the value for. </param>
        /// <typeparam name="TProperty"> The type of the property. </typeparam>
        /// <returns> The value for the property. </returns>
        TProperty GetOriginalValue<TProperty>([NotNull] IProperty property);

        /// <summary>
        ///     Assign a store-generated value to the property.
        /// </summary>
        /// <param name="property"> The property to set the value for. </param>
        /// <param name="value"> The value to set. </param>
        void SetStoreGeneratedValue([NotNull] IProperty property, [CanBeNull] object value);

        /// <summary>
        ///     Gets an <see cref="EntityEntry" /> for the entity being saved. <see cref="EntityEntry" /> is an API optimized for
        ///     application developers and <see cref="IUpdateEntry" /> is optimized for database providers, but there may be instances
        ///     where a database provider wants to access information from <see cref="EntityEntry" />.
        /// </summary>
        /// <returns> An <see cref="EntityEntry" /> for this entity. </returns>
        EntityEntry ToEntityEntry();

        /// <summary>
        ///     Gets the last value assigned to the property that's part of a foreign key or principal key
        /// </summary>
        /// <param name="propertyBase"> The property to get the value for. </param>
        /// <returns> The value for the property. </returns>
        object GetRelationshipSnapshotValue([NotNull] IPropertyBase propertyBase);

        /// <summary>
        ///     Gets the value assigned to the property before any store-generated values have been applied.
        /// </summary>
        /// <param name="propertyBase"> The property to get the value for. </param>
        /// <returns> The value for the property. </returns>
        object GetPreStoreGeneratedCurrentValue([NotNull] IPropertyBase propertyBase);

        /// <summary>
        ///     Checks whether the property is conceptually set to null even if the property type is not nullable.
        /// </summary>
        /// <param name="property"> The property to check. </param>
        /// <returns> <see langword="true" /> if the property is conceptually null; <see langword="false" /> otherwise. </returns>
        bool IsConceptualNull([NotNull] IProperty property);
    }
}
