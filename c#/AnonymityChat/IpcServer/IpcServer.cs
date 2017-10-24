using System;
using System.Collections.Concurrent;
using System.Threading;


namespace IPCServer
{
public class IpcServer : IIpcServer
{

  public IpcServer()
  {
    
  }
  public void Start(PipeMessageReceiverEventHandler callback)
  {
    queue = new ConcurrentQueue<IpcMessage>();
    inputServer = new InputServer(callback);
    outpuServer = new OutputServer(queue);
    Thread tw = new Thread(new ThreadStart(outpuServer.ServerThread));
    Thread tr = new Thread(new ThreadStart(inputServer.ServerThread));
    tw.Start();
    tr.Start();
    tw.Join();
    tr.Join();
  }

  public void Stop()
  {
    throw new NotImplementedException();
  }

  public void Write(IpcMessage message)
  {
    queue.Enqueue(message);
  }

  ConcurrentQueue<IpcMessage> queue; 
  InputServer inputServer;           
  OutputServer outpuServer;                    
}
}
