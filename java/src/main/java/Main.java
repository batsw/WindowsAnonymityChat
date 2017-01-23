import com.batsw.sockets.Receiver;
import com.batsw.torExpertBundleController.service.impl.*;
import com.batsw.torExpertBundleController.common.*;

import com.batsw.sockets.Publisher;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

public class Main {
    public  static final Logger log = LogManager.getLogger(Main.class);
    public static void main (String[] argv){
        try{
            ReturnValue result;
            TorchatConfigReader cfr = new TorchatConfigReader();
            TorchatcfgParser tcp = new TorchatcfgParser();
            TorrcFileParser trp = new TorrcFileParser();
            Bundle bundle = new Bundle(cfr, tcp, trp);
            result = bundle.getConfiguration();
            if (!result.getSuccess()) {
                log.error ("Invalid app configuration please contact suport");
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


        } catch (Exception e ){
            log.error("Application stopped: " + e.getMessage());
        }
    }
}