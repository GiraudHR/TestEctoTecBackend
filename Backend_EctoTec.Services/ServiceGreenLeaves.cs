using Backend_EctoTec.Core.Entities;
using Backend_EctoTec.Core.Interfaces.Repositories;
using Backend_EctoTec.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Backend_EctoTec.Services
{
    public class ServiceGreenLeaves : IServiceGreenLeaves
    {
		private readonly IRepositoryGreenLeaves _repoGreenLeaves;

        public ServiceGreenLeaves(IRepositoryGreenLeaves repoGreenLeaves)
		{
			_repoGreenLeaves = repoGreenLeaves;
        }

        public List<Address> GetAddress(string address)
        {
			try
			{
				return _repoGreenLeaves.FilterAddress(address);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
        }

    }
}
