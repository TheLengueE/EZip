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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AppResponse ShowDirectoryFiles(AppRequest request)
        {
            AppResponse response = new AppResponse();

            if (request.RequestData is string path)
            {
                try
                {
                    if (Directory.Exists(path))
                    {
                        var files = Directory.GetFiles(path);
                        response.ResponseData = files;
                        response.IsSuccessful = true;
                    }
                    else
                    {
                        response.ErrorMessage = "Directory does not exist";
                        response.IsSuccessful = false;
                        Console.WriteLine("Directory does not exist");
                    }
                }
                catch (Exception e)
                {
                    response.ErrorMessage = e.Message;
                    response.IsSuccessful = false;
                }
            }
            else
            {
                response.ErrorMessage = "Invalid request data";
                response.IsSuccessful = false;
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AppResponse ShowDirectoryDirectories(AppRequest request)
        {
            AppResponse response = new AppResponse();

            if (request.RequestData is string path)
            {
                try
                {
                    if ((Directory.Exists(path)))
                    {
                        var directories = Directory.GetDirectories(path);
                        response.ResponseData = directories;
                        response.IsSuccessful = true;
                    }
                    else
                    {
                        response.ErrorMessage = "Directory does not exist";
                        response.IsSuccessful = false;
                        Console.WriteLine("Directory does not exist");
                    }
                }
                catch (Exception e)
                {
                    response.ErrorMessage = e.Message;
                    response.IsSuccessful = false;
                }
            }
            else
            {
                response.ErrorMessage = "Invalid request data";
                response.IsSuccessful = false;
            }

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
