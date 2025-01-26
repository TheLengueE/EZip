namespace EZip.Controller
{
    using Model;

    interface ICompress
    {
        public AppResponse ZipCompress(AppRequest request);

        public AppResponse SevenZipCompress(AppRequest request);

        public AppResponse RarCompress(AppRequest request);

        public AppResponse TarCompress(AppRequest request);

        public AppResponse TarGzCompress(AppRequest request);


        // 解压缩
        public AppResponse UnpackArchive(AppRequest request);

        public AppResponse OpenArchive(string archivePath);
    }
}
