using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    enum PlayerState
    {
        Normal = 0, TimeStop, Growing
    }

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
        if (Input.GetKeyDown(KeyCode.C) && ps != PlayerState.TimeStop)
        {
            LightSpeed();
            ps = PlayerState.Normal;
        }
    }

    void LightSpeed()
    {
        Time.timeScale = 0f;
        ps = PlayerState.TimeStop;
        this.transform.position += new Vector3(20f, 0, 0);
        Time.timeScale = 1f;
    }

    IEnumerator SlowPlayerDown()
    {
        while (rb.velocity.x > 10)
        {
            rb.velocity = rb.velocity * .5f * Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(5);
        }
    }
}
