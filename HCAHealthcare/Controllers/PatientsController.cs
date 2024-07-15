using HCAHealthcare.Models;
using HCAHealthcare.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Net.Http.Headers;

namespace HCAHealthcare.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public PatientsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            var patients = context.Patients.ToList();
            return View(patients);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(PatientDto patientDto)
        {
            if (patientDto.Image == null)
            {
                ModelState.AddModelError("Image", "Image file is required");
            }
            if (!ModelState.IsValid)
            {
                return View(patientDto);
            }

            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(patientDto.Image!.FileName); 
            string path = environment.WebRootPath + "/images/" + fileName;
            using (var file = System.IO.File.Create(path))
            {
                patientDto.Image.CopyTo(file);
            }
            Patient patient = new Patient()
            {
                Name = patientDto.Name,
                Age = patientDto.Age,
                Description = patientDto.Description,
                Due = patientDto.Due,
                Image = fileName,
            };

            context.Patients.Add(patient);
            context.SaveChanges();

            return RedirectToAction("Index", "Patients");

        }

        public IActionResult Edit(int id)
        {
            var patient = context.Patients.Find(id);
            if (patient == null)
            {
                return RedirectToAction("Index", "Patients");
            }
            var patientDto = new PatientDto()
            {
                Name = patient.Name,
                Age = patient.Age,
                Description = patient.Description,
                Due = patient.Due,

            };
            ViewData["id"] = patient.Id;
            ViewData["Image"] = patient.Image;


            return View(patientDto);
        }
        [HttpPost]
        public IActionResult Edit(int id, PatientDto patientDto)
        {
            var patient = context.Patients.Find(id);
            if (patient == null)
            {
                return RedirectToAction("Index", "Patients");
            }

            if (!ModelState.IsValid)
            {
                ViewData["id"] = patient.Id;
                ViewData["Image"] = patient.Image;
                return View(patientDto);
            }

            string fileName = patient.Image;

            if (patientDto.Image != null)
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(patientDto.Image!.FileName);
                string path = environment.WebRootPath + "/images/" + fileName;
                using (var file = System.IO.File.Create(path))
                {
                    patientDto.Image.CopyTo(file);
                }

                string oldfilename= environment.WebRootPath + "/images/" + patient.Image;
                System.IO.File.Delete(oldfilename);

            }

            patient.Name = patientDto.Name;
            patient.Age = patientDto.Age;
            patient.Due = patientDto.Due;
            patient.Description = patientDto.Description;
            patient.Image = fileName;

            context.SaveChanges();

            return RedirectToAction("Index", "Patients");

        }
        public IActionResult Delete(int id)
        {
            var patient = context.Patients.Find(id);
            if (patient == null)
            {

                return RedirectToAction("Index", "Patients");
            }
            string path = environment.WebRootPath + "/images/" + patient.Image;
            System.IO.File.Delete(path);
            context.Patients.Remove(patient);
            context.SaveChanges(true);
            return RedirectToAction("Index", "Patients");

        }
    }
}
