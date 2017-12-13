using System;
using System.Collections.Immutable;
using System.Linq;
using Optional.Unsafe;
using Sharpasonne.Models.Features;
using Xunit;

namespace Sharpasonne.Models.Tests
{
    public class FeatureListValidatorTests
    {
        [Fact]
        void Given_EmptyTile_When_CheckingForOverlaps_Then_IsValid()
        {
            var featureList = Enumerable.Empty<IFeature>().ToImmutableList();
            var isValid = new FeatureListValidator().CheckFeaturesDontOverlap(featureList);

            Assert.True(isValid);
        }

        [Fact]
        void Given_OverlappingFeaturesTile_When_CheckingForOverlaps_Then_IsNotValid()
        {
            var segments = Enumerable.Repeat(Segment.Left, 2)
                .ToImmutableHashSet();
            var overlappingFeatures = Enumerable.Repeat(new Field(segments) as IFeature, 2)
                .ToImmutableList();

            var isValid = new FeatureListValidator().CheckFeaturesDontOverlap(overlappingFeatures);

            Assert.False(isValid);
        }

        [Fact]
        void Given_EmptyTile_When_CheckingForFields_Then_AllFields()
        {
            throw new NotImplementedException();
        }
    }
}
