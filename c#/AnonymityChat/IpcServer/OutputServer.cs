using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Collections.Concurrent;

namespace IPCServer
{

public delegate void PipeMessageSenderEventHandler(string messsage);

public class OutputServer
{
  private ConcurrentQueue<IpcMessage> messages;

  public NamedPipeServerStream pipeServer;

  public bool ToClose { get; set; }

  Stream pipeStream;

  public OutputServer(ConcurrentQueue<IpcMessage> messages_)
  {
    ToClose = false;
    messages = messages_;
  }

  public void ServerThread()
  {
    pipeServer = new NamedPipeServerStream(Constants.OutputServerName, PipeDirection.InOut,
      Constants.NumberOfAllowedConnections);

    pipeStream = pipeServer;
  
    while (!ToClose)
    {
      Console.WriteLine("OutputServer: Waiting connection");
      pipeServer.WaitForConnection();
      Console.WriteLine("OutputServer: Client connected");
      messages.Enqueue(new IpcMessage(IpcHeaders.IpcConection, Constants.OutputServerName));
      ManageClient();       
    }
    pipeServer.Close();
  }

  public void ManageClient()
  {
    while (!ToClose)
    {
      try
      {
        IpcMessage message;

        if (messages.TryDequeue(out message))
        {
          pipeServer.WriteByte(ControlBytes.DataMessage);
          WriteToClient(message);
        }

        pipeServer.WriteByte(ControlBytes.CheckConnection);
      }
      catch (IOException ex)
      {
        Console.WriteLine(ex.Message);
        pipeServer.Disconnect();
        break;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        pipeServer.Disconnect();
        break;
      }
    }
  
  }

  public void WriteToClient(IpcMessage message)
  {
    try
    {
      IIpcSerialize ipcSerialize = new IpcSerialize();       
      if (message == null) return;
      byte[] byteMessage = new byte[Constants.FullMessageSize];
      byteMessage = ipcSerialize.Serialize(message);
      pipeStream.Write(byteMessage, 0, Constants.FullMessageSize);
      // TODO: Wait for respone have time out discard else

    }
    catch (IOException e)
    {
      Console.WriteLine("OutputServer: Error: {0}", e.Message);
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      pipeServer.Disconnect();
    
    }
  }

}

}
