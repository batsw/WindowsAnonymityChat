package com.batsw.interProcessCommunication;

public class IpcSerialize implements IIpcSerialize {
  public byte[] Serialize(String header, String body)
    {
        byte[] pipeMessage = new byte[Constants.fullMessageSize];
        byte[] messageTmp = GetMessageSize(body);
        System.arraycopy(messageTmp, 0, pipeMessage, 0, messageTmp.length);
        messageTmp = GetMessageHeader(header);
        System.arraycopy(messageTmp, 0, pipeMessage, Constants.messageSizeHeaderSize, messageTmp.length);
        messageTmp = GetMessageBody(body);
        System.arraycopy(messageTmp, 0, pipeMessage,
                Constants.messageSizeHeaderSize + Constants.messageHeaderSize, messageTmp.length);
        return pipeMessage;
    }
    public byte[] GetMessageSize(String message){
        byte[] byteMessageSize = new byte[Constants.messageSizeHeaderSize];
        byte[] bytesHeader;
        String messageSize;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(message.length());
        messageSize = stringBuilder.toString();
        bytesHeader = messageSize.getBytes();
        System.arraycopy(bytesHeader ,0, byteMessageSize, 0, bytesHeader.length );
        return byteMessageSize;
    }
    public byte[] GetMessageHeader(String message) {
        byte[] byteMessageHeader = new byte[Constants.messageHeaderSize];
        byte[] rawHeader = message.getBytes();
        System.arraycopy(rawHeader, 0, byteMessageHeader, 0, rawHeader.length );
        return byteMessageHeader;
    }

    public byte[] GetMessageBody(String message) {
        byte[] byteMessaeBody = new byte[Constants.messageContentSize];
        byte[] rawMessage = message.getBytes();
        System.arraycopy(rawMessage, 0, byteMessaeBody, 0, rawMessage.length);
        return byteMessaeBody;
    }
}
