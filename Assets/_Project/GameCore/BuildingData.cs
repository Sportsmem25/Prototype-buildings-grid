using System;

[Serializable]
public class BuildingConfig 
{
    public string id;
    public string displayName;
    public string spritePath;
    public int width;
    public int height;
}

[Serializable]
public class BuildingConfigCollection
{
    public BuildingConfig[] buildings;
}