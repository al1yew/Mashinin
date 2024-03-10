namespace Mashinin.Entities
{
    public class Part : BaseEntity
    {
        //obshak
        public string Heading { get; set; } //max 30 words
        public string PersonalNumber { get; set; }
        public bool IsNew { get; set; }
        public bool HasWarranty { get; set; }
        public bool IsOriginal { get; set; }
        public double Price { get; set; }
        public int Currency { get; set; }
        public bool IsForBargain { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string KeyWords { get; set; }
        public string MetaData { get; set; }
        public int ImagesFolderId { get; set; }
        public string MainImage { get; set; }
        public int ViewCount { get; set; }


        public int PartCategory { get; set; } //na osnovanii enum-a budem sozdavat, znaceniya enuma ne budut menatsa
        public List<PartImage> PartImages { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
        public PartSpecification PartSpecification { get; set; } //brand or category
        public int PartSpecificationId { get; set; }


        //battery
        public DateTime ManufactureDate { get; set; }
        public string Dimensions { get; set; } //dto-dan razmerler gelsin, biz set edek databazaya kak string s dimensionami
        public double Capacity { get; set; } // Capacity of the battery in Ah 40, 50, 60 itd, naydu ix
        public int ColdCrankingAmps { get; set; } // Cold cranking amps (CCA) rating (CCA, CA, MCA, HCA) - https://en.wikipedia.org/wiki/Automotive_battery
        public int GroupSize { get; set; } // neznayu gde ix nayti voobshe, nado je useru kak to obyasnit gde vzat eti cifri bukvi - https://raybuck.com/automotive-battery-dimensions/


        //tyre
        public bool IsRunFlat { get; set; } // info qoymag
        public bool HasThorns { get; set; } //shipi est ili net
        public int Season { get; set; } //winter, summer, all-season - enums navernaka
        public string Code { get; set; } // DTOdan gelecek, men yigacam ve save edecem bazaya


        //rim
        public int BoltCount { get; set; } // skolko boltov derjit disk
        public int Color { get; set; } // Color of the rim, bazovie cveta zadat tupo
        public double Size { get; set; } // in inches, 16 17 zad olan shey.
        public int Material { get; set; } // Material of the rim (e.g., steel, alloy) - mojno dobavit steel, aluminium, carbon fiber
        public int SpokeCount { get; set; } // kolvo спиц v diske
    }
}
