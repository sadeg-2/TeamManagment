using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using System.Drawing;
using System.IO;
using System.Numerics;

using System.Reflection.PortableExecutable;

using TeamManagment.Core.Helper;
using Tesseract;

namespace TeamManagment.Infrastructure.Services
{
    public class FileService : IFileService
    {

        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<byte[]> GetFile(string folderName, string fileName)
        {
            var filePath = Path.Combine(_env.WebRootPath, folderName, fileName);
            return await File.ReadAllBytesAsync(filePath);
        }
        
        public async Task<string> GetFileBase64(string folderName, string fileName)
        {
            var file = await GetFile(folderName, fileName);
            return Convert.ToBase64String(file);
        }

        public async Task<string> SaveFile(IFormFile file, string folderName)
        {
            string fileName = null;
            if (file != null && file.Length > 0)
            {
               
                var uploads = Path.Combine(_env.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                await using var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }

        public async Task<string> SaveFile(byte[] file, string folderName, string extension)
        {
            string fileName = null;
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;
                await File.WriteAllBytesAsync(Path.Combine(uploads, fileName), file);
            }

            return fileName;
        }

        public async Task<string> SaveFile(string file, string folderName, string extension)
        {
            string fileName = null;
            if (!string.IsNullOrWhiteSpace(file))
            {
                file = file.Substring(file.IndexOf(",", StringComparison.Ordinal) + 1);
                var bytes = Convert.FromBase64String(file);
                var uploads = Path.Combine(_env.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;
                await File.WriteAllBytesAsync(Path.Combine(uploads, fileName), bytes);
            }
            return fileName;
        }

        public async Task<string> SaveImage(IFormFile file, string folderName)
        {
            MemoryStream m = new MemoryStream();
            file.CopyTo(m);
            var bytesOfFile = m.ToArray();
            var allowedImageContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            if (file.Length > 5000000 && file.Length < 0)
            {
                throw new Exception();
            }
            if (!(IsJpegImage(bytesOfFile) || IsJpgImage(bytesOfFile) || IsPngImage(bytesOfFile)))
            {
                throw new Exception();
            }
            //if (HasText(bytesOfFile))
            //{
            //    throw new Exception();
            //}
            if (!allowedImageContentTypes.Contains(file.ContentType))
            {
                throw new Exception();
            }
            return await SaveFile(file, folderName);
        }
        private bool IsJpegImage(byte[] bytes)
        {
            byte[] jpegHeader = { 0xFF, 0xD8 };
            byte[] jpegEnd = { 0xFF, 0xD9 };

            return HasHeaderAndEnd(bytes, jpegHeader, jpegEnd);
        }

        private bool IsJpgImage(byte[] bytes)
        {
            byte[] jpgHeader = { 0xFF, 0xD8 };
            byte[] jpgEnd = { 0xFF, 0xD9 };

            return HasHeaderAndEnd(bytes, jpgHeader, jpgEnd);
        }

        private bool IsPngImage(byte[] bytes)
        {
            byte[] pngHeader = { 0x89, 0x50, 0x4E, 0x47 };
            byte[] pngEnd = { 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82 };

            return HasHeaderAndEnd(bytes, pngHeader, pngEnd);
        }
        private bool HasHeaderAndEnd(byte[] bytes, byte[] header, byte[] end)
        {
            int headerLength = header.Length;
            int endLength = end.Length;
            int fileLength = bytes.Length;
            if (fileLength < headerLength + endLength)
            {
                return false;
            }
            for (int i = 0; i < headerLength; i++)
            {
                if (bytes[i] != header[i])
                {
                    return false;
                }
            }
            for (int i = 0; i < endLength; i++)
            {
                if (bytes[fileLength - endLength + i] != end[i])
                {
                    return false;
                }
            }
            return true;
        }

        public bool HasText(byte[] imageBytes)
        {
            try
            {
                var engine = new TesseractEngine("C:\\Users\\Sadeg M\\.nuget\\packages\\tesseract\\3.3.0\\x64\\libtesseract3052.dll", "eng");
                using (var img = Pix.LoadTiffFromMemory(imageBytes))
                {
                    using (var page = engine.Process(img))
                    {
                        string text = page.GetText();
                        return !string.IsNullOrWhiteSpace(text);
                    }
                }
            }
            catch (Exception)
            {

            }
            return false;
        }
        //public String DoOCR([FromForm] OcrModel request)
        //{

        //    string name = request.Image.FileName;
        //    var image = request.Image;

        //    if (image.Length > 0)
        //    {
        //        using (var fileStream = new FileStream(folderName + image.FileName, FileMode.Create))
        //        {
        //            image.CopyTo(fileStream);
        //        }
        //    }

        //    string tessPath = Path.Combine(trainedDataFolderName, "");
        //    string result = "";

        //    using (var engine = new TesseractEngine(tessPath, request.DestinationLanguage, EngineMode.Default))
        //    {
        //        using (var img = Pix.LoadFromFile(folderName + name))
        //        {
        //            var page = engine.Process(img);
        //            result = page.GetText();
        //            Console.WriteLine(result);
        //        }
        //    }
        //    return String.IsNullOrWhiteSpace(result) ? "Ocr is finished. Return empty" : result;
        //}





    }
}
