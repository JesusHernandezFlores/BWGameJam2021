using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectible : MonoBehaviour
{
    private float radius;
    [SerializeField] float radiusSpeed, theta, thetaSpeed;

    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        radius = Vector2.Distance(this.gameObject.transform.position, player.transform.position);
        radiusSpeed = 1;
        thetaSpeed = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        AbsorbedByPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            this.gameObject.SetActive(false);
    }

    private void AbsorbedByPlayer()
    {
        float t = 10 - ((Time.time * 2) % 10);

        theta = t * thetaSpeed;
        radius = t * radiusSpeed;

        transform.position = new Vector2(radius * Mathf.Cos(Mathf.Deg2Rad * theta) + player.transform.position.x, radius * Mathf.Sin(Mathf.Deg2Rad * theta) + player.transform.position.y);
    }
}
