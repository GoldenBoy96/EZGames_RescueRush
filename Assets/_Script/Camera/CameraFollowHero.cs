using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowHero : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Transform objToFollow;

    private void Start()
    {
        StopAllCoroutines();
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        yield return new WaitUntil(() => GameController.Instance.HeroController != null);

        objToFollow = GameController.Instance.HeroController.gameObject.transform;
        Debug.Log(objToFollow.name);
        virtualCamera.Follow = objToFollow;
        virtualCamera.LookAt = objToFollow;
    }

}
