using System.Threading.Tasks;
using HotelListing.Data.Model;

namespace HotelListing.Service.interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Country> CountryRepository { get; }
        IRepository<Hotel> HotelRepository { get; }

        Task Save();
    }
}
