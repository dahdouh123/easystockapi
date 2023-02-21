﻿using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartRestaurant.Application.Reclamation.Queries
{
    public class GetAllReclamationListQuery: IPagedListQuery<ReclamationDto>
    {

        public string HotelId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SearchKey { get; set; }
        public string SortOrder { get; set; }
        public string CurrentFilter { get; set; }
    }
}
