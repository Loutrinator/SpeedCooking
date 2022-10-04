using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{ 
    private static RecipeManager _recipeManager;
    [SerializeField] List<Recipe> recipes;
    [SerializeField] private List<Ingredient> ingredients;

    public static RecipeManager instance => _recipeManager;
    private void Awake()
    {
        if (_recipeManager == null)
        {
            _recipeManager = this;
        }
        else
        {
            Debug.LogError("There are two RecipeManagers. Delete one");
        }
    }

    public Recipe getMatchingRecipe(List<Ingredient> meal)
    {
        List<string> ingredientNames = GetIngredientHashNames(meal);
        
        foreach (var recipe in recipes)
        {
            List<string> recipeIngredients = recipe.GetRecipeHashNames();
            int matchedIngredients = 0;
            foreach (var ingredientName in ingredientNames)
            {
                if (ingredientName == recipeIngredients[matchedIngredients])
                {
                    matchedIngredients++;
                }
            }

            if (matchedIngredients >= recipeIngredients.Count)
            {
                return recipe;
            }
        }
        
        return null;
    }

    public static List<string> GetIngredientHashNames(List<Ingredient> meal)
    {
        List<string> ingredientNames = new List<string>();
        for (int i = 0; i < meal.Count; i++)
        {
            ingredientNames.Add (meal[i].GetHashName());
        }
        ingredientNames.Sort();
        return ingredientNames;
    }
}
