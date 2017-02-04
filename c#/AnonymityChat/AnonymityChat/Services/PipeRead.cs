using System;
using System.IO.Pipes;
using System.Threading;
using System;
using System.IO;
namespace AnonymityChat.Service
{
  public delegate void PipeMessageReceiverEventHandler(string message);
  class PipeRead
  {

    private const  String pipeServerName = "AnonimityPipeRead";
    private const int numberOfSevers = 1;

    public event PipeMessageReceiverEventHandler receivedMessage;

    public  PipeRead(PipeMessageReceiverEventHandler ceh)
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
      StreamReader streamReader = new StreamReader(pipeServer);
      StreamWriter streamWritter = new StreamWriter(pipeServer);
      string clientMessage = "";
      streamWritter.AutoFlush = true;
      while (!clientMessage.Equals("quit"))
      {
        try
        {
          clientMessage = streamReader.ReadLine();
          //Console.WriteLine("Message received from client: " + clientMessage);
          streamWritter.WriteLine("Message received");
          receivedMessage(clientMessage);                  
        }
        catch (IOException e)
        {
          Console.WriteLine("Error: {0}", e.Message);
          streamReader.Close();
          streamWritter.Close();
        }
      }
      streamReader.Close();
      streamWritter.Close();
    }

  }
}
