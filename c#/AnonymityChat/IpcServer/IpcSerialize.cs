using System;
using System.Text;

namespace IPCServer
{

public class IpcSerialize : IIpcSerialize
{
  public byte[] Serialize(IpcMessage message)
  {
      byte[] byteMessage = new byte[Constants.FullMessageSize];
      Array.Copy(GetMessageSize(message.Body), byteMessage, Constants.MessageHeaderSize);
      Array.ConstrainedCopy(GetMessageHeader(message.Header), 0, byteMessage, Constants.MessageHeaderSize,
        Constants.MessageContentHeaderSize);
      Array.ConstrainedCopy(GetMessageBody(message.Body), 0, byteMessage, Constants.MessageHeaderSize +
        Constants.MessageContentHeaderSize, Constants.MessageContentSize);
      return byteMessage;
  }

  public byte[] GetMessageSize(string message)
  {
    byte[] byteHeader = new byte[Constants.MessageHeaderSize];
    String sizeString = message.Length.ToString();
    byte[] tmp = Encoding.UTF8.GetBytes(sizeString);
    Array.Copy(tmp, byteHeader, tmp.Length);
    return byteHeader;
  }

  public byte[] GetMessageHeader(string header)
  {
    byte[] byteHeader = new byte[Constants.MessageContentHeaderSize];
    byte[] tmp = Encoding.UTF8.GetBytes(header);
    Array.Copy(tmp, byteHeader, tmp.Length);
    return byteHeader;
  }

  public byte[] GetMessageBody(string message)
  {
    byte[] byteContent = new byte[Constants.MessageContentSize];
    byte[] tmp = Encoding.UTF8.GetBytes(message);
    Array.Copy(tmp, byteContent, tmp.Length);
    return byteContent;
  }
}

}
