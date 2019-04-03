using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TeachingAppAPI.Services
{
    public class FileUploadService : IFileUploadService
    {

        private static string accessKey = "AKIAJXGSGKXO5IK4YDIA";
        private static String accessSecret = "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";
        private static String bucket = "scrotums/videos";

        public async Task<string[]> UploadImage(String base64str)
        {

            var fileName = "";
            try
            {
                byte[] bytes = Convert.FromBase64String(base64str);
                var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.APSoutheast2);
                fileName = Guid.NewGuid().ToString();
                PutObjectResponse response = null;

                using (var stream = new MemoryStream(bytes))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = bucket,
                        Key = fileName,
                        InputStream = stream,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    response = await client.PutObjectAsync(request);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("ha" + ex);
            }
            Console.WriteLine("path: " + fileName);
            string[] returnStr = { "https://s3-ap-southeast-2.amazonaws.com/scrotums/", fileName };
            return returnStr;

        }


        /**
         * Method to delete old Image in S3 bucket
         * @param filename: name of the file to be deleted
         */
        public async void DeleteOldImageFromBucket(string filename)
        {
            Console.WriteLine("file to delete: " + filename);
            DeleteObjectResponse response = null;
            var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.APSoutheast2);
            DeleteObjectRequest request = new DeleteObjectRequest();
            request.BucketName = bucket;
            request.Key = filename;
            response = await client.DeleteObjectAsync(request);
            Console.WriteLine("File deleted");
        }
    }
}
