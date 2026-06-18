using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Project.Scripts.Plugins.JsonUtilities
{
    public sealed class JsonLoader
    {
        private const string FolderConfigsName = "Configs";

        public T Load<T>(string fileName)
        {
            string path = Path.Combine(Application.streamingAssetsPath, FolderConfigsName, fileName);

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}