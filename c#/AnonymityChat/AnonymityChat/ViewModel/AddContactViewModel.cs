using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymityChat.ViewModel
{
  public class AddContactViewModel : ViewModelBase
  {
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

  }
}
