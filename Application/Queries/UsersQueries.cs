namespace API.Innovation.Queries
{
    using API.Innovation.DGT.Infrastructure.Models;
    using API.Innovation.Infrastructure.Models;
    using Dapper;
    using Microsoft.Data.Sqlite;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="UsersQueries" />.
    /// </summary>
    public class UsersQueries : IUsersQueries
    {
        /// <summary>
        /// The GetTopUser.
        /// </summary>
        /// <param name="connection">The connection<see cref="string"/>.</param>
        /// <param name="limit">The limit<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{Users}}"/>.</returns>
        public async Task<IEnumerable<Users>> GetTopUserAsync(string connection, int limit)
        {

            using (var query = new SqliteConnection("Data Source=" + connection))
            {
                query.Open();
                var result = await query.QueryAsync<Users>(
                   @"select *
                        FROM Users
                        LIMIT @limit"
                        , new { limit }
                    );

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return result;
            }
        }

        /// <summary>
        /// The GetTopUser.
        /// </summary>
        /// <param name="connection">The connection<see cref="string"/>.</param>
        /// <param name="dni">The dni<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{Users}}"/>.</returns>
        public async Task<IEnumerable<HistoryUserModel>> GetHistoryUser(string connection, string dni)
        {
            using (var query = new SqliteConnection("Data Source=" + connection))
            {
                query.Open();
                var result = await query.QueryAsync<HistoryUserModel>(
                   @"select u.DNI, 
                            u.Name,
                            u.SubName,
                            i.Description, 
                            c.Matricula, 
                            c.Marca, 
                            c.Modelo                        
                        FROM VehicleViolation vv 
                            JOIN Users u On (vv.IdUser = u.IdUser)
                            JOIN Infraction i on (vv.IdInfraction = i.IdInfraction)
                      	    JOIN Cars c on (c.DNI = u.DNI And c.CarId = vv.IdVehicle)
                        WHERE u.DNI = @dni"
                        , new { dni }
                    );

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return result;
            }
        }
    }
}
