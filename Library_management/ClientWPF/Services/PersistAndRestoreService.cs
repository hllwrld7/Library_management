using System.Collections;
using System.IO;

using ClientWPF.Contracts.Services;
using ClientWPF.Core.Contracts.Services;
using ClientWPF.Models;

using Microsoft.Extensions.Options;

namespace ClientWPF.Services;

public class PersistAndRestoreService : IPersistAndRestoreService
{
    private readonly ISqliteService _fileService;
    private readonly AppConfig _appConfig;
    private readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    public PersistAndRestoreService(ISqliteService fileService, IOptions<AppConfig> appConfig)
    {
        _fileService = fileService;
        _appConfig = appConfig.Value;
    }

    public void PersistData()
    {
    }

    public void RestoreData()
    {
        //var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
        //var fileName = _appConfig.AppPropertiesFileName;
        //var properties = _fileService.Read<IDictionary>(folderPath, fileName);
        //if (properties != null)
        //{
        //    foreach (DictionaryEntry property in properties)
        //    {
        //        App.Current.Properties.Add(property.Key, property.Value);
        //    }
        //}
    }
}
