package com.batsw.interProcessCommunication;
import java.util.Vector;
public class EventManager {
    // Colection of classes that are subcribed to listener
    protected Vector<IMessageListener> listeners;

    public void addEvenrtListener(IMessageListener listener) {
        if (listeners == null) {
            listeners = new Vector();
        }
        listeners.addElement(listener);
    }

    public void removeEventListener(IMessageListener listener) {
        listeners.remove(listener);
    }

    public void FireEvent(IpcMessage message){
        if (listeners != null && !listeners.isEmpty()){
            for (IMessageListener listener : listeners){
                listener.SendMessage(message);
            }
        }
    }
}


