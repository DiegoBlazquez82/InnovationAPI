namespace API.Innovation.Application.Command
{
    using API.Innovation.Infrastructure.Models;
    using MediatR;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines the <see cref="UpdateUserCommand" />.
    /// </summary>
    [DataContract]
    public class UpdateUserCommand : IRequest<bool>
    {
        /// <summary>
        /// Defines the Users.
        /// </summary>
        public Users Users;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserCommand"/> class.
        /// </summary>
        public UpdateUserCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserCommand"/> class.
        /// </summary>
        /// <param name="users">The users<see cref="Users"/>.</param>
        public UpdateUserCommand(Users users) : this()
        {
            Users = users;
        }
    }
}
