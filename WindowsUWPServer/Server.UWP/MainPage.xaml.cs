using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Server.UWP
{
    public class Item
    {
        public string Text { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Windows.Networking.Sockets.StreamSocketListener socketListener;

        public ObservableCollection<Item> Notifications { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            DataContext = this;
            Notifications = new ObservableCollection<Item>();
            listView.ItemsSource = Notifications;
            
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            StartTCPServer();
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
                        Notifications.Add(new Item()
                        {
                            Text = stringSocket
                        });
                    });

                //Stream outStream = args.Socket.OutputStream.AsStreamForWrite();
                //StreamWriter writer = new StreamWriter(outStream);
                //writer.WriteLine("Ok, bien reçu");
                //writer.Flush();
            }
        }
    }
}
