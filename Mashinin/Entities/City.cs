﻿namespace Mashinin.Entities
{
    public class City : BaseEntity
    {
        public string NameAz { get; set; }
        public string NameEn { get; set; }
        public string NameRu { get; set; }

        public List<Transport> Transports { get; set; }

    }
}

