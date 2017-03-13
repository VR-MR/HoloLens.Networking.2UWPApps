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
    Socket sock;
    IPEndPoint ep;
    byte[] buffer = new byte[1024];
    private byte[] _recieveBuffer = new byte[8142];

    void Start()
    {
        Instance = this;

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
    }

    private bool workInProgress = false;
    void Update()
    {
        if (!workInProgress)
        {
            workInProgress = true;
            //print("Update " + this.name + ", " + DateTime.Now.ToLongTimeString());
            //SendToTcp("Update " + this.name + ", " + DateTime.Now.ToLongTimeString());
        }
    }
    
    public void SendToTcp(string message)
    {
        buffer = ASCIIEncoding.ASCII.GetBytes(message + System.Environment.NewLine);
        print("Buffer length: " + buffer.Length);
        sock.Send(buffer);
    }
    
    private void ReceiveCallback(IAsyncResult AR)
    {
        //Check how much bytes are recieved and call EndRecieve to finalize handshake
        int recieved = sock.EndReceive(AR);

        if (recieved <= 0)
            return;

        //Copy the recieved data into new buffer , to avoid null bytes
        byte[] recData = new byte[recieved];
        Buffer.BlockCopy(_recieveBuffer, 0, recData, 0, recieved);

        //Process data here the way you want , all your bytes will be stored in recData
        print("Response : " + UTF8Encoding.UTF8.GetString(recData));

        workInProgress = false;

        //Start receiving again
        sock.BeginReceive(_recieveBuffer, 0, _recieveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
    }
}
