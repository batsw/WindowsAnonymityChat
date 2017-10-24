package com.batsw.interProcessCommunication;

public interface IIpcSerialize {
    byte[] Serialize(String header, String body);
}
