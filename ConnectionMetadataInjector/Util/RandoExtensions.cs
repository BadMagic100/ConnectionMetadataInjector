using ItemChanger;
using RandomizerCore;
using RandomizerMod.IC;

namespace ConnectionMetadataInjector.Util
{
    /// <summary>
    /// Extension methods to read rando information of an <see cref="AbstractItem"/>'s <see cref="RandoItemTag"/> tag.
    /// </summary>
    public static class RandoExtensions
    {
        /// <summary>
        /// Gets the rando placement of the item
        /// </summary>
        public static ItemPlacement RandoPlacement(this AbstractItem item)
        {
            if (item.GetTag(out RandoItemTag tag))
            {
                return RandomizerMod.RandomizerMod.RS.Context.itemPlacements[tag.id];
            }
            return default;
        }

        /// <summary>
        /// Gets the rando item name of the item
        /// </summary>
        public static string RandoItem(this AbstractItem item)
        {
            return item.RandoPlacement().item.Name ?? "";
        }

        /// <summary>
        /// Gets the rando location name of the location the item is placed at
        /// </summary>
        public static string RandoLocation(this AbstractItem item)
        {
            return item.RandoPlacement().location.Name ?? "";
        }
    }
}
