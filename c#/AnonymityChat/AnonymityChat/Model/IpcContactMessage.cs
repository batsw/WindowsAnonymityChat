using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymityChat.Model
{
  public class IpcContactMessage
  {
    public enum MessageType
    {
      Connect = 0,
      Disconnect,
      Message 
    }
    public MessageType Type { get; set; }
    public int MessageNumber { get; set; }
    public int TotalNumberOfMessages { get; set; }
    public string Destination { get; set; }
    public string Message { get; set; }

  }
}
