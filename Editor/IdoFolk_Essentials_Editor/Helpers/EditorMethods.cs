using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace IdoFolk_Essentials_Editor.Helpers
{
    public class EditorMethods 
    {
        const string extension = ".cs";

        public static void WriteToEnum<T>(string path, string name, IEnumerable<T> data)
        {
            using (StreamWriter file = File.CreateText(path + "/" + name + extension))
            {
                file.WriteLine("public enum " + name + "{");
                int i = 0;
                foreach (var line in data)
                {
                    string lineRep = line.ToString().Replace(" ", string.Empty);
                    if (!string.IsNullOrEmpty(lineRep))
                    {
                        file.WriteLine($"	{lineRep} = {i},");
                        i++;
                    }
                }

                file.WriteLine("}");
                AssetDatabase.ImportAsset(path + "/" + name + extension);
            }
        }
    }
}