using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
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
        if(TimeManager.Hour == 20 && TimeManager.Minute == 37)
        {
            StartCoroutine(MoveSquare());
        }
        
    }

    private IEnumerator MoveSquare()
    {
        transform.position = new Vector3(1549f,8f,0);
        Vector3 targetPos = new Vector3(67f,641f,0);

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
