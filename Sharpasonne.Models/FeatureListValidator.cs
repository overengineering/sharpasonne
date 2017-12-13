using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;

namespace Sharpasonne.Models
{
    public class FeatureListValidator
    {
        public bool IsValid([NotNull] IImmutableList<IFeature> featureList)
        {
            var isValid = CheckFeaturesDontOverlap(featureList);

            return isValid;
        }

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
