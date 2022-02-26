using ConnectionMetadataInjector.Util;
using ItemChanger;
using Modding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectionMetadataInjector
{
    /// <summary>
    /// Mod providing the ability to request and read supplemental metadata from custom randomizer items
    /// </summary>
    public class ConnectionMetadataInjector : Mod
    {
        internal static ConnectionMetadataInjector? Instance;

        /// <summary>
        /// A property representing the pool group of an item
        /// </summary>
        public readonly MetadataProperty<AbstractItem, string> ItemPoolGroup;
        /// <summary>
        /// A property representing the location of a placement
        /// </summary>
        public readonly MetadataProperty<AbstractPlacement, string> LocationPoolGroup;

        /// <inheritdoc/>
        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

        /// <summary>
        /// Creates an instance of the mod
        /// </summary>
        public ConnectionMetadataInjector() : base()
        {
            Instance = this;

            ItemPoolGroup = new MetadataProperty<AbstractItem, string>("PoolGroup", GetDefaultItemPoolGroup);
            LocationPoolGroup = new MetadataProperty<AbstractPlacement, string>("PoolGroup", GetDefaultLocationPoolGroup);
        }

        /// <summary>
        /// Gets connection-provided (i.e. non-default) values for a metadata property
        /// </summary>
        /// <typeparam name="TObject">The type of object holding the metadata</typeparam>
        /// <typeparam name="TValue">The value type of the property</typeparam>
        /// <param name="objects">The objects to check for values</param>
        /// <param name="metadataSelector">How to get metadata for a given object, usually one of the static methods of <see cref="SupplementalMetadata"/></param>
        /// <param name="prop">The property to get values for</param>
        /// <returns>A set of unique values provided by connections</returns>
        public static HashSet<TValue> GetConnectionProvidedValues<TObject, TValue>(
            IEnumerable<TObject> objects,
            Func<TObject, SupplementalMetadata<TObject>> metadataSelector,
            MetadataProperty<TObject, TValue> prop) where TObject : TaggableObject
        {
            return new HashSet<TValue>(objects
                .Select(item => metadataSelector(item))
                .Where(md => md.IsNonDefault(prop))
                .Select(md => md.Get(prop))
            );
        }

        private string GetDefaultItemPoolGroup(AbstractItem item)
        {
            return SubcategoryFinder.GetItemPoolGroup(item.RandoItem()).FriendlyName();
        }

        private string GetDefaultLocationPoolGroup(AbstractPlacement placement)
        {
            return SubcategoryFinder.GetLocationPoolGroup(placement.RandoLocation()).FriendlyName();
        }
    }
}