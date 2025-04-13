namespace EZip.Controller
{
    using Model;

    interface ICompress
    {
        public event Action? OnCompressCompleted;
        public int CompressProgress { get; set; }

        // 压缩文件
        public AppResponse CompressFile(AppRequest request);

        public Task<AppResponse> CompressFileAsync(AppRequest request);

        // 解压缩
        public AppResponse UnpackArchive(AppRequest request);

        public Task<AppResponse> UnpackArchiveAsync(AppRequest request);

        public AppResponse OpenArchive(string archivePath);
    }
}
