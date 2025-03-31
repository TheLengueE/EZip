namespace EZip.Controller
{
    public interface IPermissionHelper
    {
        Task<bool> RequestDocumentPermissionAsync();
    }
}
