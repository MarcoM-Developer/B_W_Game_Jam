using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private UIntReference sceneIndex;
    [SerializeField] private Vector3Variable playerWhitePosition, playerBlackPosition;
    [SerializeField] private BoolVariable loadedOtherScene;
    public static event Action OnSave;
    public static event Action OnLoad;


    // Start is called before the first frame update
    void Start()
    {
        sceneIndex.Value = (uint)SceneManager.GetActiveScene().buildIndex;

        if (OnLoad != null && loadedOtherScene.Value)
        {
            loadedOtherScene.Value = false;
            OnLoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        if (OnSave!=null)
        {
            OnSave();
        }

        SaveObject saveObject = new SaveObject { sceneIndex = (int)this.sceneIndex.Value, 
                                                 playerWhitePosition = this.playerWhitePosition.Value, 
                                                 playerBlackPosition = this.playerBlackPosition.Value 
                                               };

        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
        Debug.Log(json);
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(saveString);
            sceneIndex.Value = (uint)loadedSaveObject.sceneIndex;
            playerBlackPosition.Value = loadedSaveObject.playerWhitePosition;
            playerBlackPosition.Value = loadedSaveObject.playerBlackPosition;
        }

        if (OnLoad != null)
        {
            if (SceneManager.GetActiveScene().buildIndex != sceneIndex.Value)
            {
                loadedOtherScene.Value = true;
                SceneManager.LoadScene((int)sceneIndex.Value);
            }
            else
            {
                OnLoad();
            }
        }
    }

}



public class SaveObject
{
    public int sceneIndex;
    public Vector3 playerWhitePosition, playerBlackPosition;
}
