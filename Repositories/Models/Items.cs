using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repositories.Models
{
    public class Items
    {
        int id;
        string name;
        Owner owner;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public Owner Owner { get => owner; set => owner = value; }
    }
}