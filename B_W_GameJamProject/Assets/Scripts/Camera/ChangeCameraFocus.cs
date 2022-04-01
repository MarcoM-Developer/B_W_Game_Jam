using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraFocus : MonoBehaviour
{
    [SerializeField] private List<Player> players;
    [SerializeField] private CinemachineVirtualCamera virutalCamera;

    private void OnEnable()
    {
        Player.OnSwitchCharacter += ChangeFocus;
    }


    // Start is called before the first frame update
    void Start()
    {
        foreach(Player player in FindObjectsOfType<Player>())
        {
            players.Add(player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        Player.OnSwitchCharacter -= ChangeFocus;
    }


    private void ChangeFocus()
    {
        foreach(Player player in players)
        {
            if (player.IsActive)
            {
                virutalCamera.Follow = player.transform;
                virutalCamera.LookAt = player.transform;
                break;
            }
        }
    }
}
