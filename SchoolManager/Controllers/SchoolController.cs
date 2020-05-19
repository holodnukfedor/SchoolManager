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
    public class SchoolController : Controller
    {
        private IMapper _mapper;
        private ISchoolService _schoolService;

        public SchoolController(IMapper mapper, ISchoolService schoolService)
        {
            _mapper = mapper;
            _schoolService = schoolService;
        }
        public IActionResult Search(string q)
        {
            const int classesCount = 10;
            IdNameTuple[] schoolClasses = _schoolService.GetSchoolsByNumber(q, classesCount);
            OptionViewModel[] options = _mapper.Map<OptionViewModel[]>(schoolClasses);
            return Json(options);
        }

    }
}