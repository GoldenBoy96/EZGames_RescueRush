using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;

    public static GameController Instance { get => instance; }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Reset();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public HeroController HeroController { get; private set; }
    public LevelController LevelController { get; private set; }
    public TsunamiController TsunamiController { get; private set; }
    public List<CatController> CatControllers { get; private set; }


    public void Reset()
    {
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
        HeroController.EnableControlHero();
        TsunamiController.StartChasingHero();
    }

}
