using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFMDesctop.Models.Services
{
    internal static class SettingService
    {

        public static string CurrentTheme
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Properties.Settings.Default.CurrentTheme))
                {
                    Properties.Settings.Default.CurrentTheme = "Ligth";
                    Properties.Settings.Default.Save();
                }

                return Properties.Settings.Default.CurrentTheme;
            }
            set
            {
                if (value != "Ligth" && value != "Dark")
                    throw new ArgumentException("Не верный аргумент темы");
                Properties.Settings.Default.CurrentTheme = value;
                Properties.Settings.Default.Save();
            }
        }
    }   
}
