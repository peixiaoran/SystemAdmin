using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
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

        #region 上传 Upload

        public async Task<string> UploadAsync(string objectName, Stream data, string contentType = "application/octet-stream")
        {
            if (data == null || data.Length == 0)
                throw new ArgumentException("Stream cannot be empty", nameof(data));

            var bucket = _settings.DefaultBucket;

            var ext = Path.GetExtension(objectName); // .jpg .png .pdf
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var random = Guid.NewGuid().ToString("N")[..8]; // 8位就够
            var newObjectName = $"{timestamp}_{random}{ext}";

            await _client.PutObjectAsync(new PutObjectArgs()
                         .WithBucket(bucket)
                         .WithObject(newObjectName)
                         .WithStreamData(data)
                         .WithObjectSize(data.Length)
                         .WithContentType(contentType));

            return $"{_settings.Endpoint}/{bucket}/{newObjectName}";
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
