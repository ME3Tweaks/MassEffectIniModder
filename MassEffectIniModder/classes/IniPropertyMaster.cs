using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassEffectIniModder.classes
{
    public abstract class IniPropertyMaster
    {
        public string PropertyName { get; set; }
        public string FriendlyPropertyName { get; set; }
        public string OriginalValue { get; set; }
        public string Notes { get; set; }
        public string _currentValue;
        public string SectionFriendlyName { get; set; }
        public string CurrentValue
        {
            get
            {
                return _currentValue ?? OriginalValue;
            }
            set { _currentValue = value; }
        }

        public IniPropertyMaster()
        {

        }

        
    }
}
