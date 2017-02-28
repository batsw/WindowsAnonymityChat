package com.batsw.interProcessCommunication;

import com.batsw.interProcessCommunication.MessageListener;

import java.io.Console;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.Scanner;

public class PipeWritter implements  Runnable, MessageListener{

    private final String pipeName = "AnonimityPipeRead";
    private final String pipeRights = "rw";
    private RandomAccessFile pipeClient;

    public PipeWritter() {
    }

    public void run() {
        try {
            pipeClient = new RandomAccessFile("\\\\.\\pipe\\" + pipeName, pipeRights);
            Scanner reader = new Scanner(System.in);
            while(true){

            }
            //TODO: add pipeClient.close();
        } catch (IOException io) {

        }
    }

    public void SendMessage(String message){
        try {
           String header = "bundle";
            byte[] pipeMessage = new byte[2048];
            byte[] messageTmp = GetMessageSize(message);
            System.arraycopy(messageTmp, 0, pipeMessage, 0, messageTmp.length);
            messageTmp = GetMessageHeader(header);
            System.arraycopy(messageTmp, 0, pipeMessage, 32, messageTmp.length);
            messageTmp = GetMessageBody(message);
            System.arraycopy(messageTmp, 0, pipeMessage, 288, messageTmp.length);
            pipeClient.write(pipeMessage);
            System.out.print("Message " + message +  "sent\n\n FINISH\n");

        } catch (IOException io) {
            System.out.print(io.toString());
        }
        catch (Exception e)
        {
            System.out.print(e.toString());
        }

    }
    public byte[] GetMessageSize(String message){
        byte[] byteMessageSize = new byte[32];
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
        byte[] byteMessageHeader = new byte[256];
        byte[] rawHeader = message.getBytes();
        System.arraycopy(rawHeader, 0, byteMessageHeader, 0, rawHeader.length );
        return byteMessageHeader;
    }

    public byte[] GetMessageBody(String message) {
        byte[] byteMessaeBody = new byte[1760];
        byte[] rawMessage = message.getBytes();
        System.arraycopy(rawMessage, 0, byteMessaeBody, 0, rawMessage.length);
        return byteMessaeBody;
    }
}
