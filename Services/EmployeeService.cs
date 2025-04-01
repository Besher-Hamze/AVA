using AutoMapper;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace MongoDotNetBackend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            ICompanyRepository companyRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(string id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByCompanyIdAsync(string companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new KeyNotFoundException($"Company with ID {companyId} not found.");
            }

            var employees = await _employeeRepository.GetEmployeesByCompanyIdAsync(companyId);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }
        public async Task<EmployeeDto> GetEmployeeByEmailAsync(string email)
        {
            var employee = await _employeeRepository.GetEmployeeByEmailAsync(email);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            // Verify that the company exists
            var company = await _companyRepository.GetByIdAsync(createEmployeeDto.CompanyId);
            if (company == null)
            {
                throw new KeyNotFoundException($"Company with ID {createEmployeeDto.CompanyId} not found.");
            }

            var employeeEntity = _mapper.Map<Employee>(createEmployeeDto);
            
            // Hash the password before saving
            employeeEntity.PasswordHash = HashPassword(createEmployeeDto.Password);
            
            await _employeeRepository.CreateAsync(employeeEntity);
            return _mapper.Map<EmployeeDto>(employeeEntity);
        }

        public async Task UpdateEmployeeAsync(string id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employeeEntity = await _employeeRepository.GetByIdAsync(id);
            if (employeeEntity == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }

            // Verify that the company exists if changed
            if (employeeEntity.CompanyId != updateEmployeeDto.CompanyId)
            {
                var company = await _companyRepository.GetByIdAsync(updateEmployeeDto.CompanyId);
                if (company == null)
                {
                    throw new KeyNotFoundException($"Company with ID {updateEmployeeDto.CompanyId} not found.");
                }
            }

            _mapper.Map(updateEmployeeDto, employeeEntity);
            
            // Update password if provided
            if (!string.IsNullOrEmpty(updateEmployeeDto.Password))
            {
                employeeEntity.PasswordHash = HashPassword(updateEmployeeDto.Password);
            }
            
            await _employeeRepository.UpdateAsync(id, employeeEntity);
        }

        public async Task DeleteEmployeeAsync(string id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }

            await _employeeRepository.DeleteAsync(id);
        }
        
        public async Task ChangePasswordAsync(string id, ChangePasswordDto changePasswordDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }
            
            // Verify current password
            if (!VerifyPassword(changePasswordDto.CurrentPassword, employee.PasswordHash))
            {
                throw new UnauthorizedAccessException("Current password is incorrect.");
            }
            
            // Update to new password
            employee.PasswordHash = HashPassword(changePasswordDto.NewPassword);
            
            await _employeeRepository.UpdateAsync(id, employee);
        }
        
        public async Task<bool> ValidateEmployeeCredentialsAsync(string email, string password)
        {
            var employee = await _employeeRepository.GetEmployeeByEmailAsync(email);
            if (employee == null)
            {
                return false;
            }
            
            return VerifyPassword(password, employee.PasswordHash);
        }
        
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }
        
        private bool VerifyPassword(string password, string storedHash)
        {
            string hashedInput = HashPassword(password);
            return hashedInput == storedHash;
        }
    }

    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(string id);
        Task<IEnumerable<EmployeeDto>> GetEmployeesByCompanyIdAsync(string companyId);
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task UpdateEmployeeAsync(string id, UpdateEmployeeDto updateEmployeeDto);
        Task DeleteEmployeeAsync(string id);
        Task ChangePasswordAsync(string id, ChangePasswordDto changePasswordDto);
        Task<bool> ValidateEmployeeCredentialsAsync(string email, string password);

        Task<EmployeeDto> GetEmployeeByEmailAsync(string email);

    }
}