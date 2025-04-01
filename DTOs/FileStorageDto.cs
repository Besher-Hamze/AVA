// DTOs/FileStorageDto.cs
using System;

namespace MongoDotNetBackend.DTOs
{
    public class FileStorageDto
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public DateTime UploadDate { get; set; }
        public string PublicUrl { get; set; }
    }
}