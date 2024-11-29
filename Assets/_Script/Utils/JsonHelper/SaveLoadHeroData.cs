using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadHeroData : MonoBehaviour
{
    public static readonly string FileName = "Hero";

    public static void Save(Hero hero)
    {
        JsonHelper<Hero>.SaveFile(hero, FileName);
    }

    public static Hero Load()
    {
        return JsonHelper<Hero>.LoadFile(FileName);
    }
}
