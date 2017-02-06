package com.batsw.interProcessCommunication;

import java.util.Vector;
public class EventManager {
    // Colection of classes that are subcribed to listener
    protected Vector<MessageListener> listeners;

    public void addEvenrtListener(MessageListener listener) {
        if (listeners == null) {
            listeners = new Vector();
        }
        listeners.addElement(listener);
    }

    public void removeEventListener(MessageListener listener) {
        listeners.remove(listener);
    }

    public void FireEvent(String message){
        if (listeners != null && !listeners.isEmpty()){
            for (MessageListener listener : listeners){
                listener.SendMessage(message);
            }
        }
    }
}


