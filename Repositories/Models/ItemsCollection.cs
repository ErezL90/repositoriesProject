using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repositories.Models
{
    public class ItemsCollection
    {
        private List<Items> items;
        public List<Items> Items { get => items; set => items = value; }
    }
}