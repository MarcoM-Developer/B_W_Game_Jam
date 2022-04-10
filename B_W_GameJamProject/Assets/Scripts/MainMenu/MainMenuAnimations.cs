using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuAnimations : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuImage; //, startButton; //, loadButton;
    [SerializeField] private Image mainMenuOpacityLayer;

    [SerializeField] private float mainMenuImageHeight;
    [SerializeField] private float mainMenuImageAnimationTime;
    [SerializeField] private float opacityLayerValue;
    [SerializeField] private float opacityLayerAnimationTime;
    [SerializeField] private float buttonXScaleValue;
    [SerializeField] private float buttonScaleAnimationTime;

    private bool hasPressedKey;
    private bool needInput;

    // Start is called before the first frame update
    void Start()
    {
        //needInput = true;
        //hasPressedKey = false;
        AnimateMainMenu();
    }

    private void AnimateMainMenu()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(mainMenuImage.transform.DOMoveY(mainMenuImageHeight, mainMenuImageAnimationTime));
        //mySequence.Append(mainMenuOpacityLayer.DOFade(opacityLayerValue, opacityLayerAnimationTime));
        //mySequence.Append(startButton.transform.DOScaleX(buttonXScaleValue, buttonScaleAnimationTime));
        // mySequence.Append(loadButton.transform.DOScaleX(buttonXScaleValue, buttonScaleAnimationTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
            
    }
}
