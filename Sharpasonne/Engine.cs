using System;
using System.Collections;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using Sharpasonne.Models;

namespace Sharpasonne
{
    // TODO: Consider rename.
    public class Engine
    {
        public Board Board { get; } = new Board();

        public Engine(
            [NotNull] IImmutableQueue<IGameAction> gameActions,
            [NotNull] IImmutableDictionary<Type, IImmutableList<IRule>> rules)
        {
            var nonGameActions = rules.Keys
                .Where(k => !k.GetInterfaces().Any(i => i == typeof(IGameAction)))
                .ToList();
            if (nonGameActions.Any())
            {
                var typeNames = string.Join(", ", nonGameActions.Select(t => t.FullName));
                var message = $"'{typeNames}' don't implement {nameof(IGameAction)}";
                throw new ArgumentOutOfRangeException(message);
            }
        }

        /*public Optional.Option<Engine, IEnumerable<string>> PlaceTile(
                      Point     point,
            [NotNull] Placement placement)
        {
            
        }*/
    }

    public interface IGameAction
    {
    }
}
