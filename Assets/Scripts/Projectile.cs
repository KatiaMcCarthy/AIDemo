using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime = 0.0f;
    public int damage = 0;

    private Vector3 hitPoint;

    public GameObject deathParticle;

    // Start is called before the first frame update
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * 6.8f * speed * Time.deltaTime);

        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 1))

        {
            hitPoint = hitInfo.point;
            GameObject go = hitInfo.collider.gameObject;
            Vector3 dir = go.transform.position;

            Debug.Log(go);
            enemyBehavior eb = go.gameObject.GetComponentInParent<enemyBehavior>();
            if (eb != false)
            {
                eb.TakeDamage(go);
            }

            DestroyProjectile();
        }
      

    }

    void DestroyProjectile()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Level")
        {
            enemyBehavior eb = other.gameObject.GetComponentInParent<enemyBehavior>();
            if (eb != null)
            {
                eb.TakeDamage(other.gameObject);
            }
            DestroyProjectile();
        }
    }
}
