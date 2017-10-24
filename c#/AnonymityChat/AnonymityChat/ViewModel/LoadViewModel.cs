using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using System;
using System.Threading;
//using AnonymityChat.Service;
using AnonymityChat.Services;
using System.Diagnostics;
using System.Threading.Tasks;
using System.ComponentModel;

using AnonymityChat.Model;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.Concurrent;

using IPCServer;

namespace AnonymityChat.ViewModel
{
  public class LoadViewModel : ViewModelBase
  {
    public LoadViewModel()
    {
      ipcServerTask.WorkerSupportsCancellation = true;
      ipcServerTask.WorkerReportsProgress = true;
      ipcServerTask.DoWork += new DoWorkEventHandler(ipcServerTask_DoWork);
    }
  
    //private void StartIpcServer()
    //{
    //  ConcurrentQueue<string> queue = SimpleIoc.Default.GetInstance<ConcurrentQueue<string>>();
    //  PipeWrite pw = new PipeWrite(queue);
    //  SimpleIoc.Default.Register<PipeWrite>();
    //  PipeRead pr = new PipeRead(ShowMessage);
    //  KeyBoardInput ki = new KeyBoardInput(pw.WriteToClient);
    //  Thread tw = new Thread(new ThreadStart(pw.ServerThread));
    //  Thread tr = new Thread(new ThreadStart(pr.ServerThread));
    //  tw.Start();
    //  tr.Start();
    //  tw.Join();
    //  tr.Join();
    //}
  
    public  void ShowMessage(IpcMessage message)
    {
      Console.WriteLine("Message received from client:\n");
      Console.WriteLine(message.Body);
      TorBundleParser torBundleParser = new TorBundleParser();
      BundleLogMessage = torBundleParser.parse(message.Body);

    }
  
    public RelayCommand startIpcServerCommand;
    public RelayCommand StartIpcServerCommand
    {
      get
      {
        return startIpcServerCommand ?? (new RelayCommand(
         () =>
          {  
          }));
      }     
    }
    
    public RelayCommand starAnonimitytBundleCommand;
    public RelayCommand StartAnonimityBundleCommand
    {
      get
      {
        return starAnonimitytBundleCommand ?? (new RelayCommand(
         () =>
          {   
            IIpcServer server = SimpleIoc.Default.GetInstance<IIpcServer>();
            ipcTask = Task.Run(() => server.Start(ShowMessage));
            Thread.Sleep(5000);
            ipcServerTask.RunWorkerAsync();
          }));
      }
    }
    
    private void ipcServerTask_DoWork(object sender, DoWorkEventArgs e)
    {

      // http://stackoverflow.com/questions/873809/how-to-execute-a-java-program-from-c
      //TODO: Uncomment to start TorBundle from C#
      //  torExpertBundleController.start();
    }

    private TorExpertBundleLauncher torExpertBundleController = new TorExpertBundleLauncher();
    private Task ipcTask;
    private BackgroundWorker ipcServerTask = new BackgroundWorker();
    private BundleLogMessage bundleLogMessage = new BundleLogMessage()
    {
      LoadingPercent = 0.0,
      LogMessage = "Starting Tor Bundle Expert"
    };
    public BundleLogMessage BundleLogMessage
    {
      get
      {
        return bundleLogMessage;
      }
      set
      {
        Set(() => BundleLogMessage, ref bundleLogMessage, value);
      }
    }

    private RelayCommand<Object> loadMainWindow;

    public RelayCommand<Object> LoadMainWindow
    {
      get
      {
        return loadMainWindow ?? (new RelayCommand<Object>(
         (uiObject) =>
         {
           if (BundleLogMessage.LoadingPercent == 100.0)
           {
           
             System.Windows.Window window = uiObject as System.Windows.Window;
             Window w = SimpleIoc.Default.GetInstance<View.MainWindow>();
             w.Show();
             window.Close();
             ConcurrentQueue<string>  queue =  SimpleIoc.Default.GetInstance<ConcurrentQueue<string>>();
             queue.Enqueue("Done Model loaded");
           }
         }));
      }
    }
  }
}