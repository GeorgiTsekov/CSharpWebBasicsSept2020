using Git.Data;
using Git.ViewModels.Commits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string id ,string userId, string repositoryId, string description)
        {
            var currRepo = this.db.Repositories.FirstOrDefault(r => r.Id == repositoryId);

            if (currRepo == null)
            {
                return;
            }

            var currUser = this.db.Users.FirstOrDefault(u => u.Id == userId);

            if (currUser == null)
            {
                return;
            }

            var commit = new Commit
            {
                CreatorId = currUser.Id,
                RepositoryId = currRepo.Id,
                Description = description,
                CreatedOn = DateTime.UtcNow,
            };
            this.db.Commits.Add(commit);
            this.db.SaveChanges();
        }

        public IEnumerable<CommitViewModel> GetAll()
        {
            return this.db.Commits
                //.Where(u => u.RepositoryId == repoId && u.CreatorId == userId)
                .Select(x => new CommitViewModel
            {
                Id = x.Id,
                Repository = x.Repository.Name,
                Description = x.Description,
                CreatedOn = x.CreatedOn,
            }).ToList();
        }

        public string GetNameById(string id)
        {
            return this.db.Repositories
                .Where(x => x.Id == id)
                .Select(y => y.Name)
                .FirstOrDefault();
        }
    }
}
