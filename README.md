

# WindowsAnonymityChat

## Purpose
The purpose of the project is to help people chat without the fear that their messages will be track. The messages are sent using the Tor network(for more information check the following [link](https://torproject.org/) this making them almost imposible to track the sender. 

# Features
* Serveles application - does not depend on an existing server
* Secure transmission enviroment over Tor network
* Simple user interface

## Arhitecture 
This project contains the desktop version of AnonymityChat application and contains the following modules:
  *  a Java back end (that conects to the tor bunde networks and makes the communication over tor)
  * a C# User interface 
  * a SQLite database used to store the messages and syncronize the two applications
 
### Java back end
The java backed consists of two parts:
* [TorExpertBundleController](https://github.com/batsw/TorExpertBundleController) that manages the Tor Expert Bundle
* And a socket library used to exchange data over the Tor network

### C# User interface
The C# UI will contain 3 windows:
* a loading window(displayed when application connects to Tor network
* a main window(in whitch you have a menu bar and see the existing contacts
* a chat window(in witch you initiate a conversation with an user) 
    
### SQLite database
The main tasks of the database is to store the existing conversations and syncronize the previouse two components using triggers and stored procedures 


## The application is under devlopment and depends on the following projects
* [TorExpertBundleController](https://github.com/batsw/TorExpertBundleController) 

