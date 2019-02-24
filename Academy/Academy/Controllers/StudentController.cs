using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academy.Context;
using Academy.Model;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        private ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {

            _context = context;

            //Add at least one data//Refactore it and move to seed data
            if (!_context.Students.Any())
            {
                _context.Students.Add(new Student
                {
                    Id = 1,
                    RollNo = "C1901",
                    FullName = "Md. Mahedee Hasan",
                    FatherName = "Easin Bhuiyan",
                    MotherName = "Morium Begum",
                    BloodGroup = "A+"
                });
                _context.SaveChanges();
            }
        }


        [HttpGet]
        [Route("GetStudents")]
        public IEnumerable<Student> GetStudents()
        {
            return _context.Students;
        }



        [HttpPost]
        [Route("AddStudent")]
        public IActionResult AddStudent(Student student)
        {
            if (student == null)
                return BadRequest();

            _context.Students.Add(student);
            _context.SaveChanges();

            return RedirectToAction("GetStudents");
            //return CreatedAtRoute("GetWorkshops", new { id = workshop.Id }, workshop);
        }


        //api/Student/EditStudent?id=2
        [HttpPut] // means that this id will come from route  
        [Route("EditStudent")]
        public IActionResult UpdateStudentByID(int id, [FromBody] Student vStudent)
        {

            if (vStudent == null || vStudent.Id != id)
                return BadRequest();

            var student = _context.Students.FirstOrDefault(i => i.Id == id);
            if (student == null)
                return NotFound();

            student.FullName = vStudent.FullName;
            student.BloodGroup = vStudent.BloodGroup;
 
            _context.Students.Update(student);
            _context.SaveChanges();
            return new NoContentResult();
        }


        //api/Student/DeleteStudent?id=2
        [HttpDelete]
        [Route("DeleteStudent")]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(i => i.Id == id);
            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();

            return RedirectToAction("GetStudents");
        }


    }
}