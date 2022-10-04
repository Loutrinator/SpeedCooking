using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 0)]
public class Recipe : ScriptableObject
{
    [SerializeField] private string description;
    [SerializeField] private List<string> mainIngredients;

    public List<string> GetRecipeHashNames()
    {
        mainIngredients.Sort();
        return mainIngredients;
    }

    public string getRecipeDescription()
    {
        return description;
    }
}
