namespace EZip.Model
{
    public enum  ContentType 
    {
        k_file,
        k_directory
    }

    public class HomeContent
    {
        public ContentType Type { get; set; } = ContentType.k_file;

        public string Content { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        public double? SizeInMB { get; set; }

        public string ?AbsolutePath { get; set; }
    }

}
