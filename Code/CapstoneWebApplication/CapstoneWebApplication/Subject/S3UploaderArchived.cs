using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.S3;
using Amazon.S3.Model;
using System.Xml.Linq;
using System.IO;

namespace CapstoneWebApplication
{
    public class S3UploaderArchived
    {
        private string bucketName = "capstone.uhcl";
        private string keyName = "Problem1.csv";
        private string filePath = "C:\\S3Temp\\Problem1.csv";
        public void UploadFile()
        {
            var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast2);
            try
            {
                Amazon.S3.Model.PutObjectRequest putRequest = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = filePath,
                    ContentType = "application/vnd.ms-excel"
                };
                PutObjectResponse response = client.PutObject(putRequest);
            }
            catch(AmazonS3Exception amazonS3Exception)
            {
            if(amazonS3Exception.ErrorCode != null && 
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Check the aws credentials");
                }
                else
                {
                    throw new Exception("Error occured: " + amazonS3Exception.Message);
                }
            }
        }

    }
}