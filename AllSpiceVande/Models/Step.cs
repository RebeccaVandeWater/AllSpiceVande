using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Models;

public class Step
{
  public int Id { get; set; }

  public int? Number { get; set; }

  public string Instructions { get; set; }

  public int RecipeId { get; set; }

}
