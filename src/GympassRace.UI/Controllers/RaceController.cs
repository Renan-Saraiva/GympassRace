using GympassRace.Domain;
using GympassRace.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GympassRace.UI.Controllers
{
    public class RaceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Process(IFormFile file)
        {
            Race race = null;
            if (file != null && file.Length > 0)
            {
                if (System.IO.Path.GetExtension(file.FileName).Equals(".txt") || System.IO.Path.GetExtension(file.FileName).Equals(".log"))
                {
                    try
                    {
                        race = new Race();

                        using (FileReader fileReader = new FileReader(file.OpenReadStream()))
                        {
                           await fileReader.OpenAsync();

                            while (await fileReader.ReadLineAsync())
                                race.AddLap(fileReader.Line);
                        }

                        ProcessRace processor = new ProcessRace(race);
                        processor.Process();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                    }
                }
                else
                    ViewBag.ErrorMessage = "Extensão de arquivo invalida";
            }
            else
                ViewBag.ErrorMessage = "Arquivo vazio ou invalido";

            return View(race);
        }
    }
}
