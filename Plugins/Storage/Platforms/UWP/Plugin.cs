﻿using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin;

namespace MvxExtensions.Plugins.Storage.Platforms.UWP
{
    /// <summary>
    /// Plugin registration class
    /// </summary>
    [MvxPlugin]
    [Preserve(AllMembers = true)]
    public class Plugin : IMvxPlugin
    {
        /// <summary>
        /// Loads this instance.
        /// </summary>
        public void Load()
        {
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IStorageManager, StorageManagerUWP>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IStorageEncryptionManager, StorageEncryptionManagerUWP>();
        }
    }
}