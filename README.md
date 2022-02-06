# ConnectionMetadataInjector

A helper mod that allows Hollow Knight mods that are supplemental to Randomizer to request additional metadata information from connections, 
while allowing connections to specify this metadata without declaring hard dependencies.

## Tag spec

Data is provided by connections using an [InteropTag](https://homothetyhk.github.io/HollowKnight.ItemChanger/api/ItemChanger.Tags.InteropTag.html) with the message
`"RandoSupplementalMetadata"`. These tags can most easily be provided when creating custom items and locations in ItemChanger. Supplemental metadata can be declared on
any taggable object, and supplemental mods can specify what data they would like to have provided.

## Built-in property support

ConnectionMetadataInjector supports a handful of default metadata properties that are anticipated to be generally useful to most consumers of this mod.

| Property | Property type | Parent object type | Description | Default handling |
| -------- | ------------- | ------------------ | ----------- | ---------------- |
| `PoolGroup` | `string` | AbstractItem, AbstractLocation, or AbstractPlacement (usually item or location) | The named pool group of an item or location, such as "Relics" or "Lifeblood Cocoons". | For items, attempts to find the split group name in PoolDefs. For locations, attempts to infer the vanilla item name from the location name with standard naming conventions (i.e. `<Item_Name>-<Location>`), then attempts to find the split group name of the item in PoolDefs |
| `NearestRoom` | `string` | AbstractLocation or AbstractPlacement (usually location) | The nearest room name defined in RoomDefs that approximates the titled and map areas of the location | Attempts to get the scene name from the location's LocationDef |