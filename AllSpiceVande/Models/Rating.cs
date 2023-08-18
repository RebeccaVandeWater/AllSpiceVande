using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Models;

public class Rating
{
  public int Id { get; set; }
  public string Score { get; set; }
  public string AccountId { get; set; }
  public int RecipeId { get; set; }
}
