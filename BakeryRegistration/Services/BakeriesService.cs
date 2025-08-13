using AutoMapper;
using BakeryRegistration.Data;
using BakeryRegistration.Data.DTOs;
using BakeryRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;

namespace BakeryRegistration.Services
{
    public class BakeriesService
    {
        private IMapper _mapper;
        private ApplicationDbContext _context;
        private UserService _userService;
        private IHttpClientFactory _httpClientFactory;
        public BakeriesService(ApplicationDbContext context, IMapper mapper, UserService userService, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<BakeryModel>> GetBakeriesAsync()
        {
            var loggedUser = _userService.GetLoggedInUser();

            var bakeries = await _context.Bakeries
                .Where(b => b.Owner == loggedUser)
                .ToListAsync();

            return bakeries;
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
            var user = _userService.GetLoggedInUser();
            bakery.Owner = user;

            var fullAddress = $"{dto.AddressStreet} {dto.AddressNumber}, {dto.Neighborhood}, {dto.City}, {dto.State}";

            var client = _httpClientFactory.CreateClient();
            var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(fullAddress)}&format=json&limit=1";
            client.DefaultRequestHeaders.Add("User-Agent", "BakeryApp");
            var result = await client.GetStringAsync(url);

            var locations = JsonConvert.DeserializeObject<List<LocationResult>>(result);

            if (locations != null && locations.Any())
            {
                bakery.Latitude = decimal.Parse(locations[0].Lat, System.Globalization.CultureInfo.InvariantCulture);
                bakery.Longitude = decimal.Parse(locations[0].Lon, System.Globalization.CultureInfo.InvariantCulture);
            }

            _context.Bakeries.Add(bakery);
            var resultado = _context.SaveChanges();

            if (resultado == 0)
            {
                throw new ApplicationException("Falha ao cadastrar usuário!");
            }
            Console.WriteLine("Usuário fornecedor cadastrado!");
        }

        public void UpdateBakery(BakeryModel bakery)
        {
            var bakeryBanco = _context.Bakeries.FirstOrDefault(f => f.Id == bakery.Id);

            if (bakery.Name is not null)
            {
                bakeryBanco.Name = bakery.Name;
            }
            if (bakery.Email is not null)
            {
                bakeryBanco.Email = bakery.Email;
            }
            if (bakery.Phone is not null)
            {
                bakeryBanco.Phone = bakery.Phone;
            }
            if (bakery.AddressStreet is not null)
            {
                bakeryBanco.AddressStreet = bakery.AddressStreet;
            }
            if (bakery.AddressNumber is not null)
            {
                bakeryBanco.AddressNumber = bakery.AddressNumber;
            }
            if (bakery.AddressComplement is not null)
            {
                bakeryBanco.AddressComplement = bakery.AddressComplement;
            }
            if (bakery.Neighborhood is not null)
            {
                bakeryBanco.Neighborhood = bakery.Neighborhood;
            }
            if (bakery.City is not null)
            {
                bakeryBanco.City = bakery.City;
            }
            if (bakery.State is not null)
            {
                bakeryBanco.State = bakery.State;
            }
            _context.SaveChanges();
        }
        public void DeleteBakery(string id)
        {
            var idInt = int.Parse(id); 
            var bakery = _context.Bakeries.FirstOrDefault(f => f.Id.Equals(idInt));
            if (bakery == null) throw new KeyNotFoundException("Fornecedor não encontrado.");

            _context.Bakeries.Remove(bakery);
            _context.SaveChanges();
        }
    }
}
