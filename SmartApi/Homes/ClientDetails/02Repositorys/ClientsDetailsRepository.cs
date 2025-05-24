using Newtonsoft.Json;
using SmartApi.Commons;
using SmartApi.Homes.ClientDetails._01Models;

namespace SmartApi.Homes.ClientDetails._02Repositorys
{
    public class ClientsDetailsRepository : IClientDetailsServices
    {
        private readonly string _jsonFilePath;

        public ClientsDetailsRepository(IConnStringServices _conStar)
        {
            // Assuming _conStar provides the path to the JSON file
            _jsonFilePath = _conStar.JsonFilePath();
        }

        public IEnumerable<ClientDetailsResponse> _ClientStatusModel1Services(ClientDetailsRequest _obj)
        {
            try
            {
                string json = File.ReadAllText(_jsonFilePath);
                List<ClientDetailsResponse> result = JsonConvert.DeserializeObject<List<ClientDetailsResponse>>(json);

                if (_obj.Act == 6)
                {
                    result = result.Where(r => r.MobileNo == _obj.MobileNo).ToList();
                }

                return result;
            }
            catch (Exception Ex)
            {
                // Log or handle the exception as needed
                throw Ex;
            }
        }
    }
}
