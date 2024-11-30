using _00017102_WAD_CW_server.Data;
using _00017102_WAD_CW_server.DTOs;
using _00017102_WAD_CW_server.models;
using _00017102_WAD_CW_server.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace _00017102_WAD_CW_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IRepository<Comment> _commentsRepository;
        private readonly GeneralDbContext _context;

        public CommentsController(IRepository<Comment> commentsRepository, GeneralDbContext context)
        {
            _commentsRepository = commentsRepository;
            _context = context;
        }

        // GET: api/comments
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            try
            {
                var comments = await _commentsRepository.GetAllAsync();
                var response = comments.Select(c => new CommentResponseDTO
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreatedDate = c.CreatedDate,
                    AuthorName = c.AuthorName,
                });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/comments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            try
            {
                var comment = await _commentsRepository.GetByIdAsync(id);
                if (comment == null)
                {
                    return NotFound();
                }
                var response = new CommentResponseDTO 
                { 
                    Id = comment.Id,
                    Content = comment.Content,
                    CreatedDate = comment.CreatedDate,
                    AuthorName = comment.AuthorName,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/comments
        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentCreateDTO commentDto)
        {
            try
            {
                var comment = new Comment
                {
                    AuthorName = commentDto.AuthorName,
                    Content = commentDto.Content,
                    PostId = commentDto.PostId,
                    CreatedDate = DateTime.Now,
                };
                var result = await _commentsRepository.CreateAsync(comment);
                if (result)
                {
                    return NoContent();
                }
                return BadRequest("Invalid PostId");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
