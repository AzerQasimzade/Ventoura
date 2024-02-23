using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Domain.Enums;

namespace Ventoura.Domain.Extensions
{
    public static class FileValidator
    {
        public static bool ValidateFileType(this IFormFile file, FileHelper type)
        {
            string switchtype = file.ContentType;
            switch (type)
            {
                case FileHelper.Image:
                    return switchtype.Contains("image/");
                case FileHelper.Video:
                    return switchtype.Contains("video/");
                case FileHelper.Audio:
                    return switchtype.Contains("audio/");
            }
            return false;
        }
        public static bool ValidateFileSize(this IFormFile file, SizeHelper size)
        {
            long filesize = file.Length;
            switch (size)
            {
                case SizeHelper.kb:
                    return filesize <= 1048;
                case SizeHelper.mb:
                    return filesize <= 1048 * 1048;
                case SizeHelper.gb:
                    return filesize <= 1048 * 1048 * 1048;
            }
            return false;
        }
        public static void DeleteFile(this string filename, string root, params string[] folders)
        {
            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
               path= Path.Combine(path,folders[i]);
            }
            path = Path.Combine(path, filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        public static async Task<string> CreateFileAsync(this IFormFile file, string root, params string[] folders)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            path = Path.Combine(path, fileName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
    }
}