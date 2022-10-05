using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private string description;

    public string GetHashName()
    {
        return description;
    }

}
