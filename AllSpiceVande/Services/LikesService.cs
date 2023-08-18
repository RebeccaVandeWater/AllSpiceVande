using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Services;

public class LikesService
{
  private readonly LikesRepository _likesRepository;

  public LikesService(LikesRepository likesRepository)
  {
    _likesRepository = likesRepository;
  }

  internal Like CreateLike(Like likeData)
  {
    List<Like> commentLikes = GetCommentLikes(likeData.CommentId);

    Like userLike = commentLikes.Find(l => l.AccountId == likeData.AccountId);

    if (userLike != null)
    {
      throw new Exception("You have already liked this comment. You cannot like a comment twice.");
    }

    int likeId = _likesRepository.CreateLike(likeData);

    Like like = GetLikeById(likeId);

    return like;
  }

  internal List<Like> GetCommentLikes(int commentId)
  {
    List<Like> likes = _likesRepository.GetCommentLikes(commentId);

    return likes;
  }

  internal Like GetLikeById(int likeId)
  {
    Like like = _likesRepository.GetLikeById(likeId);

    if (like == null)
    {
      throw new Exception($"INVALID ID: {likeId}");
    }

    return like;
  }

  internal void RemoveLike(int likeId, string userId)
  {
    Like likeToRemove = GetLikeById(likeId);

    if (likeToRemove.AccountId != userId)
    {
      throw new Exception("You are not the creator of this like. You may not remove it");
    }

    _likesRepository.RemoveLike(likeId);
  }
}
