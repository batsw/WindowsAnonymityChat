using System;
using System.IO.Pipes;
using System.Threading;
using System.IO;


namespace IPCServer
{

public delegate void PipeMessageReceiverEventHandler(IpcMessage message);

class InputServer
{
  public event PipeMessageReceiverEventHandler receivedMessage;

  public NamedPipeServerStream pipeServer;
  public bool ToClose { get; set; }

  Stream pipeStream;

  public InputServer(PipeMessageReceiverEventHandler ceh)
  {
    ToClose = false;
    receivedMessage = new PipeMessageReceiverEventHandler(ceh);
  }

  public void ServerThread()
  {
    pipeServer = new NamedPipeServerStream(Constants.InputServerName, PipeDirection.InOut, 
      Constants.NumberOfAllowedConnections);
  
    while (!ToClose)
    {
      try
      {
        pipeStream = pipeServer;
        Console.WriteLine("InputServer: Waiting for connection");
        pipeServer.WaitForConnection();
        Console.WriteLine("InputServer: Client connected");
        ManageCLient();
      }
      catch (IOException ex)
      {
        Console.WriteLine("InputServer: " + ex.Message);
        pipeServer.Disconnect();
      }
      catch( Exception ex)
      {
          Console.WriteLine("InputServer: " + ex.Message);
      }
        
    }
    pipeServer.Close();
  }

  public void  ManageCLient()
  {
    IIpcParse ipcParse = new IpcParse();
    string clientMessage = "";
    byte[] message = new byte[Constants.FullMessageSize];
    //TODO: change while cndition
    while (!clientMessage.Equals("quit") && !ToClose)
    {
      try
      {
        pipeStream.Read(message, 0, Constants.FullMessageSize);
        if (!pipeServer.IsConnected)
        {
          Console.WriteLine("InputServer: Client Disconected");
          break;
        }

        IpcMessage msg = ipcParse.Parse(message);
        receivedMessage(msg);   
      }
      catch (IOException e)
      {
        Console.WriteLine("InputServer: Error: {0}", e.Message);
        break;
      }
      
    }
   
    }

}
}
