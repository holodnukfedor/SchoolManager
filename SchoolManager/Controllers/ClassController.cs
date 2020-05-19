using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SchoolManager.Models;
using SchoolManagerBLL;
using SchoolManagerDAL;

namespace SchoolManager.Controllers
{
    public class ClassController : Controller
    {
        private IMapper _mapper;
        private IClassService _classService;

        public ClassController(IMapper mapper, IClassService classService)
        {
            _mapper = mapper;
            _classService = classService;
        }

        public IActionResult Search(int schoolId, string q)
        {
            const int classesCount = 10;
            IdNameTuple[] schoolClasses =_classService.GetSchoolClassesByName(schoolId, q, classesCount);
            OptionViewModel[] options =_mapper.Map<OptionViewModel[]>(schoolClasses);
            return Json(options);
        }
    }
}