using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;

namespace MaDiTuan.CustomDialog
{
    public class Square:Grid
    {
        private bool _DaDiQua;
        public bool DaDiQua
        {
            get { return _DaDiQua;}
            set {
                _DaDiQua = value;
                if (value)
                {
                    this.Background = new SolidColorBrush(Colors.GreenYellow);
                }
            }
        }
    }
}
