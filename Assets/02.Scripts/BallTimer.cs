using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallTimer : MonoBehaviour
{
    public Text timer;
    private float timerElapsed;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("enter");
    }

    private void OnCollisionStay(Collision collision)
    {
        timerElapsed += Time.deltaTime;
        timer.text = timerElapsed.ToString();
    }

    private void OnCollisionExit(Collision collision)
    {
        timerElapsed = 0;
    }

 
}
