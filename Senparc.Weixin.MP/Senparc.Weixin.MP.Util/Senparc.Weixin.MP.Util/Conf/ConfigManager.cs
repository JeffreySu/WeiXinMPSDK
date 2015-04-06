
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Senparc.Weixin.MP.Util.Conf
{
    public static class ConfigManager
    {
        private static IDictionary<String, ConfigItem> _items;
        private static string _directoryPath;

        /// <summary>
        /// Get config of micro message by config key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ConfigItem GetConfig(String key)
        {
            if (_items == null || _items.Count == 0)
            {
                throw new ArgumentNullException("not contain any config.");
            }

            if (_items.ContainsKey(key))
            {
                return _items[key];
            }

            throw new ArgumentException("not contain config named \"" + key + "\"");
        }


        public static void Load(String directoryPath)
        {
            DirectoryInfo dir = new DirectoryInfo(directoryPath);
            if (dir.Exists)
            {
                throw new IOException(directoryPath + " not exist!");
            }
            _directoryPath = directoryPath;
            ResetItems();

            FileInfo[] confFiles = dir.GetFiles("*.config");

            foreach (FileInfo file in confFiles)
            {
                LoadFormFile(file.FullName);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void NewConfig(ConfigItem config)
        {
            config.Flush();
            Load(_directoryPath);
        }

        private static void LoadFormFile(String filePath)
        {
            ConfigItem item = new ConfigItem(filePath);
            _items.Add(item.GetKey(), item);
        }

        private static void ResetItems()
        {
            _items = new ConcurrentDictionary<string, ConfigItem>();
        }


        public static ICollection<String> GetKeys()
        {
            return _items.Keys;
        }
    }
}

