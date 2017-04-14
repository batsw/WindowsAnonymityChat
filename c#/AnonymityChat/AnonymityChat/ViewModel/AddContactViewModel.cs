using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymityChat.ViewModel
{
  public class AddContactViewModel : ViewModelBase
  {
    private RelayCommand<Object> addContact;
    public RelayCommand<Object> AddContact
    {
      get
      {
        return addContact ?? (new RelayCommand<Object>(
          (uiObject) =>
          {
            Model.Contact tmpContact = new Model.Contact();
            tmpContact.Alias = Alias;
            tmpContact.OnionUrl = OnionUrl;
            Model.ContactList tmpList = SimpleIoc.Default.GetInstance<Model.ContactList>();
            tmpList.Contacts.Add(tmpContact);
            System.Windows.Window window = uiObject as System.Windows.Window;
            window.Closed += (sender, args) =>
            {
              SimpleIoc.Default.Unregister<View.AddContactWindow>();
              SimpleIoc.Default.Register<View.AddContactWindow>();
            };
            window.Close();
            OnionUrl = "";
            Alias = "";
          }));
      }
    }
    private RelayCommand<Object> closeWindow;
    public RelayCommand<Object> CloseWindow
    {
      get
      {
        return closeWindow ?? (new RelayCommand<Object>(
          (uiObject) => 
          {
              System.Windows.Window window = uiObject as System.Windows.Window; 
              window.Close();
          }));
      }
    }

    public string Alias
    {
      get
      {
        return alias;
      }
      set
      {
        Set(() => Alias, ref alias, value);
      }
    }
    public string OnionUrl
    {
      get
      {
        return onionUrl;
      }
      set
      {
        Set(() => OnionUrl, ref onionUrl, value);
      }
    }


    private string alias;
    private string onionUrl;

  }
}
