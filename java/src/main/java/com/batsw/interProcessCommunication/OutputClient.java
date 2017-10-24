package com.batsw.interProcessCommunication;

import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.Scanner;

public class OutputClient implements  Runnable, IMessageListener {

    private final String pipeName = "AnonimityPipeRead";
    private final String pipeRights = "rw";
    private IIpcSerialize ipcSerialize;
    private RandomAccessFile pipeClient;
    private Boolean isServerReady = false;

    public OutputClient() {
        ipcSerialize = new IpcSerialize();
    }

    public void run() {
        try {
            pipeClient = new RandomAccessFile("\\\\.\\pipe\\" + pipeName, pipeRights);
            isServerReady = true;
            while(true){
            }

        } catch (IOException io) {
            CloseConnection(pipeClient);
        }
    }

    public void SendMessage(IpcMessage message){
        try {
           if (!isServerReady) Thread.sleep(2000);
            pipeClient.write( ipcSerialize.Serialize(message.getHeader(), message.getBody()));
            System.out.println("OutputCLient: Message sent \n"+ message.getHeader() + "\n"  + message.getBody() + "\n");

        } catch (IOException io) {
            System.out.print(io.toString());
        }
        catch (Exception e)
        {
            System.out.println("OutputClient error: "  + e.toString() );
        }
    }

    public void CloseConnection(RandomAccessFile pipe)
    {
        try
        {
            pipe.close();
            System.out.println("OutputClient: shutdown");
        }
        catch (IOException e)
        {
            System.out.println("OutputClient: "  + e.getMessage() );
        }
    }

}
