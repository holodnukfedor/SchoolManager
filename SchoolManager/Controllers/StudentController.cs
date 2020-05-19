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
    public class StudentController : Controller
    {
        private IMapper _mapper;
        private IStudentService _studentService;

        public StudentController(IMapper mapper, IStudentService studentService)
        {
            _mapper = mapper;
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Edit(int id, PaginationParameters paginationParameters)
        {
            StudentWithSchool student = _studentService.GetStudent(id);
            StudentViewModel studentViewModel = _mapper.Map<StudentWithSchool, StudentViewModel>(student);
            EditStudentViewModel editStudentViewModel = new EditStudentViewModel(studentViewModel, paginationParameters);
            return View(editStudentViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditStudentViewModel student)
        {
            if (!ModelState.IsValid)
                return View(student);

            _studentService.Edit(_mapper.Map<StudentDTO>(student.StudentViewModel));
            return RedirectToAction("Index", "Home", new { PageNumber = student.PaginationParameters.PageNumber, PageSize = student.PaginationParameters.PageSize });
        }
    }
}
