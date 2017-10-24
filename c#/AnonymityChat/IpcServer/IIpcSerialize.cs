namespace IPCServer
{

public  interface IIpcSerialize
{
    byte[] Serialize(IpcMessage message);
}

}
