using System;
using System.Collections;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Sharpasonne.Models;

namespace Sharpasonne
{
    // TODO: Consider rename.
    public class Engine : IEngine
    {
        public Board Board { get; } = new Board();

        public Engine()
        {
        }

        public Engine(
            [NotNull] IImmutableQueue<IGameAction> gameActions,
            [NotNull] IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> rules)
        {
            var nonGameActions = rules.Keys
                .Where(type => !typeof(IGameAction).IsAssignableFrom(type))
                .ToList();

            if (nonGameActions.Any())
            {
                var typeNames = string.Join(", ", nonGameActions.Select(t => t.FullName));
                var message = $"'{typeNames}' does not implement {nameof(IGameAction)}";
                throw new ArgumentOutOfRangeException(message);
            }
        }

        /*public Optional.Option<Engine, IEnumerable<string>> PlaceTile(
                      Point     point,
            [NotNull] Placement placement)
        {
            
        }*/
    }
}
