using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
    }

    public void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
    }

    private void TimeCheck()
    {
        if(TimeManager.Hour == 20 && TimeManager.Minute == 30)
        {
            StartCoroutine(MoveSquare());
        }
        
    }

    private IEnumerator MoveSquare()
    {
        transform.position = new Vector3(-100f,-49f,0);
        Vector3 targetPos = new Vector3(700f,-49f,0);

        Vector3 currentPos = transform.position;

        float timeElapsed = 0;
        float timeToMove = 3;

        while(timeElapsed < timeToMove){
            transform.position = Vector3.Lerp(currentPos,targetPos,timeElapsed/timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

    }
}
