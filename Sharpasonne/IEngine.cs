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
        Players Players { get; }
        int CurrentPlayerTurn { get; }
        IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> Rules { get; }
    }
}
