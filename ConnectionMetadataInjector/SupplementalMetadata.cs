using ItemChanger;
using ItemChanger.Tags;
using System.Collections.Generic;
using System.Linq;

namespace ConnectionMetadataInjector
{
    /// <summary>
    /// Represents any randomizer-supplemental metadata on a taggable object
    /// </summary>
    /// <typeparam name="TObject">The type of object that will hold the data on its tag</typeparam>
    public class SupplementalMetadata<TObject> where TObject : TaggableObject
    {
        private readonly TObject obj;
        private readonly InteropTag? tag;

        internal SupplementalMetadata(TObject obj, InteropTag? tag)
        {
            this.obj = obj;
            this.tag = tag;
        }

        /// <summary>
        /// Gets the value of a property in this metadata
        /// </summary>
        /// <typeparam name="TValue">The type of the property's value</typeparam>
        /// <param name="property">The property to get the value of</param>
        /// <returns>
        /// The value of the property, if this object contains a supplemental metadata tag and that tag contains the requested property.
        /// Otherwise, returns the default value of the property for this object.
        /// </returns>
        public TValue Get<TValue>(MetadataProperty<TObject, TValue> property)
        {
            if (tag != null && tag.TryGetProperty(property.Name, out TValue value))
            {
                return value;
            }
            else
            {
                return property.GetDefault(obj);
            }
        }
    }

    /// <summary>
    /// Represents randomizer-supplemental metadata on a taggable object
    /// </summary>
    public static class SupplementalMetadata
    {
        /// <summary>
        /// The tag message to specify on interop tags describing supplemental metadata
        /// </summary>
        public const string InteropTagMessage = "RandoSupplementalMetadata";

        /// <summary>
        /// Gets the supplemental metadata of an object
        /// </summary>
        /// <typeparam name="TObject">The type of the object; usually inferred</typeparam>
        /// <param name="obj">The object</param>
        public static SupplementalMetadata<TObject> Of<TObject>(TObject obj) where TObject : TaggableObject
        {
            IEnumerable<InteropTag> metaTags = obj.GetTags<InteropTag>().Where(t => t.Message == InteropTagMessage);
            return new SupplementalMetadata<TObject>(obj, metaTags.FirstOrDefault());
        }

        /// <summary>
        /// Gets the supplemental metadata of a placement and its underlying location(s). If you were thinking of using
        /// <see cref="Of{TObject}(TObject)"/> to get information about an <see cref="AbstractLocation"/>, you probably want this instead.
        /// </summary>
        /// <param name="plt">The placement</param>
        public static SupplementalMetadata<AbstractPlacement> OfPlacementAndLocations(AbstractPlacement plt)
        {
            IEnumerable<InteropTag> metaTags = plt.GetPlacementAndLocationTags().OfType<InteropTag>().Where(t => t.Message == InteropTagMessage);
            return new SupplementalMetadata<AbstractPlacement>(plt, metaTags.FirstOrDefault());
        }
    }
}
