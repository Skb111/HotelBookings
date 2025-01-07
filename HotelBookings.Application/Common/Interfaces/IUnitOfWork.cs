using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookings.Application.Common.Interfaces
{
	public interface IUnitOfWork
	{
		IVillaRepository Villa { get; }
		IVillaNumberRepository VillaNumber { get; }
        IAmenityRepository AmenityRep { get; }
        void Save();

	}
}
