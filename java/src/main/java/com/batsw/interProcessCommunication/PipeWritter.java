package com.batsw.interProcessCommunication;

import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.Scanner;

public class PipeWritter implements  Runnable{

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
                String message = reader.nextLine();
                pipeClient.writeUTF ( message  + "\n");
                String echoResponse = pipeClient.readLine();
                System.out.println("Response: " + echoResponse );
            }
            //TODO: add pipeClient.close();
        } catch (IOException io) {

        }
    }

}
