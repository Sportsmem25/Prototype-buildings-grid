using UnityEngine;

public class Bootstrapper : MonoBehaviour
{

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Init()
    {
        var bootstrapperPrefab = Resources.Load<GameObject>("Boostraper");
        GameObject go = GameObject.Instantiate(bootstrapperPrefab);
        DontDestroyOnLoad(go);
    }
}
