using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Orbitee : MonoBehaviour
{
    // String created to distinguish what gameobject it affects.
    public string bodyname;

    // float UT holds the number of seconds in a day
    protected float UT = 3600 * 24;

    // This sets the constant 'Unity Length' (where 1 UL = 200,000km).
    const int UL = 200000;

    //This holds the position of an out-of-script defined game object called parenttransform (the body its orbiting around).
    public Transform parenttransform;

    //Creates.
    private float semimajor;
    private float semiminor;
    private float focaldistance;
    private float angularvelocity;
    const double gravityconstant = 6.67E-20; // In km^3/kgs^2
    public float parentmass;
    public float eccentricity;
    private float originaleccentricity;
    private float originalsemimajor;
    private float originalinclination;
    private float originalangle;
    public float inclination = 0;
    public float angle = 0; // in rads
    public float initialangle; // in degrees
    private bool eccentricityinclination = true;
    public bool trailcontinue = true;

    // Use this for initialization

    void Start()

    {
        originaleccentricity = eccentricity;
        originalinclination = inclination;
        originalangle = initialangle;
        semimajor = Mathf.Sqrt(Mathf.Pow((transform.position.x - parenttransform.position.x), 2) + Mathf.Pow((transform.position.y - parenttransform.position.y), 2));
        originalsemimajor = semimajor;
        orbit();
    }

    private void orbit()
    {
        angle = Mathf.Deg2Rad * initialangle;
        semiminor = semimajor * Mathf.Sqrt(1 - (Mathf.Pow(eccentricity, 2)));
        focaldistance = Mathf.Sqrt((Mathf.Pow(semimajor, 2) - Mathf.Pow(semiminor, 2)));
       TrailBehaviour check = gameObject.GetComponent<TrailBehaviour>();
        if (check != null) {
        check.pathclear();
        }
        EclipseCalculator calc = (EclipseCalculator)GameObject.Find("Eclipsulator").GetComponent(typeof(EclipseCalculator));
        calc.resetTime();

    }

    // Update is called once per frame
    protected void Update()
    {
        float distancefromparent = Vector3.Magnitude(parenttransform.position - transform.position);
        float GM = (float)gravityconstant * parentmass / (semimajor*UL);
        float distSqu = Mathf.Pow(distancefromparent, 2) * UL;

        if (bodyname == "Moon")
        {
            angularvelocity = 2.6617e-6f;
        }
        else { angularvelocity = (semiminor * Mathf.Sqrt(GM)) / distSqu; }
        

        angle += ((Time.deltaTime * UT) * angularvelocity);
        test();

        float x = (semimajor * -Mathf.Cos(angle));
        float y = (semiminor * Mathf.Sin(angle));


        Vector3 newposition = new Vector3(x, y, 0);
        //inclination = Mathf.Deg2Rad*(inclination);
        Quaternion rotation = Quaternion.Euler(0, inclination, 0);

        newposition = rotation * newposition;
        newposition = newposition + parenttransform.position;
        newposition.x += focaldistance;
        transform.position = newposition;

    }
    public void changeUT(float scale)
    {
        UT = 3600 * 24 * scale;
    }


    public void toggleclipseulator(bool togglepressed)
    {
        if (eccentricityinclination == true)
        {
            inclination = 0;
            eccentricity = 0;

            eccentricityinclination = false;
        }
        else
        {
            inclination = originalinclination;
            eccentricity = originaleccentricity;
            semimajor = originalsemimajor;
            eccentricityinclination = true;
        }
        orbit();

    }
    public void resetbegin(bool reset)
    {
        if (reset == true)
        {
            eccentricity = originaleccentricity;
            inclination = originalinclination;
            initialangle = originalangle;
            semimajor = originalsemimajor;

            orbit();
        }

    }

    // Purpose. 
    // parameters.
    // returns.
    public void seteccentricity(float e)
    {
        eccentricity = e;
        orbit();
    }

    public void setangle(float a)
    {
        initialangle = a;
        orbit();
    }

    public void setinclination(float i)
    {
        inclination = i;
        orbit();
    }
    public void setsemimajor(float a)
    {
        semimajor = a;
        orbit();
    }
    private void test()
    {
        if (bodyname == "Earth")
        {
            var scripts = FindObjectsOfType(typeof(VelocityTest));
           foreach (VelocityTest script in scripts)
            {
               script.OrbitalPeriod(initialangle, angle);
            }
        }
    }
}


