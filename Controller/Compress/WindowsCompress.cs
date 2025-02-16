namespace EZip.Controller
{
    using Model;
    using SharpCompress.Common;
    using SharpCompress.Writers;

    public class WindowsCompress : ICompress
    {
        public AppResponse CompressFile(AppRequest request)
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

                if (compressMessage.compressType == CompressType.k_zip)
                {
                    // 调用实际的压缩逻辑
                    PerformZipCompress(compressMessage.CompressPath, compressMessage.OutputFilePath);
                }
                else if (compressMessage.compressType == CompressType.k_sevenzip)
                {
                    // 调用实际的压缩逻辑
                    PerformSevenZipCompress(compressMessage.CompressPath, compressMessage.OutputFilePath);
                }
                else if (compressMessage.compressType == CompressType.k_tar) 
                {
                    PerformTarCompress(compressMessage.CompressPath, compressMessage.OutputFilePath);
                    // 调用实际的压缩逻辑
                }
                else
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = "Unsupported compression type.";
                }

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

        public void PerformRarCompress( string[] directories, string outputFilePath)
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

        public void PerformTarGzCompress(string[] directories, string outputFilePath)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
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
