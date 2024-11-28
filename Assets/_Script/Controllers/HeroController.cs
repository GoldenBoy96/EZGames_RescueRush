using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroController : MonoBehaviour
{
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

    public CharacterController Controller { get => controller; set => controller = value; }
    public Vector3 PlayerVelocity { get => playerVelocity; set => playerVelocity = value; }
    public float HeroSpeed { get => heroSpeed; set => heroSpeed = value; }
    public float JumpHeight { get => jumpHeight; set => jumpHeight = value; }
    public float GravityValue { get => gravityValue; set => gravityValue = value; }
    public float HeroBaseSpeed { get => heroBaseSpeed; set => heroBaseSpeed = value; }

    private void Start()
    {
        GameController.Instance.RegisterController(this);

        if (Controller == null) Controller = gameObject.AddComponent<CharacterController>();
        playerVelocity.y = 0f;
        Observer.Notify(ObserverConstants.UpdateSpeed, speedString + heroSpeed);
    }

    void Update()
    {
        ControlHero();
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
        } else
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

    public void UpgradeSpeed(int additionalSpeed)
    {
        heroSpeed += additionalSpeed;
        Observer.Notify(ObserverConstants.UpdateSpeed, speedString + heroSpeed);
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag + " | " + TagConstants.Cat);
        if (collision.gameObject.CompareTag(TagConstants.Cat))
        {
            TriggerCatFollower(collision);
        }
        if (collision.gameObject.CompareTag(TagConstants.Tsunami))
        {
            TriggerEndGame(collision);
        }

    }

    private void TriggerCatFollower(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<CatController>(out var controller))
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

    private void TriggerEndGame(Collision collision)
    {
        //Debug.Log("Tsunami");
        //Observer.Notify(ObserverConstants.EndGame);
        //if (collision.gameObject.TryGetComponent<TsunamiController>(out var controller))
        //{
        //    controller.SwitchState(StateEnum.End);
        //}
    }
}
