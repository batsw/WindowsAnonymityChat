package com.batsw.interProcessCommunication;

import java.io.IOException;
import java.io.RandomAccessFile;

public class InputClient implements  Runnable {
    private final String pipeName = "AnonimityPipeWrite";
    private final String pipeRights = "rw";
    private EventManager eventManager;
    private RandomAccessFile pipeClient;
    private IIpcParse ipcParse;

    public InputClient()
    {
        ipcParse = new IpcParse();
        eventManager = new EventManager();
    }

    public void run() {
        try {
            pipeClient = new RandomAccessFile("\\\\.\\pipe\\" + pipeName, pipeRights);

            while (true) {
                byte[] receivedMessage = new byte[Constants.fullMessageSize];
                byte controlByte = pipeClient.readByte();
                if (controlByte == Constants.dataMessage)
                {
                    pipeClient.read(receivedMessage, 0, Constants.fullMessageSize);
                    IpcMessage echoResponse = ipcParse.Parse(receivedMessage);
                    eventManager.FireEvent(echoResponse);
                }
                else
                {
                }
            }

        } catch (IOException io) {
            System.out.println("InputClient:" + io.getMessage());
            CloseConnection(pipeClient);
        }
        catch (Exception io){
            System.out.println("InputClient:" + io.getMessage());
            CloseConnection(pipeClient);
        }
    }
    public void CloseConnection(RandomAccessFile pipe)
    {
        try
        {
            pipe.close();
            System.out.println("InputClient: shutdown");
        }
        catch (IOException e)
        {
            System.out.println("InputClient: "  + e.getMessage() );
        }
    }

    public void addListener(IMessageListener listener)
    {
        eventManager.addEvenrtListener(listener);
    }
}