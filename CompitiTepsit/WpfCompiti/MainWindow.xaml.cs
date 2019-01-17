using System;
using System.Collections.Generic;
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
using System.Threading;

namespace WpfCompiti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private CancellationTokenSource token = new CancellationTokenSource();
        private CancellationTokenSource token2 = new CancellationTokenSource();
        private CancellationTokenSource token3 = new CancellationTokenSource();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (token == null)
            {
                token = new CancellationTokenSource();
            }
            Task.Factory.StartNew(() => Conteggio(token,1000,lblResult));
        }
        private void Conteggio (CancellationTokenSource token, int max, Label lblRis)
        {
            for (int i =0;i<= max; i++)
            {
                Dispatcher.Invoke(() => AggiornaUI(i, lblRis));
                Thread.Sleep(1000);
                Dispatcher.Invoke(() => AggiornaUI1(lblRis));
                Thread.Sleep(1000);
                if (token.Token.IsCancellationRequested)
                    break;
            }
            Dispatcher.Invoke(Finito);
        }
        private void Finito()
        {
            lblResult.Content= "Ho finito";
        }
        private void AggiornaUI(int i, Label lblRis)
        {
            lblResult.Content = $"Sto contando {i.ToString()}";
        }
        private void AggiornaUI1(Label lblRis)
        {
            lblResult.Content = "Sto aspettando";
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            if (token != null)
            {
                token.Cancel();
                token = null;
            }
        }

        private void BtnStart1_Click(object sender, RoutedEventArgs e)
        {
            int max = Convert.ToInt32(txtMax.Text);
            for (int i = 0; i <= max; i++)
            {
                Dispatcher.Invoke(() => UPdateUI4(i));
                token = new CancellationTokenSource();
            }
            Task.Factory.StartNew(() => Conteggio(token2, max, lblResult1));
        }
        private void UPdateUI4(int i)
        {
            lblResult1.Content = i.ToString();
        }

        private void BtnStop1_Click(object sender, RoutedEventArgs e)
        {
            if (token2 != null)
            {
                token2.Cancel();
                token2 = null;
            }
        }

        private void BtnStart2_Click(object sender, RoutedEventArgs e)
        {
            int max = Convert.ToInt32(txtMax1.Text);
            int delay = Convert.ToInt32(txtDelay.Text);
            Task.Factory.StartNew(() => DoWork(max, delay, token3));

        }

        private void DoWork(int max, int delay, CancellationTokenSource token3)
        {
            if (token3 != null)
            {
                token3 = new CancellationTokenSource();

            }
            for (int i = 0; i <= max; i++)
            {
                Dispatcher.Invoke(() => UpdateUI2(i));
                Thread.Sleep(delay);
                if (token3.Token.IsCancellationRequested)
                {
                    break;
                }
            }
        }
        private void UpdateUI2(int i)
        {
            lblResult2.Content = i.ToString();
        }

        private void BtnStop2_Click(object sender, RoutedEventArgs e)
        {
            if (token3 != null)
            {
                token3.Cancel();
                token3 = null;
            }
        }
    }
}
