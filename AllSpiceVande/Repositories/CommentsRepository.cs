using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Repositories;

public class CommentsRepository
{
  private readonly IDbConnection _db;

  public CommentsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateComment(Comment commentData)
  {
    string sql = @" 
        INSERT INTO comments(title, content, recipeId, creatorId)
        VALUES(@Title, @Content, @RecipeId, @CreatorId);
        SELECT LAST_INSERT_ID()
        ;";

    int commentId = _db.ExecuteScalar<int>(sql, commentData);

    return commentId;
  }

  internal Comment EditComment(Comment originalComment)
  {
    string sql = @"
        UPDATE comments
        SET
        title = @Title,
        content = @Content
        WHERE id = @Id;
        SELECT * from comments WHERE id = @Id
        ;";

    Comment comment = _db.QueryFirstOrDefault<Comment>(sql, originalComment);

    return comment;
  }

  internal Comment GetCommentById(int commentId)
  {
    string sql = @"
        SELECT
        comm.*,
        acc.*
        FROM comments comm
        JOIN accounts acc ON acc.id = comm.creatorId
        WHERE comm.id = @commentId
        ;";

    Comment comment = _db.Query<Comment, Profile, Comment>(
      sql,
      (comment, profile) =>
      {
        comment.Creator = profile;
        return comment;
      },
      new { commentId }
    ).FirstOrDefault();

    return comment;
  }

  internal List<Comment> GetCommentsByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT
    comm.*,
    acc.*
    FROM comments comm
    JOIN accounts acc ON acc.id = comm.creatorId
    WHERE comm.recipeId = @recipeId
    ;";

    List<Comment> comments = _db.Query<Comment, Profile, Comment>
    (
      sql,
      (comment, profile) =>
      {
        comment.Creator = profile;
        return comment;
      },
      new { recipeId }
    ).ToList();

    return comments;
  }

  internal void RemoveComment(int commentId)
  {
    string sql = "DELETE FROM comments WHERE id = @commentId LIMIT 1;";

    _db.Execute(sql, new { commentId });
  }
}
