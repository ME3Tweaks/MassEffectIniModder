using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassEffectIniModder.classes
{
    public class IniSection
    {
        public string SectionName { get; set; }
        public List<IniPropertyEnum> EnumProperties { get; set; }
        public List<IniPropertyBool> BoolProperties { get; set; }
        public List<IniPropertyInt> IntProperties { get; set; }
        public List<IniPropertyFloat> FloatProperties { get; set; }
        public List<IniPropertyName> NameProperties { get; set; }
        internal List<IniPropertyMaster> GetAllProperties()
        {
            List<IniPropertyMaster> all = new List<IniPropertyMaster>();
            all.AddRange(EnumProperties);
            all.AddRange(BoolProperties);
            all.AddRange(IntProperties);
            all.AddRange(FloatProperties);
            all.AddRange(NameProperties);
            return all;
        }

        internal void PropogateOwnership()
        {
            foreach (IniPropertyMaster prop in GetAllProperties())
            {
                prop.SectionFriendlyName = SectionName; //temp
            }
        }
    }
}
