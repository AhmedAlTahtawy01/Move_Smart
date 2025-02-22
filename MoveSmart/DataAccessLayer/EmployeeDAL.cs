using DataAccessLayer.Util;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string NationalNo { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string Phone { get; set; }
        public byte TransportationSubscriptionStatus { get; set; }
        public byte AssociatedBus { get; set; }

        public EmployeeDTO(int employeeID, string nationalNo, string name, string jobTitle, string phone,
            byte transportationSubscriptionStatus, byte associatedBus)
        {
            EmployeeID = employeeID;
            NationalNo = nationalNo;
            Name = name;
            JobTitle = jobTitle;
            Phone = phone;
            TransportationSubscriptionStatus = transportationSubscriptionStatus;
            AssociatedBus = associatedBus;
        }
    }

    public class EmployeeDAL
    {
        public static async Task<List<EmployeeDTO>> GetAllEmployeesAsync()
        {
            List<EmployeeDTO> employeesList = new List<EmployeeDTO>();

            string query = @"SELECT * FROM Employees
                            ORDER BY Name ASC;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        await conn.OpenAsync();

                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while(await reader.ReadAsync())
                            {
                                employeesList.Add(new EmployeeDTO(
                                    Convert.ToInt32(reader["EmployeeID"]),
                                    (string)reader["NationalNo"],
                                    (string)reader["Name"],
                                    (string)reader["JobTitle"],
                                    (string)reader["Phone"],
                                    Convert.ToByte(reader["TransportationSubscriptionStatus"]),
                                    Convert.ToByte(reader["AssociatedBus"])
                                ));
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return employeesList;
        }

        public static async Task<List<EmployeeDTO>> GetAllEmployeesWhoAreUsingBusAsync(byte busID)
        {
            List<EmployeeDTO> employeesList = new List<EmployeeDTO>();

            string query = @"SELECT * FROM Employees
                            WHERE AssociatedBus = @BusID
                            ORDER BY Name ASC;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("BusID", busID);

                        await conn.OpenAsync();

                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                employeesList.Add(new EmployeeDTO(
                                    Convert.ToInt32(reader["EmployeeID"]),
                                    (string)reader["NationalNo"],
                                    (string)reader["Name"],
                                    (string)reader["JobTitle"],
                                    (string)reader["Phone"],
                                    Convert.ToByte(reader["TransportationSubscriptionStatus"]),
                                    Convert.ToByte(reader["AssociatedBus"])
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return employeesList;
        }

        public static async Task<EmployeeDTO> GetEmployeeByIDAsync(int employeeID)
        {
            string query = @"SELECT * FROM Employees
                            WHERE EmployeeID = @EmployeeID;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("EmployeeID", employeeID);

                        await conn.OpenAsync();

                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new EmployeeDTO(
                                    Convert.ToInt32(reader["EmployeeID"]),
                                    (string)reader["NationalNo"],
                                    (string)reader["Name"],
                                    (string)reader["JobTitle"],
                                    (string)reader["Phone"],
                                    Convert.ToByte(reader["TransportationSubscriptionStatus"]),
                                    Convert.ToByte(reader["AssociatedBus"])
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static async Task<EmployeeDTO> GetEmployeeByNationalNoAsync(string nationalNo)
        {
            string query = @"SELECT * FROM Employees
                            WHERE NationalNo = @NationalNo;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("NationalNo", nationalNo);

                        await conn.OpenAsync();

                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new EmployeeDTO(
                                    Convert.ToInt32(reader["EmployeeID"]),
                                    (string)reader["NationalNo"],
                                    (string)reader["Name"],
                                    (string)reader["JobTitle"],
                                    (string)reader["Phone"],
                                    Convert.ToByte(reader["TransportationSubscriptionStatus"]),
                                    Convert.ToByte(reader["AssociatedBus"])
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static async Task<EmployeeDTO> GetEmployeeByPhoneAsync(string phone)
        {
            string query = @"SELECT * FROM Employees
                            WHERE Phone = @Phone;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("Phone", phone);

                        await conn.OpenAsync();

                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new EmployeeDTO(
                                    Convert.ToInt32(reader["EmployeeID"]),
                                    (string)reader["NationalNo"],
                                    (string)reader["Name"],
                                    (string)reader["JobTitle"],
                                    (string)reader["Phone"],
                                    Convert.ToByte(reader["TransportationSubscriptionStatus"]),
                                    Convert.ToByte(reader["AssociatedBus"])
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static async Task<int?> AddNewEmployeeAsync(EmployeeDTO newEmployee)
        {
            string query = @"INSERT INTO Employees
                            (EmployeeID, NationalNo, Name, JobTitle, Phone, TransportationSubscriptionStatus, AssociatedBus)
                            VALUES
                            (@EmployeeID, @NationalNo, @Name, @JobTitle, @Phone, @TransportationSubscriptionStatus, @AssociatedBus);
                            SELECT LAST_INSERT_ID();";

            try
            {
                using(MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using(MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("EmployeeID", newEmployee.EmployeeID);
                        cmd.Parameters.AddWithValue("NationalNo", newEmployee.NationalNo);
                        cmd.Parameters.AddWithValue("Name", newEmployee.Name);
                        cmd.Parameters.AddWithValue("JobTitle", newEmployee.JobTitle);
                        cmd.Parameters.AddWithValue("Phone", newEmployee.Phone);
                        cmd.Parameters.AddWithValue("TransportationSubscriptionStatus", newEmployee.TransportationSubscriptionStatus);
                        cmd.Parameters.AddWithValue("AssociatedBus", newEmployee.AssociatedBus);

                        await conn.OpenAsync();

                        object? result = await cmd.ExecuteScalarAsync();
                        if(result != null && int.TryParse(result.ToString(), out int id))
                        {
                            return id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static async Task<bool> UpdateEmployeeAsync(EmployeeDTO updatedEmployee)
        {
            string query = @"UPDATE Employees SET
                            NationalNo = @NationalNo,
                            Name = @Name,
                            JobTitle = @JobTitle,
                            Phone = @Phone,
                            TransportationSubscriptionStatus = @TransportationSubscriptionStatus,
                            AssociatedBus = @AssociatedBus
                            WHERE EmployeeID = @EmployeeID;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("EmployeeID", updatedEmployee.EmployeeID);
                        cmd.Parameters.AddWithValue("NationalNo", updatedEmployee.NationalNo);
                        cmd.Parameters.AddWithValue("Name", updatedEmployee.Name);
                        cmd.Parameters.AddWithValue("JobTitle", updatedEmployee.JobTitle);
                        cmd.Parameters.AddWithValue("Phone", updatedEmployee.Phone);
                        cmd.Parameters.AddWithValue("TransportationSubscriptionStatus", updatedEmployee.TransportationSubscriptionStatus);
                        cmd.Parameters.AddWithValue("AssociatedBus", updatedEmployee.AssociatedBus);

                        await conn.OpenAsync();

                        return Convert.ToByte(await cmd.ExecuteNonQueryAsync()) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public static async Task<bool> DeleteEmployeeAsync(int employeeID)
        {
            string query = @"DELETE FROM Employees
                            WHERE EmployeeID = @EmployeeID;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("EmployeeID", employeeID);

                        await conn.OpenAsync();

                        return Convert.ToByte(await cmd.ExecuteNonQueryAsync()) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public static async Task<bool> IsEmployeeTransportationSubscriptionValidAsync(int employeeID)
        {
            string query = @"SELECT Valid = 1 FROM Employees
                            WHERE EmployeeID = @EmployeeID AND TransportationSubscriptionStatus = 1;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("EmployeeID", employeeID);

                        await conn.OpenAsync();

                        return await cmd.ExecuteScalarAsync() != null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public static async Task<bool> IsEmployeeUsingBusAsync(int employeeID)
        {
            string query = @"SELECT UsingBus = 1 FROM Employees
                            WHERE EmployeeID = @EmployeeID AND AssociatedBus IS NOT NULL;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("EmployeeID", employeeID);

                        await conn.OpenAsync();

                        return await cmd.ExecuteScalarAsync() != null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
