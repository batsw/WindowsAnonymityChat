package com.batsw;

import com.batsw.sockets.Receiver;
import com.batsw.torExpertBundleController.service.impl.*;
import com.batsw.torExpertBundleController.common.*;
import com.batsw.interProcessCommunication.*;
import com.batsw.sockets.Publisher;

import com.batsw.interProcessCommunication.EventManager;

 import org.apache.log4j.Logger;

public class Main {

  //  public static final Logger log = Logger.getLogger(com.batsw.Main.class);

//   public  static final Logger log = LogManager.getLogManager().getLogger("com.batsw.Main.class");
  //  public  static final Logger log =
    //   private static final Logger log = Logger.getLogger(com.batsw.Main.class);
    public static void main (String[] argv){
        try {
            // IPC
            ReceivedMessage receivedMessage = new ReceivedMessage();
            IIpcClient ipcClinet = new IpcClient(receivedMessage);
            ipcClinet.start();
            // EventManger


            //Bundle
            ReturnValue result;
            TorchatConfigReader cfr = new TorchatConfigReader();
            TorchatcfgParser tcp = new TorchatcfgParser();
            TorrcFileParser trp = new TorrcFileParser();
            Bundle bundle = new Bundle(cfr, tcp, trp, ipcClinet);
            result = bundle.getConfiguration();
            if (!result.getSuccess()) {
//                //log.error ("Invalid app configuration please contact suport");
            }

            bundle.run();
            // RUN RECEIVER
            final Receiver receiver = new Receiver(Integer.parseInt(bundle.getBundleInfo().getHiddenServicePort()));
            new Thread(new Runnable() {
                public void run() {
                    while (true) {
                        receiver.run();
                    }
                }
            }).start();

            // RUN PUBLISHER
            //     Publisher publisher = new Publisher(bundle.getBundleInfo(),"ecdw3wod4uqaufbk.onion ");
            //  publisher.run();
            ipcClinet.joinClient();

        } catch (Exception e ){
//           //log.error("Application stopped: " + e.getMessage());
        }

    }
}