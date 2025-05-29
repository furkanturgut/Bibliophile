using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services
{
    public class ListLikeService : IListLikeService
    {
        private readonly IListLikeRepository _listLikeRepository;
        private readonly IBookListRepository _bookListRepository;
        private readonly UserManager<AppUser> _userManager;

        public ListLikeService(IListLikeRepository listLikeRepository, IBookListRepository bookListRepository, UserManager<AppUser> userManager)
        {
            _listLikeRepository = listLikeRepository;
            _bookListRepository = bookListRepository;
            this._userManager = userManager;
        }

        public async Task<int> GetListLikesCountAsync(int listId)
        {
            try
            {
                return await _listLikeRepository.GetListLikesCountAsync(listId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsListLikedByUserAsync(string userName, int listId)
        {
            try
            {
                return await _listLikeRepository.IsListLikedByUserAsync(userName, listId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ToggleListLikeAsync(string userName, int listId)
        {
            try
            {
                // Listenin var olup olmadığını kontrol et
                var user = await _userManager.FindByNameAsync(userName);
                var list = await _bookListRepository.GetBookListByIdAsync(listId) ?? throw new KeyNotFoundException($"ID {listId} ile liste bulunamadı");

                // Kullanıcının listeyi beğenip beğenmediğini kontrol et
                var existingLike = await _listLikeRepository.GetListLikeByUserAndListAsync(userName, listId);
                
                if (existingLike != null)
                {
                    // Beğeni varsa kaldır
                    await _listLikeRepository.RemoveListLikeAsync(existingLike);
                    return false; // Artık beğenilmiyor
                }
                else
                {
                    // Beğeni yoksa ekle
                    var newLike = new ListLike
                    {
                        ListId = listId,
                        UserId = user.Id
                    };
                    
                    await _listLikeRepository.AddListLikeAsync(newLike);
                    return true; // Şimdi beğeniliyor
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}