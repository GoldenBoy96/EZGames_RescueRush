using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayLobby : MonoBehaviour
{
    [SerializeField] TMP_Text speedLabel;
    [SerializeField] Button startButton;
    [SerializeField] Button resetButton;
    public void Awake()
    {
        Observer.AddObserver(ObserverConstants.UpdateSpeed, new(x => speedLabel.text = x[0].ToString()));
    }
}
