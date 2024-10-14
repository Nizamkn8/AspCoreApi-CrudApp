using System.Data;
using System.Data.SqlClient;

namespace CoreApi.Model
{
    public class EmployeeDB
    {
        SqlConnection con = new SqlConnection(@"Server=NIXAM\SQLEXPRESS;Database=ASP_Core;Integrated Security=True");
        public string InsertDB(Employee objcls)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_EmpInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empna", objcls.ename);
                cmd.Parameters.AddWithValue("@empaddr", objcls.eaddr);
                cmd.Parameters.AddWithValue("@empsal", objcls.esal);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return ("Inserted Successfully");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }

        //delete
        public string deleteDB(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return ("OK...");
            }
            catch (Exception ex)
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }

        //select all
        public List<Employee> SelectDB()
        {
            var list = new List<Employee>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_selectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var o = new Employee
                    {
                        eid = Convert.ToInt32(sdr["Emp_Id"]),//set
                        ename = sdr["Emp_Name"].ToString(),
                        eaddr = sdr["Emp_Address"].ToString(),
                        esal = sdr["Emp_Salary"].ToString(),
                    };
                    list.Add(o);
                }
            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
            return list;
        }

        //update
        public string UpdateDB(Employee emp)
        {
            string retVal = "";
            try
            {
                SqlCommand cmd = new SqlCommand("sp_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eid", emp.eid);
                cmd.Parameters.AddWithValue("@esal", emp.esal);
                cmd.Parameters.AddWithValue("@eaddr", emp.eaddr);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                retVal = "OK....updaated";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message);
            }
            return (retVal);
        }
    }
}
