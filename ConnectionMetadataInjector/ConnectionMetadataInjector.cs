using ConnectionMetadataInjector.Util;
using ItemChanger;
using Modding;
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
        /// <summary>
        /// A property representing the map area of a placement
        /// </summary>
        public readonly MetadataProperty<AbstractPlacement, string> LocationMapArea;

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
            LocationMapArea = new MetadataProperty<AbstractPlacement, string>("MapArea", GetDefaultLocationMapArea);
        }

        private string GetDefaultItemPoolGroup(AbstractItem item)
        {
            return SubcategoryFinder.GetItemPoolGroup(item.RandoItem()).FriendlyName();
        }

        private string GetDefaultLocationPoolGroup(AbstractPlacement placement)
        {
            return SubcategoryFinder.GetLocationPoolGroup(placement.Items.First().RandoLocation()).FriendlyName();
        }

        private string GetDefaultLocationMapArea(AbstractPlacement placement)
        {
            return SubcategoryFinder.GetLocationMapArea(placement.Items.First().RandoLocation());
        }
    }
}