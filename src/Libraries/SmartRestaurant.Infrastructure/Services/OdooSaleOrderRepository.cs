﻿using Microsoft.Extensions.Options;
using SmartRestaurant.Application.Common.Interfaces;
using SmartRestaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OdooRpc.CoreCLR.Client.Models;
using OdooRpc.CoreCLR.Client;



namespace SmartRestaurant.Infrastructure.Services
{
    public class Odoo
    {
        public string Url { get; set; }
        public string Db { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }

    public class OdooSaleOrderRepository : ISaleOrderRepository
    {
        readonly string _url;
        readonly string _db;
        readonly string _username;
        readonly string _password;
        readonly OdooRpcClient _client;




        public OdooSaleOrderRepository(IOptions<Odoo> conf)
        {
            _url = conf.Value.Url;
            _db = conf.Value.Db;
            _username = conf.Value.Username;
            _password = conf.Value.Password;
            var odooConnectionInfo = new OdooConnectionInfo
            {
                Host = _url,
                Database = _db,
                Username = _username,
                Password = _password
            };
            _client = new OdooRpcClient(odooConnectionInfo);
            

        }



        public async Task<long> CreateAsync(Order order)
        {
            await _client.Authenticate();

            var saleOrderDict = new Dictionary<string, object>
        {
                {"name", order.OrderId.ToString() },
                { "date_order", order.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")},
                {"session_id", 1},
                { "amount_total", order.TotalToPay},
                {"amount_tax", 0.0},
                {"amount_paid",order.TotalToPay},
                {"amount_return",0.0},
                {"pos_reference",order.OrderId.ToString()},

    };        // create order odoo




            long saleOrderId = await _client.Create("pos.order", saleOrderDict); // send order to odoo

            if (saleOrderId == 0)
            {
                throw new Exception("Failed to create sales order in Odoo");
            }
            var orderLineList = new List<Dictionary<string, object>>();

            if (order.Dishes.Count > 0)
                foreach (var dishLine in order.Dishes)
                {
                    var orderLineDict = new Dictionary<string, object>
            {
                 { "order_id", saleOrderId },
                { "full_product_name",dishLine.Name},
                { "qty", dishLine.Quantity },
                { "price_unit", dishLine.UnitPrice },
                { "discount", 0.0 },
                {"product_id",1 },
                { "price_subtotal", dishLine.UnitPrice*dishLine.Quantity },
                 { "price_subtotal_incl", dishLine.UnitPrice*dishLine.Quantity }

            };

                    await _client.Create("pos.order.line", orderLineDict); // add dish in odoo order

                }

            if (order.Products.Count > 0)

                foreach (var productLine in order.Products)
                {
                    var orderLineDict = new Dictionary<string, object>
            {
                { "order_id", saleOrderId },
                { "full_product_name",productLine.Name },
                { "qty", productLine.Quantity },
                { "price_unit", productLine.UnitPrice } ,
                { "discount", 0.0 },
                { "price_subtotal", productLine.UnitPrice*productLine.Quantity },
                 { "price_subtotal_incl",  productLine.UnitPrice*productLine.Quantity },
                 {"product_id",1 }


            };
                    await _client.Create("pos.order.line", orderLineDict); // add product in odoo order
                }



            return saleOrderId;
        }
    }





}
