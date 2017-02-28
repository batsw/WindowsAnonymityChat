using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Text;

namespace AnonymityChat.Services
{
  public delegate void PipeMessageSenderEventHandler(string messsage);

  public class PipeWrite
  {
    public static String pipeServerName = "AnonimityPipeWrite";
    public static int numberOfSevers = 1;
    private const int messageSizeHeaderSize = 32;
    private const int messageHeaderSize = 256;
    private const int messageContentSize = 1760;
    private const int fullMessageSize = 2048;

    public NamedPipeServerStream pipeServer;
    public event PipeMessageSenderEventHandler receivedMessage;
    Stream pipeStream;

    public PipeWrite(PipeMessageSenderEventHandler ceh)
    {
      receivedMessage = new PipeMessageSenderEventHandler(ceh);
    }

    public void ServerThread()
    {
      pipeServer = new NamedPipeServerStream(pipeServerName, PipeDirection.InOut, numberOfSevers);
      int threadId = Thread.CurrentThread.ManagedThreadId;
      Console.WriteLine("Waiting for client write");
      pipeStream = pipeServer;
      pipeServer.WaitForConnection();
      Console.WriteLine("Client connected on thread[{0}].write", threadId);
      StreamReader streamReader = new StreamReader(pipeServer);

      while (true) ;
    }

    public void WriteToClient(string message)
    {
      try
      {

        if (message == null) return;
        byte[] byteMessage = new byte[fullMessageSize];
        Array.Copy(GetMessageSize(message), byteMessage, messageSizeHeaderSize);
        Array.ConstrainedCopy(GetMessageHeader(message), 0, byteMessage, messageSizeHeaderSize, messageHeaderSize);
        Array.ConstrainedCopy(GetMessageBody(message), 0, byteMessage, messageHeaderSize + messageSizeHeaderSize, messageContentSize);
        pipeStream.Write(byteMessage, 0, fullMessageSize);

      }
      catch (IOException e)
      {
        Console.WriteLine("Error: {0}", e.Message);
      }
      Console.WriteLine("Writting message to server");
    }

    public byte[] GetMessageSize(string message)
    {
      byte[] byteHeader = new byte[messageSizeHeaderSize];

      String sizeString = message.Length.ToString();
      byte[] tmp = Encoding.UTF8.GetBytes(sizeString);
      Array.Copy(tmp, byteHeader, tmp.Length);
      return byteHeader;
    }

    public byte[] GetMessageHeader(string message)
    {
      byte[] byteHeader = new byte[messageHeaderSize];
      byte[] tmp = Encoding.UTF8.GetBytes("C# heading");
      Array.Copy(tmp, byteHeader, tmp.Length);
      return byteHeader;
    }

    public byte[] GetMessageBody(string message)
    {
      byte[] byteContent = new byte[messageContentSize];
      byte[] tmp = Encoding.UTF8.GetBytes(message);
      Array.Copy(tmp, byteContent, tmp.Length);
      return byteContent;
    }
  }
}