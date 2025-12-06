using Microsoft.AspNetCore.Http;
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
        private readonly MinioClient _client;
        private readonly MinioSettings _settings;

        public MinioService(MinioClient client, MinioSettings settings)
        {
            _client = client;
            _settings = settings;

            // 自动创建 Bucket（生产会非常有用）
            //CreateBucketIfNotExistsAsync().Wait();
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

        public async Task UploadAsync(string objectName, Stream data, string contentType = "application/octet-stream")
        {
            await _client.PutObjectAsync(new PutObjectArgs()
                .WithBucket(_settings.DefaultBucket)
                .WithObject(objectName)
                .WithStreamData(data)
                .WithObjectSize(data.Length)
                .WithContentType(contentType));
        }

        // 上传 IFormFile
        public async Task<string> UploadFileAsync(IFormFile file, string? prefix = null)
        {
            var objectName = BuildObjectName(file.FileName, prefix);

            using var stream = file.OpenReadStream();

            await UploadAsync(objectName, stream, file.ContentType);

            return objectName;
        }

        // 自动生成路径的文件名：2025/12/04/uuid.png
        private string BuildObjectName(string originalFileName, string? prefix)
        {
            string ext = Path.GetExtension(originalFileName);
            string uuid = Guid.NewGuid().ToString("N");

            string datePath = $"{DateTime.UtcNow:yyyy/MM/dd}";
            string folder = string.IsNullOrWhiteSpace(prefix) ? "" : $"{prefix.TrimEnd('/')}/";

            return $"{folder}{datePath}/{uuid}{ext}";
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

        #region Exists / Metadata

        // 判断文件是否存在
        public async Task<bool> ExistsAsync(string objectName)
        {
            try
            {
                await _client.StatObjectAsync(new StatObjectArgs()
                    .WithBucket(_settings.DefaultBucket)
                    .WithObject(objectName));

                return true;
            }
            catch (MinioException)
            {
                return false;
            }
        }

        // 获取文件元信息（大小、类型 etc）
        public async Task<ObjectStat?> GetMetadataAsync(string objectName)
        {
            try
            {
                return await _client.StatObjectAsync(new StatObjectArgs()
                    .WithBucket(_settings.DefaultBucket)
                    .WithObject(objectName));
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Presigned URL

        // 获取预签名访问 URL（前端可临时访问）
        public async Task<string> GetPresignedUrlAsync(string objectName, int expireSeconds = 3600)
        {
            var args = new PresignedGetObjectArgs()
                .WithBucket(_settings.DefaultBucket)
                .WithObject(objectName)
                .WithExpiry(expireSeconds);

            return await _client.PresignedGetObjectAsync(args);
        }

        #endregion
    }
}
