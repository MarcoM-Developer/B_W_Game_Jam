using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{

    [SerializeField] private GameObject image;
    [SerializeField] private Ease ease;

    // Start is called before the first frame update
    void Start()
    {
        image.transform.DORotate(new Vector3(0,0,-360),2,RotateMode.FastBeyond360).SetEase(ease).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
