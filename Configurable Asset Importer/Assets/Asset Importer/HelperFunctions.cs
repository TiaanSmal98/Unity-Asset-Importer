using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

/// <summary>
/// A collection of miscellaneous helper functions
/// </summary>
public class HelperFunctions
{
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

    public static string ReadFromFile(string path, string fileName)
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

    public static T JsonToClass<T>(string path, string fileName = "")
    {
        T genericType;

        genericType = JsonUtility.FromJson<T>(ReadFromFile(path, fileName));

        return genericType;
    }

}
