﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace SmartRestaurant.Domain.Entities
{
    public class VenteComptoir
    {
        public Guid Id { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Numero { get; set; }
        public string CodeVc { get; set; }
        public DateTime Date { get; set; }
        public string Heure { get; set; }
        public int Caisse { get; set; }
        public string NomCaissier { get; set; }
        public string NomVehicule { get; set; }
        public string MatriculeVeh { get; set; }
        public string Conducteur { get; set; }
        public string ConditionDePaiment { get; set; }
        public string LieuLivraison { get; set; }
        public decimal Remise { get; set; }
        public Guid VendeurId { get; set; }
        public List<VenteComptoirProducts> VenteComptoirIncludedProducts { get; set; }
        public Guid BlId { get; set; }

        public decimal CouponPrice { get; set; }
        public DateTime DateFermuture { get; set; }
        public DateTime DateEcheance { get; set; }
        public bool IsTransformed { get; set; }
        public decimal MontantTotalHT { get; set; }
        public decimal MontantTotalHTApresRemise { get; set; }
        public decimal MontantTotalTVA { get; set; }
        public decimal MontantTotalTTC { get; set; }
        public decimal Timbre { get; set; }

        public decimal RestTotal { get; set; }
        public decimal TotalReglement { get; set; }

        public string Etat { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public decimal Benifice { get; set; }
        public long Rip { get; set; }
        public long Rib { get; set; }
        public bool IsDeleted { get; set; }
        public string PaymentMethod { get; set; }


    }
}
