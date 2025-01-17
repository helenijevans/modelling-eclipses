using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrailBehaviour : MonoBehaviour {
    private bool trailcontinue = true;
    public void pathclear()
    {
        if (trailcontinue == false)
        {
            TrailRenderer path = (TrailRenderer)GameObject.Find("Trail").GetComponent(typeof(TrailRenderer));
            path.Clear();
           
        }
    }
    public void toggle()
    {
       trailcontinue =! trailcontinue;
    }


}

