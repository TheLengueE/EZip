using Microsoft.Maui.Controls.PlatformConfiguration;

namespace EZip.Controller
{
    using Model;

#if ANDROID
    using Android.Content;
    using Android.Net;
    using AndroidX.Core.Content;
    using Android.Webkit;
#endif
    public class AndroidFileOperations : IFile
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


#if ANDROID
        private string GetMimeType(string filePath)
        {
            string extension = MimeTypeMap.GetFileExtensionFromUrl(filePath);
            return MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension.ToLower()) ?? "*/*";
        }
#endif


        public AppResponse OpenFile(AppRequest request)
        {
            AppResponse response = new AppResponse();

#if ANDROID
    if (request.RequestData is string path)
    {
        try
        {
            var currentActivity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var file = new Java.IO.File(path);

            if (!file.Exists())
            {
                response.ErrorMessage = "File does not exist.";
                response.IsSuccessful = false;
                return response;
            }

            // 获取文件 URI
            Uri fileUri = FileProvider.GetUriForFile(
                currentActivity,
                $"{currentActivity.PackageName}.fileprovider",
                file);

            // 创建 Intent
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(fileUri, GetMimeType(path));
            intent.AddFlags(ActivityFlags.GrantReadUriPermission);

            // 启动 Intent
            currentActivity.StartActivity(Intent.CreateChooser(intent, "选择应用打开文件"));

            response.IsSuccessful = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = $"Error opening file: {ex.Message}";
            response.IsSuccessful = false;
        }
    }
    else
    {
        response.ErrorMessage = "Invalid request data.";
        response.IsSuccessful = false;
    }
#else
            response.ErrorMessage = "OpenFile is not supported on this platform.";
            response.IsSuccessful = false;
#endif
            return response;
        }


    }
}
