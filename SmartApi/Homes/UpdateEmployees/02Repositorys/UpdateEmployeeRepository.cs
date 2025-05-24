using SmartApi.Commons;
using SmartApi.Homes.RegistrationForms._01Models;
using SmartApi.Homes.UpdateEmployees._01Models;
using System.Data;
using System.Data.SqlClient;

namespace SmartApi.Homes.UpdateEmployees._02Repositorys
{
    public class UpdateEmployeeRepository : IUpdateServices
    {
        public SqlCommand cmd;
        public SqlConnection con;
        private IConfiguration _config;
        private IConnStringServices _conStar;
        public UpdateEmployeeRepository(IConfiguration _config, IConnStringServices _conStar)
        {
            this._config = _config;
            this._conStar = _conStar;

        }

        public IEnumerable<UpdateResponse> _IUpdateServices(UpdateRequest _obj)
        {
            IList<UpdateResponse> result = new List<UpdateResponse>();
            try
            {

                using (con = new SqlConnection(_conStar.strConn()))
                {
                    con.Open();
                    string returnValue = string.Empty;
                    cmd = new SqlCommand("[dbo].[SP_UpdateEmployee]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = _obj.id.ToString();
                    cmd.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = _obj.Name.ToString();
                    cmd.Parameters.AddWithValue("@lastName", SqlDbType.VarChar).Value = _obj.lastName.ToString();
                    cmd.Parameters.AddWithValue("@Emailid", SqlDbType.VarChar).Value = _obj.Emailid.ToString();
                    cmd.Parameters.AddWithValue("@mobileNo", SqlDbType.VarChar).Value = _obj.mobileNo.ToString();
                    cmd.Parameters.AddWithValue("@age", SqlDbType.VarChar).Value = _obj.age.ToString();
                    cmd.Parameters.AddWithValue("@gender", SqlDbType.VarChar).Value = _obj.gender.ToString();
                    cmd.Parameters.AddWithValue("@AadharNo", SqlDbType.VarChar).Value = _obj.AadharNo.ToString();
                    cmd.Parameters.AddWithValue("@panno", SqlDbType.VarChar).Value = _obj.panno.ToString();
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();
                    UpdateResponse obj = new UpdateResponse();
                    obj.message = "Submit SccessFully";
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
