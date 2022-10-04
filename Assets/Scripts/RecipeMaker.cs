using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeMaker : MonoBehaviour
{
    public List<Ingredient> recipeIngredients;

    private void Cook()
    {
        Recipe matchedRecipe = RecipeManager.instance.getMatchingRecipe(recipeIngredients);
        if (matchedRecipe == null)
        {
            Debug.Log("No recipe matched");
        }
        else
        {
            Debug.Log("What a cook ! You've made a " + matchedRecipe.getRecipeDescription());
        }
    }

    private void Start()
    {
        Cook();
    }
}
