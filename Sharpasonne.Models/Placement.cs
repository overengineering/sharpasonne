using JetBrains.Annotations;
using Optional;

namespace Sharpasonne.Models
{
    public class Placement
    {
        [NotNull] public TilePlacement           TilePlacement   { get; }
                  public Option<MeeplePlacement> MeeplePlacement { get; }

        public Placement([NotNull] TilePlacement tilePlacement)
        {
            TilePlacement   = tilePlacement;
            MeeplePlacement = Option.None<MeeplePlacement>();
        }

        public Placement([NotNull] TilePlacement tilePlacement, MeeplePlacement meeplePlacement)
        {
            TilePlacement   = tilePlacement;
            MeeplePlacement = Option.Some(meeplePlacement);
        }

        public Placement AddMeeple(MeeplePlacement meeplePlacement)
        {
            return new Placement(this.TilePlacement, meeplePlacement);
        }
    }
}