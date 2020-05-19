using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using SchoolManager.Models;
using SchoolManagerBLL;
using SchoolManagerDAL;


namespace SchoolManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMapper _mapper;
        private IStudentService _studentService;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, IStudentService studentService)
        {
            _logger = logger;
            _mapper = mapper;
            _studentService = studentService;
        }

        public IActionResult Index(PaginationParameters paginationParameters)
        {
            StudentWithSchoolList students =_studentService.GetStudents(paginationParameters);
            StudentViewModelList studentsViewModel = _mapper.Map<StudentWithSchoolList, StudentViewModelList>(students);
            return View(studentsViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
