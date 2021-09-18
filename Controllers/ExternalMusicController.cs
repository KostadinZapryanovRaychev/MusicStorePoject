using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Controllers
{
    public class ExternalMusicController: Controller
    {
        public ExternalMusicController()
        {

        }


        // nishtu ne razbiram tuka s teq RestFul API alata balata
        public async Task <IActionResult> Index()
        {

            using (var httpClient = new HttpClient())
            {

               var result = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/albums");

                var text = await result.Content.ReadAsStringAsync();

                // drugiq variant e da komentirame vtoriq red i napravo dan napravim gornata funkciq GetStringAsync
            }


            return View();
        }
    }
}
