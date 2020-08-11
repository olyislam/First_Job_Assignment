using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using System.Collections.Generic;
using UnityEditor;

public class AssetsBundleLoader : MonoBehaviour
{
    #region Property
#pragma warning disable 649

   

    [Header("Server URL")]
    [SerializeField] protected string Url;
    private AssetBundle assetBundle;
    [SerializeField] protected string BundleName;

    [Header("UI References")]
    [SerializeField] private Button DownloadAndPlayButton;
    [SerializeField] private Text ButtonText;
    [SerializeField] private Slider DownloadStatus;

#pragma warning restore
    #endregion Property

    private void Start()
    {
        bool FileExist = isDownloaded;
        DownloadMessage(FileExist ? true : false);

        if (FileExist)
            ChangeBtnEvent(LoadBundleFromStroage);
        else
        {
            DownloadAndPlayButton.interactable = !FileExist;
            ChangeBtnEvent(StartDownload);
        }


    }

    #region Download operation
    public void StartDownload()
    {
        DownloadMessage(false);
        StartCoroutine(Download());
    }


    //return true if download from server
    protected IEnumerator Download()
    {
        using (WWW www = new WWW(Url))
        {
            while (!www.isDone)
            {
                DownloadProgress(www.progress, www.bytesDownloaded);
                yield return null;
            }
            yield return www;

            if (www.error != null)
                throw new System.Exception("WWW download had an error:" + www.error);
            

            assetBundle = www.assetBundle;
            SaveBndle(www.bytes);
            ChangeBtnEvent(LoadScene);
            DownloadMessage(true);
            www.Dispose();

        }
    }
    #endregion Download operation


    #region File Operation
    private bool isDownloaded => File.Exists(FilePath);

    //return bundle path location from local storage
    private string FilePath => (Application.platform == RuntimePlatform.Android ?
        Application.persistentDataPath : Application.dataPath) + "/" + BundleName;

    private void SaveBndle(byte[] bundlebytes)
    {
        if (Directory.Exists(FilePath))
            Directory.CreateDirectory(FilePath);

#if !UNITY_WEBPLAYER
        File.WriteAllBytes(FilePath, bundlebytes);
#endif
    }

    private void LoadBundleFromStroage()
    { 
        byte[] bundle = File.ReadAllBytes(FilePath);

        assetBundle =  AssetBundle.LoadFromMemory(bundle);
        LoadScene();
        //assetBundle.Unload(false);
       
    }

    #endregion File Operation



    #region UI Message

    //pass success message to ui screen
    private void DownloadMessage( bool isComplete)
    {
        DownloadAndPlayButton.interactable = isComplete;
        ButtonText.text = isComplete ? "Play " +BundleName : "Download " + BundleName;
        DownloadStatus.value = isComplete? 1 : 0;
    }

    private void ChangeBtnEvent(UnityEngine.Events.UnityAction ButtonAction)
    {
       DownloadAndPlayButton.onClick.RemoveAllListeners();
       DownloadAndPlayButton.onClick.AddListener(ButtonAction);  
    }

    //show download status on ui screen
    private void DownloadProgress( float progress, int downloadedbyte)
    {
        DownloadStatus.value = progress;
        ButtonText.text = "Downloading "+ (downloadedbyte / 1024) / 1024+ " MB";
    }
    #endregion UI Message



    #region Bundle Loading Operation
    // Load Scene From Assets Bundle
    protected void LoadScene()
    {
        string[] scenesName = assetBundle.GetAllScenePaths();
        SceneHandeller.LoadScene(scenesName[scenesName.Length - 1]);//in this case load default scene from last index 
        assetBundle.Unload(false);
    }

    // Load All Assets from Assets Bundle
    protected void LoadAllAssets()
    {
        string[] assetsName = assetBundle.GetAllAssetNames();
        foreach (var name in assetsName)
        {
            GameObject playerPrefab = assetBundle.LoadAsset<GameObject>(name);
            Instantiate(playerPrefab);
        }
    }
    #endregion  Bundle Loading Operation

}
