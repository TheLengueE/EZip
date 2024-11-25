namespace EZip.Controller
{
    using Model;
    using System.Runtime.Versioning;

    public class WindowsFileOperations : IFile
    {
        public AppResponse CreateFile(AppRequest request)
        {
            // ToDo
            AppResponse response = new AppResponse();
            return response;
        }

        public AppResponse DeleteFile(AppRequest request)
        {
            // ToDo
            AppResponse response = new AppResponse();
            return response;
        }

        public AppResponse MoveFile(AppRequest request)
        {
            // ToDo
            AppResponse response = new AppResponse();
            return response;
        }

        public AppResponse CopyFile(AppRequest request)
        {
            // ToDo
            AppResponse response = new AppResponse();
            return response;
        }

        public AppResponse RenameFile(AppRequest request)
        {
            // ToDo
            AppResponse response = new AppResponse();
            return response;
        }

        [SupportedOSPlatform("windows")]
        public AppResponse OpenFile(AppRequest request)
        {
            AppResponse response = new AppResponse();

            if (request.RequestData is string path)
            {
                if (System.IO.File.Exists(path))
                {
                    try
                    {
                        var processStartInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = path, // 文件路径
                            UseShellExecute = true // 启用 shell 执行
                        };

                        System.Diagnostics.Process.Start(processStartInfo);
                        response.IsSuccessful = true;
                    }
                    catch (Exception e)
                    {
                        response.ErrorMessage = $"Failed to open file: {e.Message}";
                        response.IsSuccessful = false;
                    }
                }
                else
                {
                    response.ErrorMessage = "File does not exist";
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

    }
}
