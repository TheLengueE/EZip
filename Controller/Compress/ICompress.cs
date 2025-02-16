namespace EZip.Controller
{
    using Model;

    interface ICompress
    {
        // 压缩文件
        public AppResponse CompressFile(AppRequest request);

        // 解压缩
        public AppResponse UnpackArchive(AppRequest request);

        public AppResponse OpenArchive(string archivePath);
    }
}
