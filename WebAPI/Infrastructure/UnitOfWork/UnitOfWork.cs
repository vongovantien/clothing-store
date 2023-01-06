using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly myDBContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        //Repository
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private ITagRepository _tagRepository;
        private IMenuRepository _menuRepository;
        private IUserRepository _userRepository;
        //prop
        public IProductRepository Products { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IMenuRepository Menus { get; private set; }

        public ITagRepository Tags { get; private set; }
        public IUserRepository Users { get; private set; }

        public UnitOfWork(myDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
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
        public IUserRepository UserRepository
        {
            get { return _userRepository = _userRepository ?? new UserRepository(_context, _mapper, _configuration); }
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
