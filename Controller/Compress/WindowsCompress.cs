namespace EZip.Controller
{
    using Model;
    using SharpCompress.Archives;
    using SharpCompress.Compressors;
    using SharpCompress.Compressors.Deflate;
    using SharpCompress.Common;
    using SharpCompress.Writers;

    internal class WindowsCompress : ICompress
    {
        public AppResponse ZipCompress(AppRequest request)
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

                // 调用实际的压缩逻辑
                PerformZipCompression(compressMessage.CompressPath, compressMessage.OutputFilePath);

                // 设置成功响应
                response.IsSuccessful = true;
                response.ErrorMessage = "File compressed successfully.";
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
        private void PerformZipCompression(string[] directories, string outputFilePath)
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

        public AppResponse SevenZipCompress(AppRequest request)
        {
            throw new System.NotImplementedException();
        }
        public AppResponse RarCompress(AppRequest request)
        {
            throw new System.NotImplementedException();
        }
        public AppResponse TarCompress(AppRequest request)
        {
            throw new System.NotImplementedException();
        }
        public AppResponse TarGzCompress(AppRequest request)
        {
            throw new System.NotImplementedException();
        }

        public AppResponse UnpackArchive(AppRequest request)
        {
            throw new System.NotImplementedException();
        }

        public AppResponse OpenArchive(string archivePath)
        {
            throw new System.NotImplementedException();
        }
    }
}
