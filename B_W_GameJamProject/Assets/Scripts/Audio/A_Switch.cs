using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Switch : MonoBehaviour
{

    GridManager gridManager;
    
    [SerializeField] private AK.Wwise.Event pSwitch;

    // Start is called before the first frame update
    void Start()
    {
        gridManager = GetComponent<GridManager>();
      
    }

    // Update is called once per frame
    void Update()
    {

        if (gridManager.whiteOnSwitch || gridManager.blackOnSwitch)
            pSwitch.Post(gameObject);
    }



}//END MAIN
