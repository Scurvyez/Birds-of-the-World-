using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace BOTW
{
    public class PawnRenderNode_AnimalRandomizedGraphics : PawnRenderNode_AnimalPart
    {
        public PawnRenderNode_AnimalRandomizedGraphics(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree) : base(pawn, props, tree)
        {
            // don't need anything in our ctor
        }

        public override Graphic GraphicFor(Pawn pawn)
        {
            Graphic graphic = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.Graphic;
            if (!pawn.TryGetComp(out CompAnimalColorRandomizer comp)) return base.GraphicFor(pawn);
            
            // Ensure the current life stage is the last one available (adults)
            if (pawn.ageTracker.CurLifeStageIndex == pawn.RaceProps.lifeStageAges.Count - 1)
            {
                var modExt = pawn.def.GetModExtension<ModExtension_RandomAdultGraphics>();

                if (modExt != null)
                {
                    List<AlternateGraphic> graphicList = pawn.gender == Gender.Male ? modExt.adultGraphicsMale : modExt.adultGraphicsFemale;
                    if (!graphicList.NullOrEmpty())
                    {
                        AlternateGraphic selectedGraphic = graphicList.RandomElement();
                        return GraphicDatabase.Get<Graphic_Multi>(selectedGraphic.graphicData.texPath, ShaderDatabase.CutoutComplex, 
                            selectedGraphic.graphicData.drawSize, comp.newColor, comp.newColorTwo);
                    }
                }
                else
                {
                    graphic = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.Graphic;
                    return GraphicDatabase.Get<Graphic_Multi>(graphic.path, ShaderDatabase.CutoutComplex, 
                        graphic.drawSize, comp.newColor, comp.newColorTwo);
                }
            }
            return GraphicDatabase.Get<Graphic_Multi>(graphic.path, ShaderDatabase.CutoutComplex, graphic.drawSize, comp.newColor, comp.newColorTwo);
        }
    }
}
