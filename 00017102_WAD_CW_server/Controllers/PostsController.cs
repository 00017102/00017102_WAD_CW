using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _00017102_WAD_CW_server.Data;
using _00017102_WAD_CW_server.models;
using _00017102_WAD_CW_server.Repositories;
using _00017102_WAD_CW_server.DTOs;

namespace _00017102_WAD_CW_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IRepository<Post> _postRepository;
        private readonly GeneralDbContext _context;

        public PostsController(IRepository<Post> postRepository, GeneralDbContext context)
        {
            _postRepository = postRepository;
            _context = context;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var posts = await _postRepository.GetAllAsync();
                var response = posts.Select(p => new PostResponseDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    AuthorName = p.AuthorName,
                    CreatedDate = p.CreatedDate,
                    LastModifiedDate = p.LastModifiedDate,
                    CategoryName = p.Category.Name,
                });
                return Ok(response);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        // GET: api/posts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            try
            {
                var post = await _postRepository.GetByIdAsync(id);
                if (post != null)
                {
                    var comments = post.Comments.Select(c => new CommentResponseDTO
                    {
                        Id = c.Id,
                        AuthorName = c.AuthorName,
                        CreatedDate = c.CreatedDate,
                        Content = c.Content,
                    }).ToList();
                    var response = new PostWithCommentsResponseDTO
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Content = post.Content,
                        AuthorName = post.AuthorName,
                        CreatedDate = post.CreatedDate,
                        LastModifiedDate = post.LastModifiedDate,
                        CategoryName = post.Category.Name,
                        Comments = comments,
                    };
                    return Ok(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/posts
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostCreateDTO postDTO)
        {
            try
            {
                var post = new Post
                {
                    Title = postDTO.Title,
                    Content = postDTO.Content,
                    AuthorName = postDTO.AuthorName,
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    CategoryId = postDTO.CategoryId,
                };
                var result = await _postRepository.CreateAsync(post);
                if (result)
                {
                    return NoContent();
                }
                return BadRequest("Invalid CategoryId");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/posts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, PostCreateDTO postDTO)
        {
            Post post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return BadRequest();
            }
            try
            {
                post.Title = postDTO.Title;
                post.Content = postDTO.Content;
                post.AuthorName = postDTO.AuthorName;
                post.LastModifiedDate = DateTime.Now;
                post.CategoryId = postDTO.CategoryId;
                var result = await _postRepository.UpdateAsync(post);
                if (result)
                {
                    return NoContent();
                }
                return BadRequest("Invalid CategoryId");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE: api/posts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                bool result = await _postRepository.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
