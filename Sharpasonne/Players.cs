using System;
using System.Linq;
using Optional;

namespace Sharpasonne
{
    /// <summary>
    /// Represent a group of players.
    /// </summary>
    public class Players
    {
        /// <summary>
        /// Fewest number of players allowed to create a <see cref="Players" /> instance.
        /// </summary>
        public const int FewestPlayers = 2;

        /// <summary>
        /// Most number of players allowed to create a <see cref="Players" /> instance.
        /// </summary>
        public const int MostPlayers = 5;

        /// <summary>
        /// Range size of the lower and upper bound.
        /// </summary>
        protected const int PlayerRangeSize = (MostPlayers - FewestPlayers);

        /// <summary>
        /// Number of players, always within the inclusive range <see cref="FewestPlayers" />...
        /// <see cref="MostPlayers" />.
        /// </summary>
        public virtual int Count { get; }

        /// <summary>
        /// Attempts to create a <see cref="Players" /> instance, fails and returns a none if
        /// <paramref name="count" /> is outside the inclusive player range <see cref="FewestPlayers" />
        /// ...<see cref="MostPlayers" />.
        /// </summary>
        public static Option<Players, Exception> Create(int count)
        {
            if (!Enumerable.Range(FewestPlayers, PlayerRangeSize).Contains(count)) {
                return Option.None<Players, Exception>(
                    new ArgumentOutOfRangeException(
                        nameof(count),
                        $"must be in the inclusive range ${FewestPlayers}-${MostPlayers}."
                    )
                );
            }

            return Option.Some<Players, Exception>(new Players(count));
        }

        /// <summary>
        /// Creates a <see cref="Players" /> instance with <see cref="FewestPlayers" /> number
        /// of players. This is only intended for use with Moq, yse <see cref="Create(int)" />
        /// instead.
        /// </summary>
        protected Players() : this(FewestPlayers)
        {
        }

        /// <summary>
        /// Creates a <see cref="Players" /> instance without doing any checks for consistency of
        /// <paramref name="count" /> argument. Use <see cref="Create(int)" /> instead.
        /// </summary>
        protected Players(int count)
        {
            this.Count = count;
        }

        /// <summary>
        /// Gets the next player after the given <paramref name="player" /> cycling around from
        /// last to first:
        /// <example>
        /// Assert.Equals(1, Players.Create(2).NextPlayer(2));
        /// </example>
        /// If <paramref name="player" /> is outside the number of players, 1 is always returned.
        /// </summary>
        /// <param name="player">1-index number of the player.</param>
        /// <returns>1-index number of the next player.</returns>
        public int NextPlayer(int player)
        {
            if (!Enumerable.Range(1, this.Count).Contains(player)) {
                return 1;
            }

            // As the player's number is 1-indexed we can mod by the total number of players to
            // cycle around from the end getting the next player's 0-indexed number. Then add 1
            // to whatever value this is.
            // E.g. in a 4 player games player 4 => 4%4 = 0, add 1 => player 1.
            return (player % this.Count) + 1;
        }
    }
}