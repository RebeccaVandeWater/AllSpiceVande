using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Services;

public class CommentsService
{
  private readonly CommentsRepository _commentsRepository;

  public CommentsService(CommentsRepository commentsRepository)
  {
    _commentsRepository = commentsRepository;
  }

  internal Comment CreateComment(Comment commentData)
  {
    int commentId = _commentsRepository.CreateComment(commentData);

    Comment comment = GetCommentById(commentId);

    return comment;
  }

  internal Comment EditComment(Comment commentData, string userId, int commentId)
  {
    Comment originalComment = GetCommentById(commentId);

    if (originalComment.CreatorId != userId)
    {
      throw new Exception($"You are not the owner of the comment titled {originalComment.Title}. You may not edit it.");
    }

    originalComment.Title = commentData.Title ?? originalComment.Title;
    originalComment.Content = commentData.Content ?? originalComment.Content;

    Comment editedComment = _commentsRepository.EditComment(originalComment);

    Comment comment = GetCommentById(editedComment.Id);

    return comment;
  }

  internal Comment GetCommentById(int commentId)
  {
    Comment comment = _commentsRepository.GetCommentById(commentId);

    if (comment == null)
    {
      throw new Exception($"INVALID ID {commentId}");
    }

    return comment;
  }

  internal List<Comment> GetCommentsByRecipeId(int recipeId)
  {
    List<Comment> comments = _commentsRepository.GetCommentsByRecipeId(recipeId);

    return comments;
  }

  internal void RemoveComment(int commentId, string userId)
  {
    Comment commentToRemove = GetCommentById(commentId);

    if (commentToRemove.CreatorId != userId)
    {
      throw new Exception($"You are not the creator of the comment titled: {commentToRemove.Title}. You may not delete it.");
    }

    _commentsRepository.RemoveComment(commentId);
  }
}
