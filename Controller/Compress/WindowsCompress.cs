namespace EZip.Controller
{
    using Model;
    using SharpCompress.Archives;
    using SharpCompress.Common;
    using SharpCompress.Writers;

    public class WindowsCompress : ICompress
    {
        public AppResponse CompressFile(AppRequest request)
        {
            AppResponse response = new AppResponse();
            // 检查请求是否为 null
            if (request == null)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "Request cannot be null.";
                return response;
            }

            try
            {
                // 检查 RequestData 是否为 HomeCompressMessage 类型
                if (request.RequestData is not HomeCompressMessage compressMessage)
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = "Invalid request data.";
                    return response;
                }

                // 确保输出文件路径和待压缩的目录有效
                if (string.IsNullOrEmpty(compressMessage.OutputFilePath))
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = "Output file path cannot be empty.";
                    return response;
                }

                if (compressMessage.CompressPath == null || compressMessage.CompressPath.Length == 0)
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = "No directories specified for compression.";
                    return response;
                }

                if (compressMessage.compressType == CompressType.k_zip)
                {
                    PerformZipCompress(compressMessage.CompressPath, compressMessage.OutputFilePath);
                }
                else if (compressMessage.compressType == CompressType.k_sevenzip)
                {
                    PerformSevenZipCompress(compressMessage.CompressPath, compressMessage.OutputFilePath);
                }
                else if (compressMessage.compressType == CompressType.k_tar) 
                {
                    PerformTarCompress(compressMessage.CompressPath, compressMessage.OutputFilePath);
                }
                else
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = "Unsupported compression type.";
                }

                // 设置成功响应
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                // 捕获异常并设置失败响应
                response.IsSuccessful = false;
                response.ErrorMessage = $"An error occurred while compressing the file: {ex.Message}";
            }

            return response;
        }

        /// <summary>
        /// 实际执行 ZIP 压缩的逻辑
        /// </summary>
        /// <param name="directories">需要压缩的目录数组</param>
        /// <param name="outputFilePath">压缩文件输出路径</param>
        private void PerformZipCompress(string[] directories, string outputFilePath)
        {
            using (var zipStream = File.OpenWrite(outputFilePath))
            using (var writer = WriterFactory.Open(zipStream, ArchiveType.Zip, CompressionType.Deflate))
            {
                foreach (var dir in directories)
                {
                    if (Directory.Exists(dir))
                    {

                        // 遍历目录中的所有文件并压缩
                        foreach (var filePath in Directory.GetFiles(dir, "*", SearchOption.AllDirectories))
                        {
                            string relativePath = Path.GetRelativePath(dir, filePath);
                            writer.Write(relativePath, File.OpenRead(filePath));
                        }
                    }
                    else if (File.Exists(dir)) 
                    {
                        string relativePath = Path.GetFileName(dir); // 使用文件名作为相对路径
                        writer.Write(relativePath, File.OpenRead(dir));
                    }
                    else
                    {
                        Console.WriteLine($"目录不存在: {dir}");
                    }
                }
            }
        }

        public void PerformSevenZipCompress(string[] directories, string outputFilePath)
        {
            throw new System.NotImplementedException();
        }

        public void PerformTarCompress(string[] directories, string outputFilePath) 
        {
            using (var tarStream = File.OpenWrite(outputFilePath))
            using (var writer = WriterFactory.Open(tarStream, ArchiveType.Tar, CompressionType.None))
            {
                foreach (var dir in directories)
                {
                    if (Directory.Exists(dir))
                    {
                        // 遍历目录中的所有文件并压缩
                        foreach (var filePath in Directory.GetFiles(dir, "*", SearchOption.AllDirectories))
                        {
                            string relativePath = Path.GetRelativePath(dir, filePath);
                            writer.Write(relativePath, File.OpenRead(filePath));
                        }
                    }
                    else if (File.Exists(dir))
                    {
                        string relativePath = Path.GetFileName(dir); // 使用文件名作为相对路径
                        writer.Write(relativePath, File.OpenRead(dir));
                    }
                    else
                    {
                        Console.WriteLine($"目录不存在: {dir}");
                    }
                }
            }
        }

        public AppResponse UnpackArchive(AppRequest request) 
        {
            AppResponse response = new AppResponse();
            try
            {
                // 检查请求是否为 null
                if (request == null)
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = "Request cannot be null.";
                    return response;
                }

                // 检查 RequestData 是否为 HomeCompressMessage 类型
                if (request.RequestData is not HomeUnpackMessage unpackMessage)
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = "Invalid request data.";
                    return response;
                }

                Unpack(unpackMessage.ArchivePath,unpackMessage.OutputPath);
                response.IsSuccessful = true;
            }
            catch 
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "An error occurred while unpacking the file.";
            }
            return response;
        }

        public void Unpack(string archivePath,string destinationPath)
        {
            using (var archive = ArchiveFactory.Open(archivePath))
            {
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                    {
                        entry.WriteToDirectory(destinationPath, new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
            }
        }

        public AppResponse OpenArchive(string archivePath)
        {
            throw new System.NotImplementedException();
        }

    }
}
