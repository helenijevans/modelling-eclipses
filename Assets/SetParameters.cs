using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetParameters : MonoBehaviour
{
    // gets gameobject 'earth' from Orbitee Script.
    public Orbitee earth;

    // gets gameobject 'moon' from Orbitee Script.
    public Orbitee moon;

    // creates boolean 'readysetgo', which is true until proven false.
    protected bool readysetgo = true;

    // creates empty ArrayList 'fields'
    ArrayList fields = new ArrayList();

    // purpose of this method is to make sure that all inputs in the input fields are valid before setting them.
    public void finalcheck()
    {
        fields.Add("EccentricityEarth");
        fields.Add("EccentricityMoon");
        fields.Add("EarthA");
        fields.Add("MoonA");
        fields.Add("MoonInclination");
        fields.Add("SemiMEarth");
        fields.Add("SemiMMoon");

        //for loop checks if each input in each field is valid. If it's not readysetgo = false
        foreach (string i in fields)
        {
            Validator obj = (Validator)GameObject.Find(i).GetComponent(typeof(Validator));
            if (obj.valid == false)
            {
                readysetgo = false;
            }
        }
    }

    //  This method sets the altered parameters if readysetgo = true
    // It does this by passing the new values to the orbitee script in methods: seteccentricity, setangle, setinclination and setsemimajor.
    public void set()
    {
        finalcheck();
        if (readysetgo == true)
        {
            InputField value = (InputField)GameObject.Find((string)fields[0]).GetComponent(typeof(InputField));
            earth.seteccentricity(float.Parse(value.text));

            value = (InputField)GameObject.Find((string)fields[1]).GetComponent(typeof(InputField));
            moon.seteccentricity(float.Parse(value.text));

            value = (InputField)GameObject.Find((string)fields[2]).GetComponent(typeof(InputField));
            earth.setangle(float.Parse(value.text));
        
            value = (InputField)GameObject.Find((string)fields[3]).GetComponent(typeof(InputField));
            moon.setangle(float.Parse(value.text));

           value = (InputField)GameObject.Find((string)fields[4]).GetComponent(typeof(InputField));
           moon.setinclination(float.Parse(value.text));

            value = (InputField)GameObject.Find((string)fields[5]).GetComponent(typeof(InputField));
            earth.setsemimajor(float.Parse(Validator.conversion(value.text)));

            value = (InputField)GameObject.Find((string)fields[6]).GetComponent(typeof(InputField));
            moon.setsemimajor(float.Parse(Validator.conversion(value.text)));

           var t = (Text)GameObject.Find("Eclipsulator").GetComponent(typeof(Text));
            t.text = "";
        }

    }
}