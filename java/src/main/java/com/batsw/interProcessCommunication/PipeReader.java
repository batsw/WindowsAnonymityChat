package com.batsw.interProcessCommunication;

import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.Scanner;

public class PipeReader implements  Runnable{
    private final String pipeName = "AnonimityPipeWrite";
    private final String pipeRights = "rw";
    private RandomAccessFile pipeClient;

    public PipeReader() {

    }

    public void run() {
        try {
            pipeClient = new RandomAccessFile("\\\\.\\pipe\\" + pipeName, pipeRights);

            while(true){
                String echoResponse = pipeClient.readLine();
                System.out.println("Message from server: " + echoResponse);

            }
            // TODO: add pipeClient.close();
        } catch (IOException io) {
            System.out.println(io);
        }
    }

}
