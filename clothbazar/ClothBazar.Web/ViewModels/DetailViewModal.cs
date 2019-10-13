using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClothBazar.Entity;
using ClothBazar.Web.Models;
namespace ClothBazar.Web.ViewModels
{
    public class DetailViewModal
    {
        public Orders order { get; set; }
        public ApplicationUser user { get; set; }
        public List<string> AvailableStatus { get; set; }
    }
}