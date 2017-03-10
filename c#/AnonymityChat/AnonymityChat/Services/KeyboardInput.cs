using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymityChat.Services
{
  public delegate void ServerEventHandler(string message);

  public class KeyBoardInput
  {
    private event ServerEventHandler SendMessage;

    public KeyBoardInput(ServerEventHandler sv)
    {
      SendMessage = new ServerEventHandler(sv);
    }

    public void ReadInput()
    {
      Console.ReadKey();
      while (true)
      {
        Console.WriteLine("To send");
        String message = Console.ReadLine();
        SendMessage(message + "\n");
      }
    }
  }
}
