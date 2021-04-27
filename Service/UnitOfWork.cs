﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HotelListing.Data;
using HotelListing.Data.model;
using HotelListing.Service.interfaces;
using Microsoft.EntityFrameworkCore;


namespace HotelListing.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IRepository<Country> _countryRepository;
        private IRepository<Hotel> _hotelRepository;

        public IRepository<Country> CountryRepository => _countryRepository ??= new Repository<Country>(_context);
        public IRepository<Hotel> HotelRepository => _hotelRepository ??= new Repository<Hotel>(_context);
        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }


        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
