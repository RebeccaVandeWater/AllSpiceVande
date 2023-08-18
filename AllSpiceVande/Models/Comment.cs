using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Models;

public class Comment
{
  public int Id { get; set; }

  public string Title { get; set; }

  public string Content { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public int RecipeId { get; set; }

  public string CreatorId { get; set; }

  public Profile Creator { get; set; }
}
