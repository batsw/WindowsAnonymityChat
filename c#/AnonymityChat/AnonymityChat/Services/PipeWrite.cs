using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace AnonymityChat.Services
{
  public delegate void PipeMessageSenderEventHandler(string messsage);

  public class PipeWrite
  {
    public NamedPipeServerStream pipeServer;
    public  event PipeMessageSenderEventHandler receivedMessage;
    
    public  PipeWrite(PipeMessageSenderEventHandler ceh )
    {
      //Send message to listener
      receivedMessage = new PipeMessageSenderEventHandler(ceh);
    }
    public static String pipeServerName = "AnonimityPipeWrite";
    public static int numberOfSevers = 1;
    StreamWriter streamWritter;
    StreamReader streamReader;
    
    public void ServerThread()
    {
      
       pipeServer= 
        new NamedPipeServerStream(pipeServerName, PipeDirection.InOut, numberOfSevers);
     
      int threadId = Thread.CurrentThread.ManagedThreadId;
      Console.WriteLine("Waiting for client write");
      pipeServer.WaitForConnection();
      Console.WriteLine("Client connected on thread[{0}].write", threadId);
      StreamReader streamReader = new StreamReader(pipeServer);
      streamWritter = new StreamWriter(pipeServer);
      streamWritter.AutoFlush = true;
      while (true) ;
    }

    public void WriteToClient(string message)
    {
      try
      {

        if (message == null) return;
        //    StreamWriter streamWritter1 = new StreamWriter(pipeServer);
        //  StreamReader streamReader1 = new StreamReader(pipeServer); 
        streamWritter.AutoFlush = true;
        streamWritter.WriteLine(message);
      //  string echo = streamReader.ReadLine();
       // receivedMessage(echo);
      }
      catch (IOException e){
        Console.WriteLine("Error: {0}", e.Message);
      }
      Console.WriteLine("Writting message to server");
    
    }
  }
}