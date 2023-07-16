using StayCation.Models;

namespace StayCation.Repository
{
    public interface IRepository
    {
        List<Customers> ReadCustomersFromFile(string filePath);
    }
}
