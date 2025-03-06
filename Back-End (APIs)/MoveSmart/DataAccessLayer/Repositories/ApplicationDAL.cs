using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer.Repositories
{

    public enum enStatus
    {
        Confirmed,
        Rejected,
        Pending,
        Canceled
    }

    public class Application
    {

        public int ApplicationId { get; set; }
        public DateTime CreationDate { get; set; }
        public enStatus Status { get; set; }
        public int ApplicationType { get; set; }
        public string ApplicationDescription { get; set; }
        public int UserId { get; set; }

        public Application(int applicationId, DateTime creationDate, enStatus status, int applicationType, string applicationDescription, int createdByUser)
        {
            this.ApplicationId = applicationId;
            this.CreationDate = creationDate;
            this.Status = status;
            this.ApplicationType = applicationType;
            this.ApplicationDescription = applicationDescription;
            this.UserId = createdByUser;
        }
    }

    public class ApplicationDAL
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

        private Application MapApplication(DbDataReader reader)
        {
            enStatus status;
            Enum.TryParse(reader.GetString(reader.GetOrdinal("Status")), out status);

            return new Application
            (
                reader.GetInt32("ApplicationID"),
                reader.GetDateTime("CreationDate"),
                status,
                reader.GetInt32("ApplicationType"),
                reader.GetString("ApplicationDescription"),
                reader.GetInt32("CreatedByUserID")
            );
        }

        public async Task<List<Application>> GetAllApplicationsAsync()
        {
            var applicationsList = new List<Application>();

            await using (var conn = GetConnection())
            {
                var query = @"
                            SELECT ApplicationID, CreationDate, Status, ApplicationType, ApplicationDescription, CreatedByUserID
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
                        throw new Exception("Database error occurred in GetAllApplicationsAsync.", ex);
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
                            SELECT ApplicationID, CreationDate, Status, ApplicationType, ApplicationDescription, CreatedByUserID
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
                            SELECT ApplicationID, CreationDate, Status, ApplicationType, ApplicationDescription, CreatedByUserID
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

        public async Task<List<Application>> GetApplicationsByUserIdAsync(int createdByUser)
        {
            var applicationsList = new List<Application>();

            await using (var conn = GetConnection())
            {
                var query = @"
                            SELECT ApplicationID, CreationDate, Status, ApplicationType, ApplicationDescription, CreatedByUserID
                            FROM applications 
                            WHERE CreatedByUserID = @createdByUser";

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
                        throw new Exception("Database error occurred in GetApplicationsByUserIdAsync.", ex);
                    }
                }
            }

            return applicationsList;
        }

        public async Task<List<Application>> GetApplicationsByStatus(enStatus status)
        {
            var applicationsList = new List<Application>();

            await using (var conn = GetConnection())
            {
                var query = @"
                            SELECT ApplicationID, CreationDate, Status, ApplicationType, ApplicationDescription, CreatedByUserID
                            FROM applications 
                            WHERE Status = @status";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status.ToString());

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
                        throw new Exception("Database error occurred in GetApplicationsByStatus.", ex);
                    }
                }

            }

            return applicationsList;
        }

        public async Task<int> CountAllApplicationsAsync()
        {
            await using (var conn = GetConnection())
            {
                var query = "SELECT COUNT(*) FROM applications";

                using (var cmd = GetCommand(query, conn))
                {
                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        return Convert.ToInt32(await cmd.ExecuteScalarAsync().ConfigureAwait(false));
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in CountAllApplicationsAsync.", ex);
                    }
                }
            }
        }

        public async Task<int> CountApplicationsByStatusAsync(enStatus status)
        {
            await using (var conn = GetConnection())
            {
                var query = "SELECT COUNT(*) FROM applications WHERE Status = @status";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status.ToString());

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        return Convert.ToInt32(await cmd.ExecuteScalarAsync().ConfigureAwait(false));
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in CountApplicationsByStatusAsync.", ex);
                    }
                }
            }
        }

        public async Task<int> CountApplicationsByTypeAsync(int applicationType)
        {
            await using (var conn = GetConnection())
            {
                var query = "SELECT COUNT(*) FROM applications WHERE ApplicationType = @applicationType";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@applicationType", applicationType);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        return Convert.ToInt32(await cmd.ExecuteScalarAsync().ConfigureAwait(false));
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in CountApplicationsByTypeAsync.", ex);
                    }
                }
            }
        }

        public async Task<bool> UpdateStatusAsync(int applicationId, enStatus status)
        {
            await using (var conn = GetConnection())
            {
                var query = @"
                    UPDATE applications
                    SET 
                        Status = @status
                    WHERE
                        ApplicationID = @applicationId";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@applicationId", applicationId);
                    cmd.Parameters.Add("@status", MySqlDbType.Enum).Value = status.ToString();

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        var rowsAffected = await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);

                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in UpdateStatusAsync.", ex);
                    }
                }
            }
        }

        public async Task<int> CreateApplicationAsync(Application application)
        {
            await using (var conn = GetConnection())
            {
                var query = @"
                    INSERT INTO applications (CreationDate, Status, ApplicationType, ApplicationDescription, CreatedByUserID) 
                    VALUES (@creationDate, @status, @applicationType, @applicationDescription, @createdByUser);
                    SELECT LAST_INSERT_ID()";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@creationDate", application.CreationDate);
                    cmd.Parameters.AddWithValue("@status", enStatus.Pending.ToString());
                    cmd.Parameters.AddWithValue("@applicationType", application.ApplicationType);
                    cmd.Parameters.AddWithValue("@applicationDescription", application.ApplicationDescription);
                    cmd.Parameters.AddWithValue("@createdByUser", application.UserId);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        var newApplicationId = Convert.ToInt32(await cmd.ExecuteScalarAsync().ConfigureAwait(false));

                        return newApplicationId;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in CreateApplicationAsync.", ex);
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
                        Status = @status,
                        ApplicationType = @applicationType,
                        ApplicationDescription = @applicationDescription,
                        CreatedByUserID = @createdByUser
                    WHERE
                        ApplicationID = @applicationId";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", application.Status.ToString());
                    cmd.Parameters.AddWithValue("@applicationType", application.ApplicationType);
                    cmd.Parameters.AddWithValue("@applicationDescription", application.ApplicationDescription);
                    cmd.Parameters.AddWithValue("@createdByUser", application.UserId);
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