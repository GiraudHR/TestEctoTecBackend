using Backend_EctoTec.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_EctoTec.Core.Interfaces.Services
{
    public interface IServiceGreenLeaves
    {
        List<Address> GetAddress(string address);

    }
}
