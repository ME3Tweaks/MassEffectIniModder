using MassEffectIniModder.classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace MassEffectIniModder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string configFileFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\BioWare\Mass Effect\Config";
            if (Directory.Exists(configFileFolder))
            {
                List<KeyValuePair<ListView, string>> loadingMap = new List<KeyValuePair<ListView, string>>();
                loadingMap.Add(new KeyValuePair<ListView, string>(ListView_BIOEngine, "BioEngineMapping.xml"));
                loadingMap.Add(new KeyValuePair<ListView, string>(ListView_BIOGame, "BioGameMapping.xml"));
                loadingMap.Add(new KeyValuePair<ListView, string>(ListView_BIOParty, "BioPartyMapping.xml"));

                foreach (KeyValuePair<ListView, string> kp in loadingMap)
                {
                    if (File.Exists(kp.Value))
                    {
                        XElement rootElement = XElement.Load(kp.Value);

                        var linqlist = (from e in rootElement.Elements("Section")
                                        select new IniSection
                                        {
                                            SectionName = (string)e.Attribute("name"),
                                            BoolProperties = e.Elements("boolproperty").Select(f => new IniPropertyBool
                                            {
                                                PropertyName = (string)f.Attribute("propertyname"),
                                                FriendlyPropertyName = (string)f.Attribute("friendlyname"),
                                                Notes = (string)f.Attribute("notes"),
                                                OriginalValue = (string)f.Value

                                            }).ToList(),
                                            IntProperties = e.Elements("intproperty").Select(f => new IniPropertyInt
                                            {
                                                PropertyName = (string)f.Attribute("propertyname"),
                                                FriendlyPropertyName = (string)f.Attribute("friendlyname"),
                                                Notes = (string)f.Attribute("notes"),
                                                OriginalValue = (string)f.Value
                                            }).ToList(),
                                            FloatProperties = e.Elements("floatproperty").Select(f => new IniPropertyFloat
                                            {
                                                PropertyName = (string)f.Attribute("propertyname"),
                                                FriendlyPropertyName = (string)f.Attribute("friendlyname"),
                                                Notes = (string)f.Attribute("notes"),
                                                OriginalValue = (string)f.Value
                                            }).ToList(),
                                            EnumProperties = e.Elements("enumproperty").Select(f => new IniPropertyEnum
                                            {
                                                PropertyName = (string)f.Attribute("propertyname"),
                                                FriendlyPropertyName = (string)f.Attribute("friendlyname"),
                                                Notes = (string)f.Attribute("notes"),
                                                Choices = f.Elements("enumvalue").Select(g => new IniPropertyEnumValue
                                                {
                                                    FriendlyName = (string)g.Attribute("friendlyname"),
                                                    IniValue = (string)f.Value
                                                }).ToList()
                                            }).ToList(),
                                            NameProperties = e.Elements("nameproperty").Select(f => new IniPropertyName
                                            {
                                                PropertyName = (string)f.Attribute("propertyname"),
                                                FriendlyPropertyName = (string)f.Attribute("friendlyname"),
                                                Notes = (string)f.Attribute("notes"),
                                                OriginalValue = (string)f.Value
                                            }).ToList(),

                                        }).ToList();

                        List<IniPropertyMaster> testItems = new List<IniPropertyMaster>();
                        foreach (IniSection sec in linqlist)
                        {
                            sec.PropogateOwnership();
                            testItems.AddRange(sec.GetAllProperties());
                        }
                        kp.Key.ItemsSource = testItems;
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(kp.Key.ItemsSource);
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("SectionFriendlyName");
                        view.GroupDescriptions.Add(groupDescription);
                    }
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
