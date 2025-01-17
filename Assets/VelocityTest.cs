using UnityEngine;
using UnityEngine.UI;


public class VelocityTest : MonoBehaviour
{
    private float timeperiod = 0;
    private bool isnegative = true;

     private void outputtext()
    {
        Text textbox;
        textbox = GetComponent<Text>();

        timeperiod = Time.time - timeperiod;
        textbox.text += "Orbital Period: " + timeperiod + "\n";
        ResetTime();       
    }

    public void OrbitalPeriod(float initialangle, float angle)
    {
        var secondangle = angle % (Mathf.PI * 2);
        var difference = (Mathf.Deg2Rad * initialangle) - secondangle;
        // Debug.Log(difference);
        var prev = isnegative; 
        isnegative = (difference < 0);
        if (prev == false && isnegative == true)
        {
           outputtext();
        }
    }

    public void ResetTime()
    {
        timeperiod = Time.time;
    }
}