﻿namespace Mashinin.Entities
{
    public class Model : BaseEntity
    {
        public string Name { get; set; }
        public int TurboAzId { get; set; }


        public Make Make { get; set; }
        public int MakeId { get; set; }
    }
}