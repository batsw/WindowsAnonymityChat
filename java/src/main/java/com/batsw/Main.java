package com.batsw;

import com.batsw.sockets.Receiver;
import com.batsw.torExpertBundleController.service.impl.*;
import com.batsw.torExpertBundleController.common.*;
import com.batsw.interProcessCommunication.*;
import com.batsw.sockets.Publisher;

import com.batsw.interProcessCommunication.EventManager;
//
// import org.apache.log4j.Logger;

public class Main {

    //public static final Logger log = Logger.getLogger(com.batsw.Main.class);

//    public  static final Logger log = LogManager.getLogManager().getLogger("com.batsw.Main.class");
//    public  static final Logger log =
//       private static final Logger log = Logger.getLogger(com.batsw.Main.class);
    public static void main (String[] argv){
        try{
            // IPC
            PipeWritter pw = new PipeWritter();
            PipeReader  pr = new PipeReader();
            EventManager eventManager = new EventManager();
            eventManager.addEvenrtListener(pw);
            Thread tr = new Thread(pr);
            tr.start();
            Thread tw = new Thread(pw);
            tw.start();

            // EventManger


            //Bundle
            ReturnValue result;
            TorchatConfigReader cfr = new TorchatConfigReader();
            TorchatcfgParser tcp = new TorchatcfgParser();
            TorrcFileParser trp = new TorrcFileParser();
            Bundle bundle = new Bundle(cfr, tcp, trp,eventManager);
            result = bundle.getConfiguration();
            if (!result.getSuccess()) {
//                //log.error ("Invalid app configuration please contact suport");
            }

            bundle.run();
            // RUN RECEIVER
            final Receiver receiver = new Receiver(Integer.parseInt(bundle.getBundleInfo().getHiddenServicePort()));
            new Thread(new Runnable() {
                public void run(){
                    while(true){
                        receiver.run();
                    }
                }
            }).start();
            
            // RUN PUBLISHER
            Publisher publisher = new Publisher(bundle.getBundleInfo(),"jjnleshvsrqchcl7.onion");
            publisher.run();
            tr.join();
            tw.join();

        } catch (InterruptedException io) {

        } catch (Exception e ){
//           //log.error("Application stopped: " + e.getMessage());
        }

    }
}