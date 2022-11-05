using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly myDBContext _context;
        private readonly IMapper _mapper;

        //Repository
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private ITagRepository _tagRepository;
        private IMenuRepository _menuRepository;

        //prop
        public IProductRepository Products { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IMenuRepository Menus { get; private set; }

        public ITagRepository Tags { get; private set; }

        public UnitOfWork(myDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IProductRepository ProductRepository
        {
            get { return _productRepository = _productRepository ?? new ProductRepository(_context, _mapper); }
        }
        public ICategoryRepository CategoryRepository
        {
            get { return _categoryRepository = _categoryRepository ?? new CategoryRepository(_context); }
        }

        public IMenuRepository MenuRepository
        {
            get { return _menuRepository = _menuRepository ?? new MenuRepository(_context); }
        }

        public ITagRepository TagRepository
        {
            get { return _tagRepository = _tagRepository ?? new TagRepository(_context); }
        }



        public void Commit()
            => _context.SaveChanges();


        public async Task CommitAsync()
            => await _context.SaveChangesAsync();


        public void Rollback()
            => _context.Dispose();


        public async Task RollbackAsync()
            => await _context.DisposeAsync();
    }
}
