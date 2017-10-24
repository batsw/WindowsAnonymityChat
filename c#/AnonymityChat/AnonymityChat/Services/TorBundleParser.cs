using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnonymityChat.Model;
namespace AnonymityChat.Services
{
  // TEMPORARY CLASS
  public class TorBundleParser
  {
    // Message strucure
    // Mar 30 20:04:13.000 [notice] Bootstrapped 90%: Establishing a Tor circuit
    // MMM DD HH:MM:SS.UUU [type] Message
    public BundleLogMessage parse(String message)
    {
      BundleLogMessage parsedMessage = new BundleLogMessage() ;
      string[] splitMessage = message.Split(']');
      if (splitMessage[0].Contains("notice"))
      {
        if (splitMessage[1].Contains("Bootstrapped"))
        {
          string[] messageSplit2 = splitMessage[1].Split(' ');
          string percent = messageSplit2[2].Remove(messageSplit2[2].Length - 2);
          string[] text = message.Split('%');
          parsedMessage.LogMessage = text[1].Replace(':',' ');
          parsedMessage.LoadingPercent = Convert.ToDouble(percent);
        }
      }
      return parsedMessage;
    }
  }
}
