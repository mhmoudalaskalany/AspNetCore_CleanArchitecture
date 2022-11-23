using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;

namespace Common.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class DbContextExtension
    {
        public static long GetNextSequenceValue(this DbContext context, string name, string schema = null)
        {
            var sqlGenerator = context.GetService<IUpdateSqlGenerator>();
            var sql = sqlGenerator.GenerateNextSequenceValueOperation(name, schema ?? context.Model.GetDefaultSchema());
            var rawCommandBuilder = context.GetService<IRawSqlCommandBuilder>();
            var command = rawCommandBuilder.Build(sql);
            var connection = context.GetService<IRelationalConnection>();
            var logger = context.GetService<IDiagnosticsLogger<DbLoggerCategory.Database.Command>>();
            var parameters = new RelationalCommandParameterObject(connection, null, null, context, null);
            var result = command.ExecuteScalar(parameters);
            return Convert.ToInt64(result, CultureInfo.InvariantCulture);
        }
    }
}
