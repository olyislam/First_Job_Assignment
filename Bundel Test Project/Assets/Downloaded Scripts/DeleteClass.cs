using System.Collections;
using UnityEngine;
using System;
using System.IO;  //Most important Library

public class DeleteClass : MonoBehaviour
{
    public void DeleteAsset()
    {
        //If the file exits, Delete the file
        if (File.Exists(Application.persistentDataPath + "/yourPath/fileName"))
            File.Delete(Application.persistentDataPath + "/yourPath/fileName");

        
        //Your file was delete from memory
    }
}