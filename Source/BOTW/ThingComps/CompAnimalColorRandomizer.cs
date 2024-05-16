using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace BOTW
{
    /// <summary>
    /// This class allows for semi-random coloration via the CutoutComplex shader on an animals' graphics.
    /// </summary>
    public class CompAnimalColorRandomizer : ThingComp
    {
        // reference to our comp properties which is what we set in xml with the respective color channel values
        public CompProperties_AnimalColorRandomizer Props => (CompProperties_AnimalColorRandomizer)props;

        public Color newColor; // the color to be applied for red areas of our mask texture
        public Color newColorTwo; // the color to be applied for green areas of our mask texture
        private readonly float alpha = 1f; // the alpha channel value of our two colors (final opacity)

        /// <summary>
        /// Called when we spawn our animal. The workhorse of this class.
        /// </summary>
        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            
            // if we are not respawning the animal after we load a save and if the animal is in fact a "Pawn"...
            if (!respawningAfterLoad && parent is Pawn pawn)
            {
                // depending on the animals' sex, we set our two colors for use with the CutoutComplex mask texture
                // as a safety check, if the sex is "none" just fallback to pure white for both colors
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
                
                // we "dirty" the graphics once the colors have been set
                // if we don't, the colors do not get applied ;)
                pawn.Drawer.renderer.SetAllGraphicsDirty();
            }
        }

        /// <summary>
        /// Called on save/load of a game.
        /// While these colors most likely won't change between that time, save them anyway.
        /// </summary>
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref newColor, "newColor");
            Scribe_Values.Look(ref newColorTwo, "newColorTwo");
        }
    }

    /// <summary>
    /// This is the class we reference in xml to define our random colors for our animal.
    /// </summary>
    public class CompProperties_AnimalColorRandomizer : CompProperties
    {
        // the two male colors
        public FloatRange maleRRangeOne;
        public FloatRange maleGRangeOne;
        public FloatRange maleBRangeOne;
        public FloatRange maleRRangeTwo;
        public FloatRange maleGRangeTwo;
        public FloatRange maleBRangeTwo;

        // the two female colors
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

        /// <summary>
        /// Config errors! :)
        /// Any of these warning messages will pop in our log the second our game loads up if any of our xml color values are not defined.
        /// </summary>
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
