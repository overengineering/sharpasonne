using JetBrains.Annotations;
using Optional;

namespace Sharpasonne.Models
{
    /// <summary>
    /// The location a <see cref="Tile"/> and its contents claims on a
    /// <see cref="Board"/>.
    /// </summary>
    public class Placement
    {
        [NotNull] public TilePlacement           TilePlacement   { get; }
                  public Option<MeeplePlacement> MeeplePlacement { get; }

        public Placement([NotNull] TilePlacement tilePlacement)
        {
            TilePlacement   = tilePlacement;
            MeeplePlacement = Option.None<MeeplePlacement>();
        }

        public Placement([NotNull] TilePlacement tilePlacement, [NotNull] MeeplePlacement meeplePlacement)
        {
            TilePlacement   = tilePlacement;
            MeeplePlacement = Option.Some(meeplePlacement);
        }
    }
}