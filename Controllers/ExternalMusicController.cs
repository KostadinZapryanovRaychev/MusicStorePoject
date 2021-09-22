using Microsoft.AspNetCore.Mvc;
using MvcMusicStoreWebProject.Data;
using MvcMusicStoreWebProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcMusicStoreWebProject.Controllers
{
    public class ExternalMusicController: Controller
    {
        // kak taka Repositoryto raboti s bazata danni a ne tova izobshto ne razbiram kva e logikata iskam posledovatelnostta
        public ExternalMusicController(IHttpClientFactory httpClientFactory , IRepository repo)
        {
            HttpClientFactory = httpClientFactory;
            Repo = repo;
        }

        public IHttpClientFactory HttpClientFactory { get; }
        public IRepository Repo { get; }


        // nishtu ne razbiram tuka s teq RestFul API alata balata
        public async Task <IActionResult> Index()
        {
            //tuk veche otkakto go opravibme po toq nachin izobshto ne go chatkam
            var client = HttpClientFactory.CreateClient();
           
               var result = await client.GetStringAsync("https://jsonplaceholder.typicode.com/albums");

                //var text = await result.Content.ReadAsStringAsync();

                // drugiq variant e da komentirame vtoriq red i napravo dan napravim gornata funkciq GetStringAsync

               var deserialized = JsonConvert.DeserializeObject<List<ApiModel>>(result);

            foreach( var album in deserialized)
            {
                if (Repo.IsDuplicateTitle(album.Title))
                {
                    album.IsDuplicated = true;
                }
            }
     
            return View(deserialized);
        }

        [HttpPost]
        public async Task <IActionResult> AjaxTest(string title ,decimal price , string genre)
        {
            // entity si genirira samo ID ta ne e nujno da sa zanimavame s tva daj e nejelatelno
            var newAlbum = new Album { Title = title, Price = price, Genre = genre, ReleaseDate = DateTime.Now.AddMonths(6) };
            var result = await Repo.InsertNewAlbum(newAlbum);
            return Content(result); 
        }
    }
}
