using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassEffectIniModder.classes
{
    public class IniPropertyEnum : IniPropertyMaster
    {
        public List<IniPropertyEnumValue> Choices { get; set; }
        public IniPropertyEnum()
        {

        }
    }
}
