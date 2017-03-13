
using System;
using System.IO;
using System.Threading.Tasks;

namespace TCPClient.WindowsUWP
{
    public class TCPClient
    {
        private async Task OnConnectedToServer(string serverName, string serverPort)
        {
            try
            {
                //Create the StreamSocket and establish a connection to the echo server.
                Windows.Networking.Sockets.StreamSocket socket = new Windows.Networking.Sockets.StreamSocket();

                //The server hostname that we will be establishing a connection to. We will be running the server and client locally,
                //so we will use localhost as the hostname.
                Windows.Networking.HostName serverHost = new Windows.Networking.HostName(serverName);

                //Every protocol typically has a standard port number. For example HTTP is typically 80, FTP is 20 and 21, etc.
                //For the echo server/client application we will use a random port 1337.
                await socket.ConnectAsync(serverHost, serverPort);

                //Write data to the echo server.
                Stream streamOut = socket.OutputStream.AsStreamForWrite();
                StreamWriter writer = new StreamWriter(streamOut);
                string request = "test";
                await writer.WriteLineAsync(request);
                await writer.FlushAsync();

                //Read data from the echo server.
                Stream streamIn = socket.InputStream.AsStreamForRead();
                StreamReader reader = new StreamReader(streamIn);
                string response = await reader.ReadLineAsync();
            }
            catch (Exception e)
            {
                //Handle exception here.            
            }
        }
    }
}