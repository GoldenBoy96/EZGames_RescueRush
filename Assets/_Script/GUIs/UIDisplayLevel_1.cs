using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayLevel_1 : MonoBehaviour
{
    [SerializeField] TMP_Text speedLabel;
    [SerializeField] TMP_Text catLabel;
    [SerializeField] Button startButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button boostSpeedButton;
    [SerializeField] GameObject endGamePopup;
    [SerializeField] TMP_Text endGameText;
    public void Awake()
    {
        startButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        boostSpeedButton.gameObject.SetActive(false);
        endGamePopup.SetActive(false);

        Observer.AddObserver(ObserverConstants.UpdateSpeed, new(x => speedLabel.text = x[0].ToString()));
        Observer.AddObserver(ObserverConstants.UpdateCat, new(x => catLabel.text = "Cat: " + GameController.Instance.HeroController.FollowedCat.Count));
        Observer.AddObserver(ObserverConstants.StartGame, new(x =>
        {
            startButton.gameObject.SetActive(false);
            boostSpeedButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(false);
            endGamePopup.SetActive(false);
        }));
        Observer.AddObserver(ObserverConstants.EnterPhase_1, new(x => {
            startButton.gameObject.SetActive(false);
            boostSpeedButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
            endGamePopup.SetActive(false);
        }));
        Observer.AddObserver(ObserverConstants.EndGame, new(x => {
            startButton.gameObject.SetActive(false);
            boostSpeedButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
            endGamePopup.SetActive(true);
            endGameText.text = $"Cat rescue number: {GameController.Instance.HeroController.FollowedCat.Count}";
        }));
    }
}
