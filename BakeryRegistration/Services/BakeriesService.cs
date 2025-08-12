using AutoMapper;
using BakeryRegistration.Data;
using BakeryRegistration.Data.DTOs;
using BakeryRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BakeryRegistration.Services
{
    public class BakeriesService
    {
        private IMapper _mapper;
        private ApplicationDbContext _context;
        public BakeriesService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateBakery(CreateBakeryDto dto)
        {
            bool exists = await _context.Bakeries.AnyAsync(b => b.Name == dto.Name);
            if (exists)
            {
                throw new ApplicationException("Já existe uma padaria cadastrada com esse nome.");
            }
            BakeryModel bakery = _mapper.Map<BakeryModel>(dto);
            bakery.PhotoPath = dto.PhotoPath;

            _context.Bakeries.Add(bakery);
            var resultado = _context.SaveChanges();

            if (resultado == 0)
            {
                throw new ApplicationException("Falha ao cadastrar usuário!");
            }
            Console.WriteLine("Usuário fornecedor cadastrado!");
        }
    }
}
