using SmartApi.Commons;
using SmartApi.Homes.RegistrationForms._01Models;
using SmartApi.Homes.SingUps._01Models;
using System.Data;
using System.Data.SqlClient;

namespace SmartApi.Homes.SingUps._02Repositorys
{
    public class SignupRepository : ISignupServices
    {
        public SqlCommand cmd;
        public SqlConnection con;
        private IConfiguration _config;
        private IConnStringServices _conStar;
        public SignupRepository(IConfiguration _config, IConnStringServices _conStar)
        {
            this._config = _config;
            this._conStar = _conStar;

        }
        public IEnumerable<SignupRes> _ISignupServices(SingupReq _obj)
        {
            IList<SignupRes> result = new List<SignupRes>();
            try
            {

                using (con = new SqlConnection(_conStar.strConn()))
                {
                    con.Open();
                    string returnValue = string.Empty;
                    cmd = new SqlCommand("[dbo].[SP_Signup]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.VarChar).Value = _obj.UserId.ToString();
                    cmd.Parameters.AddWithValue("@UserName", SqlDbType.VarChar).Value = _obj.UserName.ToString();
                    cmd.Parameters.AddWithValue("@EmailId", SqlDbType.VarChar).Value = _obj.EmailId.ToString();
                    cmd.Parameters.AddWithValue("@Password", SqlDbType.VarChar).Value = _obj.Password.ToString();
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();
                    SignupRes obj = new SignupRes();
                    obj.Message = "Submit SccessFully";
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
