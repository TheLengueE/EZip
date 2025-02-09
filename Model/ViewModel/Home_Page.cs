namespace EZip.Model
{
    // 在这里定义Home页面需要的数据结构

    // Home页面中每个文件或是目录都具有这些属性，当然有些属性是空的可能
    public class HomeContent
    {
        public ContentType Type { get; set; } = ContentType.k_file;

        public string Content { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        public double? SizeInMB { get; set; }

        public string ?AbsolutePath { get; set; }
    }

    // 当现需要压缩文件时，需要传递的数据结构，这包括这次需要压缩的相关信息
    // 后续再来进行扩展
    public class HomeCompressMessage
    {
        public string[] CompressPath { get; set; } = new string[0];

        public string OutputFilePath { get; set; } = string.Empty;

        public CompressType compressType { get; set; } = CompressType.k_zip;
    }

    // 测试用的数据结构
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
    }

}
