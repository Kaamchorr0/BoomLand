using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera playerCamera;
    public bool isShooting, readyToShoot;
    bool allowReset= true;
    public float shootingDelay= 2f;

    public int bulletsperburst= 3;
    public int burstbulletleft;

    public float spreadintensity;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity= 30f;
    public float bulletlife= 3f;
    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }
    public ShootingMode Currentshootingmode;
    void Awake()
    {
        readyToShoot= true;
        burstbulletleft= bulletsperburst;
    }
    void Update()
    {
        if (Currentshootingmode== ShootingMode.Auto)
        {
            isShooting= Input.GetKey(KeyCode.Mouse0);
        }
        else if (Currentshootingmode== ShootingMode.Single || Currentshootingmode== ShootingMode.Burst)
        {
            isShooting= Input.GetKeyDown(KeyCode.Mouse0);
        }
        if(readyToShoot && isShooting)
        {
            burstbulletleft= bulletsperburst;
            FireWeapon();
        }
    }
    void FireWeapon()
    {
        readyToShoot= false;
        Vector3 shootingDirection= CalculateDirectionandSpread().normalized;
        GameObject bullet= Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized*bulletVelocity, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletlife));

        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset= false;
        }
        if(Currentshootingmode== ShootingMode.Burst && burstbulletleft> 1)
        {
            burstbulletleft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }
    private void ResetShot()
    {
        readyToShoot= true;
        allowReset= true;
    }
    public Vector3 CalculateDirectionandSpread()
    {
        Ray ray= playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint= hit.point;
        }
        else
        {
            targetPoint= ray.GetPoint(100);
        }
        Vector3 direction= targetPoint- bulletSpawn.position;
        float x= UnityEngine.Random.Range(-spreadintensity, spreadintensity);
        float y= UnityEngine.Random.Range(-spreadintensity, spreadintensity);
        return direction+ new Vector3(x,y,0);
    }

    IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
