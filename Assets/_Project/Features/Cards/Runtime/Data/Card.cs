using System.Collections.Generic;
using UnityEngine;

namespace CardPackBattler.Features.Cards
{
    /// <summary>
    /// Data-only definition of a card. One asset per card.
    /// Combine with CardEffect assets to compute round deltas.
    /// </summary>
    [CreateAssetMenu(fileName = "Card_", menuName = "Card Pack Battler/Card", order = 0)]
    public class Card : ScriptableObject
    {
        [Header("Identity")]
        [Tooltip("Unique, stable ID used by save/load and balance tools.")]
        [SerializeField] private string id = "CARD_ID";
        [SerializeField] private string displayName = "New Card";
        [TextArea] [SerializeField] private string rulesText;

        [Header("Presentation")]
        [SerializeField] private Sprite artwork;
        [SerializeField] private Rarity rarity = Rarity.Common;
        [SerializeField] private Keyword keywords = 0;

        [Header("Costs / Tunables")]
        [Min(0)] [SerializeField] private int cost = 0;

        [Header("Effects (evaluated in order)")]
        [SerializeField] private List<CardEffect> effects = new List<CardEffect>();

        // ——— Public API ———
        public string Id => id;
        public string DisplayName => displayName;
        public string RulesText => rulesText;
        public Sprite Artwork => artwork;
        public Rarity CardRarity => rarity;
        public Keyword Keywords => keywords;
        public int Cost => cost;
        public IReadOnlyList<CardEffect> Effects => effects;

        /// <summary>
        /// Compute the total stat deltas this card contributes for the round.
        /// (Your round resolver can sum these across all played cards.)
        /// </summary>
        public RoundTally ComputeTally()
        {
            var tally = RoundTally.Zero;
            for (int i = 0; i < effects.Count; i++)
            {
                var e = effects[i];
                if (e != null) e.Apply(ref tally);
            }
            return tally;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Soft guardrails for early content creation.
            if (string.IsNullOrWhiteSpace(id))
                id = name; // fallback to asset name until you add a proper ID validator

            // Ensure effects list has no null holes.
            effects.RemoveAll(e => e == null);
        }
#endif
    }

    public enum Rarity { Common, Uncommon, Rare, Epic, Legendary }

    /// <summary>
    /// Card keywords as bit flags (combine multiples).
    /// </summary>
    [System.Flags]
    public enum Keyword
    {
        None     = 0,
        Grapple  = 1 << 0,
        Expose   = 1 << 1,
        Shield   = 1 << 2,
        Pierce   = 1 << 3,
        // Add more as design evolves
    }
}
