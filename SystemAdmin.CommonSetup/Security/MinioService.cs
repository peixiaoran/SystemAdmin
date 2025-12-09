using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Minio.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using SystemAdmin.CommonSetup.Options;

namespace SystemAdmin.CommonSetup.Security
{
    public class MinioService
    {
        private readonly IMinioClient _client;
        private readonly MinioSettings _settings;

        public MinioService(IMinioClient client, IOptions<MinioSettings> options)
        {
            _client = client;
            _settings = options.Value;
        }

        #region Bucket 操作

        private async Task CreateBucketIfNotExistsAsync()
        {
            bool exists = await _client.BucketExistsAsync(
                new BucketExistsArgs().WithBucket(_settings.DefaultBucket)
            );

            if (!exists)
            {
                await _client.MakeBucketAsync(
                    new MakeBucketArgs().WithBucket(_settings.DefaultBucket)
                );
            }
        }

        #endregion

        #region 上传 Upload

        public async Task<string> UploadAsync(string objectName, Stream data, string contentType = "application/octet-stream")
        {
            if (string.IsNullOrWhiteSpace(objectName))
                throw new ArgumentException("ObjectName cannot be empty", nameof(objectName));

            if (data == null || data.Length == 0)
                throw new ArgumentException("Stream cannot be empty", nameof(data));

            var bucket = _settings.DefaultBucket;

            await _client.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucket)
                .WithObject(objectName)
                .WithStreamData(data)
                .WithObjectSize(data.Length)
                .WithContentType(contentType));

            // 拼接完整文件访问 URL
            return $"{_settings.Endpoint}/{bucket}/{objectName}";
        }

        #endregion

        #region 下载 Download

        // 下载为流（推荐 WebAPI 使用）
        public async Task<MemoryStream> DownloadAsync(string objectName)
        {
            var ms = new MemoryStream();

            await _client.GetObjectAsync(new GetObjectArgs()
                .WithBucket(_settings.DefaultBucket)
                .WithObject(objectName)
                .WithCallbackStream(stream =>
                {
                    stream.CopyTo(ms);
                }));

            ms.Position = 0;
            return ms;
        }

        // 下载为字节数组
        public async Task<byte[]> DownloadBytesAsync(string objectName)
        {
            using var ms = new MemoryStream();

            await _client.GetObjectAsync(new GetObjectArgs()
                .WithBucket(_settings.DefaultBucket)
                .WithObject(objectName)
                .WithCallbackStream(s => s.CopyTo(ms)));

            return ms.ToArray();
        }

        // 下载到本地文件
        public async Task DownloadToFileAsync(string objectName, string localFilePath)
        {
            await _client.GetObjectAsync(new GetObjectArgs()
                .WithBucket(_settings.DefaultBucket)
                .WithObject(objectName)
                .WithFile(localFilePath));
        }

        #endregion

        #region 删除 Delete

        public async Task DeleteAsync(string objectName)
        {
            await _client.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(_settings.DefaultBucket)
                .WithObject(objectName));
        }

        // 批量删除
        public async Task DeleteManyAsync(IEnumerable<string> objectNames)
        {
            foreach (var name in objectNames)
            {
                await DeleteAsync(name);
            }
        }

        #endregion
    }
}
