using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachingAppAPI.Services
{
    public interface IFileUploadService
    {
        Task<string[]> UploadImage(String base64str);
        void DeleteOldImageFromBucket(string filename);
    }
}
