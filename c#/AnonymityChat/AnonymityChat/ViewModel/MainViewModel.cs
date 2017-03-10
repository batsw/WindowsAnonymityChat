using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using System;
using System.Threading;
using AnonymityChat.Service;
using AnonymityChat.Services;
using System.Diagnostics;



namespace AnonymityChat.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
          StartIpcServer();
        }

        public void StartIpcServer()
        {
          PipeWrite pw = new PipeWrite(ShowMessage);
          PipeRead pr = new PipeRead(ShowMessage);
          KeyBoardInput ki = new KeyBoardInput(pw.WriteToClient);
          Thread tw = new Thread(new ThreadStart(pw.ServerThread));
          Thread tr = new Thread(new ThreadStart(pr.ServerThread));
          tw.Start();
          tr.Start();
          ki.ReadInput();
          tw.Join();
          tr.Join();
        }

        public static void ShowMessage(string message)
        {
          Console.WriteLine("Message received from client:\n");
          Console.WriteLine(message);
        }

    public RelayCommand starAnonimitytBundleCommand;
    public RelayCommand StartAnonimityBundleCommand
    {
      get
      {
        return starAnonimitytBundleCommand ?? (new RelayCommand(
          () =>
          {
            Process bundleHandle = new Process();

          }));
      }
    }
  
    }
}