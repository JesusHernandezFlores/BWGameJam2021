using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    enum PlayerState
    {
        Normal = 0, TimeStop, Growing
    }

    [SerializeField] float movementSpeed = 10;
    PlayerState ps = PlayerState.Normal;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        ps = PlayerState.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
    }


    private void GetPlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.forward * movementSpeed * Time.unscaledDeltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-transform.forward * movementSpeed * Time.unscaledDeltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(transform.right * movementSpeed * Time.unscaledDeltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-transform.forward * movementSpeed * Time.unscaledDeltaTime);
        }

        if (Input.GetKey(KeyCode.C))
        {
            StartCoroutine("WarpTime");
        }
    }

    IEnumerator WarpTime()
    {
        Time.timeScale = .1f;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1f;
    }
}
