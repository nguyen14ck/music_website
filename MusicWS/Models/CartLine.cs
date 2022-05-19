using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicWS.Models
{
    public class CartLine
    {
        public Album Album { get; set; }
        public int SoLuong { get; set; }
    }

    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void Add(Album album, int soluong)
        {
            CartLine line = lineCollection.FirstOrDefault(p => p.Album.AlbumId == album.AlbumId);

            if (line == null)
            {
                lineCollection.Add(new CartLine { Album = album, SoLuong = soluong });
            }
            else
            {
                line.SoLuong += soluong;
            }
        }

        public void Update(int albumid, int soluong)
        {
            CartLine line = lineCollection.FirstOrDefault(p => p.Album.AlbumId == albumid);
            if (line != null)
            {
                line.SoLuong = soluong;
            }
        }

        public void Remove(int albumId)
        {
            lineCollection.RemoveAll(e => e.Album.AlbumId == albumId);
        }

        public decimal ComputeTotal()
        {
            return lineCollection.Sum(e => e.Album.GiaBan * e.SoLuong);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }
}