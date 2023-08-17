using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Models;

public class Ingredient
{
  public int Id { get; set; }
  public string Name { get; set; }
  public int Quantity { get; set; }
  public int RecipeId { get; set; }

}
