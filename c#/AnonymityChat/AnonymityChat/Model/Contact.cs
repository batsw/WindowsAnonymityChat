using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymityChat.Model
{
  public class Contact : ObservableObject
  {
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
