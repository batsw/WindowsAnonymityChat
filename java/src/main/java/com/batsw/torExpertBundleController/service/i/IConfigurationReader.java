package com.batsw.torExpertBundleController.service.i;

/**
 *  Interface that must be inherited by all configuration file readers
 */
public interface IConfigurationReader {
	public String getDefaultConfigurationFile(String name);
	public String getConfigurationFile(String path, String name);
}