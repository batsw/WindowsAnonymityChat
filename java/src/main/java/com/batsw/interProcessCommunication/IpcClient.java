package com.batsw.interProcessCommunication;

public class IpcClient  implements IIpcClient{
    private InputClient input;
    private OutputClient output;
    private EventManager eventManager;
    private Thread inputThread;
    private Thread outputThread;

    public IpcClient(ReceivedMessage receivedMessage)
    {
        input = new InputClient();
        output = new OutputClient();
        eventManager = new EventManager();
        input.addListener(receivedMessage);
        eventManager.addEvenrtListener(output);
        inputThread = new Thread(input);
        outputThread = new Thread(output);

    }
    public void sendMessage(IpcMessage message) {
        eventManager.FireEvent(message);
    }
    public void start() {
        inputThread.start();
        outputThread.start();
    }

    public void joinClient() {
        try
        {
            inputThread.join();
            inputThread.join();
        }
        catch (InterruptedException io)
        {

        }
    }
}
