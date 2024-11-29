using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField] Hero hero;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Vector3 playerVelocity;
    //[SerializeField] private bool groundedPlayer;
    [SerializeField] private float heroBaseSpeed = 2.0f;
    [SerializeField] private float heroSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private List<GameObject> followedCat = new();
    [SerializeField] private bool isControllable = false;

    private static readonly string speedString = "Speed: ";

    public Hero Hero { get => hero; set => hero = value; }
    public CharacterController Controller { get => controller; set => controller = value; }
    public Vector3 PlayerVelocity { get => playerVelocity; set => playerVelocity = value; }
    public float HeroBaseSpeed { get => heroBaseSpeed; set => heroBaseSpeed = value; }
    public float HeroSpeed { get => heroSpeed; set => heroSpeed = value; }
    public float JumpHeight { get => jumpHeight; set => jumpHeight = value; }
    public float GravityValue { get => gravityValue; set => gravityValue = value; }
    public List<GameObject> FollowedCat { get => followedCat; set => followedCat = value; }
    public bool IsControllable { get => isControllable; set => isControllable = value; }

    private void Start()
    {
        GameController.Instance.RegisterController(this);

        InitHero();

        if (Controller == null) Controller = gameObject.AddComponent<CharacterController>();
        playerVelocity.y = 0f;

        hero = SaveLoadHeroData.Load();

        heroBaseSpeed = hero.Speed;
        heroSpeed = hero.Speed;

        Observer.AddObserver(ObserverConstants.EndGame, x => isControllable = false);
        Observer.Notify(ObserverConstants.UpdateSpeed, speedString + heroSpeed);
        Observer.Notify(ObserverConstants.UpdateCat);
    }

    void Update()
    {
        ControlHero();
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
        Observer.Notify(ObserverConstants.UpdateSpeed, hero.Speed);

    }

    public void EnableControlHero()
    {
        isControllable = true;
    }
    private void ControlHero()
    {
        if (isControllable)
        {
            Vector3 move = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Controller.Move(HeroSpeed * Time.deltaTime * move);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }
            playerVelocity.y += GravityValue * Time.deltaTime;
            Controller.Move(PlayerVelocity * Time.deltaTime);
        }
        else
        {
            Vector3 move = Vector3.zero;
            Controller.Move(HeroSpeed * Time.deltaTime * move);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }
            playerVelocity.y += GravityValue * Time.deltaTime;
            Controller.Move(PlayerVelocity * Time.deltaTime);
        }
    }
    public Vector3 GetHeroPosition()
    {
        return transform.position;
    }

    public void UpgradeSpeed(float additionalSpeed)
    {
        heroSpeed += additionalSpeed;
        Observer.Notify(ObserverConstants.UpdateSpeed, speedString + heroSpeed);
    }

    public void ReduceSpeed(float reducedSpeed)
    {
        if (heroSpeed > heroBaseSpeed)
        {
            heroSpeed -= reducedSpeed;
            if (heroSpeed < heroBaseSpeed)
            {
                heroBaseSpeed = heroSpeed;
            }
            Observer.Notify(ObserverConstants.UpdateSpeed, speedString + heroSpeed);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log(collision.gameObject.tag + " | " + TagConstants.Cat);
        if (other.gameObject.CompareTag(TagConstants.Cat))
        {
            TriggerCatFollower(other);
        }
        if (other.gameObject.CompareTag(TagConstants.Tsunami))
        {
            StartCoroutine(TriggerEndGame(other));
        }

    }

    private void TriggerCatFollower(Collider other)
    {
        if (other.gameObject.TryGetComponent<CatController>(out var controller))
        {
            if (!followedCat.Contains(controller.gameObject))
            {
                if (followedCat.Count == 0)
                {
                    controller.EnableFollowHero(gameObject, this);
                    followedCat.Add(controller.gameObject);
                }
                else
                {
                    controller.EnableFollowHero(followedCat.Last(), this);
                    followedCat.Add(controller.gameObject);
                }

            }
        }
    }

    private IEnumerator TriggerEndGame(Collider other)
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Tsunami");
        Observer.Notify(ObserverConstants.EndGame);
        StopAllCoroutines();
    }

}
