namespace EZip.Controller
{
    using Model;
    using SharpCompress.Archives;
    using SharpCompress.Common;
    using SharpCompress.Writers;
    using SharpCompress.Writers.Tar;
    using SharpCompress.Writers.Zip;
    using System.Text;

    public class WindowsCompress : ICompress
    {
        private readonly EasyLogger _logger;

        public WindowsCompress(EasyLogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///  总体执行压缩的逻辑，对外部提供的请求进行验证和处理，然后依据请求类型调用具体的压缩方法
        /// </summary>
        /// <param name="request">通用的request 里面的RequestData必须不为空且为HomeCompressMessage类型</param>
        /// <returns></returns>
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

            try
            {
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
                    return response;
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
        /// <param name="directories">需要压缩的目录数组,绝对路径</param>
        /// <param name="outputFilePath">压缩文件输出路径</param>
        private void PerformZipCompress(string[] directories, string outputFilePath)
        {
            // 设置压缩选项这里指定所有的文件名都使用 UTF-8 编码，不然在Android上面会出现乱码
            var options = new ZipWriterOptions(CompressionType.Deflate)
            {
                ArchiveEncoding = new ArchiveEncoding()
                {
                    Default = Encoding.UTF8
                }
            };

            bool compressError = false;  // 标志是否发生错误

            try
            {
                using (var zipStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var writer = WriterFactory.Open(zipStream, ArchiveType.Zip, options))
                {
                    foreach (var dir in directories)
                    {
                        if (Directory.Exists(dir))
                        {
                            foreach (var filePath in Directory.GetFiles(dir, "*", SearchOption.AllDirectories))
                            {
                                string relativePath = Path.GetRelativePath(dir, filePath);
                                writer.Write(relativePath, File.OpenRead(filePath));
                            }
                        }
                        else if (File.Exists(dir))
                        {
                            string relativePath = Path.GetFileName(dir);
                            writer.Write(relativePath, File.OpenRead(dir));
                        }
                        else
                        {
                            throw new FileNotFoundException($"Directory or file not found: {dir}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                compressError = true;
                _logger.LogWarning($"压缩失败: {ex.Message}");
                throw;
            }
            finally
            {
                // 如果发生错误，则删除文件
                if (compressError && File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }
            }
        }


        /// <summary>
        /// 执行 7-Zip 压缩的逻辑
        /// </summary>
        /// <param name="directories">需要压缩的目录数组,绝对路径</param>
        /// <param name="outputFilePath">压缩文件输出路径</param>
        ///  ToDo: 7-Zip 压缩的实现
        public void PerformSevenZipCompress(string[] directories, string outputFilePath)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 执行 TAR 压缩的逻辑
        /// </summary>
        /// <param name="directories">需要压缩的目录数组,绝对路径</param>
        /// <param name="outputFilePath">压缩文件输出路径</param>
        public void PerformTarCompress(string[] directories, string outputFilePath)
        {
            var options = new TarWriterOptions(CompressionType.GZip, true)
            {
                ArchiveEncoding = new ArchiveEncoding
                {
                    Default = Encoding.UTF8
                }
            };

            bool compressError = false;  // 错误标志

            try
            {
                using (var tarStream = File.OpenWrite(outputFilePath))
                using (var writer = WriterFactory.Open(tarStream, ArchiveType.Tar, options))
                {
                    foreach (var dir in directories)
                    {
                        if (Directory.Exists(dir))
                        {
                            foreach (var filePath in Directory.GetFiles(dir, "*", SearchOption.AllDirectories))
                            {
                                string relativePath = Path.GetRelativePath(dir, filePath);
                                writer.Write(relativePath, File.OpenRead(filePath));
                            }
                        }
                        else if (File.Exists(dir))
                        {
                            string relativePath = Path.GetFileName(dir);
                            writer.Write(relativePath, File.OpenRead(dir));
                        }
                        else
                        {
                            _logger.LogWarning($"目录或文件不存在: {dir}");
                            compressError = true;  // 设置错误标志
                            break;  // 停止进一步处理
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"压缩失败: {ex.Message}");
                compressError = true;  // 异常发生时设置错误标志
            }
            finally
            {
                if (compressError)
                {
                    // 发生错误，删除可能已创建的不完整文件
                    if (File.Exists(outputFilePath))
                    {
                        File.Delete(outputFilePath);
                    }
                }
            }
        }

        /// <summary>
        /// 解压缩文件的逻辑,对外部提供的请求进行验证和处理，然后依据请求类型调用具体的解压缩方法
        /// </summary>
        /// <param name="request">通用的request 里面的RequestData必须不为空且为HomeUnpackMessage类型</param>
        /// <returns></returns>
        public AppResponse UnpackArchive(AppRequest request)
        {
            AppResponse response = new AppResponse();
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
            try
            {
                Unpack(unpackMessage.ArchivePath, unpackMessage.OutputPath);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.ToString();
                _logger.LogError($"解压缩失败: {ex.Message}");
            }
            return response;
        }

        /// <summary>
        /// 解压缩文件的具体实现
        /// </summary>
        /// <param name="archivePath"></param>
        /// <param name="destinationPath"></param>
        public void Unpack(string archivePath, string destinationPath)
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

        /// <summary>
        /// 预览压缩包的内容
        /// </summary>
        /// <param name="archivePath"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        //// ToDo: 预览压缩包的内容
        public AppResponse OpenArchive(string archivePath)
        {
            throw new System.NotImplementedException();
        }

    }
}
