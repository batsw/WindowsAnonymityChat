namespace IPCServer
{

public class IpcMessage
{
  public IpcMessage(string body="")
  {
      Header = "";
      Body = body;
      Size = "";
  }
    public IpcMessage(string header,string body, string size="")
   { 
      Header = header;
      Body = body;
      Size = size;
    }
    public string Header { get; set; }
  public string Body {
      get; set; }
  public string Size { get; set; }

  
}

}
