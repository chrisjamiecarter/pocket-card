using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PocketCards.DataLoader.Models;
using PocketCards.Domain.Entities;
using PocketCards.Domain.Services;

namespace PocketCards.DataLoader;

internal class App : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IHost _host;
    private readonly ILogger<App> _logger;
    private readonly IPocketCardService _pocketCardService;
    private readonly IPocketPackService _pocketPackService;
    private readonly string _doneDirectoryPath;
    private readonly string _errorDirectoryPath;
    private readonly string _inputDirectoryPath;
    private readonly string _workingDirectoryPath;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public App(IConfiguration configuration, IHost host, ILogger<App> logger, IPocketCardService pocketCardService, IPocketPackService pocketPackService)
    {
        _configuration = configuration;
        _host = host;
        _logger = logger;
        _pocketCardService = pocketCardService;
        _pocketPackService = pocketPackService;
        _jsonSerializerOptions = JsonSerializerOptions.Web;

        var rootDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "PocketCards.DataLoader");
        if (!Directory.Exists(rootDirectoryPath))
        {
            _logger.LogInformation("Creating: {directoryPath}", rootDirectoryPath);
            Directory.CreateDirectory(rootDirectoryPath);
        }

        _doneDirectoryPath = Path.GetFullPath(Path.Combine(rootDirectoryPath, "Done"));
        if (!Directory.Exists(_doneDirectoryPath))
        {
            _logger.LogInformation("Creating: {directoryPath}", _doneDirectoryPath);
            Directory.CreateDirectory(_doneDirectoryPath);
        }

        _errorDirectoryPath = Path.GetFullPath(Path.Combine(rootDirectoryPath, "Error"));
        if (!Directory.Exists(_errorDirectoryPath))
        {
            _logger.LogInformation("Creating: {directoryPath}", _errorDirectoryPath);
            Directory.CreateDirectory(_errorDirectoryPath);
        }

        _inputDirectoryPath = Path.GetFullPath(Path.Combine(rootDirectoryPath, "Input"));
        if (!Directory.Exists(_inputDirectoryPath))
        {
            _logger.LogInformation("Creating: {directoryPath}", _inputDirectoryPath);
            Directory.CreateDirectory(_inputDirectoryPath);
        }

        _workingDirectoryPath = Path.GetFullPath(Path.Combine(rootDirectoryPath, "Working"));
        if (!Directory.Exists(_workingDirectoryPath))
        {
            _logger.LogInformation("Creating: {directoryPath}", _workingDirectoryPath);
            Directory.CreateDirectory(_workingDirectoryPath);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            foreach (var fileInfo in new DirectoryInfo(_inputDirectoryPath).EnumerateFiles())
            {
                try
                {
                    fileInfo.MoveTo(Path.Combine(CreateWorkingSubDirectoryPath(), fileInfo.Name));

                    if (fileInfo.Extension != ".json")
                    {
                        _logger.LogInformation("Unable to process none json file: {fileName}", fileInfo.Name);
                        fileInfo.MoveTo(Path.Combine(_errorDirectoryPath, fileInfo.Name));
                        continue;
                    }

                    string jsonString = await File.ReadAllTextAsync(fileInfo.FullName, stoppingToken);

                    var pocketCardDataItems = JsonSerializer.Deserialize<List<PocketCardData>>(jsonString, _jsonSerializerOptions);
                    if (pocketCardDataItems is null || pocketCardDataItems.Count == 0)
                    {
                        _logger.LogTrace("No items to process for file: {fileName}", fileInfo.Name);
                        continue;
                    }

                    foreach (var dataItem in pocketCardDataItems)
                    {
                        var pocketPack = await _pocketPackService.ReturnByNameAsync(dataItem.Pack);
                        if (pocketPack is null)
                        {
                            pocketPack = new PocketPack
                            {
                                Id = Guid.CreateVersion7(),
                                Name = dataItem.Pack,
                            };

                            await _pocketPackService.CreateAsync(pocketPack);
                        }

                        var pocketCard = await _pocketCardService.ReturnByNumberAsync(dataItem.Number);
                        if (pocketCard is null)
                        {
                            pocketCard = new PocketCard
                            {
                                Id = Guid.CreateVersion7(),
                                Number = dataItem.Number,
                                Name = dataItem.Name,
                                ImageFilePath = _pocketCardService.GetImageFilePath(dataItem.ImageFileName),
                                Rarity = dataItem.Rarity,
                                Type = dataItem.Type,
                                HitPoints = dataItem.HitPoints,
                                Stage = dataItem.Stage,
                                PackPoints = dataItem.PackPoints,
                                PocketPackId = pocketPack.Id,
                            };

                            await _pocketCardService.CreateAsync(pocketCard);
                        }
                    }

                    _logger.LogTrace("Finished processing: {fileName}", fileInfo.Name);
                    fileInfo.MoveTo(Path.Combine(_doneDirectoryPath, fileInfo.Name));
                }
                catch (Exception exception)
                {
                    _logger.LogWarning("Issue processing {fileName}: {message}", fileInfo.Name, exception.Message);
                    fileInfo.MoveTo(Path.Combine(_errorDirectoryPath, fileInfo.Name));
                }
            }

            PerformHousekeeping();
        }
        catch (Exception exception)
        {
            _logger.LogError("{message}", exception.Message);
        }

        await _host.StopAsync(stoppingToken);
    }

    private string CreateWorkingSubDirectoryPath()
    {
        string directoryName = $"{DateTime.Now:yyyyMMdd-HHmmss}";
        string directoryPath = Path.Combine(_workingDirectoryPath, directoryName);

        while (Directory.Exists(directoryPath))
        {
            Thread.Sleep(1000);

            directoryName = $"{DateTime.Now:yyyyMMdd-HHmmss}";
            directoryPath = Path.Combine(_workingDirectoryPath, directoryName);
        }

        _logger.LogInformation("Creating: {directoryPath}", directoryPath);
        Directory.CreateDirectory(directoryPath);
        return directoryPath;
    }

    private void PerformHousekeeping()
    {
        _logger.LogDebug("TODO");
        //throw new NotImplementedException();
    }
}
