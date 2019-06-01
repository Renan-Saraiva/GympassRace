using GympassRace.UI.Models;
using GympassRace.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Text;

namespace GympassRace.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessRaceFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    using (FileReader fileReader = new FileReader(file.OpenReadStream(), "\t", "\t"))
                    {
                        fileReader.Open();

                        StringBuilder builder = new StringBuilder();

                        while (fileReader.ReadLine())
                        {
                            builder.AppendLine(fileReader.Line.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
