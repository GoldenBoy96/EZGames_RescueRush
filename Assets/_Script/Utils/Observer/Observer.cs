using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Observer: MonoBehaviour
{
    static Dictionary<string, List<Action<object[]>>> Listeners =
        new();

    public static void AddObserver(string name, Action<object[]> callback)
    {
        if (!Listeners.ContainsKey(name))
        {
            Listeners.Add(name, new List<Action<object[]>>());
        }

        Listeners[name].Add(callback);
    }

    public static void RemoveObserver(string name, Action<object[]> callback)
    {
        if (!Listeners.ContainsKey(name))
        {
            return;
        }

        Listeners[name].Remove(callback);
    }

    public static void Notify( string name, params object[] data)
    {
        if (!Listeners.ContainsKey(name))
        {
            return;
        }

        foreach (var listener in Listeners[name].ToList())
        {
            try
            {
                listener.Invoke(data);
                //Debug.Log("Notify " + name);
            }
            catch (Exception ex)
            {
                RemoveObserver(name, listener);
                Notify(name, data);
                //Debug.LogError("Error on invoke listener: " + ex);
            }
        }
    }

}
