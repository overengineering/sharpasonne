using System;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using Sharpasonne.GameActions;

namespace Sharpasonne.Rules
{
    /// <summary>
    /// Builds map of IGameActions to rules that apply to it.
    /// </summary>
    /// <remarks>Exists due to Type System limitations that prevent restricting
    /// the key to match the <see cref="IGameAction"/> in the value.</remarks>
    public class RuleMapBuilder
    {
        public ImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> RuleMap { get; }
            = ImmutableDictionary.Create<Type, IImmutableList<IRule<IGameAction>>>();

        /// <inheritdoc cref="RuleMapBuilder"/>
        public RuleMapBuilder()
        {
        }

        private RuleMapBuilder([NotNull] ImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> ruleMap)
        {
            this.RuleMap = ruleMap;
        }
        
        public RuleMapBuilder Set<TGameAction>([NotNull] IImmutableList<IRule<TGameAction>> rules)
            where TGameAction : IGameAction
        {
            return new RuleMapBuilder(
                this.RuleMap.SetItem(
                    typeof(TGameAction),
                    rules.Cast<IRule<IGameAction>>().ToImmutableList()
                )
            );
        }
    }
}