using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.Common;
using System.Security.Cryptography;

namespace DataAccessLayer.Repositories
{
    public enum enUserRole
    {
        SuperUser,
        HospitalManager,
        GeneralManager,
        GeneralSupervisor,
        PatrolsSupervisor,
        WorkshopSupervisor,
        AdministrativeSupervisor
    }

    public class UserDTO
    {
        //private enum _enPermissions
        //{
        //    All = -1,
        //    None = 0,
        //    AddConsumablesAndSpareParts = 1,
        //    ReadConsumablesAndSpareParts = 2,
        //    UpdateConsumablesAndSpareParts = 4,
        //    DeleteConsumablesAndSpareParts = 8,
        //    ReadAll = 16,
        //    ApproveAllApplications = 32,
        //    UpdateDrivers = 64,
        //    UpdateVehicles = 128,
        //    ManipulatePatrols = 256,
        //    ManipulateJobOrder = 512,
        //    ManipulateMaintenance = 1024,
        //    ManipulateConsumablesPurchaseOrdersAndSparePartsPurchaseOrders = 2048,
        //    ManipulateConsumablesWithdrawApplicationsAndSparePartsWithdrawApplications = 4096,
        //    ManipulateMissions = 8192,
        //}

        public int UserId { get; set; }
        public string NationalNo { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public enUserRole Role { get; set; }
        public int AccessRight { get; set; }

        public UserDTO(int userId, string nationalNo, string password, string name, enUserRole role, int accessRight)
        {
            UserId = userId;
            NationalNo = nationalNo;
            Password = password;
            Name = name;
            Role = role;
            AccessRight = accessRight;
        }
    }

    public class UserDAL
    {
        private readonly string _connectionString;

        public UserDAL()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private MySqlConnection GetConnection() => new MySqlConnection(_connectionString);

        private MySqlCommand GetCommand(string query, MySqlConnection conn)
        {
            using var command = new MySqlCommand(query, conn) { CommandType = CommandType.Text };
            return command;
        }
        
        private UserDTO MapUser(DbDataReader reader)
        {
            if (!Enum.TryParse(reader["Role"].ToString(), out enUserRole role))
                throw new Exception($"Invalid Role value: {reader["Role"]}");

            return new UserDTO
            (
                reader.GetInt32(reader.GetOrdinal("UserID")),
                reader.GetString(reader.GetOrdinal("NationalNo")),
                reader.GetString(reader.GetOrdinal("Password")),
                reader.GetString(reader.GetOrdinal("Name")),
                role,
                reader.GetInt32(reader.GetOrdinal("AccessRight"))
            );
        }

        private string _HashPassword(string password)
        {
            using var sha265 = SHA256.Create();
            var bytes = sha265.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var usersList = new List<UserDTO>();

            await using var conn = GetConnection();
            const string query = "SELECT UserID, NationalNo, Password, Name, Role, AccessRight FROM users";

            using var cmd = GetCommand(query, conn);
            try
            {
                await conn.OpenAsync().ConfigureAwait(false);
                using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                while (await reader.ReadAsync()) usersList.Add(MapUser(reader));
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error occurred in GetAllUsersAsync.", ex);
            }

            return usersList;
        }

        public async Task<UserDTO> GetUserByIdAsync(int userId)
        {
            await using var conn = GetConnection();
            const string query = "SELECT UserID, NationalNo, Password, Name, Role, AccessRight FROM users WHERE UserID = @userId";

            using var cmd = GetCommand(query, conn);
            cmd.Parameters.AddWithValue("@userId", userId);

            try
            {
                await conn.OpenAsync().ConfigureAwait(false);
                using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                return await reader.ReadAsync() ? MapUser(reader) : null;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error occurred in GetUserByIdAsync.", ex);
            }
        }

        public async Task<UserDTO> GetUserByNationalNoAsync(string nationalNo)
        {
            await using var conn = GetConnection();
            const string query = "SELECT UserID, NationalNo, Password, Name, Role, AccessRight FROM users WHERE NationalNo = @nationalNo";

            using var cmd = GetCommand(query, conn);
            cmd.Parameters.AddWithValue("@nationalNo", nationalNo);

            try
            {
                await conn.OpenAsync().ConfigureAwait(false);
                using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                return await reader.ReadAsync() ? MapUser(reader) : null;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error occurred in GetUserByNationalNoAsync.", ex);
            }
        }

        public async Task<bool> NationalNoExistsAsync(string nationalNo)
        {
            await using var conn = GetConnection();
            const string query = "SELECT COUNT(*) FROM users WHERE NationalNo = @nationalNo";

            using var cmd = GetCommand(query, conn);
            cmd.Parameters.AddWithValue("@nationalNo", nationalNo);

            try
            {
                await conn.OpenAsync().ConfigureAwait(false);
                return Convert.ToInt32(await cmd.ExecuteScalarAsync().ConfigureAwait(false)) > 0;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error occurred in UserExistsAsync.", ex);
            }
        }

        public async Task<int> CreateUserAsync(UserDTO user)
        {
            await using var conn = GetConnection();
            const string query = @"
                INSERT INTO users (NationalNo, Password, Name, Role, AccessRight)
                VALUES (@NationalNo, @Password, @Name, @Role, @AccessRight);
                SELECT LAST_INSERT_ID();";

            using var cmd = GetCommand(query, conn);
            cmd.Parameters.AddWithValue("@NationalNo", user.NationalNo);
            cmd.Parameters.AddWithValue("@Password", _HashPassword(user.Password));
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Role", user.Role.ToString());
            cmd.Parameters.AddWithValue("@AccessRight", user.AccessRight);

            try
            {
                await conn.OpenAsync().ConfigureAwait(false);
                return Convert.ToInt32(await cmd.ExecuteScalarAsync().ConfigureAwait(false));
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error occurred in CreateUserAsync.", ex);
            }
        }

        public async Task<bool> UpdateUserAsync(UserDTO user)
        {
            await using var conn = GetConnection();
            const string query = @"
                UPDATE users
                SET NationalNo = @NationalNo, Password = @Password, Name = @Name, Role = @Role, AccessRight = @AccessRight
                WHERE UserID = @UserID";

            using var cmd = GetCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserID", user.UserId);
            cmd.Parameters.AddWithValue("@NationalNo", user.NationalNo);
            cmd.Parameters.AddWithValue("@Password", _HashPassword(user.Password));
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Role", user.Role.ToString());
            cmd.Parameters.AddWithValue("@AccessRight", user.AccessRight);

            try
            {
                await conn.OpenAsync().ConfigureAwait(false);
                return await cmd.ExecuteNonQueryAsync().ConfigureAwait(false) > 0;                
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error occurred in UpdateUserAsync.", ex);
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            await using var conn = GetConnection();
            const string query = "DELETE FROM users WHERE UserID = @UserID";

            using var cmd = GetCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserID", userId);

            try
            {
                await conn.OpenAsync().ConfigureAwait(false);
                return await cmd.ExecuteNonQueryAsync().ConfigureAwait(false) > 0;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error occurred in DeleteUserAsync.", ex);
            }
        }
    }
}
