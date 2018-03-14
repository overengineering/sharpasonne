using System;
using System.Collections.Immutable;
using Sharpasonne.Models;
using Sharpasonne.Rules;

namespace Sharpasonne.GameActions
{
    public class EngineState : IEngine
    {
        public EngineState(Board board, IEngine engine)
        {
            this.Board             = board;
            this.Rules             = engine.Rules;
            this.Players           = engine.Players;
            this.CurrentPlayerTurn = engine.CurrentPlayerTurn;
        }

        public Board                                                          Board             { get; }
        public Players                                                        Players           { get; }
        public int                                                            CurrentPlayerTurn { get; }
        public IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> Rules             { get; }
    }
}