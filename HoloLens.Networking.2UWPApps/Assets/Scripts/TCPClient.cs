using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class TCPClient : MonoBehaviour {
    
    public static TCPClient Instance { get; set; }

    public int ServerPort;
    public string ServerName;

    void Start()
    {
        Instance = this;
        StartTCPClient();
    }

#if NETFX_CORE
    private Windows.Networking.Sockets.StreamSocket socket;
#else
    Socket sock;
    IPEndPoint ep;
    byte[] buffer = new byte[1024];
    private byte[] _recieveBuffer = new byte[8142];
#endif

    private void StartTCPClient()
    {
#if NETFX_CORE
        try
        {
            //Create the StreamSocket and establish a connection to the echo server.
            socket = new Windows.Networking.Sockets.StreamSocket();

            //The server hostname that we will be establishing a connection to. We will be running the server and client locally,
            //so we will use localhost as the hostname.
            Windows.Networking.HostName serverHost = new Windows.Networking.HostName(ServerName);

            //Every protocol typically has a standard port number. For example HTTP is typically 80, FTP is 20 and 21, etc.
            //For the echo server/client application we will use a random port 1337.
            string serverPort = ServerPort.ToString();
            socket.ConnectAsync(serverHost, serverPort).AsTask().Wait();
        
            SendToTcp("Bonjour" + System.Environment.NewLine);
        }
        catch (Exception e)
        {
            //Handle exception here.            
        }
#else
        ep = new IPEndPoint(IPAddress.Parse(ServerName), ServerPort);
        sock = new Socket(ep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            sock.Connect(ep);
            print("Connected to server " + ServerName);
            print("Connected : " + sock.Connected);
            SendToTcp("Bonjour" + System.Environment.NewLine);

            //sock.BeginReceive(_recieveBuffer, 0, _recieveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
        }
        catch
        {
            print("Could not connect to server! " + ServerName);
        }
#endif
    }

    public void SendToTcp(string request)
    {
#if NETFX_CORE
        //Write data to the echo server.
        Stream streamOut = socket.OutputStream.AsStreamForWrite();
        StreamWriter writer = new StreamWriter(streamOut);
        writer.WriteLineAsync(request).Wait();
        writer.FlushAsync().Wait();
#else
        buffer = ASCIIEncoding.ASCII.GetBytes(request + System.Environment.NewLine);
        sock.Send(buffer);
#endif
        print(request);
    }

    void Update()
    {
    }
    
    //public void SendToTcp(string message)
    //{
    //    buffer = ASCIIEncoding.ASCII.GetBytes(message + System.Environment.NewLine);
    //    print("Buffer length: " + buffer.Length);
    //    sock.Send(buffer);
    //}
    
    //private void ReceiveCallback(IAsyncResult AR)
    //{
    //    //Check how much bytes are recieved and call EndRecieve to finalize handshake
    //    int recieved = sock.EndReceive(AR);

    //    if (recieved <= 0)
    //        return;

    //    //Copy the recieved data into new buffer , to avoid null bytes
    //    byte[] recData = new byte[recieved];
    //    Buffer.BlockCopy(_recieveBuffer, 0, recData, 0, recieved);

    //    //Process data here the way you want , all your bytes will be stored in recData
    //    print("Response : " + UTF8Encoding.UTF8.GetString(recData));

    //    workInProgress = false;

    //    //Start receiving again
    //    sock.BeginReceive(_recieveBuffer, 0, _recieveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
    //}
}
