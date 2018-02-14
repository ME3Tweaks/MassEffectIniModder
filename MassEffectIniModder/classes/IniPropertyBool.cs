using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassEffectIniModder.classes
{
    public class IniPropertyBool : IniPropertyMaster
    {
        private int _selectedIndex = 0;
        public int CurrentSelectedBoolIndex
        {
            get
            {
                return CurrentValue.ToLower() == "true" ? 0 : 1;
            }
            set
            {
                _selectedIndex = value;
            }
        }
    }
}
