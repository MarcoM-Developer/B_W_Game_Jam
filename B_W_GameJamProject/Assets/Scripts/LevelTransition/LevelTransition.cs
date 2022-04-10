using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelTransition : MonoBehaviour
{
    public static Action OnTransitionEnded;

    private GameStateManager gameStateManager;

    [SerializeField] private Image blackTransient;
    [SerializeField] private Image whiteTransient;

    [SerializeField] private float sBlackFinalPosition;
    [SerializeField] private float sWhitFinalPosition;
    [SerializeField] private float eBlackFinalPosition;
    [SerializeField] private float eWhitFinalPosition;

    private void OnEnable()
    {
        FinishBlock.OnFinished += LoadNextLevel;
    }

    // Start is called before the first frame update
    private void Start()
    {
        gameStateManager = FindObjectOfType<GameStateManager>();

        ActiveTransientsObjectsAndEnterTransientState();

        whiteTransient.GetComponent<RectTransform>().DOAnchorPosX(sWhitFinalPosition, 1.5f);
        blackTransient.GetComponent<RectTransform>().DOAnchorPosX(sBlackFinalPosition, 1.5f).OnComplete(DisableTransientAndEndTransientState);
    }

    private void OnDisable()
    {
        FinishBlock.OnFinished -= LoadNextLevel;
    }

    public void LoadNextLevel(int sceneBuild)
    {
        ActiveTransientsObjectsAndEnterTransientState();
        blackTransient.GetComponent<RectTransform>().DOAnchorPosX(eBlackFinalPosition, 1);
        whiteTransient.GetComponent<RectTransform>().DOAnchorPosX(eWhitFinalPosition, 1).OnComplete(()=>SceneManager.LoadScene(sceneBuild, LoadSceneMode.Single));
    }

    public void LoadSavedGame()
    {
        ActiveTransientsObjectsAndEnterTransientState();
        blackTransient.GetComponent<RectTransform>().DOAnchorPosX(eBlackFinalPosition, 1);
        whiteTransient.GetComponent<RectTransform>().DOAnchorPosX(eWhitFinalPosition, 1).OnComplete(CheckThenLaunchEvent);
    }

    public void LoadMainMenu()
    {
        ActiveTransientsObjectsAndEnterTransientState();
        blackTransient.GetComponent<RectTransform>().DOAnchorPosX(eBlackFinalPosition, 1);
        whiteTransient.GetComponent<RectTransform>().DOAnchorPosX(eWhitFinalPosition, 1).OnComplete(() => SceneManager.LoadScene("MainMenu", LoadSceneMode.Single));
    }

    public void CheckThenLaunchEvent()
    {
        if (OnTransitionEnded!=null)
        {
            OnTransitionEnded();
        }
    }

    private void DisableTransientAndEndTransientState()
    {
        blackTransient.gameObject.SetActive(false);
        whiteTransient.gameObject.SetActive(false);
        gameStateManager.CurrentGameState.EndingState();
    }

    private void ActiveTransientsObjectsAndEnterTransientState()
    {
        blackTransient.gameObject.SetActive(true);
        whiteTransient.gameObject.SetActive(true);
        gameStateManager.CurrentGameState = gameStateManager.TransitionState;
        gameStateManager.CurrentGameState.StartState();
    }
}
