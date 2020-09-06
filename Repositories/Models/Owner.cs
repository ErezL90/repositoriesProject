using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repositories.Models
{
    public class Owner
    {
        int id;
        string avatar_url;
        public int Id { get => id; set => id = value; }
        public string Avatar_url { get => avatar_url; set => avatar_url = value; }
    }
}