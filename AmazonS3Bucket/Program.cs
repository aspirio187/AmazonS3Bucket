using System.Net;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using AmazonS3Bucket.Models;

namespace AmazonS3Bucket;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("AWS S3 Bucket sample application");

        SecretAppsettingReader secretAppsettingReader = new();
        AwsOptions awsOptions = new AwsOptions()
        {
            AccessKey = secretAppsettingReader.ReadValue<string>("AWS:AWS_ACCESS_KEY"),
            SecretKey = secretAppsettingReader.ReadValue<string>("AWS:AWS_SECRET_KEY"),
        };

        AmazonS3Config config = new() { RegionEndpoint = RegionEndpoint.EUCentral1 };

        IAmazonS3 client = new AmazonS3Client(awsOptions.AccessKey, awsOptions.SecretKey, config);

        Console.WriteLine("Listing the buckets");
        Console.WriteLine("===================");

        List<S3Bucket> buckets = await ListBucketsAsync(client);

        buckets.ForEach(bucket => Console.WriteLine($"Bucket name: {bucket.BucketName}"));

        Console.WriteLine();
        Console.WriteLine("Putting the object into the bucket");
        Console.WriteLine("==================================");

        string bucketName = "csharps3template";
        string objectName = "CV - S Hatsijev2.pdf";
        string filePath = "C:\\Users\\Soultan\\Desktop\\CV - S Hatsijev.pdf";

        Console.WriteLine(
            $"The result of uploading the file is: {await UploadFileAsync(client, bucketName, objectName, filePath)}"
        );

        Console.WriteLine();
        Console.WriteLine("Putting the public object into the bucket");
        Console.WriteLine("=========================================");

        string publicObjectName = "CV - Soultan HATSIJEV.pdf";
        string publicFilePath = "C:\\Users\\Soultan\\Desktop\\CV - Soultan HATSIJEV.pdf";

        Console.WriteLine(
            $"The result of uploading the file is: {await UploadPublicFileAsync(client, bucketName, publicObjectName, publicFilePath)}"
        );
    }

    static async Task<List<S3Bucket>> ListBucketsAsync(IAmazonS3 client)
    {
        ListBucketsResponse response = await client.ListBucketsAsync();

        return response.Buckets;
    }

    static async Task<bool> UploadFileAsync(
        IAmazonS3 client,
        string bucketName,
        string objectName,
        string filePath
    )
    {
        PutObjectRequest request =
            new()
            {
                BucketName = bucketName,
                Key = objectName,
                FilePath = filePath,
                TagSet = new List<Tag>()
                {
                    new Tag() { Key = "Name", Value = "Soultan" },
                    new Tag() { Key = "Surname", Value = "Hatsijev" },
                },
            };

        PutObjectResponse response = await client.PutObjectAsync(request);

        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    static async Task<bool> UploadPublicFileAsync(
        IAmazonS3 client,
        string bucketName,
        string objectName,
        string filePath
    )
    {
        PutObjectRequest request =
            new()
            {
                BucketName = bucketName,
                Key = objectName,
                FilePath = filePath,
                TagSet = new List<Tag>()
                {
                    new Tag() { Key = "Scope", Value = "Public" }
                },
            };

        PutObjectResponse response = await client.PutObjectAsync(request);

        return response.HttpStatusCode == HttpStatusCode.OK;
    }
}
