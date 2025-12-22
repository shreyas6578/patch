using System;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace WebApplication5
{
    /// <summary>
    /// Summary description for FileUploadHandler
    /// </summary>
    public class FileUploadHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            try
            {
                // Check if request has files
                if (context.Request.Files.Count == 0)
                {
                    SendErrorResponse(context, "No files uploaded.");
                    return;
                }

                // Get destination path
                string destinationPath = context.Request.Form["destinationPath"];
                if (string.IsNullOrEmpty(destinationPath))
                {
                    SendErrorResponse(context, "Destination path is required.");
                    return;
                }

                // Security check: Ensure destination is within Uploads directory
                string uploadsRoot = HttpContext.Current.Server.MapPath("~/Uploads/");
                if (!IsPathWithinUploads(destinationPath, uploadsRoot))
                {
                    SendErrorResponse(context, "Access denied. You can only upload to the Uploads directory.");
                    return;
                }

                // Create directory if it doesn't exist
                if (!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                }

                // Process each file
                HttpPostedFile file = context.Request.Files[0];
                string fileName = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(destinationPath, fileName);

                // Check if overwrite is allowed
                bool overwrite = context.Request.Form["overwrite"] == "true";

                if (File.Exists(filePath) && !overwrite)
                {
                    // Generate unique filename
                    fileName = GetUniqueFileName(destinationPath, fileName);
                    filePath = Path.Combine(destinationPath, fileName);
                }

                // Check file size (limit to 100MB)
                if (file.ContentLength > 100 * 1024 * 1024) // 100MB
                {
                    SendErrorResponse(context, "File size exceeds 100MB limit.");
                    return;
                }

                // Check file extension (optional security)
                string extension = Path.GetExtension(fileName).ToLower();
                string[] allowedExtensions = {
                    ".txt", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx",
                    ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".zip", ".rar", ".7z",
                    ".patch", ".diff", ".sql", ".xml", ".json", ".config", ".dll"
                };

                if (!IsExtensionAllowed(extension, allowedExtensions))
                {
                    SendErrorResponse(context, "File type not allowed.");
                    return;
                }

                // Save the file
                file.SaveAs(filePath);

                // Return success response
                var response = new
                {
                    success = true,
                    fileName = fileName,
                    filePath = filePath,
                    fileSize = file.ContentLength,
                    message = "File uploaded successfully"
                };

                string jsonResponse = new JavaScriptSerializer().Serialize(response);
                context.Response.Write(jsonResponse);
            }
            catch (Exception ex)
            {
                SendErrorResponse(context, "Error uploading file: " + ex.Message);
            }
        }

        private bool IsPathWithinUploads(string path, string uploadsRoot)
        {
            try
            {
                string normalizedPath = Path.GetFullPath(path).TrimEnd('\\', '/');
                string normalizedRoot = Path.GetFullPath(uploadsRoot).TrimEnd('\\', '/');

                return normalizedPath.StartsWith(normalizedRoot, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private bool IsExtensionAllowed(string extension, string[] allowedExtensions)
        {
            // If no restrictions, allow all
            if (allowedExtensions == null || allowedExtensions.Length == 0)
                return true;

            return Array.Exists(allowedExtensions, ext => ext.Equals(extension, StringComparison.OrdinalIgnoreCase));
        }

        private string GetUniqueFileName(string directory, string fileName)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            int counter = 1;

            while (File.Exists(Path.Combine(directory, fileName)))
            {
                fileName = $"{nameWithoutExtension}_{counter}{extension}";
                counter++;
            }

            return fileName;
        }

        private void SendErrorResponse(HttpContext context, string errorMessage)
        {
            var response = new
            {
                success = false,
                error = errorMessage
            };

            string jsonResponse = new JavaScriptSerializer().Serialize(response);
            context.Response.Write(jsonResponse);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}