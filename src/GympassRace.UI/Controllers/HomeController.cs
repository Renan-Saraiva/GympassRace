using GympassRace.UI.Models;
using GympassRace.Domain;
using GympassRace.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Text;
using System.Globalization;

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
                    Race race = new Race();
                    using (FileReader fileReader = new FileReader(file.OpenReadStream()))
                    {
                        fileReader.Open();

                        while (fileReader.ReadLine())
                            race.AddLap(fileReader.Line);
                    }

                    ProcessRace processor = new ProcessRace(race);
                    processor.Process();

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
