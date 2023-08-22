using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Repositories;

public class StepsRepository
{
  private readonly IDbConnection _db;

  public StepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateStep(Step stepData)
  {
    string sql = @"
        INSERT INTO steps
        (number, instructions, recipeId)
        VALUES
        (@Number, @Instructions, @RecipeId);
        SELECT LAST_INSERT_ID()
        ;";

    int stepId = _db.ExecuteScalar<int>(sql, stepData);

    return stepId;
  }

  internal Step EditStep(Step originalStep)
  {
    string sql = @"
        UPDATE steps
        SET
        number = @Number,
        instructions = @Instructions
        WHERE id = @Id;
        SELECT * FROM steps WHERE id = @Id
        ;";

    Step step = _db.QueryFirstOrDefault<Step>(sql, originalStep);

    return step;
  }

  internal Step GetStepById(int stepId)
  {
    string sql = "SELECT * FROM steps WHERE id = @stepId";

    Step step = _db.QueryFirstOrDefault<Step>(sql, new { stepId });

    return step;
  }
}
