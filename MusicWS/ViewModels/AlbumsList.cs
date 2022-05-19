using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicWS.Models;

namespace MusicWS.ViewModels
{
    public class AlbumsList
    {
        public IEnumerable<Album> Albums { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}