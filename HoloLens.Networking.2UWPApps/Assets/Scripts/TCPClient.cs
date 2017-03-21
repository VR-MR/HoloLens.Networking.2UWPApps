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
            Windows.Networking.HostName serverHost = new Windows.Networking.HostName("192.168.137.103");

            //Every protocol typically has a standard port number. For example HTTP is typically 80, FTP is 20 and 21, etc.
            //For the echo server/client application we will use a random port 1337.
            string serverPort = "18526";
            socket.ConnectAsync(serverHost, serverPort).AsTask().Wait();
        }
        catch (Exception e)
        {
            //Handle exception here.            
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
