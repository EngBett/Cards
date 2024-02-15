using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Cards.Infrastructure.DataAccess.Extension
{
    public static class ApiContextExtension
    {

        public static async Task<int> NextValueForSequence(this AppDbContext pCtx, Sequence pSequence)
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            var sequenceIdentifier = pSequence.GetType()
                        .GetMember(pSequence.ToString())
                        .First()
                        .GetCustomAttribute<DescriptionAttribute>()
                        ?.Description;
            await pCtx.Database.ExecuteSqlRawAsync($"SELECT @result = (NEXT VALUE FOR [{sequenceIdentifier}])", result);
            return (int)result.Value;
        }

    }
}
