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


  }
}
