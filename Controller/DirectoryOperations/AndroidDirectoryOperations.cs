namespace EZip.Controller
{
    using Model;
    public class AndroidDirectoryOperations : IDirectory
    {
        public string NowPath { get; set; }

        public AndroidDirectoryOperations()
        {
            NowPath = "/storage/emulated/0";
        }

        public AppResponse GetDirectoryPath()
        {
            // ToDo
            AppResponse response = new AppResponse();
            return response;
        }

        public AppResponse ShowDirectoryFiles()
        {
            // ToDo
            AppResponse response = new AppResponse();
            return response;
        }

        public AppResponse CreateDirectory(AppRequest request)
        {
            // ToDo
            AppResponse response = new AppResponse();
            return response;
        }

        public AppResponse DeleteDirectory(AppRequest request)
        {
            // ToDo
            AppResponse response = new AppResponse();
            return response;
        }

        public AppResponse MoveDirectory(AppRequest request)
        {
            // ToDo
            AppResponse response = new AppResponse();
            return response;
        }

        public AppResponse CopyDirectory(AppRequest request)
        {
            // ToDo
            AppResponse response = new AppResponse();
            return response;
        }

        public AppResponse RenameDirectory(AppRequest request)
        {
            // ToDo
            AppResponse response = new AppResponse();
            return response;
        }
    }
}
