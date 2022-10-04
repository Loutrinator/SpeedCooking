using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private string name;

    public string GetHashName()
    {
        return name;
    }

}
