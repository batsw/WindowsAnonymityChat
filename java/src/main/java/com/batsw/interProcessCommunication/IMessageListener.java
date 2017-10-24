package com.batsw.interProcessCommunication;

/**
 * Created by adria on 06-Jun-17.
 */
public interface IMessageListener {
    void SendMessage(IpcMessage message);
}
