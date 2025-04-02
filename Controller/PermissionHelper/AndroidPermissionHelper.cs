using System.Threading.Tasks;

#if ANDROID
using Android.Content;
using Android.Provider;
#endif

namespace EZip.Controller
{
    public class PermissionHelper : IPermissionHelper
    {
        public async Task<bool> RequestDocumentPermissionAsync()
        {
#if ANDROID
            if (!Android.OS.Environment.IsExternalStorageManager)
            {
                var intent = new Intent(Settings.ActionManageAllFilesAccessPermission);
                intent.AddFlags(ActivityFlags.NewTask); 
                Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.StartActivity(intent);

                // 等待用户开启权限
                await Task.Delay(3000);
            }

            return Android.OS.Environment.IsExternalStorageManager;
#else
            return true;
#endif
        }
    }
}
