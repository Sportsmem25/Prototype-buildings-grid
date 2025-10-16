using System.IO;
using UnityEngine;

public class ConfigLoader : MonoBehaviour
{
    public BuildingConfigCollection Config { get; private set; }

    private void Awake()
    {
        var path = Path.Combine(Application.dataPath, "_Project/Config/ConfigsBuilding.json");
        if (!File.Exists(path))
        {
            Debug.LogError("Config not found: "+ path);
            return;
        }

        var json = File.ReadAllText(path);
        Config = JsonUtility.FromJson<BuildingConfigCollection>(json);
    }
}
