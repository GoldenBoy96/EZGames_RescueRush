using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CatController : MonoBehaviour
{
    [SerializeField] private Cat cat;

    [SerializeField] private GameObject heroObject;
    [SerializeField] private HeroController heroController;
    [SerializeField] private bool isFollowHero = false;
    [SerializeField] private float speed = 0;
    [SerializeField] private float distance = 0;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        speed = cat.Speed;
        distance = cat.Distance;
        animator.Play(AnimConstants.Idle);
    }

    private void Start()
    {
        GameController.Instance.RegisterController(this);
        Observer.AddObserver(ObserverConstants.UpdateSpeed, new((x) => SyncSpeedWithHero()));

        if (cat.IsRandomRotate)
        {
            float randomEuler = Random.Range(0, 360);
            transform.Rotate(new(0, randomEuler, 0));
        }
        else
        {
            transform.Rotate(new(0, cat.Rotation, 0));
        }
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
        Observer.Notify(ObserverConstants.UpdateCat);
        animator.Play(AnimConstants.Walk);
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
            var q = Quaternion.LookRotation(heroObject.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 100 * Time.deltaTime);
        }
    }
}
