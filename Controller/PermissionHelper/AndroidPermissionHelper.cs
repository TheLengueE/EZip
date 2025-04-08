#if ANDROID
using Android.Content;
using Android.Provider;
#endif

using EZip.Controller;

namespace EZip.Controller
{
    public class AndroidPermissionHelper : IPermissionHelper
    {
        LocalLanguageService _localLanguageService;

        public AndroidPermissionHelper(LocalLanguageService localLanguageService)
        {
            _localLanguageService = localLanguageService;
        }

        public async Task<bool> RequestDocumentPermissionAsync()
        {

#if ANDROID
    if (!Android.OS.Environment.IsExternalStorageManager)
    {
        bool userAgreed = await Application.Current.MainPage.DisplayAlert
        (
              _localLanguageService.GetString("AndroidPermissionHelper-PermissionRequests"),
                _localLanguageService.GetString("AndroidPermissionHelper-PermissionRequestMessage"),
                _localLanguageService.GetString("AndroidPermissionHelper-ToSettings"),
                _localLanguageService.GetString("AndroidPermissionHelper-Cancel")
         );


        if (userAgreed)
        {
            var intent = new Intent(Settings.ActionManageAllFilesAccessPermission);
            intent.AddFlags(ActivityFlags.NewTask);
            Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.StartActivity(intent);

            await Task.Delay(3000); // 可根据情况做轮询或其他反馈机制
        }
    }

    return Android.OS.Environment.IsExternalStorageManager;
#else
            await Task.Delay(1);
            return true;
#endif
        }

    }
}
