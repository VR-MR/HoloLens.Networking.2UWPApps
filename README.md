# HoloLens.Networking.2UWPApps
POC for communication between two UWP Apps (One on Hololens, the other one on an other device)
The goal is to send informations to a Windows 10 application deployed on a device from a Hololens application.

The connection between two apps is managed by a basic socket connection.

## HoloLens application
This application contains a car that will follow the main camera.
On collision with a cube, a notification will be send to the Windows 10 application.
The collision is managed by the C# "TaxiCollider" script, linked to the "taxi" object.

### Configuration to connect to the Windows 10 app
The TcpClient script manage the connection to the remote device.
It's associated with the Main camera scene object.
You can configure the IP and Port to use as parameters of the script :

    Server port : xxxx
    Server name : x.x.x.x

## Windows 10 application
The application will start to listen on a specific port on the device.

### Select listen socket port
The configuration is managed in the MainPage.xaml.cs, method StartTCPServer() :

    await socketListener.BindServiceNameAsync("18526");

You can change the port from 18526 to any open port on your device.

### Package manifest
Don't forget to add these capabilities to the Server.UWP application manifest :
* Internet (Client & Server)
* Private networks (Client & Server)
The goal is to be able to listen on specific ports on the device.