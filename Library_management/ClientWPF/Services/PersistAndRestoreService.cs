using System.Collections;
using System.IO;

using ClientWPF.Contracts.Services;
using ClientWPF.Core.Contracts.Services;
using ClientWPF.Models;

using Microsoft.Extensions.Options;

namespace ClientWPF.Services;

public class PersistAndRestoreService : IPersistAndRestoreService
{
    private readonly ILibraryManagementService _fileService;
    private readonly AppConfig _appConfig;
    private readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    public PersistAndRestoreService(ILibraryManagementService fileService, IOptions<AppConfig> appConfig)
    {
        _fileService = fileService;
        _appConfig = appConfig.Value;
    }

    public void PersistData()
    {
        _fileService.RewriteDbTables();
    }

    public void RestoreData()
    {
        _fileService.RestoreData();
    }
}
