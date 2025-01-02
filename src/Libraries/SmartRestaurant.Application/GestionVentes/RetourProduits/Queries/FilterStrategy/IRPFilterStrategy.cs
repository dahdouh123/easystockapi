﻿using Microsoft.EntityFrameworkCore;
using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Common.Extensions;
using SmartRestaurant.Application.GestionVentes.VenteComptoir.Queries;
using SmartRestaurant.Application.GestionVentes.VenteParFac.Queries;
using SmartRestaurant.Application.Stock.Queries;
using SmartRestaurant.Domain.Entities;
using System;

namespace SmartRestaurant.Application.GestionVentes.RetourProduits.Queries.FilterStrategy
{
    public interface IRPFilterStrategy
    {     
        public PagedResultBase<Domain.Entities.RetourProduitClient> FetchData(DbSet<Domain.Entities.RetourProduitClient> bon,GetListOfRetourProduitsQuery request);
       

    }

}

