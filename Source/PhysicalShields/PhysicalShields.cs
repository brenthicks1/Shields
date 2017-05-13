using System;
using RimWorld;
using Verse;

namespace PhysicalShields
{
    [StaticConstructOnStartup]

    public class PhysicalShields : Apparel
    {
        private const float MinDrawSize = 1.2f;

        private const float MaxDrawSize = 1.55f;

        private float Durability;

        private int ticksToBreak = 0;

        private float DurabilityLossPerDamage = 0.025f;

        private float ApparelScorePerDurabilityMax = 0.025f;

        private float DurabilityMax
        {
            get
            {
                return this.GetStatValue(ShieldStatDefof.PhysicalShieldDurabilityMax, true);
            }
        }

        private float DurabilityPerTick
        {
            get
            {
                return this.GetStatValue(ShieldStatDefOf.PhysicalShieldDurabilityBase, true);
            }
        }

        public float Durability
        {
            get
            {
                return this.Durability;
            }
        }


        public ShieldState ShieldState
        {
            get
            {
                if (this.ticksToBreak > 0)
                {
                    return ShieldState.Usable;
                }
                else if (this.ticksToBreak <= 0 )
                {
                    return ShieldState.Broken;
                }
                return ShieldState.Active;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.LookValue<float>(ref this.Durability, "durability", 0f, false);
            Scribe_Values.LookValue<int>(ref this.ticksToBreak, "ticksToBreak", -1, false);
        }
        
        [DebuggerHidden]
        public override IEnumerable<apparel>



    }
}