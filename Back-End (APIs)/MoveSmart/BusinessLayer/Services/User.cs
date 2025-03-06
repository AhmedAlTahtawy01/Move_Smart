using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DataAccessLayer.Repositories;
using System.Linq;
using System.Text;

namespace BusinessLayer.Services
{
    public class User
    {
        // Properties
        private readonly UserDAL _dal;

        public int UserId { get; set; }
        public string Name { get; set; }
        public string NationalNo { get; set; }
        public string Password { get; set; }
        public enUserRole Role { get; set; }
        public int AccessRight { get; set; }

        public enum enMode
        {
            AddNew = 0,
            Update = 1
        };
        public enMode Mode { get; private set; } = enMode.AddNew;
        public UserDTO UserDTO => new UserDTO(UserId, NationalNo, Password, Name, Role, AccessRight);

        // Constructor
        public User(UserDTO userDTO, enMode mode = enMode.AddNew)
        {
            UserId = userDTO.UserId;
            Name = userDTO.Name;
            NationalNo = userDTO.NationalNo;
            Password = userDTO.Password;
            Role = userDTO.Role;
            _dal = new UserDAL();

            Mode = mode;
        }

        // Methods
        private string _HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private async Task<bool> _CreateUserAsync()
        {

            if (await _dal.NationalNoExistsAsync(NationalNo))
                throw new Exception($"User with National No {NationalNo} already exists.");

            UserId = await _dal.CreateUserAsync(UserDTO);
            if (UserId > 0)
            {
                Mode = enMode.Update;
                return true;
            }
            return false;
        }

        private async Task<bool> _UpdateUserAsync()
        {
            return await _dal.UpdateUserAsync(UserDTO);
        }

        public async Task<bool> SaveAsync()
        {
            if (UserDTO == null)
                throw new Exception("UserDTO is null.");

            if (NationalNo == null || NationalNo.Length != 14)
                throw new Exception("National No must be 14 digits.");

            return Mode switch
            {
                enMode.AddNew => await _CreateUserAsync(),
                enMode.Update => await _UpdateUserAsync(),
                _ => throw new Exception("Invalid mode."),
            };
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            return await _dal.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var userDTO = await _dal.GetUserByIdAsync(userId);
            return userDTO != null ? new User(userDTO, enMode.Update) : null;
        }

        public async Task<User> GetUserByNationalNoAsync(string nationalNo)
        {
            var userDTO = await _dal.GetUserByNationalNoAsync(nationalNo);
            return userDTO != null ? new User(userDTO, enMode.Update) : null;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _dal.DeleteUserAsync(userId);
        }

        public async Task<User> LoginAsync(string nationalNo, string password)
        {
            var userDTO = await _dal.GetUserByNationalNoAsync(nationalNo);
            
            if (userDTO == null)
                throw new Exception("User not found.");

            if (userDTO.Password != _HashPassword(password))
                throw new Exception("Invalid password.");

            return new User(userDTO, enMode.Update);
        }

        public async Task<bool> ChangePasswordAsync(string newPassword)
        {
            if (newPassword == null || newPassword.Length < 6)
                throw new Exception("Password must be at least 6 characters.");

            Password = newPassword;
            return await _dal.UpdateUserAsync(UserDTO);
        }

    }
}
