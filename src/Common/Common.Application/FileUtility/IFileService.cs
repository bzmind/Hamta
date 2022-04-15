using Microsoft.AspNetCore.Http;

namespace Common.Application.FileUtility;

public interface IFileService
{
    /// <summary>
    /// Saves the file with original file name
    /// </summary>
    /// <param name="file"></param>
    /// <param name="directoryPath"></param>
    /// <returns></returns>
    Task SaveFile(IFormFile file, string directoryPath);

    /// <summary>
    /// Saves the files with their original file name
    /// </summary>
    /// <param name="files"></param>
    /// <param name="directoryPath"></param>
    /// <returns></returns>
    Task SaveMultipleFile(List<IFormFile> files, string directoryPath);

    /// <summary>
    /// Saves the file with a unique name and returns the file name
    /// </summary>
    /// <param name="file"></param>
    /// <param name="directoryPath"></param>
    /// <returns></returns>
    Task<string> SaveFileAndGenerateName(IFormFile file, string directoryPath);

    /// <summary>
    /// Saves the file with a unique name and returns the file name
    /// </summary>
    /// <param name="files"></param>
    /// <param name="directoryPath"></param>
    /// <returns></returns>
    Task<List<string>> SaveMultipleFilesAndGenerateNames(List<IFormFile> files, string directoryPath);
    void DeleteFile(string path, string fileName);
    void DeleteMultipleFiles(string path, List<string> fileNames);
    void DeleteFile(string filePath);
    void DeleteDirectory(string directoryPath);
}