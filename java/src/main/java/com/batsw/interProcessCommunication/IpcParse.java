package com.batsw.interProcessCommunication;

public class IpcParse implements IIpcParse {
    public IpcMessage Parse(byte[] message)
    {
        IpcMessage ipcMessage = new IpcMessage();
        ipcMessage.setBody(GetBody(message));
        ipcMessage.setSize(GetBodySize(message));
        ipcMessage.setHeader(GetHeader(message));
        return ipcMessage;
    }

    private String GetBodySize(byte[] message) {
        byte[] rawSize = new byte[Constants.messageSizeHeaderSize];
        System.arraycopy(message, 0, rawSize, 0, Constants.messageSizeHeaderSize);
        byte[] withouZeros = RemoveZeros(rawSize);
        return new String(rawSize);

    }

    private String GetHeader(byte[] message) {
        byte[] rawHeader = new byte[Constants.messageHeaderSize];
        System.arraycopy(message, Constants.messageSizeHeaderSize, rawHeader, 0, Constants.messageHeaderSize);
        byte[] withouZeros = RemoveZeros(rawHeader);
        return new String(withouZeros);
    }

    private String GetBody(byte[] message) {
        byte[] rawBody = new byte[Constants.messageContentSize];
        System.arraycopy(message, Constants.messageHeaderSize + Constants.messageSizeHeaderSize, rawBody, 0, Constants.messageContentSize);
        byte[] withouZeros = RemoveZeros(rawBody);
        return new String(withouZeros);
    }

    private byte[] RemoveZeros(byte[] byteMessage) {
        int zeroIndex = -1;
        for (int i = 0; i < byteMessage.length; i++) {
            if (byteMessage[i] == 0) {
                zeroIndex = i;
                break;
            }
        }
        byte[] zerolessMessage = new byte[zeroIndex + 1];
        System.arraycopy(byteMessage, 0, zerolessMessage, 0, zeroIndex + 1);
        return zerolessMessage;
    }
}
