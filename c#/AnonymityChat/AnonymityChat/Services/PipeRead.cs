using System;
using System.IO.Pipes;
using System.Threading;
using System.Text;
using System;
using System.IO;
namespace AnonymityChat.Service
{
  public delegate void PipeMessageReceiverEventHandler(string message);
  class PipeRead
  {

    private const String pipeServerName = "AnonimityPipeRead";
    private const int numberOfSevers = 1;
    private const int messageSizeHeaderSize = 32;
    private const int messageHeaderSize = 256;
    private const int messageContentSize = 1760;
    private const int fullMessageSize = 2048;

    public event PipeMessageReceiverEventHandler receivedMessage;

    public PipeRead(PipeMessageReceiverEventHandler ceh)
    {
      receivedMessage = new PipeMessageReceiverEventHandler(ceh);
    }
    public void ServerThread()
    {
      NamedPipeServerStream pipeServer = new NamedPipeServerStream
        (pipeServerName, PipeDirection.InOut, numberOfSevers);

      int threadId = Thread.CurrentThread.ManagedThreadId;
      Console.WriteLine("Waiting for client Read");
      pipeServer.WaitForConnection();
      Console.WriteLine("Client connected on thread[{0}]. Read", threadId);
      Stream pipeStream = pipeServer;
      string clientMessage = "";
      byte[] message = new byte[fullMessageSize];
      while (!clientMessage.Equals("quit"))
      {
        try
        {
          pipeStream.Read(message, 0, fullMessageSize);
          clientMessage = MessageDecoder(message);
          receivedMessage(clientMessage);
        }
        catch (IOException e)
        {
          Console.WriteLine("Error: {0}", e.Message);
        }
      }

    }

    public string MessageDecoder(byte[] byteMessage)
    {
      // LINK: http://stackoverflow.com/questions/1003275/how-to-convert-byte-to-string
      string message = "";

      byte[] sizeOfBody = new byte[messageSizeHeaderSize];
      byte[] header = new byte[messageHeaderSize];
      byte[] body = new byte[messageContentSize];

      Array.Copy(byteMessage, sizeOfBody, messageSizeHeaderSize);
      String sizeString = System.Text.Encoding.UTF8.GetString(removeNUllCharacters(sizeOfBody));
      Array.ConstrainedCopy(byteMessage, 32, header, 0, messageHeaderSize);
      String headerString = System.Text.Encoding.UTF8.GetString(removeNUllCharacters(header));
      Array.ConstrainedCopy(byteMessage, 288, body, 0, 1760);
      message = System.Text.Encoding.UTF8.GetString(removeNUllCharacters(body));
      return message;
    }

    public byte[] removeNUllCharacters(byte[] message)
    {
      int index = Array.FindIndex<byte>(message, b => b == 0);
      byte[] resizedMessage = new byte[index];
      Array.Copy(message, resizedMessage, index);
      return resizedMessage;
    }
  }
}
