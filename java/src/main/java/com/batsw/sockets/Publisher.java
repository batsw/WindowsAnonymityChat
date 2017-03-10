package com.batsw.sockets;

import com.batsw.torExpertBundleController.model.TorBundleInformation;
import org.apache.log4j.Logger;

import java.io.*;
import java.net.InetSocketAddress;
import java.net.Proxy;
import java.net.Socket;
import java.net.SocketAddress;
import java.net.UnknownHostException;
import java.util.Scanner;




public class Publisher {
	//public static final Logger log = Logger.getLogger(Publisher.class);

	private final String LOCALHOST = "127.0.0.1";
	private final int OUTPUT_PORT = 80;

	private TorBundleInformation torBundleInformation;
	private String destinationAddress;
	private int hiddenServicePort ;
	private int socksPort ;
	private String message = "";

	private static Socket socket;
	private DataOutputStream writeMessage;
	private DataInputStream readMessage;

	public Publisher(TorBundleInformation torBundleInformation, String destinationAddress){
		this.torBundleInformation = torBundleInformation;
		this.destinationAddress = destinationAddress;
	}

	public void run() {
		try {
			OutputStream outToServer;
			InputStream sentByServer;
			//log.info("Connecting to local proxy");
			convertToInt();
			SocketAddress proxyAddress = new InetSocketAddress(LOCALHOST, socksPort);
			Proxy proxy = new Proxy (Proxy.Type.SOCKS, proxyAddress);
			socket = new Socket(proxy);

			//log.info("Connecting to " + destinationAddress);
			InetSocketAddress destination = new InetSocketAddress(destinationAddress, OUTPUT_PORT);
			socket.connect(destination);
			//log.info("Connected to " + destinationAddress);

			outToServer = socket.getOutputStream();
			sentByServer = socket.getInputStream();
			writeMessage = new DataOutputStream(outToServer);
			readMessage = new DataInputStream(sentByServer);

			Scanner input = new Scanner(System.in);
			while (!message.equals("QUIT")) {
				//log.info("Please Enter Message");
				message = input.nextLine();

				String EOL = System.lineSeparator();
				writeMessage.writeUTF(message + EOL);
				writeMessage.flush();
				String s = readMessage.readUTF();
				System.out.println("Server says " + s);
			}
			input.close();

		} catch (UnknownHostException unknownHost) {
			System.err.println("You are trying to connect to an unknown host!");
		} catch (IOException ioException) {
			ioException.printStackTrace();
		}
	}

	private Boolean convertToInt( ) {
		try {
			socksPort = Integer.parseInt(torBundleInformation.getSocksPort());
			hiddenServicePort = Integer.parseInt(torBundleInformation.getHiddenServicePort());
		} catch ( Exception e) {
			return false;
		}
		return true;
	}
}
