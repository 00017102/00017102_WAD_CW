using _00017102_WAD_CW_server.Data;
using _00017102_WAD_CW_server.DTOs;
using _00017102_WAD_CW_server.models;
using _00017102_WAD_CW_server.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;

namespace _00017102_WAD_CW_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly GeneralDbContext _context;

        public CategoriesController(ICategoryRepository categoryRepository, GeneralDbContext context)
        {
            _categoryRepository = categoryRepository;
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                var response = categories.Select(c => new CategoryResponseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    
                });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // GET: api/categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                var response = new CategoryResponseDTO { Id = category.Id, Name = category.Name};
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET: api/categories/filter/{id}
        [HttpGet("filter/{id}")]
        public async Task<IActionResult> GetCategoryWithPosts(int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryWithPostsAsync(id);
                if (category != null)
                {
                    var posts = category.Posts.Select(p => new PostResponseDTO
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Content = p.Content,
                        AuthorName = p.AuthorName,
                        CreatedDate = p.CreatedDate,
                        LastModifiedDate = p.LastModifiedDate,
                        CategoryName = p.Category.Name,
                    }).ToList();
                    var response = new CategoryWithPostsResponseDTO { Id = category.Id, Name = category.Name, Posts = posts };
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

        // POST: api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateDTO categoryDTO)
        {
            try
            {
                var category = new Category
                {
                    Name = categoryDTO.Name,
                };
                var result = await _categoryRepository.CreateAsync(category);
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

        // PUT: api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryCreateDTO categoryDTO)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return BadRequest();
            }
            try
            {
                category.Name = categoryDTO.Name;
                var result = await _categoryRepository.UpdateAsync(category);

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

        // DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                bool result = await _categoryRepository.DeleteAsync(id);
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
