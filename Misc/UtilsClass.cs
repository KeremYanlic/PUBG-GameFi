using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class UtilsClass
{
    // <summary>
    // Calculate bullet output direction
    // </summary>
    public static Vector3 CalculateDirection(Vector3 bulletSpawnPos)
    {
        // Shooting from the middle of the screen to check where are pointing at
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // If hitting something then return hit.point else return a point at 100 distance units along the ray.
        Vector3 targetPoint = Physics.Raycast(ray, out hit) ? hit.point : ray.GetPoint(100);

        Vector3 direction = targetPoint - bulletSpawnPos;
        return direction.normalized;
    }

    // <summary>
    // Get distance between two location
    // <summary>
    public static float GetDistance(Vector3 firstLoc, Vector3 secondLoc)
    {
        return Vector3.Distance(firstLoc, secondLoc);
    }

    // <summary>
    // Convert the linear volume scale to decibels
    // </summary>
    public static float LinearToDecibels(int linear)
    {
        float linearScaleRange = 20f;

        // formula to convert from the linear scale to the logarithmic decibel scale
        return Mathf.Log10((float)linear / linearScaleRange) * 20f;
    }

    // <summary>
    // Null value debug check
    // </summary>
    public static bool ValidateCheckNullValue(System.Object thisObject, string fieldName, UnityEngine.Object valueToCheck)
    {
        bool error = false;

        if (valueToCheck == null)
        {
            Debug.Log(fieldName + " is null and must contain a value in object " + thisObject.ToString());
            error = true;
        }
        else
        {
            error = false;
        }
        return error;
    }
    // <summary>
    // Enumerable value debug check
    // </summary>
    public static bool ValidateCheckEnumerableValues(System.Object thisObject, string fieldName, IEnumerable enumerableObjectToCheck)
    {
        bool error = false;
        int count = 0;

        if(enumerableObjectToCheck == null)
        {
            Debug.Log(fieldName + " is null in object " + thisObject);
            error = true;
        }
        foreach(var item in enumerableObjectToCheck)
        {
            if(item == null)
            {
                Debug.Log(fieldName + " has null value in object " + thisObject);
                error = true;
            }
            else
            {
                count++;
            }
        }
        if(count == 0)
        {
            Debug.Log(fieldName + " has no values in object " + thisObject);
            error = true;
        }
        return error;
    }   
    // <summary>
    // empty string debug check
    // </summary>
    public static bool ValidateCheckEmptyString(System.Object thisObject, string fieldName, string stringToCheck)
    {
        bool error = false;

        if(stringToCheck == "")
        {
            Debug.Log(fieldName + "is empty and it must contain at least one character in object " + thisObject.ToString());
            error = true;
        }
        else
        {
            error = false;
        }
        return error;
    }
    // <summary>
    // Positive value debug check. - If zero is allowed , the valuetoCheck can be at least 0. However, if zero is not allowed then it must be bigger than 0.
    // </summary>
    public static bool ValidateCheckPositiveValue(System.Object thisObject, string fieldName, float valueToCheck, bool isZeroAllowed)
    {
        bool error = false;

        if (isZeroAllowed)
        {
            if (valueToCheck < 0)
            {
                Debug.Log(fieldName + "'s value is smaller than zero. It must be at least 0 in object " + thisObject.ToString());
                error = true;
            }
            else { error = false; }
        }
        else
        {
            if (valueToCheck <= 0)
            {
                Debug.Log(fieldName + "'s value is equal or smaller than zero. It must be bigger than 0 in object " + thisObject.ToString());
                error = true;
            }
            else { error = false; }
        }
        return error;
    }
    public static bool ValidateCheckPositiveVectorValues(System.Object thisObject, string fieldName, Vector3 valueToCheck, bool isZeroAllowed)
    {
        bool error = false;

        if (isZeroAllowed)
        {
            if (valueToCheck.x < 0 || valueToCheck.y < 0 || valueToCheck.z < 0)
            {
                Debug.Log(fieldName + "'s values are smaller than zero. It must be at least 0 in object " + thisObject.ToString());
                error = true;
            }
            else { error = false; }
        }
        else
        {
            if (valueToCheck.x < 0 || valueToCheck.y < 0 || valueToCheck.z < 0)
            {
                Debug.Log(fieldName + "'s values are equal or smaller than zero. It must be bigger than 0 in object " + thisObject.ToString());
                error = true;
            }
            else { error = false; }
        }
        return error;
    }
    public static bool ValidateCheckPositiveRange(System.Object thisObject, string minValueFieldName, float minValueToCheck, string maxValueFieldName, float maxValueToCheck)
    {
        bool error = false;

        if (minValueToCheck > maxValueToCheck)
        {
            Debug.Log(minValueToCheck + " must be less than or equal to the " + maxValueToCheck + " in object " + thisObject.ToString());
            error = true;
        }

        return error;

    }
    
    public static bool ValidateCheckRange(System.Object thisObject, string fieldName, float valueToCheck, float minValue, float maxValue)
    {
        bool error = false;

        if (valueToCheck < minValue || valueToCheck > maxValue)
        {
            Debug.Log(fieldName + "'s value is outside the valid range [" + minValue + ", " + maxValue + "] in object " + thisObject.ToString());
            error = true;
        }
        else
        {
            error = false;
        }
        return error;
    }
    public static bool ValidateCheckEnumValue<T>(System.Object thisObject, string fieldName, T enumValue) where T : struct
    {
        bool error = false;

        if (!Enum.IsDefined(typeof(T), enumValue))
        {
            Debug.Log(fieldName + " has an invalid enum value in object " + thisObject.ToString());
            error = true;
        }
        else
        {
            error = false;
        }
        return error;
    }
    public static bool ValidateCheckObjectType(System.Object thisObject, string fieldName, UnityEngine.Object objectToCheck, UnityEngine.Object expectedType)
    {
        bool error = false;

        if (objectToCheck == null || objectToCheck.GetType() != expectedType.GetType())
        {
            Debug.Log(fieldName + " is either null or not of the expected type in object " + thisObject.ToString());
            error = true;
        }
        else
        {
            error = false;
        }
        return error;
    }
    
}
