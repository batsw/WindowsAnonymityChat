namespace IPCServer
{

public interface IIpcServer
{
  void Start(PipeMessageReceiverEventHandler callback);
  void Write(IpcMessage message);
  void Stop(); 
}

}
