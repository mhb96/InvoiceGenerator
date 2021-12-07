using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace InvoiceGenerator.Common.Extensions
{
    /// <summary>
    /// Extensions for an image file type.
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// A constant for minimum bytes for image.
        /// </summary>
        public const int ImageMinimumBytes = 1024;

        /// <summary>
        /// Checks if file type is image.
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns>
        /// True if filetype is image; otherwise returns false.
        /// </returns>
        public static bool IsImage(this IFormFile formFile)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (!string.Equals(formFile.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(formFile.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(formFile.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(formFile.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(formFile.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(formFile.ContentType, "image/webp", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(formFile.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            var postedFileExtension = Path.GetExtension(formFile.FileName);
            if (!string.Equals(postedFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".gif", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".webp", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!formFile.OpenReadStream().CanRead)
                {
                    return false;
                }
                //------------------------------------------
                //   Check whether the image size exceeding the limit or not
                //------------------------------------------ 
                if (formFile.OpenReadStream().Length < ImageMinimumBytes)
                {
                    return false;
                }

                byte[] buffer = new byte[ImageMinimumBytes];
                formFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new System.Drawing.Bitmap(formFile.OpenReadStream()))
                {
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                formFile.OpenReadStream().Position = 0;
            }

            return true;
        }

    }
}
