using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayController : MonoBehaviour
{
    [SerializeField] TMP_Text speedLabel;
    [SerializeField] Button startButton;
    [SerializeField] Button restartButton;
    public void Awake()
    {
        Observer.AddObserver(ObserverConstants.UpdateSpeed, new(x => speedLabel.text = x[0].ToString()));
        Observer.AddObserver(ObserverConstants.StartGame, new(x => startButton.gameObject.SetActive(false)));
        Observer.AddObserver(ObserverConstants.EndGame, new(x => restartButton.gameObject.SetActive(true)));
    }
}
