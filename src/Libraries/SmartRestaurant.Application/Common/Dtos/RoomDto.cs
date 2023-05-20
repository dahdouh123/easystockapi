﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmartRestaurant.Application.Common.Dtos
{
	public class RoomDto 
	{
	
		  public Guid Id { get; set; }
		  public Guid BuildingId { get; set; }
		  public Guid ClientId { get; set; }
		  public int  RoomNumber { get; set; }
		  public int  FloorNumber { get; set; }
		  public int NumberOfBeds { get; set; }
		  public string ClientEmail { get; set; }
		  public bool IsBooked { get; set; }
		  public int Price { get; set; }
          public bool AvailableForCheckin { get; set; }
          public DateTime StartDayOfCheckin { get; set; }
          public int DurationOfCheckin { get; set; }
          public bool Cleaned { get; set; }
          public DateTime DateCheckout { get; set; }
    }  
}
