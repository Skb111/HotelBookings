﻿using HotelBookings.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookings.Application.Common.Interfaces
{
	public interface IVillaNumberRepository : IRepository<VillaNumber>
	{
		void Update(VillaNumber entity);

	}
}
