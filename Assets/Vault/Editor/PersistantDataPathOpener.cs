using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Vault
{
    public class PersistantDataPathOpener : Editor
    {
        [MenuItem("Tools/Open Persistent Data Path")]
        public static void OpenPersistentDataPath()
        {
            string path = Application.persistentDataPath + "/Saves/";
            OpenInFileBrowser(path);
        }

        [MenuItem("Tools/Clear Persistent Data Path")]
        public static void ClearPersistentDataPath()
        {
            string path = Application.persistentDataPath + "/Saves/";

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    subDirectory.Delete(true);
                }

                EditorUtility.DisplayDialog("Clear Persistent Data Path", "All files and directories in the persistent data path have been deleted.", "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Clear Persistent Data Path", "Persistent data path does not exist.", "OK");
            }
        }

        private static void OpenInFileBrowser(string path)
        {
#if UNITY_EDITOR_WIN
            Process.Start("explorer.exe", path.Replace("/", "\\"));
#elif UNITY_EDITOR_OSX
        Process.Start("open", path);
#elif UNITY_EDITOR_LINUX
        Process.Start("xdg-open", path);
#endif
        }
    }
}


