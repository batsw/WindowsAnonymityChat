using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnonymityChat.ViewModel
{
  public class MainViewModel : ViewModelBase
  {

    public MainViewModel()
    {
      ContactList = SimpleIoc.Default.GetInstance<Model.ContactList>();
      ContactList.Contacts.Add(new Model.Contact() {  Alias = "test",  OnionUrl = "test.onion" });
    }

    private RelayCommand startChat;
    public RelayCommand StartChat
    {
      get
      {
        return startChat ?? (new RelayCommand(
        () =>
        {

        }));
        }
    }

    private RelayCommand loadAddContactWindow;
    public RelayCommand LoadAddContactWindow
    {
      get
      {
        return loadAddContactWindow ?? (new RelayCommand(
         () =>
         {
           Window w = SimpleIoc.Default.GetInstance<View.AddContactWindow>();
           w.Show();        
         }));
      }
    }

    public  Model.ContactList ContactList
    {
      get
      {
        return contactList;
      }
      set
      {
        Set(() => ContactList, ref contactList, value);
      }
    }
    private Model.ContactList contactList ;
  }
}
