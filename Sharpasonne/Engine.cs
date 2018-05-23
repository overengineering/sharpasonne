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
    using RuleMap = ImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>>;

    // TODO: Consider rename.
    public class Engine : IEngine
    {
        public Board Board { get; }

        /// <inheritdoc />
        public Players Players { get; }

        /// <inheritdoc />
        public int CurrentPlayerTurn { get; } = 1;

        /// <summary>
        /// Map of IGameAction to rules that apply to that IGameAction.
        /// Keys must be assignable from IGameAction.
        /// </summary>
        /// <remarks>Type system can't enforce this itself.</remarks>
        public IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> Rules { get; }
            = RuleMap.Empty;

        /// <summary>
        /// Starts a Game.
        /// </summary>
        /// <param name="rules">Must provide list for every action to be used by
        /// <see cref="Perform"/>.</param>
        public Engine(
            [NotNull] RuleMapBuilder               rules,
            [NotNull] Players                      players)
            : this(rules.RuleMap, players, new Board())
        {
        }

        /// <summary>
        /// Continues a Game.
        /// </summary>
        private Engine(
            [NotNull] IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> rules,
            [NotNull] Players                                                        players,
            [NotNull] Board                                                          board,
                      int                                                            currentPlayerTurn = 1)
        {
            this.Rules             = rules;
            this.Players           = players;
            this.Board             = board;
            this.CurrentPlayerTurn = currentPlayerTurn;
        }

        private static Engine NextTurn(IEngine engine)
        {
            var nextTurn = new Engine(
                engine.Rules,
                engine.Players,
                engine.Board,
                engine.Players.NextPlayer(engine.CurrentPlayerTurn));

            return nextTurn;
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

            var newEngine = Engine.NextTurn(action.Perform(this));

            return Option.Some<Engine, IEnumerable<string>>(newEngine);
        }
    }
}
