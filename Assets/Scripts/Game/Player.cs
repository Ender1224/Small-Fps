using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("visuals")]
    public Camera playerCamera;

    public GameObject bulletPrefab;

    [Header("Gameplay")]
    public int initialHealth = 100;
    public int initialAmmo = 12;
    public float knockbackForce = 10;
    public float hurtDuration = 0.5f;

    private int health;
    public int Health { get { return health; } }

    private int ammo;
    public int Ammo { get { return ammo; } }

    private bool isHurt;

    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
        ammo = initialAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))                                                                                           //How to fire the gun
        {
            ObjectPoolingManager.Instance,GetBullet ();
            if (Input.GetMouseButtonDown(0))
            {
                if (ammo > 0)
                {
                    ammo--;

                    GameObject bulletObject = Instantiate(bulletPrefab);                                                       //Make the gun fire the bullet prefab
                    bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;        //Make the mouse 1 button fire the bullet prefab on the player
                    bulletObject.transform.forward = playerCamera.transform.forward;
                }

            }

        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.GetComponent<AmmoCrate>() != null)                                                                    //Collect ammo crate
        {
            AmmoCrate ammoCrate = hit.collider.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;

            Destroy(ammoCrate.gameObject);
        }
        else if (hit.collider.GetComponent<Enemy>() != null)
        {

            if (isHurt == false)
            {
                {
                    Enemy enemy = hit.collider.GetComponent<Enemy>();                                                            //Enemy damage
                    health -= enemy.damage;

                    isHurt = true;

                    Vector3 hurtDirectrion = (transform.position - enemy.transform.position).normalized;                          //Perform the knockback effect    
                    Vector3 hurtDirection = default;
                    Vector3 KnockbackDirection = (hurtDirection + Vector3.up).normalized;
                    GetComponent<ForceReceiver> ().AddForce (KnockbackDirection, knockbackForce);

                    StartCoroutine(HurtRoutine());
                }



            }
        } 
    }
    IEnumerator HurtRoutine ()
    {
        yield return new WaitForSeconds(hurtDuration);

        isHurt = false;
    }
}
