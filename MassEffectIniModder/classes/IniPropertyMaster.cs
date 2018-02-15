﻿/*=============================================
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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MassEffectIniModder.classes
{
    public abstract partial class IniPropertyMaster : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string PropertyName { get; set; }
        public string FriendlyPropertyName { get; set; }
        public virtual string OriginalValue { get; set; }
        public string Notes { get; set; }
        public string SectionName { get; set; }
        public string SectionFriendlyName { get; set; }
        public string _currentValue;
        public string CurrentValue
        {
            get
            {
                return _currentValue ?? OriginalValue;
            }
            set
            {
                if (_currentValue != value)
                {
                    _currentValue = value;
                    this.OnPropertyChanged("CurrentValue");
                    this.OnPropertyChanged("ResetEnabled");
                    this.OnPropertyChanged("DefaultText");

                }
            }
        }

        public virtual string ValueToWrite
        {
            get { return CurrentValue; }
        }

        public virtual bool ResetEnabled
        {
            get
            {
                return CurrentValue != OriginalValue;
            }
        }

        public string DefaultText
        {
            get
            {
                return ResetEnabled ? "Reset to default" : "Already default";
            }
        }

        public IniPropertyMaster()
        {

        }

        internal void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void LoadCurrentValue(IniFile configIni)
        {
            string val = configIni.Read(PropertyName, SectionName);
            if (val.Contains(";"))
            {
                val = val.Substring(0,val.IndexOf(';')).Trim();
            }
            if (val != null)
            {
                CurrentValue = val;
            }
        }
    }
}
