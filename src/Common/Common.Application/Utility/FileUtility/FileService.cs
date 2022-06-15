using Microsoft.AspNetCore.Http;

namespace Common.Application.Utility.FileUtility;

public class FileService : IFileService
{
    public void DeleteDirectory(string directoryPath)
    {
        if (Directory.Exists(directoryPath))
            Directory.Delete(directoryPath, true);
    }

    public async Task<List<string>> SaveMultipleFilesAndGenerateNames(List<IFormFile> files, string directoryPath)
    {
        var fileNames = new List<string>();

        foreach (var file in files)
        {
            var fileName = await SaveFileAndGenerateName(file, directoryPath);
            fileNames.Add(fileName);
        }

        return fileNames;
    }

    public void DeleteFile(string path, string fileName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), path,
            fileName);
        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    public void DeleteMultipleFiles(string path, List<string> fileNames)
    {
        foreach (var fileName in fileNames)
        {
            DeleteFile(path, fileName);
        }
    }

    public void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    public async Task SaveFile(IFormFile file, string directoryPath)
    {
        if (file == null)
            throw new InvalidDataException("File is null");

        var fileName = file.FileName;

        var folderName = Path.Combine(Directory.GetCurrentDirectory(), directoryPath.Replace("/", "\\"));

        if (!Directory.Exists(folderName))
            Directory.CreateDirectory(folderName);

        var path = Path.Combine(folderName, fileName);
        await using var stream = new FileStream(path, FileMode.Create);

        await file.CopyToAsync(stream);
    }

    public async Task SaveMultipleFile(List<IFormFile> files, string directoryPath)
    {
        foreach (var file in files)
        {
            await SaveFile(file, directoryPath);
        }
    }

    public async Task<string> SaveFileAndGenerateName(IFormFile file, string directoryPath)
    {
        if (file == null)
            throw new InvalidDataException("File is null");

        var fileName = file.FileName;

        fileName = Guid.NewGuid() + DateTime.Now.TimeOfDay.ToString()
            .Replace(":", "")
            .Replace(".", "") + Path.GetExtension(fileName);

        var folderName = Path.Combine(Directory.GetCurrentDirectory(), directoryPath.Replace("/", "\\"));

        if (!Directory.Exists(folderName))
            Directory.CreateDirectory(folderName);

        var path = Path.Combine(folderName, fileName);

        await using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);
        return fileName;
    }
}