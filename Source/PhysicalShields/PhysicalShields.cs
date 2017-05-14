using System;
using RimWorld;
using Verse;
using UnityEngine;

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
                return this.GetStatValue(StatDefOf.MaxHitPoints, true);
            }
        }

        public override bool CheckPreAbsorbDamage(DamageInfo dinfo)
        {
            if (this.ShieldState == ShieldState.Usabel && ((dinfo.Instigator != null && !dinfo.Instigator.Position.AdjacentTo8WayOrInside(this.wearer.Position)) || dinfo.Def.isExplosive))
            {
                if (dinfo.Instigator != null)
                {
                    AttachableThing attachableThing = dinfo.Instigator as AttachableThing;
                    if (attachableThing != null && attachableThing.parent == this.wearer)
                    {
                        return false;
                    }
                }
                this.Durability -= (float)dinfo.Amount * this.DurabilityLossPerDamage;
                if (this.Durability < 0f)
                {
                    this.Break();
                }
                else
                {
                    this.AbsorbedDamage(dinfo);
                }
                return true;
            }
            return false;
        }

        public ShieldState ShieldState
        {
            get
            {
                if (this.ticksToBreak > 0)
                {
                    return ShieldState.Usable;
                }
                else if (this.ticksToBreak <= 0)
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

        public override void Tick()
        {
            base.Tick();
            if (this.wearer == null)
            {
                this.energy = 0f;
                return;
            }
            if (this.ShieldState == ShieldState.Damaged)
            {
                this.ticksToBreak--; 
                if (this.ticksToBreak <= 0)
                {
                    this.Broken();
                }
                else
                {
                    this.Usable();
                }
            }
        }
    }//end class PhysicalShield
}//end namespace PhysicalShield