using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using randompasscodes.Models;
using Microsoft.AspNetCore.Http;

namespace randompasscodes.Controllers
{
    public class HomeController : Controller
    {   
        [HttpGet("")]
        public IActionResult Index()
        {
            Random rand = new Random();
            string alphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] stringChar = new char[16];
            for(int i = 0; i<16; i++){
                stringChar[i]=(alphaNumeric[rand.Next(0,36)]);
            }
            string randomCode = new String(stringChar);
            ViewBag.RandomStr = randomCode;
            if(HttpContext.Session.GetInt32("count") == null){
                HttpContext.Session.SetInt32("count", 0);
            }
            int? count = HttpContext.Session.GetInt32("count");
            count++;
            HttpContext.Session.SetInt32("count", (int) count);
               
            return View(count);
        }

        [HttpGet("/clear")]
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
