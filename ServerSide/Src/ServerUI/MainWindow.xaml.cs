using ServerSocket;
using System;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Media;

namespace ServerUI
{
    public partial class MainWindow : Window
    {
        ServerStartUp serverStartUp;
        Trasmission trasmission;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void changestate(string state , SolidColorBrush colors)
        {
            State.Content = state;
            State.Foreground = colors;
        }

        private void StartEnable(bool Sate) 
        {
            startbtn.IsEnabled = Sate;
            Port.IsEnabled = Sate;
            startbtn.IsEnabled = !Sate;
        }

        private async void startbtn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
               
                changestate("Starting Server...", new SolidColorBrush(Colors.Orange));
                StartEnable(false);
                serverStartUp = new ServerStartUp(int.Parse(Port.Text), IP.Text, acceptedCallback, acceptedErrorCallback);
                serverStartUp.InitServer();
                changestate("Connect TO client...", new SolidColorBrush(Colors.Blue));
                await serverStartUp.AcceptAsync();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message + "\n Try Again", "init server Error", MessageBoxButton.OK, MessageBoxImage.Error);
                changestate("Stop Server", new SolidColorBrush(Colors.Red));
                StartEnable(true);


            }
        }

        private void acceptedErrorCallback(string error)
        {
            this.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(error + "\n Try Again", "Accepted Error" , MessageBoxButton.OK , MessageBoxImage.Error);
            });
        }

        private async void acceptedCallback(Socket AcceptedSocket)
        {
            this.Dispatcher.Invoke(() =>
            {
                changestate("AcceptedClient", new SolidColorBrush(Colors.Orange));
            });
            trasmission = new Trasmission(AcceptedSocket, recievescallback, recieveErrorCallback);
          
            this.Dispatcher.Invoke(() =>
            {
                SendBtn.IsEnabled = true;
            });
            await trasmission.RecieveAsync();
        }

        private async void recieveErrorCallback(string Error)
        {
            serverStartUp.CloseClient(trasmission.socket);
            trasmission = null;

            this.Dispatcher.Invoke(() =>
            {
                SendBtn.IsEnabled = false;
                changestate("Connect TO client...", new SolidColorBrush(Colors.Blue));
                MessageBox.Show(Error + "\n Try Again", "Recieve Error", MessageBoxButton.OK, MessageBoxImage.Error);
            });
            await serverStartUp.AcceptAsync();
        }

        private void recievescallback(string RecieveMassage)
        {
            this.Dispatcher.Invoke(() =>
            {
                RecieveBox.Text += $" Recieved : {RecieveMassage}\n";
            });
           
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                trasmission.Send(Sendbox.Text);
                RecieveBox.Text += $"Me :{Sendbox.Text}\n ";
                Sendbox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try Again", "sending Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void Stopbtn_Click(object sender, RoutedEventArgs e)
        {

            if (trasmission != null)
            {
                serverStartUp.CloseClient(trasmission.socket);
                trasmission = null;
            }
            
            serverStartUp.CloseServer();
            SendBtn.IsEnabled = false;
            changestate("Stop Server", new SolidColorBrush(Colors.Red));
            StartEnable(true);
        }
    }
}
