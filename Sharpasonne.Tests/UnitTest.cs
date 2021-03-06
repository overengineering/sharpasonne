﻿using System.Linq;
using Optional.Unsafe;
using Sharpasonne.GameActions;
using Sharpasonne.Models;
using Xunit;
using Moq;
using Sharpasonne.Rules;

namespace Sharpasonne.Tests
{
    public abstract class UnitTest
    {
        protected PlaceTileGameAction MakePlaceTile(
            int x,
            int y,
            Tile tile = null)
        {
            tile = tile ?? new TileBuilder()
                .CreateTile()
                .ValueOrFailure();

            return new PlaceTileGameAction(new Point(x, y), tile);
        }

        protected Board MakeBoard(params PlaceTileGameAction[] actions)
        {
            return new Board(actions.ToDictionary(
                a => a.Point,
                a => new Placement(a.Tile)
            ));
        }

        protected IEngine MockEngine(Board board)
        {
            var engine = new Mock<IEngine>();

            engine
                .Setup(mockEngine => mockEngine.Board)
                .Returns(board);

            return engine.Object;
        }
    }
}

