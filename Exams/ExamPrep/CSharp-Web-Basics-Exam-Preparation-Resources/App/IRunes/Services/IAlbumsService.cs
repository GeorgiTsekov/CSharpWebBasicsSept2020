using IRunes.ViewModels.Albums;
using System.Collections.Generic;

namespace IRunes.Services
{
    public interface IAlbumsService
    {
        void Create(AddAllbumInputModel input);

        IEnumerable<AlbumViewModel> GetAll();

        AlbumDetailsViewModel GetDetails(string albumId);
    }
}
