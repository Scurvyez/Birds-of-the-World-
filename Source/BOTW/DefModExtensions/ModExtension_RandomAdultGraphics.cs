using System.Collections.Generic;
using Verse;

namespace BOTW
{
    /// <summary>
    /// This class allows us to create two separate lists of graphics for an animal in xml.
    /// Based on sex, "Male" and "Female".
    /// </summary>
    public class ModExtension_RandomAdultGraphics : DefModExtension
    {
        public List<GraphicData> adultGraphicsMale; // list of male graphics
        public List<GraphicData> adultGraphicsFemale; // list of female graphics
    }
}
