namespace EZip.Model
{
    /// <summary>
    /// The type of request
    /// </summary>
    public enum ContentType
    {
        k_file,
        k_directory,
        k_compress
    }

    /// <summary>
    /// The type of compression to use
    /// </summary>
    public enum CompressType
    {
        k_zip,
        k_sevenzip,
        k_rar,
        k_tar,
        k_targz
    }
}
