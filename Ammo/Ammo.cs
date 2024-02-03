using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, IFireable
{

    #region Tooltip
    [Tooltip("Populate with child trail renderer component")]
    #endregion
    [SerializeField] private TrailRenderer trailRenderer;

    private float ammoRange = 0f; //the range of each ammo
    private float ammoSpeed;
    private Vector3 fireDirectionVector;
    private BulletDetailsSO ammoDetailsSO;
    private bool isAmmoMaterialSet = false;

    private void Awake()
    {
        // Load components
    }
    private void Update()
    {
        //Calculate distance vector to move ammo
        //Vector3 distanceVector = fireDirectionVector * ammoSpeed * Time.deltaTime;

        //transform.position += distanceVector;

       //ammoRange -= distanceVector.magnitude;

        //if(ammoRange <= 0f)
        //{
        //    DisableAmmo();
        //}
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Settings.targetTag))
        {
            Debug.Log("hit " + collision.gameObject.name + " !");
            CreateBulletImpactEffect(collision);
            DisableAmmo();
        }
        if (collision.gameObject.CompareTag(Settings.wallTag))
        {
            Debug.Log("hit a wall");
            CreateBulletImpactEffect(collision);
            DisableAmmo();
        }
        if (collision.gameObject.CompareTag(Settings.beerBottleTag))
        {
            Debug.Log("hit a beer bottle");
            collision.gameObject.TryGetComponent<BeerBottle>(out BeerBottle beerBottle);
            beerBottle.Shatter();
            DisableAmmo();
        }
    }

    // <summary>
    // Initialise the ammo being fired - using the ammo details, weaponAimDirectionVector
    // </summary>
    public void InitialiseAmmo(BulletDetailsSO ammoDetailsSO,float ammoSpeed,Vector3 fireDirectionVector)
    {
        #region Ammo
        this.ammoDetailsSO = ammoDetailsSO;

        this.fireDirectionVector = UtilsClass.CalculateDirection(fireDirectionVector);

        // Set ammo range
        ammoRange = ammoDetailsSO.ammoRange;

        // Set ammo speed
        this.ammoSpeed = ammoSpeed;

        gameObject.SetActive(true);

        transform.forward = fireDirectionVector;

        gameObject.GetComponent<Rigidbody>().AddForce(this.fireDirectionVector * ammoSpeed, ForceMode.Impulse);

        #endregion

        #region 
        if (ammoDetailsSO.isAmmoTrail)
        {
            trailRenderer.gameObject.SetActive(true);
            trailRenderer.emitting = true;
            trailRenderer.material = ammoDetailsSO.ammoMaterial;
            trailRenderer.startWidth = ammoDetailsSO.ammoTrailStartWidth;
            trailRenderer.endWidth = ammoDetailsSO.ammoTrailEndWidth;
            trailRenderer.time = ammoDetailsSO.ammoTrailTime;
        }
        else
        {
        }
        #endregion
    }
    private void CreateBulletImpactEffect(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffect,
            contactPoint.point,
            Quaternion.LookRotation(contactPoint.normal)
            );

        hole.transform.SetParent(collision.gameObject.transform);
    }


    
    // <summary>
    // Disable ammo
    // </summary>
    private void DisableAmmo()
    {
        gameObject.SetActive(false);
    }
  
    public GameObject GetGameObject()
    {
        throw new System.NotImplementedException();
    }

   
}
