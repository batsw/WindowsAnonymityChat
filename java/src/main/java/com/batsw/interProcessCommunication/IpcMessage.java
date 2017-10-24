package com.batsw.interProcessCommunication;


public class IpcMessage {
    public IpcMessage(){}
    public IpcMessage(String header_, String body_)
    {
        setHeader(header_);
        setBody(body_);
    }

    public String getHeader() {
        return header;
    }

    public void setHeader(String header) {
        this.header = header;
    }

    public String getBody() {
        return body;
    }

    public void setBody(String body) {
        this.body = body;
    }

    public String getSize() {
        return size;
    }

    public void setSize(String size) {
        this.size = size;
    }

    private String body;
    private String size;
    private String header;
}
