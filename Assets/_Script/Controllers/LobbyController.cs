using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private float bonusSpeedLevel = 1;
    private Hero hero = null;

    private static readonly string speedString = "Speed: ";

    public void Start()
    {
        InitHero();
    }

    private void InitHero()
    {
        if (hero == null)
        {
            hero = SaveLoadHeroData.Load();
        }
        if (hero == null)
        {
            hero = new();
            SaveLoadHeroData.Save(hero);
        }
        Observer.Notify(ObserverConstants.UpdateSpeed, speedString + hero.Speed);

    }
    public void UpgradeHeroSpeed()
    {
        if (hero == null)
        {
            InitHero();
        }

        hero.Speed += bonusSpeedLevel;
        SaveLoadHeroData.Save(hero);
        Observer.Notify(ObserverConstants.UpdateSpeed, speedString + hero.Speed);

    }

    public void ResetHero()
    {
        hero = new();
        SaveLoadHeroData.Save(hero);
        Observer.Notify(ObserverConstants.UpdateSpeed, speedString + hero.Speed);
    }
}
