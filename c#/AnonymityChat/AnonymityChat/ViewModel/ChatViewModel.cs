using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnonymityChat.ViewModel
{
  public class ChatViewModel : ViewModelBase
  {
    public ObservableCollection<string> MessageHistory
    {
      get
      {
        return messageHistory;
      }

      set
      {
        Set(() => MessageHistory, ref messageHistory, value);
      }
    }
    private ObservableCollection<string> messageHistory = new ObservableCollection<string>();

    public RelayCommand<TextBox> AddMessageToHIstory
    {
      get
      {
        return addMessageToHistory ?? (new RelayCommand<TextBox>(
          (textBox) =>
          {
            MessageHistory.Add(textBox.Text);
            textBox.Clear();
          }));
      }
    }
    private RelayCommand<TextBox> addMessageToHistory;

  }
}
