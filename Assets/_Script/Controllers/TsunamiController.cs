using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsunamiController : MonoBehaviour
{
    [SerializeField]  Tsunami tsunami;
    [SerializeField] int phase = 0;
    [SerializeField] bool isChasingHero = false;

    public Tsunami Tsunami { get => tsunami; set => tsunami = value; }

    private void Awake()
    {
        Debug.Log(tsunami + " | " + Tsunami);
    }

    private void Start()
    {
        GameController.Instance.RegisterController(this);
        //StartChasingHero();
    }

    public void Update()
    {
        ChasingHero();
    }

    private void ChasingHero()
    {
        if (isChasingHero)
        {
            var target = new Vector3(transform.position.x, transform.position.y, GameController.Instance.HeroController.GetHeroPosition().z);
            switch (phase)
            {
                case 0:
                    transform.position = Vector3.MoveTowards(transform.position, target, Tsunami.SpeedPhase_0 * Time.deltaTime);
                    break;
                case 1:
                    transform.position = Vector3.MoveTowards(transform.position, target, Tsunami.SpeedPhase_1 * Time.deltaTime);
                    break;
                case 2:
                    transform.position = Vector3.MoveTowards(transform.position, target, Tsunami.SpeedPhase_2 * Time.deltaTime);
                    break;
                default:
                    transform.position = Vector3.MoveTowards(transform.position, target, Tsunami.SpeedPhase_0 * Time.deltaTime);
                    break;
            }
        }
    }
    public void StartChasingHero()
    {
        isChasingHero = true;
    }
    public void StopChasingHero()
    {
        isChasingHero = false;
    }

}
