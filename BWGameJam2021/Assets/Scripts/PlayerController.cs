using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
            transform.Translate(-transform.right * movementSpeed * Time.unscaledDeltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * movementSpeed * Time.unscaledDeltaTime);
        }

        if(Input.GetKeyDown(KeyCode.Q) && GameController.gs == GameState.Normal)
        {
            StartCoroutine("SetGravityWaves");
        }
        
        //Checks if GameState is normal. Can only use on ability at a time (at least for now)
        //
        if (Input.GetKeyDown(KeyCode.C) && GameController.gs == GameState.Normal)
        {
            StartCoroutine("WarpTime");
        }
    }

    //Change GameState to TimeWarp and reduce time scale to .1 for 5 seconds
    //Revert time scale and GameState afterwards
    IEnumerator WarpTime()
    {
        GameController.gs = GameState.TimeWarp;
        Time.timeScale = .1f;
        //Time.fixedDeltaTime = Time.timeScale * 0.02f;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1f;
        GameController.gs = GameState.Normal;
    }

    IEnumerator SetGravityWaves()
    {
        GameController.gs = GameState.GravityWaves;
        yield return new WaitForSecondsRealtime(1);
        GameController.gs = GameState.Normal;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "object")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
