namespace ForumSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostsService
    {
        Task<int> CreateAsynk(string title, string content, int categoryId, string userId);

        T GetById<T>(int id);

        IEnumerable<T> GetByCategoryId<T>(int categoriId, int? take = null, int skip = 0);

        int GetCountByCategoryId(int categoryId);
    }
}
