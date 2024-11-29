using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;

    public static GameController Instance { get => instance; }

    public void Awake()
    {
        Reset();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private bool isPlaying = false;

    private void Start()
    {
        Observer.AddObserver(ObserverConstants.StartGame, x => isPlaying = true);
        Observer.AddObserver(ObserverConstants.WinGame, x => isPlaying = false);
        Observer.AddObserver(ObserverConstants.EndGame, x => isPlaying = false);
    }

    public HeroController HeroController { get; private set; }
    public LevelController LevelController { get; private set; }
    public TsunamiController TsunamiController { get; private set; }
    public List<CatController> CatControllers { get; private set; }

    public UIDisplayLevel_1 UIDisplayController { get; private set; }
    public DynamicJoystick Joystick { get => joystick; set => joystick = value; }
    public bool IsPlaying { get => isPlaying; set => isPlaying = value; }

    public void Reset()
    {
        StopAllCoroutines();
        HeroController = null;
        LevelController = null;
        TsunamiController = null;
        CatControllers = new();

    }

    public void RegisterController(HeroController heroController)
    {
        this.HeroController = heroController;
    }
    public void RegisterController(LevelController levelController)
    {
        this.LevelController = levelController;
    }
    public void RegisterController(TsunamiController tsunamiController)
    {
        this.TsunamiController = tsunamiController;
    }
    public void RegisterController(CatController catController)
    {
        if (CatControllers.Contains(catController))
        {
            return;
        }
        else
        {
            CatControllers.Add(catController);
        }
    }

    public void StartLevel()
    {
        StartCoroutine(StartLevelCoroutine());
    }

    public IEnumerator StartLevelCoroutine()
    {
        Observer.Notify(ObserverConstants.StartGame);
        yield return new WaitForSeconds(LevelController.Level.PrepareTime);
        HeroController.EnableControlHero(); //need to change to observer
        //TsunamiController.StartChasingHero();
        Observer.Notify(ObserverConstants.EnterPhase_1);
        //AudioController.Instance.PlayMusic(AudioEnum.tsunami);
        ReduceSpeed();
    }

    public void RestartLevel()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(SceneConstants.Level_1);
        Observer.Notify(ObserverConstants.RestartGame);
    }


    public void BoostSpeed()
    {
        if (HeroController != null)
        {
            HeroController.UpgradeSpeed(LevelController.Level.BoostSpeedAmount);
        }
    }

    public void ReduceSpeed()
    {
        StartCoroutine(ReduceSpeedCoroutine());
    }

    private IEnumerator ReduceSpeedCoroutine()
    {
        yield return new WaitForSeconds(LevelController.Level.ReduceSpeedDeltaTime);
        if (HeroController != null)
        {
            HeroController.ReduceSpeed(LevelController.Level.ReduceSpeedAmount);
        }
        StartCoroutine(ReduceSpeedCoroutine());
    }

    
}
