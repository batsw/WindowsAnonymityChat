namespace IPCServer
{

public class Constants
{
  public const int MessageHeaderSize = 32;
  public const int MessageContentHeaderSize = 256;
  public const int MessageContentSize = 1760;
  public const int FullMessageSize = 2048;

  public const int NumberOfAllowedConnections = 1;
  public const string InputServerName = "AnonimityPipeRead";
  public const string OutputServerName = "AnonimityPipeWrite";
  
  
}

public class ControlBytes
{
  public const byte CheckConnection = 42;
  public const byte DataMessage = 60;
}

public class IpcHeaders
{
    public const string IpcConection = "IpcConnection";
    public const string BundleMessage = "Bundle";
    public const string Contact = "Contact";

}

}
