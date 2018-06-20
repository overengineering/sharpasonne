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
        [NotNull] public Tile           Tile   { get; }
                  public Option<MeeplePlacement> MeeplePlacement { get; }

        public Placement([NotNull] Tile tile)
        {
            this.Tile = tile;
            this.MeeplePlacement = Option.None<MeeplePlacement>();
        }

        public Placement(
            [NotNull] Tile tile,
            [NotNull] MeeplePlacement meeplePlacement)
        {
            this.Tile = tile;
            this.MeeplePlacement = Option.Some(meeplePlacement);
        }
    }
}