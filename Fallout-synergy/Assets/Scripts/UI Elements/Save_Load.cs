using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UI_Elements
{


    public class Save_Load
    {
        private static string Folder = Application.dataPath + "/Saves/";
        private static bool isInit = false;

        public static void Init()
        {
            if (!isInit)
            {
                isInit = true;
                if (!Directory.Exists(Folder))
                {
                    Directory.CreateDirectory(Folder);
                }
            }
        }


        public static void SaveObject(object saveObject)
        {
            SaveObject("save", saveObject, false);
        }
        public static void SaveObject(string fileName, object saveObject, bool overwrite)
        {
            Init();
            string json = JsonUtility.ToJson(saveObject);
            Save(fileName, json, overwrite);
        }

        public static void Save(string fileName, string saveString, bool overwrite)
        {
            Init();
            string saveFile = fileName;
            if (!overwrite)
            {
                int saveNumber = 1;
                while (File.Exists(Folder + saveFile + ".txt"))
                {
                    saveNumber++;
                    saveFile = fileName + "_" + saveNumber;
                }
            }

            File.WriteAllText(Folder + saveFile + ".txt", saveString);
        }

        public static TSaveObject LoadObject<TSaveObject>(string name)
        {
            Init();
            string saveString = Load(name);
            if (saveString != null)
            {
                TSaveObject saveObject = JsonUtility.FromJson<TSaveObject>(saveString);
                return saveObject;
            }

            return default(TSaveObject);
        }

        public static string Load(string name)
        {
            Init();
            if (File.Exists(Folder + name + ".txt"))
            {
                return File.ReadAllText(Folder + name + ".txt");
            }

            return null;
        }

        public static void LoadLast()
        {
            Init();
            DirectoryInfo directoryInfo = new DirectoryInfo(Folder);

            FileInfo[] saves = directoryInfo.GetFiles(("*.") + Folder);

            FileInfo recent = null;
            foreach (var file in saves)
            {
                if (recent == null)
                {
                    recent = file;
                }
                else if (file.LastWriteTime > recent.LastWriteTime)
                {
                    recent = file;
                }
            }
        }
    }
}
