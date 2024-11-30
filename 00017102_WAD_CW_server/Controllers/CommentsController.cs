using _00017102_WAD_CW_server.Data;
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
                return Ok(comments);
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
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/comments
        [HttpPost]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            try
            {
                await _commentsRepository.CreateAsync(comment);
                return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
