using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Repositories
{
    public class ApplicationType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public ApplicationType(int typeId, string typeName)
        {
            this.TypeId = typeId;
            this.TypeName = typeName;
        }
    }

    public class ApplicationTypeDAL
    {
        static string _connectionString = "Server=localhost;Database=MoveSmart;User Id=root;Password=ahmedroot;";

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

        public async Task<ApplicationType> GetApplicationTypeByIdAsync(int typeId)
        {
            await using (var conn = GetConnection())
            {
                var query = "SELECT TypeID, TypeName FROM applicationtypes WHERE TypeID = @typeId";

                using (var command = GetCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@typeId", typeId);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync())
                            {
                                return new ApplicationType
                                    (
                                        reader.GetInt32("TypeID"),
                                        reader.GetString("TypeName")
                                    );
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in GetApplicationTypeByIdAsync.", ex);
                    }
                }
            }
        }

        public async Task<int> CreateApplicationTypeAsync(ApplicationType applicationType)
        {
            await using (var conn = GetConnection())
            {

                var query = @"
                    INSERT INTO applicationtypes (TypeName) 
                    VALUES (@typeName);
                    SELECT LAST_INSERT_ID();";

                using (var command = GetCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@TypeName",applicationType.TypeName);

                    try
                    {

                        await conn.OpenAsync().ConfigureAwait(false);
                        var newApplicationTypeId = Convert.ToInt32(await command.ExecuteScalarAsync().ConfigureAwait(false));

                        return newApplicationTypeId;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in CreateApplicationTypeAsync.", ex);
                    }
                }
            }
        }

    }
}
