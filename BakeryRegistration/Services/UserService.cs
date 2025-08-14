using AutoMapper;
using BakeryRegistration.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.WSIdentity;
using BakeryRegistration.Data;
using BakeryRegistration.Data.DTOs;
using BakeryRegistration.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace BakeryRegistration.Services
{
    public class UserService
    {
        private IMapper _mapper;
        private UserManager<UserModel> _userManager;
        private SignInManager<UserModel> _signInManager;
        private TokenService _tokenService;
        private ApplicationDbContext _context;
        private IHttpContextAccessor _contextAccessor;


        public UserService(ApplicationDbContext context, IMapper mapper, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, TokenService tokenService, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _contextAccessor = contextAccessor;
        }

        public async Task CreateUserDto(CreateUserDto dto)
        {
            UserModel usuario = _mapper.Map<UserModel>(dto);

            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuário!");
            }

        }


        public async Task<string> Login(LoginUserDto dto)
        {
            
            var resultado = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            if (!resultado.Succeeded)
            {
                return null;
            }
            var usuario = _signInManager.UserManager.Users.FirstOrDefault(u => u.UserName.Equals(dto.Username));

            var token = _tokenService.GerarToken(usuario);

            return token;
        }

        public UserModel GetLoggedInUser()
        {
            var nomeUsuario = _contextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Name || c.Type == JwtRegisteredClaimNames.Sub)
            ?.Value;
            if (string.IsNullOrEmpty(nomeUsuario))
            {
                return null;
            }

            var user = _context.Users.FirstOrDefault(u => u.UserName.Equals(nomeUsuario));

            return user;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

    }
}