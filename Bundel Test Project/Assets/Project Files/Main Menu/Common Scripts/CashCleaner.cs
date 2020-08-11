using UnityEditor;
using UnityEngine;

public class CashCleaner :MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Cash/CleanCache")]
    public static void CleanCache()
    {
       Debug.Log(Caching.ClearCache() ? "Successfully cleaned the cache" : "Cache is being used");
    }
#endif

    public void Clean()
    {
        Debug.Log(Caching.ClearCache() ? "Successfully cleaned the cache" : "Cache is being used");
    }
}
