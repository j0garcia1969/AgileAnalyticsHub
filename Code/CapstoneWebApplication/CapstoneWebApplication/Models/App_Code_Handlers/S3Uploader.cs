using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using CapstoneWebApplication.Subject;
using System.Web.UI;
using WebApplicationCapstone.Models;
using Amazon.S3;
using Amazon.S3.Model;
using CapstoneWebApplication;
using Microsoft.AspNet;
using System.Threading.Tasks;

namespace WebApplicationCapstone.Controllers
{
    public class S3Uploader
    {
        private string bucketName = "capstone.uhcl";
        private string keyName = "Problem5.csv";
        //String filePath = "~/Desktop/CapstoneWebApplication/AWS path/Problem1.csv";
        //private string filePath = @"C:/Users/Lenovo-pc/Desktop/CapstoneWebApplication/CapstoneWebApplication/App_Data/data.csv";

        public void UploadFile(string filepath)
        {
            // string filePath = Server.MapPath("~/App_Data/data.csv");
            var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast2);
            try
            {
                Amazon.S3.Model.PutObjectRequest putRequest = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = filepath,
                    ContentType = "application/vnd.ms-excel"
                };
                PutObjectResponse response = client.PutObject(putRequest);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
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