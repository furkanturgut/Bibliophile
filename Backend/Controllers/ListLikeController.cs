using Backend.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/lists/{listId}/likes")]
    public class ListLikeController : ControllerBase
    {
        private readonly IListLikeService _listLikeService;

        public ListLikeController(IListLikeService listLikeService)
        {
            _listLikeService = listLikeService;
        }

        /// <summary>
        /// Bir listenin beğeni sayısını getirir
        /// </summary>
        /// <returns>Beğeni sayısı</returns>
        [HttpGet("count")]
        public async Task<IActionResult> GetListLikesCount(int listId)
        {
            try
            {
                var likesCount = await _listLikeService.GetListLikesCountAsync(listId);
                return Ok(new { Count = likesCount });
            }
            catch (Exception)
            {
                return StatusCode(500, "Beğeni sayısı getirilirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Giriş yapmış kullanıcının, listeyi beğenip beğenmediğini kontrol eder
        /// </summary>
        /// <returns>Beğeni durumu</returns>
        [HttpGet("is-liked")]
        public async Task<IActionResult> IsListLikedByUser(int listId)
        {
            try
            {
                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                var isLiked = await _listLikeService.IsListLikedByUserAsync(userId, listId);
                return Ok(new { IsLiked = isLiked });
            }
            catch (Exception)
            {
                return StatusCode(500, "Beğeni durumu kontrol edilirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Bir listeyi beğenir veya beğeniyi kaldırır
        /// </summary>
        /// <returns>Güncel beğeni durumu</returns>
        [HttpPost("toggle")]
        public async Task<IActionResult> ToggleListLike(int listId)
        {
            try
            {
                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                var isLiked = await _listLikeService.ToggleListLikeAsync(userId, listId);
                return Ok(new { IsLiked = isLiked });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Beğeni işlemi sırasında bir hata oluştu");
            }
        }
    }
}