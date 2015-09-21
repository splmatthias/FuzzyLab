using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace RoboSim
{
    public class ViewService
    {
        public string OpenFileDialog()
        {
            var dialog = new OpenFileDialog();

            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                return dialog.FileName;
            }
            return "";
        }

        public string SaveFileDialog()
        {
            var dialog = new SaveFileDialog();

            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                return dialog.FileName;
            }
            return "";
        }

        public void ShowError(string message)
        {
            
        }
    }
}
