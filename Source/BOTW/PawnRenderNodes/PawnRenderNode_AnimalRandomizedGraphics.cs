using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace BOTW
{
    /// <summary>
    /// This class allows for animals with specific ThingComps and DefModExtensions to be generated with semi-random
    /// and realistic graphics based on sex and life stage.
    /// </summary>
    public class PawnRenderNode_AnimalRandomizedGraphics : PawnRenderNode_AnimalPart
    {
        private readonly ModExtension_RandomAdultGraphics _modExt; // reference to our mod extension in xml
        
        /// <summary>
        /// Our class' constructor, we set our readonly fields here, and we never touch them again.
        /// We just want to know what the values are.
        /// </summary>
        public PawnRenderNode_AnimalRandomizedGraphics(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree) : base(pawn, props, tree)
        {
            _modExt = pawn.def.GetModExtension<ModExtension_RandomAdultGraphics>();
        }
        
        /// <summary>
        /// Here, we replace the default Graphic for the animal if we need to.
        /// </summary>
        public override Graphic GraphicFor(Pawn pawn)
        {
            Graphic defaultGraphic = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.Graphic; // the animals default graphic
            Vector2 finalDrawSize = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.drawSize; // default graphic draw size
            
            // if our animal doesn't have the random colors comp, just use the default graphic
            if (!pawn.TryGetComp(out CompAnimalColorRandomizer comp)) 
                return base.GraphicFor(pawn);
            
            // if our animal does have the random colors comp...
            // if the animal is not an adult (last life stage presumably) OR if the mod extension doesn't exist...
            // just use the default graphic with our random colors applied
            // lists always start at 0, hence the "- 1" to ensure we get the last element in the list and not the second last
            if (pawn.ageTracker.CurLifeStageIndex != pawn.RaceProps.lifeStageAges.Count - 1 || _modExt == null)
                return GraphicDatabase.Get<Graphic_Multi>(defaultGraphic.path, ShaderDatabase.CutoutComplex,
                    finalDrawSize, comp.newColor, comp.newColorTwo);
            
            // if our animal is an adult and the mod extension does exist...
            // create a new list of graphics to pick from based on sex and the graphics we define in the mod extension
            // we assume the animal can only ever be male or female here and not "none"
            List<GraphicData> finalGraphicsList = new List<GraphicData>(pawn.gender == Gender.Male ? _modExt.adultGraphicsMale : _modExt.adultGraphicsFemale);
            
            // if our animals' sex is male... add the default male graphic to our list from above for extra options
            // if our animal is female... same thing
            // again, we assume the animal can never be "none" for the sex but if it is, don't do anything
            switch (pawn.gender)
            {
                case Gender.Male:
                    finalGraphicsList.Add(pawn.kindDef.lifeStages.Last().bodyGraphicData);
                    break;
                case Gender.Female:
                    finalGraphicsList.Add(pawn.kindDef.lifeStages.Last().femaleGraphicData);
                    break;
                case Gender.None:
                    break;
            }
            
            // if our list of available graphics to pick from is empty or null for some reason...
            // just use the default graphic with our random colors applied
            if (finalGraphicsList.NullOrEmpty())
                return GraphicDatabase.Get<Graphic_Multi>(defaultGraphic.path, ShaderDatabase.CutoutComplex,
                    finalDrawSize, comp.newColor, comp.newColorTwo);
            
            // almost done, now let's pick a random graphic from our list!
            GraphicData selectedGraphic = finalGraphicsList.RandomElement();
            
            // if the graphic we picked has a valid texPath defined in xml...
            if (selectedGraphic?.texPath != null)
            {
                // if our graphic should be grayscale...
                // convert our colors for the mask texture to grayscale and apply them
                if (_modExt.grayscaleAdultGraphics)
                {
                    Color gsColorOne = new Color(comp.newColor.grayscale, comp.newColor.grayscale, comp.newColor.grayscale);
                    Color gsColorTwo = new Color(comp.newColorTwo.grayscale, comp.newColorTwo.grayscale, comp.newColorTwo.grayscale);
                    
                    return GraphicDatabase.Get<Graphic_Multi>(selectedGraphic.texPath, ShaderDatabase.CutoutComplex, 
                        finalDrawSize, gsColorOne, gsColorTwo
                    );
                }
                
                // use the graphic we picked with our random colors applied
                return GraphicDatabase.Get<Graphic_Multi>(selectedGraphic.texPath, ShaderDatabase.CutoutComplex, 
                    finalDrawSize, comp.newColor, comp.newColorTwo
                );
            }
            
            // if everything fails none of the above situations are true, fallback to our animals' default graphic
            // (with our random colors applied)
            return GraphicDatabase.Get<Graphic_Multi>(defaultGraphic.path, ShaderDatabase.CutoutComplex,
                finalDrawSize, comp.newColor, comp.newColorTwo);
        }
    }
}
