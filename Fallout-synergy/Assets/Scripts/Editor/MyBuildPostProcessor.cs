using System;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

public class MyBuildPostProcessor
{
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        string path = Path.GetDirectoryName(pathToBuiltProject) + "\\data";
        if(Directory.Exists(path)) Directory.Delete(path, true);
        
        FileUtil.CopyFileOrDirectory("data", path);
    }
}