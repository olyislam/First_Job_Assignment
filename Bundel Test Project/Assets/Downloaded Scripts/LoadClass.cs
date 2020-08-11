using System.Collections;
using UnityEngine;
using System;

public class LoadClass : MonoBehaviour
{
    //Variable to hold the asset bundle
    private AssetBundle bundle;

    public void LoadAsset()
    {
        //Check to load the bundle only once
        if (bundle != null)
            bundle.Unload(true);

        //Load an AssetBundle in a specific path
        bundle = AssetBundle.LoadFromFile(Application.persistentDataPath + "/yourPath/fileName");
      
        //Your file is Now loaded, you can access to your data              
    }
}