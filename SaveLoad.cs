using System;
using System.IO;
using System.Xml.Serialization;

namespace parent_house_framework {
    public static class SaveLoad {
        public static void Save(SaveData saveData, string filePath) {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
            try {
                if (!Directory.Exists(Path.GetDirectoryName(filePath))) {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                    UnityEngine.Debug.Log(
                        $"Path's directory did not exist. Creating directory at {Path.GetDirectoryName(filePath)}");
                }

                using StreamWriter writer = new StreamWriter(filePath);
                serializer.Serialize(writer, saveData);
            }
            catch (Exception e) {
                UnityEngine.Debug.LogError($"Trying to save file that isn't SaveData or can't exist at: {filePath} because {e}");
            }
        }

        public static SaveData Load(string filePath) {
            try {
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                using StreamReader reader = new StreamReader(filePath);
                return (SaveData)serializer.Deserialize(reader);
            }
            catch (Exception e) {
                UnityEngine.Debug.LogError($"Trying to load file that isn't SaveData or doesn't exist at: {filePath} because {e}");
                return null;
            }
        }

        public static void DeleteSaveData(string filePath) {
            try {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    UnityEngine.Debug.Log($"File '{filePath}' has been deleted.");
                }
                else
                {
                    UnityEngine.Debug.LogError($"File '{filePath}' does not exist.");
                }
            }
            catch (Exception e) {
                UnityEngine.Debug.LogError($"Trying to delete file that isn't savedata or doesn't exist at: {filePath}");
            }
        }

        public static bool HasPath(string path) {
            return File.Exists(path);
        }
    }

    [Serializable]
    public abstract class SaveData {
    }
}
