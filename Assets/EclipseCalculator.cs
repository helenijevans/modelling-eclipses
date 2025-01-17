using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.UI;


public class EclipseCalculator : MonoBehaviour {
    Text textbox;
    public Transform earthpos;
    public Transform moonpos;
    public float moonradius;
    public float sunradius;
    public float earthradius;
    private bool eccentricityinclination = false;
    protected bool state = true;
    protected float UT = 1;
    const int UL = 200000;
    bool pasthit;
    private DateTime timetrack;
    private string eclipsetype;

    // Use this for initialization
    void Start()
    {
        textbox = GetComponent<Text>();
        timetrack = new DateTime(2016, 9, 16, 20, 5, 0);
    }


    // Update is called once per frame
    void Update() {
        bool hitforward = raycalculator(Vector3.forward);
        bool hitback = raycalculator(Vector3.back);
        bool hit = hitback && hitforward;

        if (hit != pasthit)
        {
            if (moonpos.position.magnitude < earthpos.position.magnitude) {
                eclipsetype = "Solar";
            }
            else
            {
                eclipsetype = "Lunar";
            }
            resultoutput(hit, state);

        }

        timetrack = timetrack.AddDays(UT * Time.deltaTime);

        pasthit = hit;
    }

    public void resultoutput(bool hit, bool state)
    {
        if (state == true)
        {
            CultureInfo culture = new CultureInfo("en-GB");
            if (eccentricityinclination == true && moonpos.position.magnitude < earthpos.position.magnitude)
            {
                eclipsetype = "New Moon";
            }
            else if (eccentricityinclination == true && moonpos.position.magnitude > earthpos.position.magnitude)
            {
                eclipsetype = "Full Moon";
            }

            textbox.text += (eclipsetype + "-" + timetrack.ToString("F", culture)) + ": " + hit.ToString() + "\n";
            writetofile(timetrack.ToString("F", culture), hit.ToString(), eclipsetype);
        }
    }

    public void resultsonoff(bool pressed)
    {
       if (pressed == true)
        {
            if (state == true)
            {
                state = false;
                textbox.text = ""; 
            }
            else
            {
                state = true;
            }
        }
       

    }

    public void changeUT(float scale)
    {
        UT = scale;
    }

    bool raycalculator(Vector3 direction)
    {
        
            Vector3 perp = Vector3.Cross(direction, moonpos.position);
            perp /= perp.magnitude;
            Vector3 sunperp = perp * sunradius / UL;
            Vector3 moonperp = perp * moonradius / UL;
            Vector3 moonedge = moonperp + moonpos.position;
            return Physics.Raycast(sunperp, (moonedge - sunperp));
    }
    public void toggleclipseulator(bool togglepressed)
    {
        if (eccentricityinclination == true)
        {

            eccentricityinclination = false;

        }
        else
        {
            eccentricityinclination = true;
        }
    }

    public void writetofile(string time, string hit, string eclipsetype)
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Helen\Documents\results.txt", true)) 
        {
            file.WriteLine(time + "," + eclipsetype + "," + hit);
        }
    }

    public void resetTime()
    {
        timetrack = new DateTime(2016, 9, 16, 20, 5, 0);
    }

}