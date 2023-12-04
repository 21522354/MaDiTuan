using MaDiTuan.CustomDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MaDiTuan
{
    public partial class MainWindow : Window
    {
        public int KichThuoc { get; set; }
        public int ToaDoX { get; set; }
        public int ToaDoY { get; set; }
        public Square[][] BanCo { get; set; }

        // thuộc tính để chạy thuật toán
        public int[][] A { get; set; }
        public int[] X { get; set; }
        public int[] Y { get; set; }
        public List<int[][]> tapDapAn { get; set; } 

        //
        public MainWindow()
        {
            InitializeComponent();
            X = new int[] { -2, -2, -1, -1, 1, 1, 2, 2 };
            Y = new int[] { -1, 1, -2, 2, -2, 2, -1, 1 };
            tapDapAn = new List<int[][]>();
        }

        private void BatDau_Click(object sender, RoutedEventArgs e)
        {
            KhoiTao khoiTao = new KhoiTao(this);
            khoiTao.ShowDialog();
            grd_banCo.Children.Clear();
            grd_banCo.ColumnDefinitions.Clear();
            grd_banCo.RowDefinitions.Clear();
            tapDapAn = new List<int[][]>();
            ChayThuatToan();
            tbSoTruongHop.Text = tapDapAn.Count.ToString();
            KhoiTaoBanCo();
        }

        private void ChayThuatToan()
        {
            A = new int[KichThuoc][];

            for (int i = 0; i < KichThuoc; i++)
            {
                A[i] = new int[KichThuoc];
            }
            A[ToaDoX][ToaDoY] = 1;
            diChuyen(2, ToaDoX, ToaDoY);
        }
        private void diChuyen(int i, int x, int y)
        {
            for (int j = 0; j < 8; j++)
            {
                int x1 = x + X[j];
                int y1 = y + Y[j];
                if (x1 >= 0 && x1 < KichThuoc && y1 >= 0 && y1 < KichThuoc && A[x1][y1] == 0)
                {
                    A[x1][y1] = i;
                    if (i == KichThuoc * KichThuoc)
                    {
                        ghiNhanDapAn();
                    }
                    else
                        diChuyen(i + 1, x1, y1);
                    A[x1][y1] = 0;
                }
            }
        }

        private void ghiNhanDapAn()
        {
            int[][] duplicateA = new int[KichThuoc][];
            for (int i = 0; i < KichThuoc; i++)
            {
                duplicateA[i] = new int[KichThuoc];
                for (int j = 0; j < KichThuoc; j++)
                {
                    duplicateA[i][j] = A[i][j];
                }
            }
            tapDapAn.Add(duplicateA);
        }

        private void KhoiTaoBanCo()
        {
            BanCo = new Square[KichThuoc][];
            for (int i = 0; i < KichThuoc; i++)
            {
                BanCo[i] = new Square[KichThuoc];
            }


            for (int i = 0; i < KichThuoc; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                RowDefinition rowDefinition = new RowDefinition();
                grd_banCo.ColumnDefinitions.Add(columnDefinition);
                grd_banCo.RowDefinitions.Add(rowDefinition);
            }

            for(int i = 0;i < KichThuoc; i++)
            {
                for (int j = 0; j < KichThuoc; j++)
                {
                    BanCo[i][j] = new Square();
                    if (((i + j) % 2) == 0)
                    {
                        BanCo[i][j].Background = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        BanCo[i][j].Background = new SolidColorBrush(Colors.Black);
                    }
                    Grid.SetRow(BanCo[i][j], i);
                    Grid.SetColumn(BanCo[i][j], j);
                    grd_banCo.Children.Add(BanCo[i][j]);
                }
            }
            Image img = new Image();
            img.HorizontalAlignment = HorizontalAlignment.Stretch;
            img.VerticalAlignment = VerticalAlignment.Stretch;
            img.Source = new BitmapImage(new Uri("images/icon_chess.png", UriKind.Relative));
            BanCo[ToaDoX][ToaDoY].Children.Add(img);
            BanCo[ToaDoX][ToaDoY].DaDiQua = true;

        }

        private void MinhHoa_click(object sender, RoutedEventArgs e)
        {
            int truongHopI;
            try
            {
                truongHopI = int.Parse(tb_cachDi.Text);
                if(truongHopI < 0 || truongHopI > tapDapAn.Count)
                {
                    MessageBox.Show("Vui lòng điền giá trị cách đi phù hợp");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng điền giá trị cách đi phù hợp");
                return;
            }
            if(tapDapAn.Count == 0 && KichThuoc == 0)
            {
                MessageBox.Show("Vui lòng nhấn nút bắt đầu để khởi tạo chương trình");
                return;
            }
            resetBanCo();
            MinhHoaMa(truongHopI);
        }

        private void resetBanCo()
        {
            for(int i = 0; i< KichThuoc; i++)
            {
                for (int j = 0; j < KichThuoc; j++)
                {
                    BanCo[i][j].DaDiQua = false;
                    if (((i + j) % 2) == 0)
                    {
                        BanCo[i][j].Background = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        BanCo[i][j].Background = new SolidColorBrush(Colors.Black);
                    }
                }
            }
            Image img = new Image();
            img.HorizontalAlignment = HorizontalAlignment.Stretch;
            img.VerticalAlignment = VerticalAlignment.Stretch;
            img.Source = new BitmapImage(new Uri("images/icon_chess.png", UriKind.Relative));
            BanCo[ToaDoX][ToaDoY].Children.Add(img);
            BanCo[ToaDoX][ToaDoY].DaDiQua = true;
        }

        private async void MinhHoaMa(int truongHopI)
        {
            int[][] dapAnThuI = tapDapAn[truongHopI - 1];
            int i = 1;
            while(i != KichThuoc*KichThuoc + 1)
            {
                ChuyenToaDoMa(i, dapAnThuI);
                i++;
                await Task.Delay(1000);
            }
        }

        private async void ChuyenToaDoMa(int k, int[][] dapAnThuI)
        {
            for(int i = 0; i < KichThuoc; i++)
            {
                for(int j = 0; j < KichThuoc; j++)
                {
                    if (dapAnThuI[i][j] == k)
                    {
                        BanCo[i][j].DaDiQua = true;
                        Image img = new Image();
                        img.HorizontalAlignment = HorizontalAlignment.Stretch;
                        img.VerticalAlignment = VerticalAlignment.Stretch;
                        img.Source = new BitmapImage(new Uri("images/icon_chess.png", UriKind.Relative));
                        BanCo[i][j].Children.Add(img);

                        await Task.Delay(1000);
                        BanCo[i][j].Children.Clear();
                        return;
                    }
                }
            }
        }
    }
}
