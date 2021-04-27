using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HotelListing.Data.model;

namespace HotelListing.Service.interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Country> CountryRepository { get; }
        IRepository<Hotel> HotelRepository { get; }

        Task Save();
    }
}
