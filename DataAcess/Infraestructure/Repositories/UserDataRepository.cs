﻿using BlazorApp.Data.EF;
using BlazorApp.DataAcess.Bases;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DataAcess.Infraestructure.Repositories
{
    public class UserDataRepository :  IUserDataRepository
    {
        private readonly Context _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }


        public UserDataRepository(Context context) {
                _context = context ?? throw new ArgumentNullException(nameof(context));   
        }

        public async Task<UserData> GetAsync(String id) => await _context.UserData.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);

        public UserData Add(UserData userdata)
        {
            userdata.CreatedDate = DateTime.Now;

            return  _context.UserData.Add(userdata).Entity;
        }


        public UserData Update(UserData userdata)
        {
            userdata.UpdatedDate = DateTime.Now;

            return _context.UserData.Update(userdata).Entity;
        }

        public async Task<UserData> GetEmailAsync(string email)
        {
            return await _context.UserData.AsNoTracking().FirstOrDefaultAsync(item => item.email == email);
        }
    }
}
