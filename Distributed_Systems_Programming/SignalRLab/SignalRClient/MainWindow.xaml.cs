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
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected HubConnection connection;
        public MainWindow()
        {
            connection = new HubConnectionBuilder().WithUrl("http://10.39.39.140:5000/chatroom").Build();
            connection.On<string, string>("GetMessage", new Action<string, string>((username, message) => GetMessage(username, message)));
            
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GetMessage(string username, string message)
        {
            this.Dispatcher.Invoke(() =>
            {
                var chat = $"{username}: {message}";
                MessagesListBox.Items.Add(chat);
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.StartAsync();
                MessagesListBox.Items.Add("Connection opened");
            }
            catch
            {
                MessagesListBox.Items.Add("Connection failed");
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.InvokeAsync("BroadcastMessage", UsernameTextBox.Text, MessageTextBox.Text);
            }
            catch (Exception ex)
            { 
                MessagesListBox.Items.Add(ex.Message);
            }
        }
    }
}
