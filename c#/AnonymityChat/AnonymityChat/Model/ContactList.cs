using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymityChat.Model
{
  public class ContactList: ObservableObject
  {

    public ContactList()
    {
     
    }
      

    public ObservableCollection<Contact> Contacts
    {
      get
      {
        return contactList;
      }
      set
      {
        Set(() => Contacts, ref contactList, value);
      }
    }
    private ObservableCollection<Contact> contactList = new ObservableCollection<Contact>();

  }
}

