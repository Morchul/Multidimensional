using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float lifeTime;
    private float lifeTimeTimer = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);

        lifeTimeTimer += Time.deltaTime;
        if (lifeTimeTimer > lifeTime)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
            Destroy(this.gameObject);
    }
}
