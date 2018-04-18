using System;
using System.Collections.Immutable;
using Sharpasonne.GameActions;
using Sharpasonne.Models;
using Sharpasonne.Rules;

namespace Sharpasonne
{
    public interface IEngine
    {
        Board Board { get; }

        /// <summary>
        /// Players collection for managing player stats and turns.
        /// </summary>
        Players Players { get; }

        /// <summary>
        /// 1-index number of the player who's turn it is to play an action.
        /// </summary>
        int CurrentPlayerTurn { get; }

        IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> Rules { get; }
    }
}
