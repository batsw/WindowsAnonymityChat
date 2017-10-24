package com.batsw.interProcessCommunication;
public interface IIpcClient {
    void start();
    void joinClient();
    void sendMessage(IpcMessage message);
}
