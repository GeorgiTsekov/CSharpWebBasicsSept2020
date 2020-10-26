using Git.ViewModels.Commits;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface ICommitsService
    {
        void Create(string id, string userId, string repositoryId, string description);

        IEnumerable<CommitViewModel> GetAll();

        string GetNameById(string id);
    }
}
