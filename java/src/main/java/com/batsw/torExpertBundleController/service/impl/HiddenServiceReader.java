package com.batsw.torExpertBundleController.service.impl;

import com.batsw.torExpertBundleController.common.FileHandler;
import com.batsw.torExpertBundleController.common.ReturnValue;
import org.apache.log4j.Logger;


/**
 *  Provides methods for finding  and reading the hostname of the tor bundle
 */
public class HiddenServiceReader {
	public static final Logger log = Logger.getLogger(HiddenServiceReader.class);
	private String hiddenServiceDirectory;
	private final String HOSTNAME_FILE = "hostname";
	private final String PRIVATE_KEY_FILE = "private_key";
	
	public HiddenServiceReader(String path) {
		hiddenServiceDirectory = path;
	}
	
	public ReturnValue getHiddenServiceStatus() {
	 	log.info("Hidden service location " + hiddenServiceDirectory);
		ReturnValue returnValue = new ReturnValue();
		if(!FileHandler.verifyIfFolderExists(hiddenServiceDirectory)) {
			returnValue.Add("Hidden service directory not found");
			log.error("Hidden Service directory not found verify  torrc file");
			returnValue.setSuccess(false);
		}
		else {
			if (!(FileHandler.verifyIfFileExists(hiddenServiceDirectory + "\\" + HOSTNAME_FILE) &&
				FileHandler.verifyIfFileExists(hiddenServiceDirectory +  "\\" + PRIVATE_KEY_FILE))) {
				returnValue.Add("Hidden Service directory is empty");
				log.error("Hidden Service directory empty");
				returnValue.setSuccess(false);
			}
		}
		return returnValue;
	}		
	public String getHostname() {
		ReturnValue hiddenServiceStatus = getHiddenServiceStatus();
		log.info("Searching for  file " + HOSTNAME_FILE + " in " + hiddenServiceDirectory);
		if (hiddenServiceStatus.getSuccess()) {

			return FileHandler.readFile(hiddenServiceDirectory + "\\" + HOSTNAME_FILE);
		}
		return null;
	}
	public String getKey(){
		ReturnValue hiddenServiceStatus = getHiddenServiceStatus();
		if (hiddenServiceStatus.getSuccess()) {
			return FileHandler.readFile(hiddenServiceDirectory + "\\" + PRIVATE_KEY_FILE);
		}
		return null;
	}
}