using Microsoft.Data.SqlClient; // Corrected this line
using InsurancePoliciesCRUDApp.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System;

namespace InsurancePoliciesCRUDApp.Data
{
    public class InsurancePolicyRepository
    {
        private readonly string _connectionString;

        public InsurancePolicyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<InsurancePolicy> GetAllPolicies()
        {
            var policies = new List<InsurancePolicy>();

            using (var conn = new SqlConnection(_connectionString)) // Corrected this line
            {
                string sql = "SELECT * FROM InsurancePolicies";
                using (var cmd = new SqlCommand(sql, conn)) // Corrected this line
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var policy = new InsurancePolicy
                            {
                                Id = (int)reader["Id"],
                                PolicyNumber = reader["PolicyNumber"].ToString(),
                                PolicyHolderName = reader["PolicyHolderName"].ToString(),
                                StartDate = (DateTime)reader["StartDate"],
                                EndDate = (DateTime)reader["EndDate"],
                                Type = reader["Type"].ToString(),
                                PremiumAmount = (decimal)reader["PremiumAmount"]
                            };

                            policies.Add(policy);
                        }
                    }
                }
            }

            return policies;
        }

        public InsurancePolicy GetPolicyById(int id)
        {
            InsurancePolicy policy = null;

            using (var conn = new SqlConnection(_connectionString)) // Corrected this line
            {
                string sql = "SELECT * FROM InsurancePolicies WHERE Id = @Id";
                using (var cmd = new SqlCommand(sql, conn)) // Corrected this line
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            policy = new InsurancePolicy
                            {
                                Id = (int)reader["Id"],
                                PolicyNumber = reader["PolicyNumber"].ToString(),
                                PolicyHolderName = reader["PolicyHolderName"].ToString(),
                                StartDate = (DateTime)reader["StartDate"],
                                EndDate = (DateTime)reader["EndDate"],
                                Type = reader["Type"].ToString(),
                                PremiumAmount = (decimal)reader["PremiumAmount"]
                            };
                        }
                    }
                }
            }

            return policy;
        }

        //public void AddPolicy(InsurancePolicy policy)
        //{
        //    using (var conn = new SqlConnection(_connectionString)) // Corrected this line
        //    {
        //        string sql = "INSERT INTO InsurancePolicies (PolicyNumber, PolicyHolderName, StartDate, EndDate, Type, PremiumAmount) VALUES (@PolicyNumber, @PolicyHolderName, @StartDate, @EndDate, @Type, @PremiumAmount)";
        //        using (var cmd = new SqlCommand(sql, conn)) // Corrected this line
        //        {
        //            cmd.Parameters.AddWithValue("@PolicyNumber", policy.PolicyNumber);
        //            cmd.Parameters.AddWithValue("@PolicyHolderName", policy.PolicyHolderName);
        //            cmd.Parameters.AddWithValue("@StartDate", policy.StartDate);
        //            cmd.Parameters.AddWithValue("@EndDate", policy.EndDate);
        //            cmd.Parameters.AddWithValue("@Type", policy.Type);
        //            cmd.Parameters.AddWithValue("@PremiumAmount", policy.PremiumAmount);
        //            conn.Open();

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
        public async Task AddPolicyAsync(InsurancePolicy policy)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO InsurancePolicies (PolicyNumber, PolicyHolderName, StartDate, EndDate, Type, PremiumAmount) OUTPUT INSERTED.Id VALUES (@PolicyNumber, @PolicyHolderName, @StartDate, @EndDate, @Type, @PremiumAmount)";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PolicyNumber", policy.PolicyNumber);
                    cmd.Parameters.AddWithValue("@PolicyHolderName", policy.PolicyHolderName);
                    cmd.Parameters.AddWithValue("@StartDate", policy.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", policy.EndDate);
                    cmd.Parameters.AddWithValue("@Type", policy.Type);
                    cmd.Parameters.AddWithValue("@PremiumAmount", policy.PremiumAmount);
                    await conn.OpenAsync();
                    policy.Id = (int)await cmd.ExecuteScalarAsync();
                }
            }
        }

        public void UpdatePolicy(InsurancePolicy policy)
        {
            using (var conn = new SqlConnection(_connectionString)) // Corrected this line
            {
                string sql = "UPDATE InsurancePolicies SET PolicyNumber = @PolicyNumber, PolicyHolderName = @PolicyHolderName, StartDate = @StartDate, EndDate = @EndDate, Type = @Type, PremiumAmount = @PremiumAmount WHERE Id = @Id";
                using (var cmd = new SqlCommand(sql, conn)) // Corrected this line
                {
                    cmd.Parameters.AddWithValue("@Id", policy.Id);
                    cmd.Parameters.AddWithValue("@PolicyNumber", policy.PolicyNumber);
                    cmd.Parameters.AddWithValue("@PolicyHolderName", policy.PolicyHolderName);
                    cmd.Parameters.AddWithValue("@StartDate", policy.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", policy.EndDate);
                    cmd.Parameters.AddWithValue("@Type", policy.Type);
                    cmd.Parameters.AddWithValue("@PremiumAmount", policy.PremiumAmount);
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletePolicy(int id)
        {
            using (var conn = new SqlConnection(_connectionString)) // Corrected this line
            {
                string sql = "DELETE FROM InsurancePolicies WHERE Id = @Id";
                using (var cmd = new SqlCommand(sql, conn)) // Corrected this line
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
