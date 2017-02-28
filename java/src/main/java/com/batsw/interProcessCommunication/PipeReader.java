package com.batsw.interProcessCommunication;

import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.Scanner;

public class PipeReader implements  Runnable{
    private final String pipeName = "AnonimityPipeWrite";
    private final String pipeRights = "rw";
    private RandomAccessFile pipeClient;
    private final int messageSizeHeaderSize = 32;
    private final int messageHeaderSize = 256;
    private final int messageContentSize = 1760;
    private final int fullMessageSize = 2048;

    public PipeReader() {

    }

    public void run() {
        try {
            pipeClient = new RandomAccessFile("\\\\.\\pipe\\" + pipeName, pipeRights);

            while(true){
                byte[] receivedMessage = new byte[fullMessageSize];
                 pipeClient.read(receivedMessage,0,fullMessageSize);
                String echoResponse = GetBody(receivedMessage);
                System.out.println("Message from server: " + echoResponse);

            }
            // TODO: add pipeClient.close();
        } catch (IOException io) {
            System.out.println(io);
        }
    }

    private String GetBodySize(byte[] message){
        byte[] rawSize = new byte[messageSizeHeaderSize];
        System.arraycopy(message,0,rawSize,0,messageSizeHeaderSize);
        byte[] withouZeros = RemoveZeros(rawSize);
        return new String(rawSize);

    }

    private String GetHeader(byte[] message){
        byte[] rawHeader = new byte[messageHeaderSize];
        System.arraycopy(message,messageSizeHeaderSize,rawHeader,0,messageHeaderSize);
        byte[] withouZeros = RemoveZeros(rawHeader);
        return new String(withouZeros);
    }

    private String GetBody(byte[] message){
        byte[] rawBody  = new byte[messageContentSize];
        System.arraycopy(message,messageHeaderSize + messageSizeHeaderSize,rawBody,0,messageContentSize);
        byte[] withouZeros = RemoveZeros(rawBody);
        return new String(withouZeros);
    }

    private byte[] RemoveZeros(byte[] byteMessage){
        int zeroIndex = -1;
        for ( int  i= 0; i <byteMessage.length; i++)
        {
            if ( byteMessage[i] == 0) {
                zeroIndex = i;
                break;
            }
        }
        byte[] zerolessMessage = new byte[zeroIndex+1];
        System.arraycopy(byteMessage,0,zerolessMessage,0, zeroIndex+1);
        return zerolessMessage;
    }
}
