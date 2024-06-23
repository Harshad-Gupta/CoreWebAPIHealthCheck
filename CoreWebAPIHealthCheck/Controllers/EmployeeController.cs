using CoreWebAPIHealthCheck.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPIHealthCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        static List<Employee> _employeeList = new List<Employee>()
        {
            new Employee{Id = 1, Name = "Harshad Gupta", Gender = "Male", Email = "harshad@gmail.com", MobileNo = "9786524101"},
            new Employee{Id = 2, Name = "Gauresh Gawade", Gender = "Male", Email = "gauresh@gmail.com", MobileNo = "7893526172"},
            new Employee{Id = 3, Name = "Sayali Dhamane", Gender = "Female", Email = "sayali@gmail.com", MobileNo = "8935201837"},
            new Employee{Id = 4, Name = "Sagar Joshi", Gender = "Male", Email = "sagar@gmail.com", MobileNo = "7838928361"},
            new Employee{Id = 5, Name = "Prajakta Shelar", Gender = "Female", Email = "prajakta@gmail.com", MobileNo = "9526172637"}
        };

        [HttpGet]
        [Route("GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            return Ok(_employeeList);
        }

        [HttpGet]
        [Route("GetEmployeeById/{EmpId}")]
        public IActionResult GetEmployeeById(int EmpId)
        {
            var employee = _employeeList.FirstOrDefault(e => e.Id == EmpId);

            if(employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        [Route("AddNewEmployee")]
        public IActionResult AddNewEmployee(Employee employee)
        {
            _employeeList.Add(employee);

            return Ok(employee);
        }

        [HttpPut]
        [Route("UpdateEmployee/{Id}")]
        public IActionResult UpdateEmployee(int Id, Employee employee)
        {
            if(Id != employee.Id)
            {
                return BadRequest();
            }

            var existingEmployee = _employeeList.FirstOrDefault(e => e.Id == Id);
            if(existingEmployee == null)
            {
                return NotFound(employee);
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Email = employee.Email;
            existingEmployee.Gender = employee.Gender;
            existingEmployee.MobileNo = employee.MobileNo;

            return Ok("Employee Details Updated.");
        }

        [HttpDelete]
        [Route("DeleteEmployeeById/{Id}")]
        public IActionResult DeleteEmployeeById(int Id)
        {
            var emp = _employeeList.FirstOrDefault(_e => _e.Id == Id);
            
            if(emp == null) 
            { 
                return NotFound();
            }

            _employeeList.Remove(emp);
            return Ok("Employee Deleted Successfully.");
        }
    }
}
