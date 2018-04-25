using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;

namespace Sharpasonne.Models
{
    /// <summary>
    /// Ensure that a list of <see cref="IFeature"/> is valid when used in a
    /// <see cref="Tile"/>.
    /// </summary>
    public class FeatureListValidator
    {
        public bool IsValid([NotNull] IImmutableList<IFeature> featureList)
        {
            var isValid = CheckFeaturesDontOverlap(featureList);

            return isValid;
        }

        // TODO: make sure only one monastery, because they don't have connections.
        public bool CheckFeaturesDontOverlap(
            [NotNull] [ItemNotNull] IImmutableList<IFeature> featureList)
        {
            var segments = featureList
                .SelectMany(f => f.Connections)
                .ToList();

            var distinctSegments = segments.Distinct();

            return segments.Count() == distinctSegments.Count();
        }
    }
}
