using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02_apis.Db;
using _02_apis.Dtos;
using _02_apis.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _02_apis.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly DbContextProvider _dbContext;

        public CommentController(ILogger<CommentController> logger, DbContextProvider dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _dbContext.Comments
            .Include(c => c.User)
            .ToListAsync();
            if (comments == null || comments.Count == 0)
            {
                _logger.LogWarning("No comments found in the database.");
                return NotFound("No comments found");
            }
            _logger.LogInformation($"Retrieved {comments.Count} comments from the database.");
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentDto newComment)
        {
            var comment = new Comment
            {
                UserId = newComment.UserId,
                Content = newComment.Content
            };

            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllComments), new { id = comment.Id }, comment);
        }

        [HttpGet("/comment/{userId}")]
        public async Task<IActionResult> GetCommentsByUserId([FromRoute] int userId, [FromQuery] int commentId)
        {
            var user = await _dbContext.Users
            .Include(u => u.Comments)
            .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                _logger.LogWarning($"User with ID = {userId} not found.");
                return NotFound($"User with ID = {userId} not found");
            }

            var comment = user.Comments?.Find(c => c.Id == commentId);
            
            if (comment == null)
            {
                _logger.LogWarning($"Comment with ID = {commentId} for User ID = {userId} not found.");
                return NotFound($"Comment with ID = {commentId} for User ID = {userId} not found");
            }
            _logger.LogInformation($"Retrieved comment with ID = {commentId} for User ID = {userId} from the database.");
            return Ok(comment);
        }
    }
}