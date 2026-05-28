using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            CreateBulletImpactEffect(collision);
            Debug.Log("hit "+ collision.gameObject.name+ " !");
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            CreateBulletImpactEffect(collision);
            Debug.Log("hit a Wall");
            Destroy(gameObject);
        }
    }
    void CreateBulletImpactEffect(Collision objectHit)
    {
        ContactPoint contact= objectHit.contacts[0];
        GameObject hole= Instantiate(GlobalReferences.Instance.bulletImpactEffectPrefab,
        contact.point, 
        Quaternion.LookRotation(contact.normal)
        );
        hole.transform.SetParent(objectHit.gameObject.transform);
    }
}
