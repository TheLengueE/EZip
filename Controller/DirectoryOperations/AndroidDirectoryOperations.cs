namespace EZip.Controller
{
#if  ANDROID
    using System;
    using System.IO;
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Runtime;
#endif



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

#if ANDROID
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
                if (Directory.Exists(path)) 
                {
                    var files = Directory.GetFiles(path)
                        .Select(filePath =>
                        {
                            var fileInfo = new FileInfo(filePath);
                            return new HomeContent
                            {
                                Type = ContentType.k_file,
                                Content = fileInfo.Name,
                                CreateTime = fileInfo.CreationTime,
                                UpdateTime = fileInfo.LastWriteTime,
                                AbsolutePath = fileInfo.FullName,
                                SizeInMB = fileInfo.Length / 1024 / 1024
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
                }

            }

             
            return response;
        }
#else
        public AppResponse ShowDirectoryFiles(AppRequest request)
        {
            AppResponse response = new AppResponse();


            return response;
        }
#endif

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
