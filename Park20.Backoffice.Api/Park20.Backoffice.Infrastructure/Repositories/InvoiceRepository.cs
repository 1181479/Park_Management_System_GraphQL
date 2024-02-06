using Microsoft.Extensions.Configuration;
using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.Payment;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.DatabaseManagement.Sql.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Park20.Backoffice.Infrastructure.Repositories
{
    public class InvoiceRepository(IConfiguration configuration) : IInvoiceRepository
    {
        public async Task<Invoice> CreateInvoice(Invoice invoice, string licensePlate)
        {
            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();

            var result = await connection.ExecuteAsync(InvoiceQueries.CreateInvoice,
                new { LicensePlate = licensePlate, invoice.TotalCost, invoice.IsPayed });

            if (result > 0)
                return invoice;

            return default;



        }
    }
}
