using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.Common;

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
        static string _connectionString = "Server=localhost;Database=move_smart;User Id=root;Password=ahmedroot;";

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

        private ApplicationType MapApplicationType(DbDataReader reader)
        {
            return new ApplicationType
            (
                reader.GetInt32("TypeID"),
                reader.GetString("TypeName")
            );
        }

        public async Task<List<ApplicationType>> GetAllApplicationTypesAsync()
        {
            var applicationTypes = new List<ApplicationType>();

            await using (var conn = GetConnection())
            {
                var query = "SELECT TypeID, TypeName FROM applicationtypes";

                using (var cmd = GetCommand(query, conn))
                {
                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);

                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                applicationTypes.Add(MapApplicationType(reader));
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Databse error occurred in GetAllApplicationTypesAsync.", ex);
                    }
                }

            }

            return applicationTypes;

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

        public async Task<int> CreateApplicationTypeAsync(string typeName)
        {
            await using (var conn = GetConnection())
            {

                var query = @"
                    INSERT INTO applicationtypes (TypeName) 
                    VALUES (@typeName);
                    SELECT LAST_INSERT_ID();";

                using (var command = GetCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@typeName", typeName);

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

        public async Task<bool> UpdateApplicationTypeAsync(ApplicationType applicationType)
        {
            await using (var conn = GetConnection())
            {
                var query = @"
                    UPDATE applicationtypes 
                    SET 
                        TypeName = @typeName
                    WHERE 
                        TypeID = @typeId;";

                using (var command = GetCommand(query, conn))
                {
                    command.Parameters.Add("@typeId", MySqlDbType.Int32).Value = applicationType.TypeId;
                    command.Parameters.Add("@typeName", MySqlDbType.VarChar).Value = applicationType.TypeName;

                    try
                    {

                        await conn.OpenAsync().ConfigureAwait(false);
                        var rowsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in UpdateApplicationTypeAsync.", ex);
                    }
                }
            }
        }

        public async Task<bool> DeleteApplicationTypeAsync(int applicationTypeId)
        {
            await using (var conn = GetConnection())
            {
                var query = "DELETE FROM applicationtypes WHERE TypeID = @applicationTypeId";
                using (var command = GetCommand(query, conn))
                {
                    command.Parameters.Add("@applicationTypeId", MySqlDbType.Int32).Value = applicationTypeId;

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        var rowsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in DeleteApplicationTypeAsync.", ex);
                    }
                }
            }
        }


    }
}
