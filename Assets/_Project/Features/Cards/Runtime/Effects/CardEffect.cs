using UnityEngine;

namespace CardPackBattler.Features.Cards
{
    /// <summary>
    /// Base class for card effects. Create concrete ScriptableObjects that
    /// modify the RoundTally (attack/defense/grapple/expose).
    /// </summary>
    [CreateAssetMenu(fileName = "CardEffect_", menuName = "Card Pack Battler/CardEffect", order = 1)]
    public class CardEffect : ScriptableObject
    {
        [Header("Identity")]
        [Tooltip("Unique, stable ID used by save/load and balance tools.")]
        [SerializeField] private string id = "CARDEFFECT_ID";
        [SerializeField] private string displayName = "New CardEffect";
        [SerializeField] private string keyword = "Keyword";
        [TextArea][SerializeField] private string effectText;
        [SerializeField] private int priority = 0;
        [SerializeField] private int value = 0;

        public string DisplayName => displayName;
        public string Keyword => keyword;
        public string EffectText => effectText;
        public int Priority => priority;
        public int Value => value;
        
        
    }
}
