using AlwaysForum.Api.Extensions;
using AlwaysForum.Api.Models.Dtos;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Repositories.CommentUpVotes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlwaysForum.Api.Controllers;

[ApiController] [Route("api/commentvote")]
public class CommentUpVoteController : ControllerBase
{
    private readonly ICommentVotesRepository _commentVotesRepository;

    public CommentUpVoteController(ICommentVotesRepository commentVotesRepository)
    {
        _commentVotesRepository = commentVotesRepository;
    }

    [HttpGet("{commentId:int}")]
    public async Task<int> GetVoteCount(int commentId)
    {
        return await _commentVotesRepository.GetVoteCount(commentId);
    }

    [Authorize]
    [HttpGet("isvoted/{commentId:int}")]
    public async Task<CommentVoteStatus> IsVoted(int commentId)
    {
        return await _commentVotesRepository.IsVotedByUser(commentId, User.GetId());
    }

    [Authorize]
    [HttpPost]
    public async Task Vote([FromBody] CommentVoteDto commentDto)
    {
        await _commentVotesRepository.VoteAsync(commentDto.CommentId, User.GetId(), commentDto.IsUpVote);
    }
}