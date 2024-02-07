namespace Mashinin.Entities
{
    public class Transport : BaseEntity
    {
        //relations
        public Make Make { get; set; }
        public int MakeId { get; set; }
        public Model Model { get; set; }
        public int ModelId { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
        public Color Color { get; set; }
        public int ColorId { get; set; }
        public int ViewCount { get; set; }
        public int ImagesFolderId { get; set; }
        //List<VehicleImages>
        //List<PriceHistory>
        //user

        //price logics
        //public class PriceHistory
        //{
        //    public int Id { get; set; }
        //    public int TransportId { get; set; } // Foreign key to Transport entity
        //    public decimal Price { get; set; }
        //    public DateTime Date { get; set; }
        //    prop Currency toje nado derjat, nam eshe nujno estimate v manatax pisat tipa ~ skolko to manat v price history
        //}

        //details
        public int Year { get; set; } //year, budet select option gde user viberet. Array.from sdelaem, s 1900 do nineshneqo sdelayem
        public double Odometer { get; set; } //tut vse yasno, km ili mi, mi v backende convertiruem v km, i v db idet kak km!
        public int OwnersCount { get; set; } // kolvo vladelcev
        public int PersonPlacesCount { get; set; } // kolvo mest v mashine, 1-8, i bolshe chem 8
        public string Vin { get; set; } //vin est vin
        public string Description { get; set; } // user sam napishet, pust xot v trex yazikax pishet, nam pofiq
        public int EngineCapacity { get; set; } // ms v kube, obyem dvigatela
        public int EnginePower { get; set; } // loshadinie sili
        public string FrontImage { get; set; } //foto pered
        public string RearImage { get; set; } //foto zadnica
        public string NumberPlate { get; set; } // nomer mashini
        public string KeyWords { get; set; } // tegi, keywordi, chel sam dobavit
        public string MetaDatas { get; set; } //meta tegi dla nextjs
        public double PriceForRepair { get; set; } // esli is damaged, bunu yazmalidi


        //enums
        public int FuelType { get; set; } //all fuel types, hybrid, benzin, etc - enum mojno? V DB poydut integeri, no mi po enumam i v fronte, i v backende, budem znat kto est kto!
        public int DrivingWheels { get; set; } // vedushie kolesa, perednie, zadnie, ili all wheel
        public int ExporterCountry { get; set; } // hansi bazardan gelib / resmi dilerden alinib
        public int TransmissionType { get; set; } // korobka peredach
        public int TransportType { get; set; } // avto, moto, bus, itd 
        public int BodyType { get; set; } //sedan kupe kabrio zad


        //booleans --- dobavit kakie to eshe
        public bool IsNew { get; set; } // mashin yenidir ya yox
        public bool IsCredit { get; set; } //kreditnen verilir ya yox
        public bool IsBarter { get; set; } // barter olunur ya yox
        public bool IsDamaged { get; set; } // vurulub ya yox, esli damaged, on napishet cenu za pocinku +-
        public bool IsRepainted { get; set; } // kraskalanib ya yox
        public bool IsForParts { get; set; } // o geder pis gundedi ki, zapcast kimi satilir
        public bool ImportTaxesPaid { get; set; } //esli importirovano, gomruk odenilib ya yox. esli yox, frontda hesablayib yazmag rusumu. Esli vibran yerli dilerden alinib, bu true olsun, ashagida olan IsImported=false olsun
        public bool IsImported { get; set; } //xarici bazardan gelen mashin - lazimdi? i tak ukajet stranu otkuda mashina
        public bool IsForBargain { get; set; } //torq umesten ili net


        //opcii mashini ---- nado dobavit eshe kakie to
        public bool HasAlloyWheels { get; set; }
        public bool HasGasCylinder { get; set; } //gaz balon avadanliglari var? esli vibran Qaz, avtomatik set olunsun kak true
        public bool HasFoggyHeadlights { get; set; }
        public bool HasBluetooth { get; set; }
        public bool HasScreen { get; set; }
        public bool Has360Camera { get; set; }
        public bool HasElectronicWindows { get; set; }
        public bool HasTrunkReleaseButton { get; set; }
        public bool HasUSB { get; set; }
        public bool HasABS { get; set; }
        public bool HasSunroof { get; set; }
        public bool HasRainSensor { get; set; }
        public bool HasCentralLocking { get; set; }
        public bool HasParkingRadar { get; set; }
        public bool HasAirConditioning { get; set; }
        public bool HasSeatHeating { get; set; }
        public bool HasLeatherSalon { get; set; }
        public bool HasXenonLamps { get; set; }
        public bool HasRearViewCamera { get; set; }
        public bool HasSideCurtains { get; set; }
        public bool HasSeatVentilation { get; set; }




        //moto props
    }
}
