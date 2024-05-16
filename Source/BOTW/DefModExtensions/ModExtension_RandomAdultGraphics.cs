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
        public bool grayscaleAdultGraphics; // false by default
        public List<GraphicData> adultGraphicsMale = null; // list of male graphics
        public List<GraphicData> adultGraphicsFemale = null; // list of female graphics
    }
}
