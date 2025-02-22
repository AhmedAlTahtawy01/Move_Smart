using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Util;
using MySqlConnector;

namespace DataAccessLayer
{
    public class VehicleDTO
    {
        public short VehicleID { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string PlateNumbers { get; set; }
        public byte VehicleType { get; set; }
        public string AssociatedHospital { get; set; }
        public string AssociatedTask { get; set; }
        public byte Status { get; set; }
        public byte FuelType { get; set; }
        public byte FuelConsumptionRate { get; set; }
        public byte OilConsumptionRate { get; set; }

        public VehicleDTO(short vehicleID, string brandName, string modelName, string plateNumbers,
            byte vehicleType, string associatedHospital, string associatedTask, byte status,
            byte fuelType, byte fuelConsumptionRate, byte oilConsumptionRate)
        {
            VehicleID = vehicleID;
            BrandName = brandName;
            ModelName = modelName;
            PlateNumbers = plateNumbers;
            VehicleType = vehicleType;
            AssociatedHospital = associatedHospital;
            AssociatedTask = associatedTask;
            Status = status;
            FuelType = fuelType;
            FuelConsumptionRate = fuelConsumptionRate;
            OilConsumptionRate = oilConsumptionRate;
        }
    }

    public class VehicleDAL
    {
        public static async Task<List<VehicleDTO>> GetAllVehiclesAsync()
        {
            List<VehicleDTO> vehiclesList = new List<VehicleDTO>();

            string query = @"SELECT * FROM Vehicles
                            ORDER BY Status ASC;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        await conn.OpenAsync();

                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                vehiclesList.Add(new VehicleDTO
                                (
                                    Convert.ToInt16(reader["VehicleID"]),
                                    (string)reader["BrandName"],
                                    (string)reader["ModelName"],
                                    (string)reader["PlateNumbers"],
                                    Convert.ToByte(reader["VehicleType"]),
                                    (string)reader["AssociatedHospital"],
                                    (string)reader["AssociatedTask"],
                                    Convert.ToByte(reader["Status"]),
                                    Convert.ToByte(reader["FuelType"]),
                                    Convert.ToByte(reader["FuelConsumptionRate"]),
                                    Convert.ToByte(reader["OilConsumptionRate"])
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return vehiclesList;
        }

        public static async Task<List<VehicleDTO>> GetVehiclesByVehicleTypeAsync(byte vehicleType)
        {
            List<VehicleDTO> vehiclesList = new List<VehicleDTO>();

            string query = @"SELECT * FROM Vehicles
                            WHERE VehicleType = @VehicleType
                            ORDER BY Status ASC;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("VehicleType", vehicleType);

                        await conn.OpenAsync();

                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                vehiclesList.Add(new VehicleDTO
                                (
                                    Convert.ToInt16(reader["VehicleID"]),
                                    (string)reader["BrandName"],
                                    (string)reader["ModelName"],
                                    (string)reader["PlateNumbers"],
                                    Convert.ToByte(reader["VehicleType"]),
                                    (string)reader["AssociatedHospital"],
                                    (string)reader["AssociatedTask"],
                                    Convert.ToByte(reader["Status"]),
                                    Convert.ToByte(reader["FuelType"]),
                                    Convert.ToByte(reader["FuelConsumptionRate"]),
                                    Convert.ToByte(reader["OilConsumptionRate"])
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return vehiclesList;
        }

        public static async Task<List<VehicleDTO>> GetVehiclesByFuelTypeAsync(byte FuelType)
        {
            List<VehicleDTO> vehiclesList = new List<VehicleDTO>();

            string query = @"SELECT * FROM Vehicles
                            WHERE FuelType = @FuelType
                            ORDER BY Status ASC;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("FuelType", FuelType);

                        await conn.OpenAsync();

                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                vehiclesList.Add(new VehicleDTO
                                (
                                    Convert.ToInt16(reader["VehicleID"]),
                                    (string)reader["BrandName"],
                                    (string)reader["ModelName"],
                                    (string)reader["PlateNumbers"],
                                    Convert.ToByte(reader["VehicleType"]),
                                    (string)reader["AssociatedHospital"],
                                    (string)reader["AssociatedTask"],
                                    Convert.ToByte(reader["Status"]),
                                    Convert.ToByte(reader["FuelType"]),
                                    Convert.ToByte(reader["FuelConsumptionRate"]),
                                    Convert.ToByte(reader["OilConsumptionRate"])
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return vehiclesList;
        }

        public static async Task<VehicleDTO> GetVehicleByIDAsync(short vehicleID)
        {
            string query = @"SELECT * FROM Vehicles
                            WHERE VehicleID = @VehicleID";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("VehicleID", vehicleID);

                        await conn.OpenAsync();

                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new VehicleDTO
                                (
                                    Convert.ToInt16(reader["VehicleID"]),
                                    (string)reader["BrandName"],
                                    (string)reader["ModelName"],
                                    (string)reader["PlateNumbers"],
                                    Convert.ToByte(reader["VehicleType"]),
                                    (string)reader["AssociatedHospital"],
                                    (string)reader["AssociatedTask"],
                                    Convert.ToByte(reader["Status"]),
                                    Convert.ToByte(reader["FuelType"]),
                                    Convert.ToByte(reader["FuelConsumptionRate"]),
                                    Convert.ToByte(reader["OilConsumptionRate"])
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }

        public static async Task<VehicleDTO> GetVehicleByPlateNumbersAsync(string plateNumbers)
        {
            string query = @"SELECT * FROM Vehicles
                            WHERE PlateNumbers = @PlateNumbers";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("PlateNumbers", plateNumbers);

                        await conn.OpenAsync();

                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new VehicleDTO
                                (
                                    Convert.ToInt16(reader["VehicleID"]),
                                    (string)reader["BrandName"],
                                    (string)reader["ModelName"],
                                    (string)reader["PlateNumbers"],
                                    Convert.ToByte(reader["VehicleType"]),
                                    (string)reader["AssociatedHospital"],
                                    (string)reader["AssociatedTask"],
                                    Convert.ToByte(reader["Status"]),
                                    Convert.ToByte(reader["FuelType"]),
                                    Convert.ToByte(reader["FuelConsumptionRate"]),
                                    Convert.ToByte(reader["OilConsumptionRate"])
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }

        public static async Task<short?> AddNewVehicleAsync(VehicleDTO newVehicle)
        {
            string query = @"INSERT INTO Vehicles
                            (BrandName, ModelName, PlateNumbers, VehicleType, AssociatedHospital, AssociatedTask, Status, FuelType, FuelConsumptionRate, OilConsumptionRate)
                            VALUES
                            (@BrandName, @ModelName, @PlateNumbers, @VehicleType, @AssociatedHospital, @AssociatedTask, @Status, @FuelType, @FuelConsumptionRate, @OilConsumptionRate);
                            SELECT LAST_INSERT_ID();";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("BrandName", newVehicle.BrandName);
                        cmd.Parameters.AddWithValue("ModelName", newVehicle.ModelName);
                        cmd.Parameters.AddWithValue("PlateNumbers", newVehicle.PlateNumbers);
                        cmd.Parameters.AddWithValue("VehicleType", newVehicle.VehicleType);
                        cmd.Parameters.AddWithValue("AssociatedHospital", newVehicle.AssociatedHospital);
                        cmd.Parameters.AddWithValue("AssociatedTask", newVehicle.AssociatedTask);
                        cmd.Parameters.AddWithValue("Status", newVehicle.Status);
                        cmd.Parameters.AddWithValue("FuelType", newVehicle.FuelType);
                        cmd.Parameters.AddWithValue("FuelConsumptionRate", newVehicle.FuelConsumptionRate);
                        cmd.Parameters.AddWithValue("OilConsumptionRate", newVehicle.OilConsumptionRate);

                        await conn.OpenAsync();

                        object? result = await cmd.ExecuteScalarAsync();
                        if (result != null && short.TryParse(result.ToString(), out short id))
                        {
                            return id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }

        public static async Task<bool> UpdateVehicleAsync(VehicleDTO updatedVehicle)
        {
            string query = @"UPDATE Vehicles SET
                            BrandName = @BrandName,
                            ModelName = @ModelName,
                            PlateNumbers = @PlateNumbers,
                            VehicleType = @VehicleType,
                            AssociatedHospital = @AssociatedHospital,
                            AssociatedTask = @AssociatedTask,
                            Status = @Status,
                            FuelType = @FuelType,
                            FuelConsumptionRate = @FuelConsumptionRate,
                            OilConsumptionRate = @OilConsumptionRate
                            WHERE VehicleID = @VehicleID;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("VehicleID", updatedVehicle.VehicleID);
                        cmd.Parameters.AddWithValue("BrandName", updatedVehicle.BrandName);
                        cmd.Parameters.AddWithValue("ModelName", updatedVehicle.ModelName);
                        cmd.Parameters.AddWithValue("PlateNumbers", updatedVehicle.PlateNumbers);
                        cmd.Parameters.AddWithValue("VehicleType", updatedVehicle.VehicleType);
                        cmd.Parameters.AddWithValue("AssociatedHospital", updatedVehicle.AssociatedHospital);
                        cmd.Parameters.AddWithValue("AssociatedTask", updatedVehicle.AssociatedTask);
                        cmd.Parameters.AddWithValue("Status", updatedVehicle.Status);
                        cmd.Parameters.AddWithValue("FuelType", updatedVehicle.FuelType);
                        cmd.Parameters.AddWithValue("FuelConsumptionRate", updatedVehicle.FuelConsumptionRate);
                        cmd.Parameters.AddWithValue("OilConsumptionRate", updatedVehicle.OilConsumptionRate);

                        await conn.OpenAsync();

                        return Convert.ToByte(await cmd.ExecuteNonQueryAsync()) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return false;
        }

        public static async Task<bool> DeleteVehicleAsync(string plateNumbers)
        {
            string query = @"DELETE FROM Vehicles
                            WHERE PlateNumbers = @PlateNumbers;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionSettings.connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("PlateNumbers", plateNumbers);

                        await conn.OpenAsync();

                        return Convert.ToByte(await cmd.ExecuteNonQueryAsync()) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return false;
        }
    }
}
