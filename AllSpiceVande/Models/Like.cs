using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Models;

public class Like
{
  public int Id { get; set; }
  public string AccountId { get; set; }
  public int CommentId { get; set; }
  public Profile Creator { get; set; }
}
