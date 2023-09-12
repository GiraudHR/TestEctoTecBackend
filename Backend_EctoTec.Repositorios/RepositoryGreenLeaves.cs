using Backend_EctoTec.Core.Entities;
using Backend_EctoTec.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend_EctoTec.Repositorios
{
    public class RepositoryGreenLeaves : IRepositoryGreenLeaves
    {
        public List<Address> FilterAddress(string addressF)
        {
            List<Address> filter = new List<Address>();
			try
			{
				List<Address> addresses = new List<Address>{
					new Address
					{
						Id = 1,
						AddressFull = "Monterrey, Nuevo León, México"
					},
                    new Address
                    {
                        Id = 2,
                        AddressFull = "Monterrey, Bío-Bío, Chile"
                    },
                    new Address
                    {
                        Id = 3,
                        AddressFull = "México"
                    },
                    new Address
                    {
                        Id = 4,
                        AddressFull = "Baja California"
                    },
                    new Address
                    {
                        Id = 5,
                        AddressFull = "Monterrey, Asturias, Spain"
                    },
                    new Address
                    {
                        Id = 6,
                        AddressFull = "Monterrey, Tabasco, México"
                    },
                    new Address
                    {
                        Id = 6,
                        AddressFull = "Ciudad de Mexico, Iztapala, México"
                    },
                };
                filter = addresses.Where(address => address.AddressFull.Contains(addressF, StringComparison.OrdinalIgnoreCase)).ToList();
				return filter;
            }
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
        }
    }
}
