using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private Cat cat;

    [SerializeField] private GameObject heroObject;
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
    }
    private void Update()
    {
        FollowHero();
    }

    public void EnableFollowHero(GameObject heroObject)
    {
        Debug.Log(heroObject.name);
        this.heroObject = heroObject;
        isFollowHero = true;
        if(heroObject.TryGetComponent<HeroController>(out var controller))
        {
            speed = controller.PlayerSpeed;
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
