using Git.Data;
using Git.ViewModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public void Create(string name, string repositoryType, string userId)
        {
            var ownerName = this.db.Users.Where(x => x.Id == userId).Select(y => y.Username).FirstOrDefault();
            var owner = this.db.Users.Where(x => x.Id == userId).FirstOrDefault();

            var repository = new Repository
            {
                Name = name,
                IsPublic = repositoryType == "Public" ? true : false,
                CreatedOn = DateTime.UtcNow,
                OwnerId = owner.Id,
                Commits = new HashSet<Commit>(),
            };
            this.db.Repositories.Add(repository);
            this.db.SaveChanges();
        }

        public IEnumerable<RepositoryViewModel> All()
        {
            return this.db.Repositories
                .Where(x => x.IsPublic)
                .Select(x => new RepositoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Owner = x.Owner.Username,
                CreatedOn = x.CreatedOn,
                Commits = x.Commits.Count(),
            }).ToList();
        }
    }
}
