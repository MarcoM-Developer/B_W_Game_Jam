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
    [SerializeField] private BoolVariable isSceneLoaded;
    [SerializeField] private BoolVariable isPickedUpWhiteGoal;
    [SerializeField] private BoolVariable isPickedUpBlackGoal;
    public static event Action OnSave;
    public static event Action OnLoad;

    private void OnEnable()
    {
        LevelTransition.OnTransitionEnded += Load;
    }

    // Start is called before the first frame update
    private void Start()
    {
        sceneIndex.Value = (uint)SceneManager.GetActiveScene().buildIndex;

        if (OnLoad != null && isSceneLoaded.Value)
        {
            isSceneLoaded.Value = false;
            OnLoad();
        }
    }

    private void OnDisable()
    {
        LevelTransition.OnTransitionEnded -= Load;
    }

    public void Save()
    {
        if (OnSave!=null)
        {
            OnSave();
        }

        SaveObject saveObject = new SaveObject { sceneIndex = (int)this.sceneIndex.Value, 
                                                 playerWhitePosition = this.playerWhitePosition.Value, 
                                                 playerBlackPosition = this.playerBlackPosition.Value,
                                                 isPickedUpBlackGoal = this.isPickedUpBlackGoal.Value,
                                                 isPickedUpWhiteGoal = this.isPickedUpWhiteGoal.Value,
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
            isPickedUpBlackGoal.Value = loadedSaveObject.isPickedUpBlackGoal;
            isPickedUpWhiteGoal.Value = loadedSaveObject.isPickedUpWhiteGoal;
        }

        /*if (SceneManager.GetActiveScene().buildIndex != sceneIndex.Value)
        {
            loadedOtherScene.Value = true;
            SceneManager.LoadScene((int)sceneIndex.Value, LoadSceneMode.Single);
        }*/
        SceneManager.LoadScene((int)sceneIndex.Value, LoadSceneMode.Single);
        isSceneLoaded.Value = true;
        /*if (OnLoad != null)
        {
            OnLoad();
        }*/
    }

   /* public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }*/

}



public class SaveObject
{
    public int sceneIndex;
    public Vector3 playerWhitePosition, playerBlackPosition;
    public bool isPickedUpWhiteGoal, isPickedUpBlackGoal;
}