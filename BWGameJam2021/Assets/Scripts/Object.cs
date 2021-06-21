using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Object : MonoBehaviour
{
    private bool isOrbitingPlayer;
    [SerializeField] private Transform center;
    [SerializeField] private Vector3 axis = Vector3.up;
    private Vector3 torque;
    private float speed = 3;

    [SerializeField] private float radius = 2.0f;
    [SerializeField] private float radialSpeed = .5f;
    [SerializeField] private float rotationalSpeed = 80.0f;

    // Start is called before the first frame update
    void OnEnable()
    {
        isOrbitingPlayer = false;
        Debug.Log(isOrbitingPlayer);
        torque.x = Random.Range(-50, 50);
        torque.y = Random.Range(-50, 50);
        torque.z = Random.Range(-50, 50);
        GetComponent<ConstantForce>().torque = torque;
    }

    // Update is called once per frame
    void Update()
    {
        //In english: (if GravityWaves in use and distance to player is <= 10 and is not orbiting the player) or (is currently orbiting player) call Orbit Player
        if ((GameController.gs == GameState.GravityWaves && (Vector3.Distance(center.position, transform.position) <= 10 && !isOrbitingPlayer)) || isOrbitingPlayer)
        {
            OrbitPlayer();
        }

        transform.position += new Vector3(0.0f, 0.0f, -Random.Range(1, 3)) * Time.deltaTime * speed;
    }

    private void OrbitPlayer()
    {
        isOrbitingPlayer = true;
        Debug.Log(isOrbitingPlayer);
        //Code for orbiting player.
        //TODO: Make it so that multiple objects and orbits are supported. Maybe about 4?
        transform.RotateAround(center.position, axis, rotationalSpeed * Time.unscaledDeltaTime);
        Vector3 desiredPos = (transform.position - center.position).normalized * radius + center.position;

        transform.position = Vector3.MoveTowards(transform.position, desiredPos, Time.unscaledDeltaTime * radialSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //player will be dodging bullets or something else. If an object is shielding 
        //the player and the bullet hits it, it is destroyed
        if (collision.gameObject.name == "BackWall" && !isOrbitingPlayer)
            gameObject.SetActive(false);
    }
}