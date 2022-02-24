using Modding;
using RandomizerMod.RandomizerData;
using System;
using System.Collections.Generic;

namespace ConnectionMetadataInjector.Util
{
    /// <summary>
    /// Utility class to find subcategories for named rando items and locations with a sensible default value when none could be inferred.
	/// These are used as default implementations of some built-in metadata properties.
    /// </summary>
    public static class SubcategoryFinder
	{
		private static readonly Loggable log = ScopedLoggers.GetLogger();

		private static readonly HashSet<string> ShopNames = new(new string[] {
			"Sly", "Sly_(Key)", "Iselda", "Salubra", "Salubra_(Requires_Charms)",
			"Leg_Eater", "Grubfather", "Seer", "Egg_Shop"
		});

		/// <summary>
		/// A catchall subcategory to use as a default value when none could be found
		/// </summary>
		public const string OTHER = "Other";

		/// <summary>
		/// Gets the pool group of a rando item by its name. You probably want to be using <see cref="ConnectionMetadataInjector.ItemPoolGroup"/> to handle custom items.
		/// </summary>
		/// <param name="cleanItemName">The name of the item, e.g. "Mimic_Grub" or "Isma's_Tear"</param>
		public static PoolGroup GetItemPoolGroup(string cleanItemName)
		{
			switch (cleanItemName)
			{
				case "Dreamer":
					return PoolGroup.Dreamers;
				case "Split_Mothwing_Cloak":
				case "Split_Crystal_Heart":
				case "Downslash":
					return PoolGroup.Skills;
				case "Double_Mask_Shard":
				case "Full_Mask":
					return PoolGroup.MaskShards;
				case "Double_Vessel_Fragment":
				case "Full_Soul_Vessel":
					return PoolGroup.VesselFragments;
				case "Grimmchild1":
				case "Grimmchild2":
					return PoolGroup.Charms;
				case "Grub":
					return PoolGroup.Grubs;
				case "One_Geo":
					return PoolGroup.GeoChests;
				default:
					break;
			}

			foreach (PoolDef poolDef in Data.Pools)
			{
				foreach (string includeItem in poolDef.IncludeItems)
				{
					if (includeItem.StartsWith(cleanItemName))
					{
						PoolGroup group = (PoolGroup)Enum.Parse(typeof(PoolGroup), poolDef.Group);

						return group;
					}
				}
			}

			log.LogWarn($"{cleanItemName} not found in PoolDefs");
			return PoolGroup.Other;
		}

		/// <summary>
		/// Gets the pool group of a rando location. You probably want to be using <see cref="ConnectionMetadataInjector.LocationPoolGroup"/> to handle custom locations.
		/// </summary>
		/// <param name="location">The location name, e.g. "Mimic_Grub-Crystal_Peak" or "Isma's_Tear"</param>
		public static PoolGroup GetLocationPoolGroup(string location)
		{
			if (ShopNames.Contains(location))
			{
				return PoolGroup.Shops;
			}
			string nameWithoutLocation = location.Split('-')[0];
			return GetItemPoolGroup(nameWithoutLocation);
		}

		/// <summary>
		/// Gets the map area of a transition. Transitions aren't taggable, so use this if needed; transitions in custom map areas will always return "Other"
		/// </summary>
		/// <param name="transition">The transition name, e.g. "Crossroads_01[top1]"</param>
		public static string GetTransitionMapArea(string transition)
		{
			if (Data.IsTransition(transition))
			{
				return Data.GetTransitionDef(transition).MapArea;
			}
			log.LogWarn($"{transition} not found in TransitionDefs");
			return OTHER;
		}
	}
}
