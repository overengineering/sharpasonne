using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using Optional;
using Sharpasonne.GameActions;
using Sharpasonne.Models;
using Sharpasonne.Rules;

namespace Sharpasonne
{
    public class Players
    {
        /// <summary>
        /// Least number of players allowed to create a <see cref="Players" /> instance.
        /// </summary>
        public const int LOWER_BOUND = 2;

        /// <summary>
        /// Most number of players allowed to create a <see cref="Players" /> instance.
        /// </summary>
        public const int UPPER_BOUND = 5;

        /// <summary>
        /// Range size of the lower and upper bound.
        /// </summary>
        protected const int RANGE_SIZE = (UPPER_BOUND - LOWER_BOUND);

        /// <summary>
        /// Number of players, always within the inclusive range <see cref="LOWER_BOUND" />...
        /// <see cref="UPPER_BOUND" />.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Attempts to create a <see cref="Players" /> instance, fails and returns a none if
        /// <paramref name="count" /> is outside the inclusive player range <see cref="LOWER_BOUND" />
        /// ...<see cref="UPPER_BOUND" />.
        /// </summary>
        public static Option<Players, Exception> Create(int count)
        {
            if (!Enumerable.Range(LOWER_BOUND, RANGE_SIZE).Contains(count)) {
                return Option.None<Players, Exception>(
                    new ArgumentOutOfRangeException(
                        nameof(count),
                        $"must be in the inclusive range ${LOWER_BOUND}-${UPPER_BOUND}."
                    )
                );
            }

            return Option.Some<Players, Exception>(new Players(count));
        }

        /// <summary>
        /// Creates a <see cref="Players" /> instance without doing any checks for consistency of
        /// <paramref name="count" /> argument. Use <see cref="Create(int)" /> instead.
        /// </summary>
        protected Players(int count)
        {
            this.Count = count;
        }
    }
}