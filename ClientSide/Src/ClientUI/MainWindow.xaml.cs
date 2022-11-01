using ClientSocket;
using System;
using System.Windows;
using System.Windows.Media;

namespace ClientUI
{
    public partial class MainWindow : Window
    {
         ClientStartUp _Client;
         Trasmission _trasmission;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ChangeState(string state, SolidColorBrush color)
        {
            State.Content = state;
            State.Foreground = color;
        }

        public void ConnectBtnEnabled(bool State) 
        {
            ConnectBtn.IsEnabled = State;
            Port.IsEnabled = State;
            StopBtn.IsEnabled = !State;
        }
       
        private async void ConnectBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ConnectBtnEnabled(false);
                ChangeState("Init Client...", new SolidColorBrush(Colors.Orange));
                _Client = new ClientStartUp(int.Parse(Port.Text), IP.Text, connectCallBack);
                _Client.InitClient();
                ChangeState("Connectiong to server ...", new SolidColorBrush(Colors.Orange));
                await _Client.ConnectAsync();
            }
            catch (Exception ex)
            {

                ChangeState("Not Connect", new SolidColorBrush(Colors.Red));
                MessageBox.Show(ex.Message + "\nTry agian", "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ConnectBtnEnabled(true);
            }
        }

        private async void connectCallBack(string error)
        {
            if (string.IsNullOrEmpty(error))
            {

                this.Dispatcher.Invoke(() =>
                {
                    ChangeState("Connection to server.", new SolidColorBrush(Colors.Green));
                });

                _trasmission = new Trasmission(_Client.ClientSocket, recieveCallback);
               
                this.Dispatcher.Invoke(() =>
                {
                    SendBtn.IsEnabled = true;
                });
                await _trasmission.RecieveAsync();
            }
            else
            {
                this.Dispatcher.Invoke(() => {
                    ChangeState("Not Connect", new SolidColorBrush(Colors.Red));
                    MessageBox.Show(error,"Connection Error",MessageBoxButton.OK, MessageBoxImage.Error);
                    ConnectBtnEnabled(true);
                });
            }
          

        }

        private void recieveCallback(string RecievedMessage, string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                this.Dispatcher.Invoke(() => {

                    RecieveBox.Text += $"Recieved : {RecievedMessage}\n";
                });

            }
            else
            {
                _Client.Close();
                this.Dispatcher.Invoke(() => 
                {
                    ChangeState("Not Connect", new SolidColorBrush(Colors.Red));
                    ConnectBtnEnabled(true);
                    MessageBox.Show(error, "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    SendBtn.IsEnabled = false;
                    _trasmission = null;
                });
               
            }
        }
        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _trasmission.Send(Sendbox.Text);
                RecieveBox.Text += $"Me : {Sendbox.Text}\n";
                Sendbox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try Again", "sending Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_trasmission != null)
            {
                _trasmission = null;
                _Client.Close();
                SendBtn.IsEnabled = false;
                ChangeState("Not Connect", new SolidColorBrush(Colors.Red));
                ConnectBtnEnabled(true);
            }
        }
    }
}
