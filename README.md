

# WindowsAnonymityChat
A  serverless chat application that uses Tor network to send messages.


## Arhitecture 

This project will contain the AnonymityChat graphical interface and will communicate (IPC over sockets) with [TorExpertBundleController](https://github.com/batsw/TorExpertBundleController). 

[TorExpertBundleController](https://github.com/batsw/TorExpertBundleController) is an application written in java that manages the Tor Expert Bundle. Also TorExpertBundleController will use the [JSocketIpc](https://github.com/batsw/JSocketIpc) to communicate with  WindowsAnonymityChat


## The application is under devlopment and depends on the following projects
* [TorExpertBundleController](https://github.com/batsw/TorExpertBundleController) 
* [JSocketIpc](https://github.com/batsw/JSocketIpc)


##  Development Status
* [TorExpertBundleController](https://github.com/batsw/TorExpertBundleController) IN PROGRESS 90% DONE
* [JSocketIpc](https://github.com/batsw/JSocketIpc)                               IN PROGRESS 0-5% DONE                              
* [WindowsAnonymityChat](https://github.com/batsw/WindowsAnonymityChat)           OPEN 
