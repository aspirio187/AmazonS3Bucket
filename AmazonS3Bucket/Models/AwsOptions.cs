using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonS3Bucket.Models;

public class AwsOptions
{
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"AccessKey: {AccessKey}, SecretKey: {SecretKey}";
    }
}
