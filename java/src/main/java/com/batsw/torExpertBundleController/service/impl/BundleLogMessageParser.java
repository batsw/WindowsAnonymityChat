package com.batsw.torExpertBundleController.service.impl;

import com.batsw.torExpertBundleController.common.StatusEnum;
import com.batsw.torExpertBundleController.service.i.IParser;
//import org.apache.log4j.Logger;


/**
 * Provides methods to parse the log messages received from tor bundle at start-up
 */
public class BundleLogMessageParser implements IParser<StatusEnum> {
//	public static final Logger log = Logger.getLogger(BundleLogMessageParser.class);
	public final String NOTICE_LOG = "[notice]";
	public final String WARNING_LOG = "[warn]";
	public final String ERROR_LOG = "[err]";
	public final String NO_INTERNET_CONNECTION_WARNING = "(No route to host";

	
	public StatusEnum parse(String logToParse) {
		if (logToParse == null ) return StatusEnum.ERROR;
		StatusEnum returnValue;
		String messageType = logToParse.substring(logToParse.indexOf("["), logToParse.indexOf("]")+1);	
		switch (messageType) {
			case NOTICE_LOG:
				if (logToParse.contains("Bootstrapped 100%: Done")) {
					returnValue = StatusEnum.DONE;	
				}
				else {
					returnValue = StatusEnum.LOADING;
				}
				break;
			case WARNING_LOG:
				returnValue = StatusEnum.LOADING;
				break;
			case ERROR_LOG:
				returnValue = StatusEnum.ERROR;
				break;
			default:
				returnValue = StatusEnum.ERROR;
				break;
		}
		return returnValue;
	}
}