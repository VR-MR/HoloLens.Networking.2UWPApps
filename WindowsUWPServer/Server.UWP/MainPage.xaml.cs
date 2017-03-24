using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Server.UWP
{
    public class Item
    {
        public string ContentString { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Windows.Networking.Sockets.StreamSocketListener socketListener;

        public ObservableCollection<Item> IPs { get; set; }

        public ObservableCollection<Item> Actions { get; set; }


        public MainPage()
        {
            this.InitializeComponent();
            DataContext = this;
            IPs = new ObservableCollection<Item>();
            Actions = new ObservableCollection<Item>();

            ipListe.ItemsSource = IPs;
            actionsListe.ItemsSource = Actions;

            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoadIPs();
            StartTCPServer();
        }

        private void LoadIPs()
        {
            foreach (HostName localHostName in NetworkInformation.GetHostNames())
            {
                if (localHostName.IPInformation != null)
                {
                    if (localHostName.Type == HostNameType.Ipv4)
                    {
                        IPs.Add(new Item()
                        {
                            ContentString = localHostName.ToString()
                        });
                    }
                }
            }
        }

        private async void StartTCPServer()
        {
            try
            {
                //Create a StreamSocketListener to start listening for TCP connections.
                socketListener = new Windows.Networking.Sockets.StreamSocketListener();

                //Hook up an event handler to call when connections are received.
                socketListener.ConnectionReceived += SocketListener_ConnectionReceived;
                
                //Start listening for incoming TCP connections on the specified port. You can specify any port that' s not currently in use.
                await socketListener.BindServiceNameAsync("18526");

                //Handle exception.
                Debug.WriteLine("Connection opened");
            }
            catch (Exception e)
            {
                //Handle exception.
                Debug.WriteLine(e.Message);
            }
        }

        private void SocketListener_ConnectionReceived(Windows.Networking.Sockets.StreamSocketListener sender, Windows.Networking.Sockets.StreamSocketListenerConnectionReceivedEventArgs args)
        {
            Debug.WriteLine("ConnectionReceived");

            while (true)
            {
                //Read line from the remote client.
                Stream inStream = args.Socket.InputStream.AsStreamForRead();
                StreamReader reader = new StreamReader(inStream);
                var stringSocket = reader.ReadLine();

                Debug.WriteLine(stringSocket);

                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority
                    .Normal, () =>
                    {
                        waitConnection.Visibility = Visibility.Collapsed;
                        actionReceivedTitle.Visibility = Visibility.Visible;

                        string[] actionContent = stringSocket.Split(';');

                        Actions.Clear();

                        if (actionContent.Length > 0)
                        {
                            actionTriggerWith.Text = "Trigger with " + actionContent[0];

                            for (int i = 1; i < actionContent.Length; i++)
                            {
                                Actions.Add(new Item()
                                {
                                    ContentString = actionContent[i]
                                });
                            }
                        }
                    });

                //Stream outStream = args.Socket.OutputStream.AsStreamForWrite();
                //StreamWriter writer = new StreamWriter(outStream);
                //writer.WriteLine("Ok, bien reçu");
                //writer.Flush();
            }
        }
    }
}
