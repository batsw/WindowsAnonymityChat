using GalaSoft.MvvmLight;
using System;

namespace AnonymityChat.Model
{
  public class BundleLogMessage : ObservableObject
  {
    public BundleLogMessage()
    {
      loadingPercent = 0.0;
      logMessage = "Starting Tor Bundle Expert";
    }
    public double LoadingPercent
    {
      get
      {
        return loadingPercent;
      }
      
      set
      {
        Set<double>(() => this.LoadingPercent, ref loadingPercent, value);
      }
    }

    public String LogMessage
    {
      get
      {
        return logMessage;
      }
      set
      {
        Set<String>(() => this.LogMessage, ref logMessage, value);
      }
    }

    private double loadingPercent;
    private String logMessage;
  }
}