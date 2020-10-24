using IRunes.Data;
using IRunes.ViewModels.Albums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.Services
{
    public class AlbumsService : IAlbumsService
    {
        private readonly ApplicationDbContext db;
        private const decimal StartingPrice = 0.0M;

        public AlbumsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(AddAllbumInputModel input)
        {
            var album = new Album
            {
                Name = input.Name,
                Cover = input.Cover,
                Price = StartingPrice,
            };
            this.db.Albums.Add(album);
            this.db.SaveChanges();
        }

        public AlbumDetailsViewModel GetDetails(string albumId)
        {
            var album = this.db.Albums
                .Where(a => a.Id == albumId)
                .Select(a => new AlbumDetailsViewModel
                {
                    Cover = a.Cover,
                    Name = a.Name,
                    Id = a.Id,
                    Price = a.Price,
                    Tracks = a.Tracks.Select(t => new TrackInfoViewModel 
                    {
                        Id = t.Id,
                        Name = t.Name,
                    })
                })
                .FirstOrDefault();
            return album;
        }

        public IEnumerable<AlbumViewModel> GetAll()
        {
            return this.db.Albums.Select(x => new AlbumViewModel
            {
                Name = x.Name,
                Id = x.Id,
            }).ToList();
        }
    }
}
