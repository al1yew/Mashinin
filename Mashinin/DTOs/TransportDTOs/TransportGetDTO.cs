using Mashinin.DTOs.CityDTOs;
using Mashinin.DTOs.ColorDTOs;
using Mashinin.DTOs.MakeDTOs;
using Mashinin.DTOs.ModelDTOs;

namespace Mashinin.DTOs.TransportDTOs
{
    public class TransportGetDTO
    {
        public int Id { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public string UpdatedAt { get; set; }
        public string DeletedAt { get; set; }
        public string CreatedAt { get; set; }


        //relations
        //make
        public int MakeId { get; set; }
        public int MakeTurboAzId { get; set; }
        public string MakeName { get; set; }
        //model
        public int ModelId { get; set; }
        public int ModelTurboAzId { get; set; }
        public string ModelName { get; set; }
        public string ModelClass { get; set; }
        //city
        public int CityId { get; set; }
        public string CityNameAz { get; set; }
        public string CityNameRu { get; set; }
        public string CityNameEn { get; set; }
        //color
        public int ColorId { get; set; }
        public string ColorHexCode { get; set; }
        public string ColorNameAz { get; set; }
        public string ColorNameEn { get; set; }
        public string ColorNameRu { get; set; }


        public int ViewCount { get; set; }
        public int ImagesFolderId { get; set; }
        public List<PriceGetDTO> Prices { get; set; }
        public List<TransportImageGetDTO> TransportImages { get; set; }
        public string FrontImage { get; set; } //foto pered
        public string FrontImageLowResolution { get; set; } //foto pered - low resolution
        public string RearImage { get; set; } //foto zadnica
        public string RearImageLowResolution { get; set; } //foto zadnica - low resolution
        //user is salon / is person


        public int AdType { get; set; } //arenda, prodaja, order


        //if rent:
        public int? PeriodOfTime { get; set; } //1-30 days

        //if order:
        public string ValidUntil { get; set; }
        public string ValidFrom { get; set; }


        //details
        public int Year { get; set; } //year, budet select option gde user viberet. Array.from sdelaem, s 1900 do nineshneqo sdelayem
        public double Odometer { get; set; } //tut vse yasno, km ili mi, mi v backende convertiruem v km, i v db idet kak km!
        public int? OwnersCount { get; set; } // kolvo vladelcev
        public int PersonPlacesCount { get; set; } // kolvo mest v mashine, 1-8, i bolshe chem 8, dalda ortani saymagnan biryerde, info goymag
        public string Vin { get; set; } //vin est vin
        public string Description { get; set; } // user sam napishet, pust xot v trex yazikax pishet, nam pofiq
        public int EngineVolume { get; set; } // ms v kube, obyem dvigatela
        public int EnginePower { get; set; } // loshadinie sili
        public string KeyWords { get; set; } // tegi, keywordi, chel sam dobavit, dla poiska
        public string MetaDatas { get; set; } //meta tegi dla nextjs
        public double? PriceForRepair { get; set; } // esli is damaged, bunu yazmalidi
        public double? FuelConsumptionAverage { get; set; } //100 kilometre yediyi benzin ortalama
        public double? FuelConsumptionCity { get; set; } //100 kilometre yediyi benzin sheherde
        public double? FuelConsumptionHighway { get; set; } //100 kilometre yediyi benzin trasda


        //enums
        public int FuelType { get; set; } //all fuel types, hybrid, etc ; na moto tolko benzin dizel elektrik
        public int DrivingWheels { get; set; } // vedushie kolesa, perednie, zadnie, ili all wheel; na moto tolko arxa/tam bivayet
        public int? ExporterCountry { get; set; } // hansi bazardan gelib ve ya gelecek / resmi dilerden alinib
        public int TransmissionType { get; set; } // korobka peredach // na moto avto/mexanik/yari avtomat - mojet dobavit shto to dla elektrokarov?
        public int BodyType { get; set; } //sedan kupe kabrio zad
        public int? HeadlightType { get; set; } // tipi far -- proverit ix!!! kakei eshe bivayut?



        //booleans --- dobavit kakie to eshe
        public bool IsNew { get; set; } // mashin yenidir ya yox
        public bool IsCredit { get; set; } //kreditnen verilir ya yox
        public bool IsBarter { get; set; } // barter olunur ya yox
        public bool IsDamaged { get; set; } // vurulub ya yox, esli damaged, on napishet cenu za pocinku +-
        public bool IsRepainted { get; set; } // kraskalanib ya yox
        public bool IsForParts { get; set; } // o geder pis gundedi ki, zapcast kimi satilir
        public bool IsImported { get; set; } //xarici bazardan gelen mashin - lazimdi? i tak ukajet stranu otkuda mashina
        public bool ImportTaxesPaid { get; set; } //esli importirovano, gomruk odenilib ya yox. esli yox, frontda hesablayib yazmag rusumu. Esli vibran yerli dilerden alinib, bu true olsun, ashagida olan IsImported=false olsun
        public bool IsForBargain { get; set; } //torq umesten ili net
        public bool IsRunning { get; set; } //otur sur yoxsa ubitaya?


        public bool IsVip { get; set; } //vip olanda
        public string VipExpireDate { get; set; } //esli vip, nado obazatelno sdelat expire date
                                                  //mi pramo na stranicke dobavleniya budem govorit useru vistavit vip, premium, ili sade. vne zavisimosti ot togo shto on viberet,
                                                  //mi vse ravno sozdadim elan. no, esli on ne oplatit, vip/premium olmayacag, tipa bank sehifesinnen obratno qayidsa
        public bool IsPremium { get; set; }
        public Nullable<DateTime> PremiumExpireDate { get; set; } //esli premium, nado obazatelno sdelat expire date

        #region booleanOptionsOfCar

        public bool HasAlloyWheels { get; set; } //legkosplavnie diski
        public bool HasGasCylinder { get; set; } //gaz balon avadanliglari var? esli vibran Qaz, avtomatik set olunsun kak true
        public bool HasFoggyHeadlights { get; set; } //protivotumanki
        public bool HasBluetooth { get; set; } //blutuz 
        public bool HasScreen { get; set; } //ekran
        public bool Has360Camera { get; set; } //360 kamera
        public bool HasElectronicWindows { get; set; } //avto stekla podnimateli
        public bool HasTrunkReleaseButton { get; set; } //otkritie bagajnika s knopki
        public bool HasUSB { get; set; } // usb 
        public bool HasABS { get; set; } // abs
        public bool HasSunroof { get; set; } //lyuk
        public bool HasRainSensor { get; set; } //datcik dojda
        public bool HasCentralLocking { get; set; } // acarnan acmag olur mashini
        public bool HasParkingRadar { get; set; } //parktronik
        public bool HasAirConditioning { get; set; } //kondicioner
        public bool HasSeatHeating { get; set; } //podogrev sideniy
        public bool HasLeatherSalon { get; set; } //koja salon
        public bool HasRearViewCamera { get; set; } //zadnaya kamera
        public bool HasSideCurtains { get; set; } // shtorki
        public bool HasSeatVentilation { get; set; } //ventilaciya sideniy
        public bool HasCruiseControl { get; set; } //kruiz
        public bool HasTractionControl { get; set; } //traction control
        public bool HasAppleCarPlay { get; set; } //car play
        public bool HasAndroidAuto { get; set; } //android auto
        public bool HasAUX { get; set; } //aux
        public bool HasPanoramaRoof { get; set; }//panarama
        public bool HasKeylessGo { get; set; }// acarsiz mashini acmag 
        public bool HasStartStop { get; set; } //start stop knopka
        public bool HasMemoryPackage { get; set; } //memori paket var ya yox
        public bool HasHandGearChange { get; set; }//+- za rulem peredaci pereklucat
        public bool HasWirelessCharging { get; set; } // zaradka dla telefona tipa
        public bool HasParkingAssistant { get; set; } //avtoparkovka eden var ya yox
        public bool HasIsofix { get; set; } //dla detskix sideniy
        public bool HasPneumaticSuspension { get; set; } //pnevmasi var ya yox
        public bool HasHook { get; set; } //kryuk na zadnem bampere

        #endregion
    }
}
