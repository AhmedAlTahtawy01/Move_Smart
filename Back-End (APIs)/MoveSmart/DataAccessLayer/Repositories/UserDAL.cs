using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.Common;

namespace DataAccessLayer.Repositories
{
    public class User
    {
        public int UserId { get; set; }
        public string NationalNo { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int AccessRight { get; set; }

        public User(int userId, string nationalNo, string name, string role, int accessRight)
        {
            this.UserId = userId;
            this.NationalNo = nationalNo;
            this.Name = name;
            this.Role = role;
            this.AccessRight = accessRight;
        }
    }

    public class UserDAL
    {
        private static readonly string _connectionString = "Server=localhost;Database=move_smart;User Id=root;Password=ahmedroot;";

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        private MySqlCommand GetCommand(string query, MySqlConnection conn)
        {
            var command = new MySqlCommand(query, conn);
            command.CommandType = CommandType.Text;
            return command;
        }
        
        private User MapUser(DbDataReader reader)
        {
            return new User
            (
                reader.GetInt32("UserID"),
                reader.GetString("NationalNo"),
                reader.GetString("Name"),
                reader.GetString("Role"),
                reader.GetInt32("AccessRight")
            );
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var usersList = new List<User>();

            await using (var conn = GetConnection())
            {
                var query = "SELECT UserID, NationalNo, Name, Role, AccessRight FROM users";

                using (var cmd = GetCommand(query, conn))
                {
                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);

                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                usersList.Add(MapUser(reader));
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Databse error occurred in GetAllUsersAsync.", ex);
                    }
                }

            }

            return usersList;

        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            await using (var conn = GetConnection())
            {
                var query = "SELECT UserID, NationalNo, Name, Role, AccessRight FROM users WHERE UserID = @userId";

                using (var command = GetCommand(query, conn))
                {
                    command.Parameters.Add("@userId", MySqlDbType.Int32).Value = userId;

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync())
                            {
                                return MapUser(reader);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in GetUserByIdAsync.", ex);
                    }
                }
            }
        }

        public async Task<User> GetUserByNationalNoAsync(string nationalNo)
        {
            await using (var conn = GetConnection())
            {
                var query = "SELECT UserID, NationalNo, Name, Role, AccessRight FROM users WHERE NationalNo = @nationalNo";

                using (var command = GetCommand(query, conn))
                {
                    command.Parameters.Add("@nationalNo", MySqlDbType.VarChar).Value = nationalNo;

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync())
                            {
                                return MapUser(reader);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in GetUserByNationalNoAsync.", ex);
                    }
                }
            }
        }

        public async Task<int> CreateUserAsync(User user)
        {
            await using (var conn = GetConnection())
            {

                var query = @"
                    INSERT INTO users (NationalNo, Name, Role, AccessRight) 
                    VALUES (@NationalNo, @Name, @Role, @AccessRight);
                    SELECT LAST_INSERT_ID();";

                using (var command = GetCommand(query, conn))
                {
                    command.Parameters.Add("@NationalNo", MySqlDbType.VarChar).Value = user.NationalNo;
                    command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = user.Name;
                    command.Parameters.Add("@Role", MySqlDbType.VarChar).Value = user.Role;
                    command.Parameters.Add("@AccessRight", MySqlDbType.Int64).Value = user.AccessRight;

                    try
                    {

                        await conn.OpenAsync().ConfigureAwait(false);
                        var newUserId = Convert.ToInt32(await command.ExecuteScalarAsync().ConfigureAwait(false));

                        return newUserId;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in CreateUserAsync.", ex);
                    }
                }
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            await using (var conn = GetConnection())
            {
                var query = @"
                    UPDATE users 
                    SET 
                        NationalNo = @NationalNo, 
                        Name = @Name, 
                        Role = @Role, 
                        AccessRight = @AccessRight 
                    WHERE 
                        UserID = @UserID;";

                using (var command = GetCommand(query, conn))
                {
                    command.Parameters.Add("@NationalNo", MySqlDbType.VarChar).Value = user.NationalNo;
                    command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = user.Name;
                    command.Parameters.Add("@Role", MySqlDbType.VarChar).Value = user.Role;
                    command.Parameters.Add("@AccessRight", MySqlDbType.Int32).Value = user.AccessRight;
                    command.Parameters.Add("@UserID", MySqlDbType.Int32).Value = user.UserId;

                    try
                    {

                        await conn.OpenAsync().ConfigureAwait(false);
                        var rowsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in UpdateUserAsync.", ex);
                    }
                }
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            await using (var conn = GetConnection())
            {
                var query = "DELETE FROM users WHERE UserID = @UserId";
                using (var command = GetCommand(query, conn))
                {
                    command.Parameters.Add("@UserId", MySqlDbType.Int32).Value = userId;
                        
                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        var rowsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in DeleteUserAsync.", ex);
                    }
                }
            }
        }
    }
}
