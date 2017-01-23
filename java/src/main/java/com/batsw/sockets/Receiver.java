package com.batsw.sockets;

import java.io.*;
import java.net.ServerSocket;
import java.net.Socket;

public class Receiver {

	private ServerSocket serverSocket;
	private Socket clientSocket = null;

	private int serverPort;

	private DataInputStream dataInputStream;
	private DataOutputStream dataOutputStream;

	public Receiver(int serverPort){
		this.serverPort = serverPort;
	}

	public void run() {
		try {

			serverSocket = new ServerSocket(serverPort, 10);
			System.out.println("Waiting for clientSocket");
			clientSocket = serverSocket.accept();
			System.out.println("Connection received from " + clientSocket.getInetAddress().getHostName());

			dataInputStream = new DataInputStream(clientSocket.getInputStream());
			dataOutputStream =new DataOutputStream(clientSocket.getOutputStream());

			new Thread(new Runnable() {
				public void run() {
					while (true) {
						String incomingMessageFromServer;
						try {
							if (dataInputStream != null) {
								incomingMessageFromServer = dataInputStream.readUTF();
								System.out.println("Message Received " + incomingMessageFromServer);
								String EOL = System.lineSeparator();
								dataOutputStream.writeUTF("Received" + EOL);
								dataOutputStream.flush();
							}
						} catch (IOException e) {
							//TODO : I don't think the connetion should be closed ... EVER ... for the future

							try {
								clientSocket.close();
							} catch (IOException e1) {
								// TODO Auto-generated catch block
								e1.printStackTrace();
							}
							System.err.println("error: " + e.getMessage());
							break;
						}
					}
				}
			}).start();

		} catch (IOException ioException) {
			ioException.printStackTrace();
		} finally {
			try {
				serverSocket.close();
			} catch (IOException ioException) {
				ioException.printStackTrace();
			}
		}
	}
}
