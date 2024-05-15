using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace BOTW
{
    public class CompAnimalColorRandomizer : ThingComp
    {
        public CompProperties_AnimalColorRandomizer Props => (CompProperties_AnimalColorRandomizer)props;

        public Color newColor;
        public Color newColorTwo;
        private readonly float alpha = 1f;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            if (!respawningAfterLoad && parent is Pawn pawn)
            {
                if (pawn.gender == Gender.Male)
                {
                    newColor = new Color(Props.maleRRangeOne.RandomInRange, Props.maleGRangeOne.RandomInRange, Props.maleBRangeOne.RandomInRange, alpha);
                    newColorTwo = new Color(Props.maleRRangeTwo.RandomInRange, Props.maleGRangeTwo.RandomInRange, Props.maleBRangeTwo.RandomInRange, alpha);
                }
                else if (pawn.gender == Gender.Female)
                {
                    newColor = new Color(Props.femaleRRangeOne.RandomInRange, Props.femaleGRangeOne.RandomInRange, Props.femaleBRangeOne.RandomInRange, alpha);
                    newColorTwo = new Color(Props.femaleRRangeTwo.RandomInRange, Props.femaleGRangeTwo.RandomInRange, Props.femaleBRangeTwo.RandomInRange, alpha);
                }
                else
                {
                    newColor = Color.white;
                    newColorTwo = Color.white;
                }
                pawn.Drawer.renderer.SetAllGraphicsDirty();
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref newColor, "newColor");
            Scribe_Values.Look(ref newColorTwo, "newColorTwo");
        }
    }

    public class CompProperties_AnimalColorRandomizer : CompProperties
    {
        public FloatRange maleRRangeOne;
        public FloatRange maleGRangeOne;
        public FloatRange maleBRangeOne;
        public FloatRange maleRRangeTwo;
        public FloatRange maleGRangeTwo;
        public FloatRange maleBRangeTwo;

        public FloatRange femaleRRangeOne;
        public FloatRange femaleGRangeOne;
        public FloatRange femaleBRangeOne;
        public FloatRange femaleRRangeTwo;
        public FloatRange femaleGRangeTwo;
        public FloatRange femaleBRangeTwo;

        public CompProperties_AnimalColorRandomizer()
        {
            compClass = typeof(CompAnimalColorRandomizer);
        }

        public override IEnumerable<string> ConfigErrors(ThingDef parentDef)
        {
            foreach (var error in base.ConfigErrors(parentDef))
            {
                yield return error;
            }
            if (maleRRangeOne == null)
            {
                BOTWLog.Warning("[CompProperties_AnimalColorRandomizer] maleRRangeOne is null. Please provide a range.");
            }
            if (maleGRangeOne == null)
            {
                BOTWLog.Warning("[CompProperties_AnimalColorRandomizer] maleGRangeOne is null. Please provide a range.");
            }
            if (maleBRangeOne == null)
            {
                BOTWLog.Warning("[CompProperties_AnimalColorRandomizer] maleBRangeOne is null. Please provide a range.");
            }
            if (maleRRangeTwo == null)
            {
                BOTWLog.Warning("[CompProperties_AnimalColorRandomizer] maleRRangeTwo is null. Please provide a range.");
            }
            if (maleGRangeTwo == null)
            {
                BOTWLog.Warning("[CompProperties_AnimalColorRandomizer] maleGRangeTwo is null. Please provide a range.");
            }
            if (maleBRangeTwo == null)
            {
                BOTWLog.Warning("[CompProperties_AnimalColorRandomizer] maleBRangeTwo is null. Please provide a range.");
            }
            if (femaleRRangeOne == null)
            {
                BOTWLog.Warning("[CompProperties_AnimalColorRandomizer] femaleRRangeOne is null. Please provide a range.");
            }
            if (femaleGRangeOne == null)
            {
                BOTWLog.Warning("[CompProperties_AnimalColorRandomizer] femaleGRangeOne is null. Please provide a range.");
            }
            if (femaleBRangeOne == null)
            {
                BOTWLog.Warning("[CompProperties_AnimalColorRandomizer] femaleBRangeOne is null. Please provide a range.");
            }
            if (femaleRRangeTwo == null)
            {
                BOTWLog.Warning("[CompProperties_AnimalColorRandomizer] femaleRRangeTwo is null. Please provide a range.");
            }
            if (femaleGRangeTwo == null)
            {
                BOTWLog.Warning("[CompProperties_AnimalColorRandomizer] femaleGRangeTwo is null. Please provide a range.");
            }
            if (femaleBRangeTwo == null)
            {
                BOTWLog.Warning("[CompProperties_AnimalColorRandomizer] femaleBRangeTwo is null. Please provide a range.");
            }
        }
    }
}
