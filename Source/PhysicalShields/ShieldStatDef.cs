using System;
using RimWorld;
using Verse;
using PhyiscalShields;
using UnityEngine;

namespace ShieldStatDef
{
    public class ShieldStatDef : StatDef
    {

        public ShieldStatDef(List ShieldStatFactors, List ShieldParts, List ShieldSkillNeedFactors) : base(statFactors, parts, skillNeedFactors);

        public StatWoker ShieldWork = base.Worker;
        public Type shieldWorkerClass = base.workerClass;
        public bool shieldShowOnPawns = base.showOnPawns;

   




    }//end ShieldStatDef

}//end Namespace




