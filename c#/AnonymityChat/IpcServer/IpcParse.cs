using System;

namespace IPCServer
{

public class IpcParse :IIpcParse 
{

    public IpcMessage Parse(byte[] message)
    {
      IpcMessage ipcMessage = new IpcMessage();
      if (message.Length != Constants.FullMessageSize)
      {
        return null;
      }

      // LINK: http://stackoverflow.com/questions/1003275/how-to-convert-byte-to-string
      // TODO: set size to 2048/ size of message

      byte[] sizeOfBody = new byte[Constants.MessageHeaderSize];
      byte[] header = new byte[Constants.MessageContentHeaderSize];
      byte[] body = new byte[Constants.MessageContentSize];

      Array.Copy(message, sizeOfBody, Constants.MessageHeaderSize);

      ipcMessage.Size = System.Text.Encoding.UTF8.GetString(removeNUllCharacters(sizeOfBody));
      Array.ConstrainedCopy(message, 32, header, 0, Constants.MessageContentHeaderSize);
      ipcMessage.Header = System.Text.Encoding.UTF8.GetString(removeNUllCharacters(header));
      Array.ConstrainedCopy(message, 288, body, 0, 1760);
      ipcMessage.Body = System.Text.Encoding.UTF8.GetString(removeNUllCharacters(body));
      return ipcMessage;
    }

  public byte[] removeNUllCharacters(byte[] message)
  {
    int index = Array.FindIndex<byte>(message, b => b == 0);
    byte[] resizedMessage = new byte[index];
    Array.Copy(message, resizedMessage, index);
    return resizedMessage;
  }
}

}
