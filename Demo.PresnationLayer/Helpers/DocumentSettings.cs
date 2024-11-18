using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PresnationLayer.Helpers
{
    public static class DocumentSettings
    {
        // Upload
        public static string UploadFile(IFormFile file, string FolderName)
        {
            // 1- Get Located Folder Path
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            // 2- Get File Name And Make It Unique
            string FileName = $"{Guid.NewGuid()}{file.FileName}";
            // 3- Get File Path[Folder Path + FileName ] 
            string FilePath = Path.Combine(FolderPath, FileName);
            // 4- Save File As Streams
           using var Fs = new FileStream(FilePath , FileMode.Create);
            file.CopyTo(Fs);
            // 5- Return File Name 
            return FileName;


        }

        // Delete
    }
}