using System;
using System.Collections.Immutable;
using Sharpasonne.Models;
using Sharpasonne.Rules;

namespace Sharpasonne.GameActions
{
    public class PlaceTileGameAction : IGameAction
    {
        public Placement Placement { get; }
        public Point Point { get; }

        public PlaceTileGameAction(Point point, Placement placement)
        {
            this.Point = point;
            this.Placement = placement;
        }

        public IEngine Perform(IEngine engine)
        {
            return new EngineState(new Board(), engine.Rules);
        }
    }

    public class EngineState : IEngine
    {
        public EngineState(Board board, IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> rules)
        {
            Board = board;
            Rules = rules;
        }

        public Board Board { get; }
        public IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> Rules { get; }
    }
}
