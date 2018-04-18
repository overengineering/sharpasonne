using System;
using System.Collections.Immutable;
using Sharpasonne.Models;
using Sharpasonne.Rules;

namespace Sharpasonne.GameActions
{
    using IRuleMap = IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>>;

    /// <summary>
    /// Is the state of an engine without any functionality, because an Engine
    /// cannot be constructed from the outside with this state.
    /// </summary>
    internal class EngineState : IEngine
    {
        public EngineState(Board board, IEngine engine)
        {
            this.Board             = board;
            this.Rules             = engine.Rules;
            this.Players           = engine.Players;
            this.CurrentPlayerTurn = engine.CurrentPlayerTurn;
        }

        public Board    Board             { get; }
        public Players  Players           { get; }
        public int      CurrentPlayerTurn { get; }
        public IRuleMap Rules             { get; }
    }
}