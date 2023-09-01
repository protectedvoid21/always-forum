using AlwaysForum.Api.Extensions;
using AlwaysForum.Api.Models.Dtos;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Services.CommentUpVotes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlwaysForum.Api.Controllers;

[ApiController] [Route("api/commentvote")]
public class CommentUpVoteController : ControllerBase
{
    private readonly ICommentVotesService _commentVotesService;

    public CommentUpVoteController(ICommentVotesService commentVotesService)
    {
        _commentVotesService = commentVotesService;
    }

    [HttpGet("{commentId:int}")]
    public async Task<int> GetVoteCount(int commentId)
    {
        return await _commentVotesService.GetVoteCount(commentId);
    }

    [Authorize]
    [HttpGet("isvoted/{commentId:int}")]
    public async Task<CommentVoteStatus> IsVoted(int commentId)
    {
        return await _commentVotesService.IsVotedByUser(commentId, User.GetId());
    }

    [Authorize]
    [HttpPost]
    public async Task Vote([FromBody] CommentVoteDto commentDto)
    {
        await _commentVotesService.VoteAsync(commentDto.CommentId, User.GetId(), commentDto.IsUpVote);
    }
}