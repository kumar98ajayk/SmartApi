using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartApi.Commons;
using SmartApi.Homes.Employees._01Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml.Linq;

namespace SmartApi.Homes.Employees._02Repositorys
{
    public class EmployeRepository : IEmployeServices
    {
        public SqlCommand cmd;
        public SqlConnection con;
        private IConfiguration _config;
        private IConnStringServices _conStar;
        public EmployeRepository(IConfiguration _config, IConnStringServices _conStar)
        {
            this._config = _config;
            this._conStar = _conStar;
           
        }

        public IEnumerable<EmployeResponse> _IEmployeServices(EmployeResponse _obj)
        {
            IList<EmployeResponse> result = new List<EmployeResponse>();
            try
            {

                using (con = new (_conStar.strConn()))
                {
                       con.Open();
                       string returnValue = string.Empty;
                        cmd = new SqlCommand("[dbo].[Sp_Employee]", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                            EmployeResponse obj = new EmployeResponse();
                            obj.id = rdr["id"].ToString() ?? "";
                            obj.CustomerId = rdr["CustomerId"].ToString() ?? "";
                            obj.Name = rdr["Name"].ToString() ?? "";
                            obj.lastName = rdr["lastName"].ToString() ?? "";
                            obj.Emailid = rdr["Emailid"].ToString() ?? "";
                            obj.mobileNo = rdr["mobileNo"].ToString() ?? "";
                            obj.age = rdr["age"].ToString() ?? "";
                            obj.gender = rdr["gender"].ToString() ?? "";
                            obj.AadharNo = rdr["AadharNo"].ToString() ?? "";
                            obj.panno = rdr["panno"].ToString() ?? "";
                            result.Add(obj);
                        }                 

                }
                return result;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
