using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using timesheet.business.Dtos;
using timesheet.business.Interfaces;
using timesheet.model;
using timesheet.model.Interfaces;

namespace timesheet.business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseRepository<Employee> _empRepo;
        private readonly IConfiguration _configuration;
        public EmployeeService(IBaseRepository<Employee> empRepo, IConfiguration configuration)
        {
            _empRepo = empRepo;
            _configuration = configuration;
        }

        public async Task<EmployeeDto> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var isExist = await _empRepo.FindAsync(e => e.Code == employeeDto.Code);
            if (isExist is not null && isExist.Count()>0)
                throw new Exception("This employee code already exists");
            var empEntity = new Employee
            {
                Code = employeeDto.Code,
                CreatedDate = DateTime.UtcNow,
                Name = employeeDto.Name,
                IsActive = true,
                Password = BCrypt.Net.BCrypt.HashPassword(employeeDto.Password)
            };

            var result = await _empRepo.AddAsync(empEntity);
            return new EmployeeDto { Code = empEntity.Code, Id = empEntity.Id, Name = empEntity.Name };
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployee()
        {
            var result = await _empRepo.FindAsync(t => t.IsActive);
            return result.Select(s => new EmployeeDto { Code = s.Code, Name = s.Name, Id = s.Id });
        }

        private async Task<EmployeeDto> GetEmployeeByCode(string code)
        {
            var result = await _empRepo.FindAsync(emp=>emp.Code==code);
            var emp = result.FirstOrDefault();  
            return new EmployeeDto { Code = emp.Code, Id = emp.Id, Name = emp.Name,Password=emp.Password };
        }

        public async Task<string> EmployeeLogin(string code, string password)
        {
            var employee = await GetEmployeeByCode(code);
            bool isVerified = BCrypt.Net.BCrypt.Verify(password, employee.Password);
            if (!isVerified) 
                throw new Exception("Please enter valid employee code/password");

            var key = Encoding.UTF8.GetBytes(_configuration["Authentication:Key"]!);
            var issuer = _configuration["Authentication:Issuer"];
            var audience = _configuration["Authentication:Audience"];
            var expirationMinutes = int.Parse(_configuration["Authentication:DurationInMinutes"]!);

            // Implement JWT token generation using the above parameters
            var secretKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Email, employee.Code),
                new Claim(ClaimTypes.Name, employee.Name),
            };

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
