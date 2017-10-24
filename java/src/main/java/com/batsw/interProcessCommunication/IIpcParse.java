package com.batsw.interProcessCommunication;

public interface IIpcParse {
    public IpcMessage Parse(byte[] message);
}
