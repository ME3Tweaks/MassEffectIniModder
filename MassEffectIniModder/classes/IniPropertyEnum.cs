/*=============================================
Copyright (c) 2018 ME3Tweaks
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
=============================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MassEffectIniModder.classes
{
    public class IniPropertyEnum : IniPropertyMaster
    {
        public List<IniPropertyEnumValue> Choices { get; set; }
        public IniPropertyEnum()
        {

        }

        private int _selectedIndex = 0;
        public int CurrentSelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    this.OnPropertyChanged("CurrentSelectedIndex");
                    this.OnPropertyChanged("ResetEnabled");
                    this.OnPropertyChanged("DefaultText");

                }
            }
        }

        public override bool ResetEnabled
        {
            get
            {
                return CurrentSelectedIndex != 0;
            }
        }

        public override string ValueToWrite
        {
            get { return Choices[CurrentSelectedIndex].IniValue; }
        }

        public override void LoadCurrentValue(IniFile configIni)
        {
            string val = configIni.Read(PropertyName, SectionName);
            int index = -1;
            bool indexFound = false;
            foreach (IniPropertyEnumValue enumval in Choices)
            {
                index++;
                if (enumval.IniValue.Equals(val, StringComparison.InvariantCultureIgnoreCase))
                {
                    indexFound = true;
                    break;
                }
            }
            if (!indexFound)
            {
                //user has their own item
                IniPropertyEnumValue useritem = new IniPropertyEnumValue();
                useritem.FriendlyName = useritem.IniValue = val;
                Choices.Add(useritem);
                CurrentSelectedIndex = Choices.Count - 1;
            } else
            {
                CurrentSelectedIndex = index;
            }
        }
    }
}
