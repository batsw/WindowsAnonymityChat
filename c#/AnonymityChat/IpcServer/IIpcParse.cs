namespace IPCServer
{

public  interface IIpcParse
{
   IpcMessage Parse(byte[] message);
}

}
