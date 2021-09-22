using Microsoft.AspNetCore.Mvc;
using MvcMusicStoreWebProject.Data;
using MvcMusicStoreWebProject.Models;
using MvcMusicStoreWebProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Controllers
{
    public class MusicStoreController : Controller
    {
        public MusicStoreController(IRepository repo)
        {
            Repo = repo;
        }

        public IRepository Repo { get; }

        public IActionResult Index()
        {
            IEnumerable<Album> albums = Repo.GetAllAblums();
            return View(albums);
        }

        [HttpGet]
        public async Task <IActionResult> Edit(int AlbumID)
        {
            var album = await Repo.GetAlbumById(AlbumID);

            var viewModel = new AlbumViewModel { Album = album };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AlbumViewModel modifiedAlbum)
        {
            if (ModelState.IsValid)
            {
                var album = modifiedAlbum.Album;
                await Repo.UpdateAlbum(album);
            }
            return View(modifiedAlbum);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int AlbumID)
        {
            var deleteMessage = await Repo.DeleteAlbum(AlbumID);

            return Content(deleteMessage);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new AlbumViewModel();
            return View(viewModel);
        }


        [HttpPost]
        public async Task <IActionResult> Create(AlbumViewModel viewModel)
        {

                if (ModelState.IsValid)
                {
                    // save i DB
                    await Repo.InsertNewAlbum(viewModel.Album);
                    await Repo.Save();

                    return RedirectToAction("Create");
                }
                return View(viewModel);
        }

    }
}
