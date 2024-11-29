using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public void ToLobby()
    {
        SceneManager.LoadScene(SceneConstants.Lobby);
    }
    public void ToLevel_1()
    {
        SceneManager.LoadScene(SceneConstants.Level_1);
    }
}
