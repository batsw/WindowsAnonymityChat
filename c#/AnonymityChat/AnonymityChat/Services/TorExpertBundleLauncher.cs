using System;
using System.Diagnostics;

namespace AnonymityChat.Services
{
  public class TorExpertBundleLauncher
  {
   public TorExpertBundleLauncher()
    {
      Configure();
    }
    private void Configure()
    {

      var processInfo = new ProcessStartInfo("java","-cp TorExpertBundleController-1.0-SNAPSHOT-jar-with-dependencies.jar com.batsw.Main")
      {
       CreateNoWindow = true,
        UseShellExecute = false
      };
     // processInfo.WorkingDirectory = "C:\\Users\\adria\\Documents\\InteliJ\\tor\\New folder\\WindowsAnonymityChat\\java\\target";
      torBundleInfo = processInfo;
  
      // torBundleExpertHandle.StartInfo.Arguments = "java -cp TorExpertBundleController-1.0-SNAPSHOT-jar-with-dependencies.jar com.batsw.Main";
     // torBundleExpertHandle.StartInfo.FileName = "cmd";
     // torBundleExpertHandle.StartInfo.WorkingDirectory = "\"C:\\Users\\adria\\Documents\\InteliJ\\tor\\New folder\\WindowsAnonymityChat\\java\\target\"";
      
    }

    public void start()
    {
      // torBundleExpertHandle.Start();
      torBundleExpertHandle = Process.Start(torBundleInfo);
      if (torBundleExpertHandle == null) Console.WriteLine("cannot start tor");
      Console.WriteLine("tor started");
    }
    public Process TorBundleExpertHandle
    {
      get
      {
        return torBundleExpertHandle;
      }
    }
    private Process torBundleExpertHandle = new Process();
    private ProcessStartInfo torBundleInfo;
  }
}
