using SmartApi.Commons;
using SmartApi.Homes.ChangePasswords._01Models;
using System.Data;
using System.Data.SqlClient;

namespace SmartApi.Homes.ChangePasswords._02Repositorys
{
    public class ChangePasswordRepository : IChangePasswordServices
    {
        public SqlCommand cmd;
        public SqlConnection con;
        private IConfiguration _config;
        private IConnStringServices _conStar;
        public ChangePasswordRepository(IConfiguration _config, IConnStringServices _conStar)
        {
            this._config = _config;
            this._conStar = _conStar;

        }

        public IEnumerable<ChangePasswordResponse> _IChangePasswordServices(ChangePasswordRequest _obj)
        {
            IList<ChangePasswordResponse> result = new List<ChangePasswordResponse>();
            try
            {

                using (con = new SqlConnection(_conStar.strConn()))
                {
                    con.Open();
                    string returnValue = string.Empty;
                    cmd = new SqlCommand("[dbo].[Sp_PaswordChange]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.VarChar).Value = _obj.UserId.ToString();
                    cmd.Parameters.AddWithValue("@OldPassword", SqlDbType.VarChar).Value = _obj.OldPassword.ToString();
                    cmd.Parameters.AddWithValue("@NewPassword", SqlDbType.VarChar).Value = _obj.NewPassword.ToString();
                    cmd.Parameters.AddWithValue("@ConfirmPassword", SqlDbType.VarChar).Value = _obj.ConfirmPassword.ToString();
                    cmd.CommandTimeout = 0;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        ChangePasswordResponse obj = new ChangePasswordResponse();
                        obj.Result = rdr["Result"].ToString() ?? "";
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
