using System;
using System.Collections.Generic;
using System.Drawing.Design;
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
using System.Windows.Shapes;

namespace MaDiTuan.CustomDialog
{
    /// <summary>
    /// Interaction logic for KhoiTao.xaml
    /// </summary>
    public partial class KhoiTao : Window
    {
        public MainWindow mainWindow1 { get; set; }
        public KhoiTao()
        {
            InitializeComponent();
        }
        public KhoiTao(MainWindow mainWindow)
        {
            InitializeComponent();
            mainWindow1 = mainWindow;
            btn_xacNhan.Click += Btn_xacNhan_Click;
        }

        private void Btn_xacNhan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainWindow1.KichThuoc = int.Parse(KichThuoc.Text);
                mainWindow1.ToaDoX = int.Parse(ToaDoX.Text);
                mainWindow1.ToaDoY = int.Parse(ToaDoY.Text);
                if(mainWindow1.ToaDoX < 0 || mainWindow1.ToaDoX >= mainWindow1.KichThuoc)
                {
                    MessageBox.Show("Vui lòng nhập tọa độ X hợp lệ");
                    return;
                }
                if (mainWindow1.ToaDoY < 0 || mainWindow1.ToaDoY >= mainWindow1.KichThuoc)
                {
                    MessageBox.Show("Vui lòng nhập tọa độ Y hợp lệ");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập các thông tin là số nguyên dương");
                return;
            }
            this.Close();
        }
    }
}
