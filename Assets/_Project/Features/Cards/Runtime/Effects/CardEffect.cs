using UnityEngine;

namespace CardPackBattler.Features.Cards
{
    /// <summary>
    /// Base class for card effects. Create concrete ScriptableObjects that
    /// modify the RoundTally (attack/defense/grapple/expose).
    /// </summary>
    public abstract class CardEffect : ScriptableObject
    {
        [TextArea] [SerializeField] private string description;
        [Tooltip("Lower runs first. Use to order multiple effects on the same card.")]
        [SerializeField] private int priority = 0;

        public string Description => description;
        public int Priority => priority;

        /// <summary>
        /// Apply this effect to the running round tally.
        /// Keep effects deterministic—no random here unless your design allows it.
        /// </summary>
        public abstract void Apply(ref RoundTally tally);
    }

    /// <summary>
    /// The per-round stat contributions our game already tracks.
    /// Sum across all played cards, then resolve at end of round.
    /// </summary>
    [System.Serializable]
    public struct RoundTally
    {
        public int Attack;
        public int Defense;
        public int Grapple;
        public int Expose;

        public static RoundTally Zero => new RoundTally { Attack = 0, Defense = 0, Grapple = 0, Expose = 0 };

        public void Add(RoundTally other)
        {
            Attack  += other.Attack;
            Defense += other.Defense;
            Grapple += other.Grapple;
            Expose  += other.Expose;
        }
    }

    // ——— Example concrete effects you can duplicate ———

    /// <summary>Flat attack boost.</summary>
    [CreateAssetMenu(fileName = "CE_Attack_", menuName = "Card Pack Battler/Effects/Flat Attack", order = 10)]
    public sealed class FlatAttackEffect : CardEffect
    {
        [SerializeField, Min(1)] private int amount = 1;
        public override void Apply(ref RoundTally tally) => tally.Attack += amount;
    }

    /// <summary>Convert some Defense into Attack (simple transformation example).</summary>
    [CreateAssetMenu(fileName = "CE_ConvertDefenseToAttack_", menuName = "Card Pack Battler/Effects/Convert Defense To Attack", order = 11)]
    public sealed class ConvertDefenseToAttackEffect : CardEffect
    {
        [SerializeField, Min(1)] private int ratio = 1;
        public override void Apply(ref RoundTally tally)
        {
            var transferable = Mathf.Max(0, tally.Defense / ratio);
            tally.Defense -= transferable;
            tally.Attack  += transferable;
        }
    }
}
