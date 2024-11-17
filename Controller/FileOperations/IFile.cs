namespace EZip.Controller
{
    using Model;
    interface IFile
    {
        public AppResponse CreateFile(AppRequest request);

        public AppResponse DeleteFile(AppRequest request);

        public AppResponse MoveFile(AppRequest request);

        public AppResponse CopyFile(AppRequest request);

        public AppResponse RenameFile(AppRequest request);
    }
}
