using SmartApi.Commons;
using SmartApi.Homes.Employees._01Models;
using SmartApi.Homes.LoginApi._01Models;
using System.Data;
using System.Data.SqlClient;

namespace SmartApi.Homes.LoginApi._02Repositorys
{
    public class LoginRepository : ILoginServices
    {
        public SqlCommand cmd;
        public SqlConnection con;
        private IConfiguration _config;
        private IConnStringServices _conStar;
         public LoginRepository(IConfiguration _config, IConnStringServices _conStar) {
            this._config = _config;
            this._conStar = _conStar;
             }

        public IEnumerable<Login> _ILoginServices(Login _obj)
        {
           IList<Login> result= new List<Login>();
            try
            {

                using (con = new SqlConnection(_conStar.strConn()))
                {
                    con.Open();
                    string returnValue = string.Empty;
                    cmd = new SqlCommand("[dbo].[Sp_Login]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.VarChar).Value = _obj.UserId.ToString();
                    cmd.Parameters.AddWithValue("@Password", SqlDbType.VarChar).Value = _obj.Password.ToString();
                    cmd.CommandTimeout = 0;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Login obj = new Login();
                        obj.UserId = rdr["UserId"].ToString() ?? "";
                        obj.UserName = rdr["UserName"].ToString() ?? "";
                        obj.Emailid = rdr["Emailid"].ToString() ?? "";
                        obj.MobileNo = rdr["mobileNo"].ToString() ?? "";
                        obj.Password = rdr["Password"].ToString() ?? "";
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
