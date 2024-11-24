namespace EZip.Controller
{
    using Microsoft.Extensions.Logging;
    using Model;
    public class AndroidDirectoryOperations : IDirectory
    {
        public string NowPath { get; set; }
        private readonly EasyLogger _logger;

        public AndroidDirectoryOperations(EasyLogger logger)
        {
            NowPath = "/storage/emulated/0";
            _logger = logger;
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
                        var files = Directory.GetFiles(path)
                            .Select(filePath =>
                            {
                                var fileInfo = new FileInfo(filePath);
                                return new HomeContent
                                {
                                    Content = fileInfo.Name,
                                    CreateTime = fileInfo.CreationTime,
                                    UpdateTime = fileInfo.LastWriteTime,
                                    SizeInMB = Math.Round(fileInfo.Length / 1024.0 / 1024.0, 2)
                                };
                            })
                            .ToList();

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
                        var directories = Directory.GetDirectories(path)
                            .Select(directoryPath =>
                            {
                                var directoryInfo = new DirectoryInfo(directoryPath);
                                return new HomeContent
                                {
                                    Content = directoryInfo.Name,
                                    CreateTime = directoryInfo.CreationTime,
                                    UpdateTime = directoryInfo.LastWriteTime,
                                    SizeInMB = null
                                };
                            })
                            .ToList();

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
