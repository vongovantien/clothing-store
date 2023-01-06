using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class FileDetail
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public byte[]? FileData { get; set; }
        public string? FileType { get; set; }
    }
}
