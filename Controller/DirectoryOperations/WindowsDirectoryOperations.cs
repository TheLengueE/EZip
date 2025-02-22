namespace EZip.Controller
{
    using Model;
    class WindowsDirectoryOperations : IDirectory
    {
        public string NowPath { get; set; }
        private readonly EasyLogger _logger;

        public WindowsDirectoryOperations(EasyLogger logger)
        {
            NowPath = "C:\\";
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
                                var contentType = ContentType.k_file;

                                // 检查文件扩展名
                                var extension = fileInfo.Extension.ToLower();
                                if (extension == ".zip" || extension == ".rar" || extension == ".7z" || extension== ".tar")
                                {
                                    contentType = ContentType.k_compress;
                                }

                                return new HomeContent
                                {
                                    Type = contentType,
                                    Content = fileInfo.Name,
                                    CreateTime = fileInfo.CreationTime,
                                    UpdateTime = fileInfo.LastWriteTime,
                                    AbsolutePath = fileInfo.FullName,
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
                    if (Directory.Exists(path))
                    {
                        var directories = Directory.GetDirectories(path)
                            .Where(directoryPath =>
                            {
                                var attributes = File.GetAttributes(directoryPath);
                                return (attributes & FileAttributes.Hidden) == 0;    // exclude hidden directories
                            })
                            .Select(directoryPath =>
                            {
                                var directoryInfo = new DirectoryInfo(directoryPath);
                                return new HomeContent
                                {
                                    Type = ContentType.k_directory,
                                    Content = directoryInfo.Name,
                                    CreateTime = directoryInfo.CreationTime,
                                    UpdateTime = directoryInfo.LastWriteTime,
                                    AbsolutePath = directoryInfo.FullName,
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
