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
    // TODO: Consider rename.
    public class Engine : IEngine
    {
        public Board Board { get; } = new Board();

        /// <summary>
        /// Players collection for managing player stats and turns.
        /// </summary>
        public Players Players { get; }

        /// <summary>
        /// 1-index number of the player who's turn it is to play an action.
        /// </summary>
        public int CurrentPlayerTurn { get; } = 1;

        public IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> Rules { get; }
            = ImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>>.Empty;

        /// <summary>Attempts to create a Game engine.</summary>
        /// <param name="gameActions"></param>
        /// <param name="rules">Must provide list for every action to be used by
        /// Perform.</param>
        /// <param name="gameActions"></param>
        public static Option<Engine, Exception> Create(
            [NotNull] IImmutableQueue<IGameAction>                                   gameActions,
            [NotNull] IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> rules,
            [NotNull] Players                                                        players)
        {
            var nonGameActions = rules.Keys
                .Where(type => !typeof(IGameAction).IsAssignableFrom(type))
                .ToList();

            if (nonGameActions.Any())
            {
                var typeNames = string.Join(", ", nonGameActions.Select(t => t.FullName));
                var message = $"'{typeNames}' does not implement {nameof(IGameAction)}";
                return Option.None<Engine, Exception>(new ArgumentOutOfRangeException(nameof(gameActions), message));
            }

            return Option.Some<Engine, Exception>(new Engine(gameActions, rules, players));
        }

        private Engine(
            [NotNull] IImmutableQueue<IGameAction>                                   gameActions,
            [NotNull] IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> rules,
            [NotNull] Players                                                        players)
        {
            this.Rules = rules;
            this.Players = players;
        }

        private Engine(IEngine engine)
        {
            this.Board = engine.Board;
            this.Rules = engine.Rules;
            this.Players = engine.Players;
            this.CurrentPlayerTurn = engine.Players.NextPlayer(engine.CurrentPlayerTurn);
        }

        public Optional.Option<Engine, IEnumerable<string>> Perform(
            [NotNull] IGameAction action)
        {
            var rules = this.Rules.GetValueOrDefault(action.GetType());

            if (rules == null)
            {
                return Option.None<Engine, IEnumerable<string>>(new string[] { $"Engine does not not {action.GetType().FullName} actions" });
            }

            if (!rules.All(rule => rule.Verify(this, action)))
            {
                return Option.None<Engine, IEnumerable<string>>(new string[] {});
            }

            var newEngine = new Engine(action.Perform(this));

            return Option.Some<Engine, IEnumerable<string>>(newEngine);
        }
    }
}
