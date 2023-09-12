using Backend_EctoTec.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_EctoTec.Core.Interfaces.Repositories
{
    public interface IRepositoryGreenLeaves
    {
        List<Address> FilterAddress(string address);
    }
}
