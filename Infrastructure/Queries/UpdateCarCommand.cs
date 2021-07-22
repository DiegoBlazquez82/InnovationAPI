﻿using API.Innovation.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace API.Innovation.Application.Command
{
    [DataContract]
    public class UpdateCarCommand : IRequest<bool>
    {

        /// <summary>
        /// Gets or sets the Matricula.
        /// </summary>
        public Cars Car { get; set; }

        public UpdateCarCommand() {}

        public UpdateCarCommand(Cars car) : this()
        {
            Car = car;
        }
    }
}
