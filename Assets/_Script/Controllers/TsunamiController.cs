using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsunamiController : MonoBehaviour
{
    [SerializeField] Tsunami tsunami;
    [SerializeField] StateEnum state;
    [SerializeField] bool isChasingHero = false;
    [SerializeField] int prepareTime = 0;

    public Tsunami Tsunami { get => tsunami; set => tsunami = value; }

    private void Awake()
    {
    }

    private void Start()
    {
        GameController.Instance.RegisterController(this);

        Observer.AddObserver(ObserverConstants.StartGame, x =>
        {
            SwitchState(StateEnum.Phase_Prepare);
        });
        Observer.AddObserver(ObserverConstants.EnterPhase_1, x =>
        {
            SwitchState(StateEnum.Phase_0);
            StartChasingHero();
        });
        Observer.AddObserver(ObserverConstants.EndGame, x =>
        {
            SwitchState(StateEnum.End);
            StopAllCoroutines();
        }); 
        Observer.AddObserver(ObserverConstants.WinGame, x =>
        {
            SwitchState(StateEnum.End);
            StopAllCoroutines();
        });
        //StartChasingHero();

        StartCoroutine(UpdateSlider());
    }

    public void Update()
    {
        ChasingHero();
        CheckPhase();
    }

    private void ChasingHero()
    {
        if (isChasingHero)
        {
            var target = new Vector3(transform.position.x, transform.position.y, GameController.Instance.HeroController.GetHeroPosition().z);
            switch (state)
            {
                case StateEnum.Phase_Prepare:
                    //Do nothing
                    break;
                case StateEnum.Phase_0:
                    transform.position = Vector3.MoveTowards(transform.position, target, Tsunami.SpeedPhase_0 * Time.deltaTime);
                    break;
                case StateEnum.Phase_1:
                    transform.position = Vector3.MoveTowards(transform.position, target, Tsunami.SpeedPhase_1 * Time.deltaTime);
                    break;
                case StateEnum.Phase_2:
                    transform.position = Vector3.MoveTowards(transform.position, target, Tsunami.SpeedPhase_2 * Time.deltaTime);
                    break;
                case StateEnum.End:
                    //Do nothing
                    break;
                default:
                    //Do nothing
                    break;
            }
        }
    }

    public void StartChasingHero()
    {
        isChasingHero = true;
    }
    public void StopChasingHero()
    {
        isChasingHero = false;
    }

    private void CheckPhase()
    {
        if (state.Equals(StateEnum.End))
        {
            return;
        }
        if (transform.position.z >= 0
            && transform.position.z < GameController.Instance.LevelController.Level.FinishLineDistance_1)
        {
            SwitchState(StateEnum.Phase_1);
        }
        if (transform.position.z >= GameController.Instance.LevelController.Level.FinishLineDistance_1
            && transform.position.z < GameController.Instance.LevelController.GetFinishLineDistance())
        {
            SwitchState(StateEnum.Phase_2);
        }
        if (transform.position.z >= GameController.Instance.LevelController.GetFinishLineDistance())
        {
            Debug.Log("End game due to out of finish line 2");
            SwitchState(StateEnum.End);
        }
    }
    public void SwitchState(StateEnum stateEnum)
    {
        state = stateEnum;
    }

    private IEnumerator UpdateSlider()
    {
        yield return new WaitForSeconds(0.1f);
        Observer.Notify(ObserverConstants.WavePositionUpdate, GameController.Instance.LevelController.GetFinishLineDistance(), transform.position.z);
        StartCoroutine(UpdateSlider());
    }
}
