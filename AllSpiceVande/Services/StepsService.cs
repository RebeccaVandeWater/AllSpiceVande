using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Services;

public class StepsService
{
  private readonly StepsRepository _stepsRepository;
  private readonly RecipesService _recipesService;

  public StepsService(StepsRepository stepsRepository, RecipesService recipesService)
  {
    _stepsRepository = stepsRepository;
    _recipesService = recipesService;
  }

  internal Step GetStepById(int stepId)
  {
    Step step = _stepsRepository.GetStepById(stepId);

    if (step == null)
    {
      throw new Exception($"INVALID ID: {stepId}");
    }

    return step;
  }
  internal Step CreateStep(Step stepData, string userId)
  {
    Recipe recipe = _recipesService.GetRecipeById(stepData.RecipeId);

    if (recipe.CreatorId != userId)
    {
      throw new Exception($"You are not the creator of {recipe.Title}. You may not add a step to it.");
    }

    int stepId = _stepsRepository.CreateStep(stepData);

    Step step = GetStepById(stepId);

    return step;
  }

  internal Step EditStep(Step stepData, int stepId, string userId)
  {
    Step originalStep = GetStepById(stepId);

    Recipe recipe = _recipesService.GetRecipeById(originalStep.RecipeId);

    if (recipe.CreatorId != userId)
    {
      throw new Exception($"You are not the creator of {recipe.Title}. You may not edit steps on it.");
    }

    originalStep.Number = stepData.Number ?? originalStep.Number;
    originalStep.Instructions = stepData.Instructions ?? originalStep.Instructions;

    Step step = _stepsRepository.EditStep(originalStep);

    return step;
  }

  internal void RemoveStep(int stepId)
  {
    throw new NotImplementedException();
  }
}
