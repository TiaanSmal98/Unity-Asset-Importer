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

    public static void CreateText(string path, string fileName, string content)
    {
        path = Path.Combine(path, fileName);

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        File.WriteAllText(path, content);
    }
}
