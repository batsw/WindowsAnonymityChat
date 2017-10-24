/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:AnonymityChat"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using AnonymityChat.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Concurrent;
using IPCServer;
namespace AnonymityChat.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<LoadViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AddContactViewModel>();
            SimpleIoc.Default.Register<ChatViewModel>();

            SimpleIoc.Default.Register<MainWindow>();
            SimpleIoc.Default.Register<AddContactWindow>();
            SimpleIoc.Default.Register<ChatWindow>();
            SimpleIoc.Default.Register<ConcurrentQueue<string>>( () =>{ return new ConcurrentQueue<string>(); });
            SimpleIoc.Default.Register<Model.ContactList>();
            SimpleIoc.Default.Register<IIpcServer, IpcServer>();

    }
    public ChatViewModel Chat
    {
      get
      {
        return ServiceLocator.Current.GetInstance<ChatViewModel>();
      }     
    }

    public Model.ContactList ContactList
    {
      get
      {
        return ServiceLocator.Current.GetInstance<Model.ContactList>();
      }
      
    }

    public LoadViewModel Load
    {
        get
        {
            return ServiceLocator.Current.GetInstance<LoadViewModel>();
        }
    }
    
    public AddContactViewModel AddContact
    {
      get
      {
        return ServiceLocator.Current.GetInstance<AddContactViewModel>();
      }
    }  
    
    public IIpcServer IpcServer
    {
      get
      {
        return ServiceLocator.Current.GetInstance<IIpcServer>();
      }
    }
       
    public MainViewModel Main
    {
      get
      {
        return ServiceLocator.Current.GetInstance<MainViewModel>();
      }
    }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
            
        }
    }
}