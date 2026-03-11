using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity= 30f;
    public float bulletlife= 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
    }
    void FireWeapon()
    {
        GameObject bullet= Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized*bulletVelocity, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletlife));
    }

    IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
