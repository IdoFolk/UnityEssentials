using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace IdoFolk_Essentials_Editor.Helpers
{
    public class EditorMethods 
    {
        const string extension = ".cs";

        public static void WriteToEnum(string path, string name, IEnumerable<string> data)
        {
            List<string> enumValues = new();
            if (File.Exists(path + "/" + name + extension))
            {
                using (StreamReader reader = File.OpenText(path + "/" + name + extension))
                {
                    while (reader.Peek() >= 0)
                    {
                        var line = reader.ReadLine();
                        if(line.Contains('}') || line.Contains('{')) continue;
                        var index = line.LastIndexOf('=');
                        enumValues.Add(line.Substring(1,index-1));
                    }
                }
            }
            enumValues.AddRange(data);
            CreateEnum(path,name,enumValues);
        }

        public static void CreateEnum(string path, string name, IEnumerable<string> data)
        {
            using (StreamWriter file = File.CreateText(path + "/" + name + extension))
            {
                file.WriteLine("public enum " + name + "{");
                int i = 0;
                foreach (var line in data)
                {
                    string lineRep = line.Replace(" ", string.Empty);
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