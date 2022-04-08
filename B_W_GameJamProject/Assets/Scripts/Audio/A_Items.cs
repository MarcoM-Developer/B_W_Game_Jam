using UnityEngine;

public class A_Items : MonoBehaviour
{
    //use to play sound when item is picked up
    //get collission info perhaps

    
    #region Public
    public FinishBlock sBlackFinishBlock;
    public FinishBlock sWhiteFinishBlock;
    #endregion
    #region Private Variables
    [SerializeField] private AK.Wwise.Event itemPickUp;
    #endregion



}
