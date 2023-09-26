using AdMoney.Models;

namespace AdMoney.Repository.Interfaces
{
    public interface IAdvisorClientData
    {
        public List<Client> getAllClients(int userId);
        public Client GetClientById(int Id);

        public List<ModelSelectData> GetAllModels(string risk, int userId);

        public void AddNewClient(Client client);
        public User GetAdvisorProfile(int Id);

        public List<Client> getAllAddedClients(int userId);
    }
}
