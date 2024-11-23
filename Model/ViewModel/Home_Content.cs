namespace EZip.Model
{
    public class HomeContent
    {
        public string Content { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        public double? SizeInMB { get; set; }
    }

}
