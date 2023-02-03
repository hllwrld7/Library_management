using System.Diagnostics;

using ClientWPF.Contracts.Services;

namespace ClientWPF.Services;

public class SystemService : ISystemService
{
    public SystemService()
    {
    }

    public void OpenInWebBrowser(string url)
    {
        var psi = new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        };
        Process.Start(psi);
    }
}
