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

        [Header("Effects (evaluated in order)")]
        [SerializeField] public List<CardEffect> effects = new List<CardEffect>();
        [SerializeField] public List<int> CardEffectValues = new List<int>();

        // ——— Public API ———
        public string Id => id;
        public string DisplayName => displayName;
        public string RulesText => rulesText;
        public Sprite Artwork => artwork;
        public IReadOnlyList<CardEffect> Effects => effects;

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Soft guardrails for early content creation.
            if (string.IsNullOrWhiteSpace(id))
                id = name; // fallback to asset name until you add a proper ID validator

            // Ensure effects list has no null holes.
            effects.RemoveAll(e => e == null);

            // Grow or shrink CardEffectValues to match effects list
            if (CardEffectValues.Count < effects.Count)
            {
                while (CardEffectValues.Count < effects.Count)
                CardEffectValues.Add(0);
            }
            else if (CardEffectValues.Count > effects.Count)
            {
                while (CardEffectValues.Count > effects.Count)
                CardEffectValues.RemoveRange(effects.Count, CardEffectValues.Count - effects.Count);
            }
        }
#endif
    }
}
