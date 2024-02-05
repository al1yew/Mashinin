namespace Mashinin.Entities
{
    public class Car : BaseEntity // car? a mojet transport? vehicle?
    {
        //relations
        public Make Make { get; set; }
        public int MakeId { get; set; }
        public Model Model { get; set; }
        public int ModelId { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }


        public int Year { get; set; } // nujno mne eto?
        public double Odometer { get; set; } // nujno mne eto?
        public int OwnersCount { get; set; } // kolvo vladelcev
        public int PersonPlacesCount { get; set; }
        public string Vin { get; set; }
        public string Description { get; set; }

        //booleans
        public bool IsNew { get; set; }
        public bool IsCredit { get; set; }
        public bool IsBarter { get; set; }
        public bool IsDamaged { get; set; }
        public bool IsRepainted { get; set; }
        public bool IsForParts { get; set; }

        public bool HasAlloyWheels { get; set; }
        public bool HasABS { get; set; }
        public bool HasSunroof { get; set; }
        public bool HasRainSensor { get; set; }
        public bool HasCentralLocking { get; set; }
        public bool HasParkingRadar { get; set; }
        public bool HasAirConditioning { get; set; }
        public bool HasSeatHeating { get; set; }
        public bool HasLeatherSalon { get; set; }
        public bool HasXenonLamps { get; set; }
        public bool MyProperty { get; set; }
        public bool HasRearViewCamera { get; set; }
        public bool HasSideCurtains { get; set; }
        public bool HasSeatVentilation { get; set; }

        //main image - gabagdan
        //hover image - daldan
        //images - maks 15 dene




        //user
        //prices history, we must save prices history
        //valutu toje nado vibirat kak to
        //ban novu sozdat object
        //toplivo kakoe
        //vedushie kolesa
        //probeg v cifrax
        //probeg znacnie - milya ili kilometr
        //price otdelniy obyekt olsun, tak kak istoriyasi olacag, icinde amount ve currencyname
        //probeg ozu ozune olsun
        //probegin extensionu - ne nujno dla probega otdelniy entity sozdavat
        //km ml azn eur usd frontda ozumuz hell ederik, ki user bashqasini select ede bilmesin, fluentdede yoxlayariq shto b prixodili tolko eti


    }
}
