using Microsoft.AspNetCore.Mvc;
using CoreApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("EmpTest")]
    [ApiController]
    public class EmpTestController : ControllerBase
    {
        EmployeeDB dbobj = new EmployeeDB();
        // GET: api/<EmpTestController>
        [HttpGet]
        [Route("GetAlltab")]
        public List<Employee> Get()
        {
            return dbobj.SelectDB();
        }

        // GET api/<EmpTestController>/5
        [HttpGet]
        [Route("gettabWithId/{id}")]
        public Employee Get(int id)
        {
            var getEmployee = dbobj.SelectDB().Where(x => x.eid == id).FirstOrDefault();
            return getEmployee;
        }

        // POST api/<EmpTestController>
        [HttpPost]
        [Route("posttab")]
        public void Post(Employee clsobj)
        {
            dbobj.InsertDB(clsobj);
        }

        // PUT api/<EmpTestController>/5
        [HttpPut]
        [Route("Updatetab/{id}")] 
        public void Put(int id, Employee ob)
        {
            var updateEmployee = dbobj.SelectDB().Where(x => x.eid == id).FirstOrDefault();
            if (updateEmployee != null) 
            {
                updateEmployee.eaddr = ob.eaddr;
                updateEmployee.esal = ob.esal;
                dbobj.UpdateDB(updateEmployee);
            }
        }

        // DELETE api/<EmpTestController>/5
        [HttpDelete]
        [Route("deletetab/{id}")]
        public void Delete(int id)
        {
            dbobj.deleteDB(id);
        }
    }
}
