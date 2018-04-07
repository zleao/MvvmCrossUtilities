using System;
using System.Threading.Tasks;
using Android.Content;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid;
using Android.OS;

namespace MvxExtensions.Plugins.Storage.Droid
{
    /// <summary>
    /// Android implementation of the Storage plugin
    /// </summary>
    /// <seealso cref="MvxExtensions.Plugins.Storage.Droid.StorageManagerCommon_Droid_WPF" />
    public class StorageManager : StorageManagerCommon_Droid_WPF
    {
        private const string BASE_PRIVATE_ANDROID_DATA_PATH = "Android/data";
        private const string BASE_PUBLIC_ANDROID_DATA_PATH = "Data";

        private Context Context
        {
            get
            {
                return _context ?? (_context = Mvx.Resolve<IMvxAndroidGlobals>().ApplicationContext);
            }
        }
        private Context _context;

        /// <summary>
        /// Returns the full physical path based on a location and a relative path
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="path">The relative path.</param>
        /// <returns></returns>
        protected override string FullPath(StorageLocation location, string path)
        {
            var basePath = string.Empty;

            switch (location)
            {
                case StorageLocation.Internal:
                    basePath = Context.FilesDir.Path;
                    break;

                case StorageLocation.ExternalPrivate:
                    var cachePath = PathCombine(BASE_PRIVATE_ANDROID_DATA_PATH, Context.PackageName, "files");
                    basePath = Context.GetExternalFilesDir(cachePath).Path;
                    break;

                case StorageLocation.ExternalPublic:
                    var filesPath = PathCombine(BASE_PUBLIC_ANDROID_DATA_PATH, Context.PackageName, "files");
                    basePath = Android.OS.Environment.GetExternalStoragePublicDirectory(filesPath).Path;
                    break;

                default:
                    break;
            }

            return PathCombine(basePath, path);
        }

        /// <summary>
        /// Returns the available free space in bytes for a given path
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <returns></returns>
        public override Task<ulong> GetAvailableFreeSpaceAsync(string fullPath)
        {
            StatFs statFs = new StatFs(fullPath);
            long freeBytes = statFs.FreeBlocksLong * statFs.BlockSizeLong;

            return Task.FromResult(ConvertBytesToMegabytes(freeBytes));
        }

        private static readonly long MEGA_BYTE = 1048576;
        /// <summary>
        /// Converts the bytes to megabytes.
        /// </summary>
        /// <param name="bytesToConvert">The bytes to convert.</param>
        /// <returns></returns>
        public static ulong ConvertBytesToMegabytes(long bytesToConvert)
        {
            return (ulong)(bytesToConvert / MEGA_BYTE);
        }
    }
}