using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RecoilEvent))]
[DisallowMultipleComponent]
public class Recoil : MonoBehaviour
{
    private RecoilEvent recoilEvent;
    // Rotations
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    // Settings
    private float snappiness;
    private float returnSpeed;

    private bool isAiming;

    private void Awake()
    {
        recoilEvent = GetComponent<RecoilEvent>();
        
    }
    private void OnEnable()
    {
        // Subscribe to recoil event
        recoilEvent.OnRecoil += RecoilEvent_OnRecoil;
    }
    private void OnDisable()
    {
        // Unsubscribe from recoil event
        recoilEvent.OnRecoil -= RecoilEvent_OnRecoil;

    }

    private void RecoilEvent_OnRecoil(RecoilEvent arg1, RecoilEventArgs recoilEventArgs)
    {
        isAiming = Input.GetMouseButton(1);

        RecoilFire(recoilEventArgs.weaponRecoil, recoilEventArgs.weaponAdsRecoil, isAiming);

        //set weapon snappiness
        snappiness = recoilEventArgs.snappiness;

        //set weapon return speed
        returnSpeed = recoilEventArgs.returnSpeed;
    }

    private void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    // <summary>
    // Recoil Fire
    // </summary>
    private void RecoilFire(Vector3 normalRecoil, Vector3 aimRecoil,bool isAiming)
    {
        if (isAiming) targetRotation += new Vector3(aimRecoil.x, Random.Range(-aimRecoil.y, aimRecoil.y), Random.Range(-aimRecoil.z, aimRecoil.z));
        else targetRotation += new Vector3(normalRecoil.x, Random.Range(-normalRecoil.y, normalRecoil.y), Random.Range(-normalRecoil.z, normalRecoil.z));
    }
}
