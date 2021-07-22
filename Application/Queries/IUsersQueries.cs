namespace API.Innovation.Queries
{
    using API.Innovation.DGT.Infrastructure.Models;
    using API.Innovation.Infrastructure.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IUsersQueries" />.
    /// </summary>
    public interface IUsersQueries
    {
        /// <summary>
        /// The GetTopUser.
        /// </summary>
        /// <param name="connection">The connection<see cref="string"/>.</param>
        /// <param name="user">The user<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{Users}}"/>.</returns>
        Task<IEnumerable<Users>> GetTopUserAsync(string connection, int user);

        /// <summary>
        /// The GetHistoryUser.
        /// </summary>
        /// <param name="connection">The connection<see cref="string"/>.</param>
        /// <param name="dni">The dni<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{Users}}"/>.</returns>
        Task<IEnumerable<HistoryUserModel>> GetHistoryUser(string connection, string dni);
    }
}
