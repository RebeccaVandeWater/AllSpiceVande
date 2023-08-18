using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Repositories;

public class LikesRepository
{
  private readonly IDbConnection _db;

  public LikesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateLike(Like likeData)
  {
    string sql = @"
    INSERT INTO likes(accountId, commentId)
    VALUES(@AccountId, @CommentId);
    SELECT LAST_INSERT_ID()
    ;";

    int likeId = _db.ExecuteScalar<int>(sql, likeData);

    return likeId;
  }

  internal List<Like> GetCommentLikes(int commentId)
  {
    string sql = @"
    SELECT 
    lik.*,
    acc.* 
    FROM likes lik
    JOIN accounts acc ON acc.id = lik.accountId
    WHERE lik.commentId = @commentId
    ;";

    List<Like> likes = _db.Query<Like, Profile, Like>
    (sql,
    (like, profile) =>
    {
      like.Creator = profile;
      return like;
    },
    new { commentId }).ToList();

    return likes;
  }

  internal Like GetLikeById(int likeId)
  {
    string sql = @"
    SELECT 
    lik.*,
    acc.* 
    FROM likes lik
    JOIN accounts acc ON acc.id = lik.accountId
    WHERE lik.id = @likeId
    ;";

    Like like = _db.Query<Like, Profile, Like>
    (sql,
    (like, profile) =>
    {
      like.Creator = profile;
      return like;
    },
    new { likeId }
    ).FirstOrDefault();

    return like;
  }

  internal void RemoveLike(int likeId)
  {
    string sql = "DELETE FROM likes WHERE id = @likeId LIMIT 1;";

    _db.Execute(sql, new { likeId });
  }
}
