package com.batsw.interProcessCommunication;

public class ReceivedMessage implements IMessageListener {

    public void SendMessage(IpcMessage messge)
    {
        System.out.println("Received: \n" + messge.getHeader() + "\n" + messge.getBody() );
    }
}
