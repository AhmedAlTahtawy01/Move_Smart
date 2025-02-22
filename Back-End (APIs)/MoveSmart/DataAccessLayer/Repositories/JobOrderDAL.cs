using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class JobOrder
    {
        public int OrderId { get; set; }
        public int ApplicationId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan OrderTime { get; set; }
        public string Destination { get; set; }
        public int OdometerBefore { get; set; }
        public int OdometerAfter { get; set; }

        public JobOrder(int orderId, int applicationId, int vehicleId, int driverId,
            DateTime startDate, DateTime endDate, TimeSpan orderTime,
            string destination,
            int odometerBefore, int odometerAfter) 
        {
            this.OrderId = orderId;
            this.ApplicationId = applicationId;
            this.VehicleId = vehicleId;
            this.DriverId = driverId;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.OrderTime = orderTime;
            this.Destination = destination;
            this.OdometerBefore = odometerBefore;
            this.OdometerAfter = odometerAfter;
        }


    }

    public class JobOrderDAL
    {
        private static readonly string _connectionString = "Server=localhost;Database=MoveSmart;User Id=root;Password=ahmedroot;";

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        private MySqlCommand GetCommand(string query, MySqlConnection conn)
        {
            var cmd = new MySqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
    
        private JobOrder MapJobOrder(DbDataReader reader)
        {
            return new JobOrder
            (
                reader.GetInt32("OrderID"),
                reader.GetInt32("ApplicationID"),
                reader.GetInt32("VehicleID"),
                reader.GetInt32("DriverID"),
                reader.GetDateTime("OrderStartDate"),
                reader.GetDateTime("OrderEndDate"),
                TimeSpan.TryParse(reader["OrderTime"]?.ToString(), out TimeSpan orderTime) ? orderTime : TimeSpan.Zero,
                reader.GetString("Distination"),
                reader.GetInt32("KilometersCounterBeforeOrder"),
                reader.IsDBNull(reader.GetOrdinal("KilometersCounterAfterOrder"))
                ? 0 : reader.GetInt32("KilometersCounterAfterOrder")
            );
        }
    
        public async Task<List<JobOrder>> GetAllJobOrdersAsync()
        {
            var jobOrders = new List<JobOrder>();

            await using (var conn = GetConnection())
            {
                var query = @"
                    SELECT OrderID, ApplicationID, VehicleID, DriverID, OrderStartDate, OrderEndDate, OrderTime, Distination, KilometersCounterBeforeOrder, KilometersCounterAfterOrder
                    FROM joborders";

                using (var cmd = GetCommand(query, conn))
                {
                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);

                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                jobOrders.Add(MapJobOrder(reader));
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in GetAllJobOrdersAsync.", ex);
                    }
                }

                return jobOrders;
            }
        }
    
        public async Task<JobOrder> GetJobOrderByIdAsync(int orderId)
        {
            await using (var conn = GetConnection())
            {
                var query = @"
                    SELECT OrderID, ApplicationID, VehicleID, DriverID, OrderStartDate, OrderEndDate, OrderTime, Distination, KilometersCounterBeforeOrder, KilometersCounterAfterOrder
                    FROM joborders
                    WHERE OrderID = @orderId";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync())
                            {
                                return MapJobOrder(reader);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in GetJobOrderByIdAsync.", ex); 
                    }
                }
            }
        }
    
        public async Task<List<JobOrder>> GetJobOrdersByApplicationIdAsync(int applicationId)
        {
            var jobOrders = new List<JobOrder>();

            await using (var conn = GetConnection())
            {
                var query = @"
                    SELECT OrderID, ApplicationID, VehicleID, DriverID, OrderStartDate, OrderEndDate, OrderTime, Distination, KilometersCounterBeforeOrder, KilometersCounterAfterOrder
                    FROM joborders
                    WHERE ApplicationID = @applicationId";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@applicationId", applicationId);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                jobOrders.Add(MapJobOrder(reader));
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in GetJobOrdersByApplicationIdAsync.", ex);
                    }
                }
            }

            return jobOrders;
        }

        public async Task<List<JobOrder>> GetJobOrdersByVehicleIdAsync(int vehicleId)
        {
            var jobOrders = new List<JobOrder>();

            await using (var conn = GetConnection())
            {
                var query = @"
                    SELECT OrderID, ApplicationID, VehicleID, DriverID, OrderStartDate, OrderEndDate, OrderTime, Distination, KilometersCounterBeforeOrder, KilometersCounterAfterOrder
                    FROM joborders
                    WHERE VehicleID = @vehicleId";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@vehicleId", vehicleId);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                jobOrders.Add(MapJobOrder(reader));
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in GetJobOrdersByVehicleIdAsync.", ex);
                    }
                }
            }

            return jobOrders;
        }

        public async Task<List<JobOrder>> GetJobOrdersByDriverIdAsync(int driverId)
        {
            var jobOrders = new List<JobOrder>();

            await using (var conn = GetConnection())
            {
                var query = @"
                    SELECT OrderID, ApplicationID, VehicleID, DriverID, OrderStartDate, OrderEndDate, OrderTime, Distination, KilometersCounterBeforeOrder, KilometersCounterAfterOrder
                    FROM joborders
                    WHERE DriverID = @driverId";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@driverId", driverId);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                jobOrders.Add(MapJobOrder(reader));
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in GetJobOrdersByDriverIdAsync.", ex);
                    }
                }
            }

            return jobOrders;
        }

        public async Task<List<JobOrder>> GetJobOrdersByStartDateAsync(DateTime startDate)
        {
            var jobOrders = new List<JobOrder>();

            await using (var conn = GetConnection())
            {
                var query = @"
                    SELECT OrderID, ApplicationID, VehicleID, DriverID, OrderStartDate, OrderEndDate, OrderTime, Distination, KilometersCounterBeforeOrder, KilometersCounterAfterOrder
                    FROM joborders
                    WHERE OrderStartDate = @startDate";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                jobOrders.Add(MapJobOrder(reader));
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in GetJobOrdersByStartDateAsync.", ex);
                    }
                }
            }

            return jobOrders;
        }

        public async Task<List<JobOrder>> GetJobOrdersByDestinationIdAsync(string destination)
        {
            var jobOrders = new List<JobOrder>();

            await using (var conn = GetConnection())
            {
                var query = @"
                    SELECT OrderID, ApplicationID, VehicleID, DriverID, OrderStartDate, OrderEndDate, OrderTime, Distination, KilometersCounterBeforeOrder, KilometersCounterAfterOrder
                    FROM joborders
                    WHERE Disitination = @destination";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@destination", destination);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                jobOrders.Add(MapJobOrder(reader));
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in GetJobOrdersByDestinationIdAsync.", ex);
                    }
                }
            }

            return jobOrders;
        }

        // public async Task<list<JobOrder>> GetJobOrdersByStatus(enStatus status);

        // public async Task<list<JobOrder>> GetJobOrdersByDateRange(DateTime Date1, DateTime Date2);
        public async Task<int> CreateJobOrderAsync(JobOrder jobOrder)
        {
            await using (var conn = GetConnection())
            {
                var query = @"
                    INSERT INTO joborders (ApplicationID, VehicleID, DriverID, OrderStartDate, OrderEndDate, OrderTime, Distination, KilometersCounterBeforeOrder)
                    VALUES (@applicationId, @vehicleId, @driverId, @orderStartDate, @orderEndDate, @orderTime, @destination, @odometerBefore)
                    SELECT LAST_INSERT_ID();";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@applicationId", jobOrder.ApplicationId);
                    cmd.Parameters.AddWithValue("@vehicleId", jobOrder.VehicleId);
                    cmd.Parameters.AddWithValue("@driverId", jobOrder.DriverId);
                    cmd.Parameters.AddWithValue("@orderStartDate", jobOrder.StartDate);
                    cmd.Parameters.AddWithValue("@orderEndDate", jobOrder.EndDate);
                    cmd.Parameters.AddWithValue("@orderTime", jobOrder.OrderTime);
                    cmd.Parameters.AddWithValue("@destination", jobOrder.Destination);
                    cmd.Parameters.AddWithValue("@odometerBefore", jobOrder.OdometerBefore);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        var newJobOrderId = Convert.ToInt32(await cmd.ExecuteScalarAsync().ConfigureAwait(false));

                        return newJobOrderId;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in CreateJobOrderAsync.", ex);
                    }
                }
            }
        }

        public async Task<bool> UpdateJobOrderAsync(JobOrder jobOrder)
        {
            await using (var conn = GetConnection())
            {
                var query = @"
                    UPDATE joborders
                    SET 
                        ApplicationID = @applicationId,
                        VehicleID = @vehicleId,
                        DriverID = @driverId,
                        OrderStartDate = @startDate,
                        OrderEndDate = @endDate,
                        OrderTime = @orderTime,
                        Distination = @destination,
                        KilometersCounterBeforeOrder = @odometerBefore,
                        KilometersCounterAfterOrder = @odometerAfter
                    WHERE
                        OrderID = @orderId";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", jobOrder.OrderId);
                    cmd.Parameters.AddWithValue("@applicationId", jobOrder.ApplicationId);
                    cmd.Parameters.AddWithValue("@vehicleId", jobOrder.VehicleId);
                    cmd.Parameters.AddWithValue("@driverId", jobOrder.DriverId);
                    cmd.Parameters.AddWithValue("@orderStartDate", jobOrder.StartDate);
                    cmd.Parameters.AddWithValue("@orderEndDate", jobOrder.EndDate);
                    cmd.Parameters.AddWithValue("@orderTime", jobOrder.OrderTime);
                    cmd.Parameters.AddWithValue("@destination", jobOrder.Destination);
                    cmd.Parameters.AddWithValue("@odometerBefore", jobOrder.OdometerBefore);
                    cmd.Parameters.AddWithValue("@odometerAfter", jobOrder.OdometerAfter);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        var rowsAffected = await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);

                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in UpdateJobOrderAsync.", ex);
                    }
                }
            }
        }

        public async Task<bool> DeleteJobOrderAsync(int orderId)
        {
            await using (var conn = GetConnection())
            {
                var query = "DELETE FROM joborders WHERE OrderID = @orderId";
                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);

                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        var rowsAffected = await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);

                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database error occurred in DeleteJobOrderAsync.", ex);
                    }
                }
            }
        }

    }
}
