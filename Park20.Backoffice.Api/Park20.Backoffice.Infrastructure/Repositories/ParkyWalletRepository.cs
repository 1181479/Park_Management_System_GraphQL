using Microsoft.Extensions.Configuration;
using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.DatabaseManagement.Sql.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Park20.Backoffice.Core.Domain;
using Dapper;
using System.ComponentModel;
using Park20.Backoffice.Core.Domain.ParkyWallets;
using Park20.Backoffice.Core.Domain.Park;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;

namespace Park20.Backoffice.Infrastructure.Repositories
{
    public class ParkyWalletRepository(IConfiguration configuration) : IParkyWalletRepository
    {

        public async Task<bool> BulkAdd(List<int> parkyWalletIds, int value)
        {
            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();
            var result = await connection.ExecuteAsync(ParkyWalletQueries.BulkAssign, new { ParkyWalletIds = parkyWalletIds, Amount = value });
            return true;
        }

        public async Task<ParkyWallet> Create(int regestryValue)
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                var result = await dbConnection.ExecuteScalarAsync<int>(ParkyWalletQueries.CreateWallet + " SELECT SCOPE_IDENTITY();", new { Amount = regestryValue });
                ParkyWallet parkyWallet = new()
                {
                    Id = result,
                    CurrentBalance = regestryValue,
                };
                return parkyWallet;
            }
        }


        public async Task<ParkyWallet> GetParkyWalletByUsername(string username)
        {

            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                Dictionary<int, ParkyWallet> walletDictionary = new Dictionary<int, ParkyWallet>();


                var result = await dbConnection.QueryAsync<ParkyWallet, ParkyWalletMovements, ParkyWallet>(
                    ParkyWalletQueries.GetParkyWalletByUserName,
                    (wallet, movement) =>
                    {
                        if (!walletDictionary.TryGetValue(wallet.Id, out var existingWallet))
                        {
                            existingWallet = wallet;
                            existingWallet.Movements = new List<ParkyWalletMovements>();
                            walletDictionary.Add(existingWallet.Id, existingWallet);
                        }

                        if (movement != null)
                        {
                            existingWallet.Movements.Add(movement);
                        }

                        return existingWallet;
                    },
                    new { Username = username },
                    splitOn: "MovementId"
                );

                return result.FirstOrDefault();

            }

            
        }

        public async Task UpdateCurrentBalance(int customerParkyWalletId, decimal amount)
        {

            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {

                // Update the balance in the database
                await dbConnection.ExecuteAsync(
                "UPDATE ParkyWallet SET Amount = @NewBalance WHERE Id = @ParkyWalletId",
                new { NewBalance = amount, ParkyWalletId = customerParkyWalletId });
            }

        }
    }

}
