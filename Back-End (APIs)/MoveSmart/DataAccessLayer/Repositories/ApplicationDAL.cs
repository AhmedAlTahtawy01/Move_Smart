using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public DateTime CreationTime { get; set; }
        public int Status { get; set; }
        public int ApplicationType { get; set; }
        public string ApplicationDescription { get; set; }
        public int CreatedByUser { get; set; }

        public Application(int applicationId, DateTime creationTime, int status, int applicationType, string applicationDescription, int createdByUser)
        {
            this.ApplicationId = applicationId;
            this.CreationTime = creationTime;
            this.Status = status;
            this.ApplicationType = applicationType;
            this.ApplicationDescription = applicationDescription;
            this.CreatedByUser = createdByUser;
        }
    }

    public class ApplicationDAL
    {
        private static readonly string _connectionString = "Server=localhost;Database=MoveSmart;User Id=root;Password=ahmedroot;";

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

        private Application MapApplication(DbDataReader reader)
        {
            return new Application
            (
                reader.GetInt32("ApplicationID"),
                reader.GetDateTime("CreationDate"),
                reader.GetInt32("Status"),
                reader.GetInt32("ApplicationType"),
                reader.GetString("ApplicationDescription"),
                reader.GetInt32("CreatedByUser")
            );
        }

        public async Task<List<Application>> GetAllApplicationsAsync()
        {
            var applicationsList = new List<Application>();

            await using (var conn = GetConnection())
            {
                var query = @"
                            SELECT ApplicationID, CreationDate, Status, ApplicationType, ApplicationDescription, CreatedByUser
                            FROM applications";

                using (var cmd = GetCommand(query, conn))
                {
                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);

                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                applicationsList.Add(MapApplication(reader));
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Databse error occurred in GetAllApplicationsAsync.", ex);
                    }
                }
            }

            return applicationsList;
        }
        
        public async Task<Application> GetApplicationByIdAsync(int applicationId)
        {
            await using (var conn = GetConnection())
            {
                var query = @"
                            SELECT ApplicationID, CreationDate, Status, ApplicationType, ApplicationDescription, CreatedByUser
                            FROM applications
                            WHERE ApplicationID = @applicationId";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@applicationId", applicationId);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync())
                            {
                                return MapApplication(reader);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in GetApplicationByIdAsync.", ex);
                    }
                }
            }
        }

        public async Task<List<Application>> GetApplicationsByApplicationTypeAsync(int applicationType)
        {
            var applicationsList = new List<Application>();

            await using (var conn = GetConnection())
            {
                var query = @"
                            SELECT ApplicationID, CreationDate, Status, ApplicationType, ApplicationDescription, CreatedByUser
                            FROM applications 
                            WHERE ApplicationType = @applicationType";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@applicationType", applicationType);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);

                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                applicationsList.Add(MapApplication(reader));
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Databse error occurred in GetApplicationsByApplicationTypeAsync.", ex);
                    }
                }
            }

            return applicationsList;
        }

        public async Task<List<Application>> GetApplicationsByCreatedByUserAsync(int createdByUser)
        {
            var applicationsList = new List<Application>();

            await using (var conn = GetConnection())
            {
                var query = @"
                            SELECT ApplicationID, CreationDate, Status, ApplicationType, ApplicationDescription, CreatedByUser
                            FROM applications 
                            WHERE CreatedByUser = @createdByUser";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@createdByUser", createdByUser);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);

                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                applicationsList.Add(MapApplication(reader));
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Databse error occurred in GetApplicationsByCreatedByUserAsync.", ex);
                    }
                }
            }

            return applicationsList;
        }

        public async Task<int> CreateApplicationAsync(Application application)
        {
            await using (var conn = GetConnection())
            {
                var query = @"
                    INSERT INTO applications (CreationDate, Status, ApplicationType, ApplicationDescription, CreatedByUser) 
                    VALUES (@creationDate, @status, @applicationType, @applicationDescription, @createdByUser);
                    SELECT LAST_INSERT_ID();";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@creationDate", application.CreationTime);
                    cmd.Parameters.AddWithValue("@status", application.Status);
                    cmd.Parameters.AddWithValue("@applicationType", application.ApplicationType);
                    cmd.Parameters.AddWithValue("@applicationDescription", application.ApplicationDescription);
                    cmd.Parameters.AddWithValue("@createdByUser", application.CreatedByUser);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        var newApplicationId = Convert.ToInt32(await cmd.ExecuteScalarAsync().ConfigureAwait(false));

                        return newApplicationId;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Databse error occurred in CreateApplicationAsync.", ex);
                    }
                }
            }
        }

        public async Task<bool> UpdateApplicationAsync(Application application)
        {
            await using (var conn = GetConnection())
            {
                var query = @"
                    UPDATE applications
                    SET 
                        CreationDate = @creationDate,
                        Status = @status,
                        ApplicationType = @applicationType,
                        ApplicationDescription = @applicationDescription,
                        CreatedByUser = @createdByUser
                    WHERE
                        ApplicationID = @applicationId";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@creationDate", application.CreationTime);
                    cmd.Parameters.AddWithValue("@status", application.Status);
                    cmd.Parameters.AddWithValue("@applicationType", application.ApplicationType);
                    cmd.Parameters.AddWithValue("@applicationDescription", application.ApplicationDescription);
                    cmd.Parameters.AddWithValue("@createdByUser", application.CreatedByUser);
                    cmd.Parameters.AddWithValue("@applicationId", application.ApplicationId);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        var rowsAffected = await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);

                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in UpdateApplicationAsync.", ex);
                    }
                }
            }
        }
        
        public async Task<bool> DeleteApplicationAsync(int applicationId)
        {
            await using (var conn = GetConnection())
            {
                var query = "DELETE FROM applications WHERE ApplicationID = @applicationId";
                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@applicationId", applicationId);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        var rowsAffected = await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);

                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in DeleteApplicationAsync.", ex);
                    }
                }
            }
        }

    }
}