namespace EZip.Controller
{
    using Model;
    interface IDirectory
    {
        string NowPath { get; set; }

        public AppResponse GetDirectoryPath();

        public AppResponse ShowDirectoryFiles();

        public AppResponse CreateDirectory(AppRequest request);

        public AppResponse DeleteDirectory(AppRequest request);

        public AppResponse MoveDirectory(AppRequest request);

        public AppResponse CopyDirectory(AppRequest request);

        public AppResponse RenameDirectory(AppRequest request);
    }
}
