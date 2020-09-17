using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace BitGames.CustomAssetImporter
{

    /// <summary>
    /// A collection of miscellaneous helper functions
    /// </summary>
    public class HelperFunctions
    {
        /// <summary>
        /// Creates an empty Asset configuration file
        /// </summary>
        /// <param name="path">The directory in which to create a json config file</param>
        public static void CreateJsonFile(string path, string jsonConfigFileName, object modelToWrite)
        {
            string json = JsonUtility.ToJson(modelToWrite, true);
            CreateText(path, jsonConfigFileName, json);
        }

        /// <summary>
        /// Generic function that converts an array of generic type to list of generic type
        /// </summary>
        /// <typeparam name="T">Data Type</typeparam>
        /// <param name="array">Array of generic type to be converted to list</param>
        /// <returns>Converted List of type generic</returns>
        public static List<T> ArrayToList<T>(params T[] array)
        {
            List<T> list = new List<T>();

            foreach (var item in array)
            {
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// Generic function to create text files
        /// If the file exists, it will be overwritten
        /// </summary>
        /// <param name="path">The root directory to create the text file</param>
        /// <param name="fileName">The file name, including the file extension</param>
        /// <param name="content">The content to write to the file</param>
        public static void CreateText(string path, string fileName, string content)
        {
            path = Path.Combine(path, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllText(path, content);
        }

        /// <summary>
        /// Reads contained text from a text file
        /// </summary>
        /// <param name="path">The parent directory containing the file, or the fully disclosed path of the file</param>
        /// <param name="fileName">The files name, if not included in the path</param>
        /// <returns>The text file contents as a string</returns>
        public static string ReadFromFile(string path, string fileName = "")
        {
            string contents = "";

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                path = Path.Combine(path, fileName);
            }

            if (File.Exists(path))
            {
                contents = File.ReadAllText(path);
            }


            return contents;
        }

        /// <summary>
        /// Converts a json file to its scriptable object
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="path">The parent directory containing the json file, or the fully disclosed path of the json file</param>
        /// <param name="fileName">The jsons files name (including extension), if not included in the path</param>
        /// <returns></returns>
        public static T JsonToClass<T>(string path, string fileName = "")
        {
            T genericType;

            genericType = JsonUtility.FromJson<T>(ReadFromFile(path, fileName));

            return genericType;
        }

        /// <summary>
        /// Searches the asset database for Assets of a specific UnityEngine.Object type
        /// </summary>
        /// <typeparam name="T">The UnityEngine Object type</typeparam>
        /// <param name="path">The parent directory to search</param>
        /// <param name="recursive">Search within child directories of the supplied path, false by default</param>
        /// <returns>A list of specified generic type of assets contained in the supplied directory</returns>
        public static List<Asset> FindAssetsByType<T>(string path, bool recursive = false) where T : UnityEngine.Object
        {
            List<Asset> assets = new List<Asset>();

            string searchString = string.Format("t:{0}", typeof(T));
            searchString = searchString.Replace("UnityEngine.", "");

            string[] guids = AssetDatabase.FindAssets(searchString, new string[] { path });

            List<string> paths = AssetGuidToPath(guids);

            foreach (var currentPath in paths)
            {
                T currentAsset = AssetDatabase.LoadAssetAtPath<T>(currentPath);

                string expectedPath = CombinePathsForUnity(path, currentAsset.name);

                if (currentPath.Contains(expectedPath) || recursive) // AssetDatabase searches recursively by default, this overrules that
                {
                    assets.Add(new Asset { asset = currentAsset, path = currentPath });
                }
            }

            return assets;
        }

        public static List<string> FindAssetsByType2<T>(string path, bool recursive = false) where T : UnityEngine.Object
        {
            List<string> assets = new List<string>();

            string searchString = string.Format("t:{0}", typeof(T));
            searchString = searchString.Replace("UnityEngine.", "");

            string[] guids = AssetDatabase.FindAssets(searchString, new string[] { path });

            List<string> paths = AssetGuidToPath(guids);

            foreach (var currentPath in paths)
            {
                T currentAsset = AssetDatabase.LoadAssetAtPath<T>(currentPath);

                string expectedPath = CombinePathsForUnity(path, currentAsset.name);

                if (currentPath.Contains(expectedPath) || recursive) // AssetDatabase searches recursively by default, this overrules that
                {
                    assets.Add(currentPath);
                }
            }

            return assets;
        }


        /// <summary>
        /// Combines paths for use with the AssetDatabase and other Unity.IO methods
        /// </summary>
        /// <param name="paths">The paths or file and folder names to combine</param>
        /// <returns>A path from the combination of file and folder names supplied</returns>
        private static string CombinePathsForUnity(params string[] paths)
        {
            string path = Path.Combine(paths);

            while (path.Contains("\\") || path.Contains("//"))
            {
                path = path.Replace("\\", "/");
                path = path.Replace("//", "/");
            }

            if (path[0] == '/' && path.Length > 1)
            {
                path = path.Substring(1, path.Length - 1);
            }

            return path;
        }

        /// <summary>
        /// Finds the list of guids file paths
        /// </summary>
        /// <param name="guids">The Unity asset guids</param>
        /// <returns>A list of asset file paths</returns>
        private static List<string> AssetGuidToPath(params string[] guids)
        {
            List<string> paths = new List<string>();

            foreach (var guid in guids)
            {
                paths.Add(AssetDatabase.GUIDToAssetPath(guid));
            }

            return paths;
        }

    }
}