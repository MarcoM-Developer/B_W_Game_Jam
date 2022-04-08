using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Objects : MonoBehaviour
{
    #region Public
    public MapRotator sMapRotator;
    public GridManager sGridManager;
    #endregion
    #region Private Variables
    [SerializeField] private AK.Wwise.Event pMapRotate;
    [SerializeField] private AK.Wwise.Event pSwitch;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MapRotateSwitch();
       // WireSwitch();
    }

    private void WireSwitch()
    {
        pSwitch.Post(sGridManager.gameObject);
    }

    private void MapRotateSwitch()
    {
       if(sMapRotator.playMapRotate)
            pMapRotate.Post(gameObject);
    }
}//END MAIN
