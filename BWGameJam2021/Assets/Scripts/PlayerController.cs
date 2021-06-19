using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
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
            transform.Translate(-transform.right * movementSpeed * Time.unscaledDeltaTime);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            GameController.gs = GameState.GravityWaves;
        }

       if (Input.GetKeyDown(KeyCode.C) && GameController.gs != GameState.TimeWarp)
       {
            StartCoroutine("WarpTime");
       }
    }

    IEnumerator WarpTime()
    {
        GameController.gs = GameState.TimeWarp;
        Time.timeScale = .1f;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1f;
        GameController.gs = GameState.TimeWarp;
    }
}
