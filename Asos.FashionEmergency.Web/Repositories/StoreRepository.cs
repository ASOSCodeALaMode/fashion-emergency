using Asos.FashionEmergency.Web.Controllers.Api;

namespace Asos.FashionEmergency.Web.Repositories
{
    public class StoreRepository
    {
        public Store GetStoreById(string storeId)
        {
            return new Store {Name = "Dummy store", Postcode = "E1 8BT"};
        }
    }
}