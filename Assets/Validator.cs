using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;


public class Validator : MonoBehaviour {
    public bool valid = true; // valid is true until proven otherwise.
    const int UL = 200000; // This sets the constant 'Unity Length' (1 UL = 200,000km).



    public void eccentricityvalid(string input)
    {
        validate(input, 0, 1);
    }



    public void anglevalid(string input)
    {
        validate(input, 0, 360);
    }



    public void earthaxisvalid(string input)
    {
        input = conversion(input);
        validate(input, 50, 30000);
    }



    public void moonaxisvalid(string input)
    {
        input = conversion(input);
        validate(input, 2, 50);
    }

    public static string conversion(string input)
    {
        var i = ((float.Parse(input) * 1e6) / UL);
        return i.ToString();
    }

    public void validate(string input, float lower, float upper)
    {
        var output = GetComponent<InputField>();
        try
        {
            var cinput = float.Parse(input);
            if (cinput < lower || cinput >= upper)
            {
                invalid(output);
            }
            else { valid = true; }
        }
        catch { invalid(output); }
    }
    private void invalid(InputField output)
    {
        output.text = "Invalid Value";
        valid = false;
    }
}


