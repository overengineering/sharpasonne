using System;
using System.Collections.Immutable;
using Sharpasonne.Models;
using Sharpasonne.Rules;

namespace Sharpasonne.GameActions
{
    public class EngineState : IEngine
    {
        public EngineState(Board board, IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> rules)
        {
            Board = board;
            Rules = rules;
        }

        public Board                                                          Board { get; }
        public IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> Rules { get; }
    }
}