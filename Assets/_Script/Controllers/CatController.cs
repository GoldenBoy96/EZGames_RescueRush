using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private Cat cat;

    [SerializeField] private GameObject heroObject;
    [SerializeField] private HeroController heroController;
    [SerializeField] private bool isFollowHero = false;
    [SerializeField] private float speed = 0;
    [SerializeField] private float distance = 0;

    private void Awake()
    {
        speed = cat.Speed;
        distance = cat.Distance;
    }

    private void Start()
    {
        GameController.Instance.RegisterController(this);
        Observer.AddObserver(ObserverConstants.UpdateSpeed, new((x) => SyncSpeedWithHero()));
    }
    private void Update()
    {
        FollowHero();
    }

    public void EnableFollowHero(GameObject heroObject, HeroController heroController)
    {
        this.heroObject = heroObject;
        this.heroController = heroController;
        isFollowHero = true;
        SyncSpeedWithHero();
    }

    private void SyncSpeedWithHero()
    {
        if (heroController != null)
        {
            speed = heroController.HeroSpeed;
        }
    }

    public void DisableFollowHero()
    {
        this.heroObject = null;
        isFollowHero = false;
    }

    private void FollowHero()
    {
        if (isFollowHero)
        {
            distance = Vector3.Distance(transform.position, heroObject.transform.position);
            if (distance > cat.Distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, heroObject.transform.position, speed * Time.deltaTime);
            }
        }
    }
}
