using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Interface;
using Backend.Models;

namespace Backend.Services
{
    public class ListLikeService : IListLikeService
    {
        private readonly IListLikeRepository _listLikeRepository;
        private readonly IBookListRepository _bookListRepository;

        public ListLikeService(IListLikeRepository listLikeRepository, IBookListRepository bookListRepository)
        {
            _listLikeRepository = listLikeRepository;
            _bookListRepository = bookListRepository;
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

        public async Task<bool> IsListLikedByUserAsync(string userId, int listId)
        {
            try
            {
                return await _listLikeRepository.IsListLikedByUserAsync(userId, listId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ToggleListLikeAsync(string userId, int listId)
        {
            try
            {
                // Listenin var olup olmadığını kontrol et
                var list = await _bookListRepository.GetBookListByIdAsync(listId) ?? throw new KeyNotFoundException($"ID {listId} ile liste bulunamadı");

                // Kullanıcının listeyi beğenip beğenmediğini kontrol et
                var existingLike = await _listLikeRepository.GetListLikeByUserAndListAsync(userId, listId);
                
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
                        UserId = userId
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