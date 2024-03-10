using FluentValidation;
using Mashinin.Entities;
using Mashinin.Enums;
using Mashinin.Localization;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;

namespace Mashinin.DTOs.TransportDTOs
{
    public class TransportCreateDTO
    {
        //relations
        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public int CityId { get; set; }
        public int ColorId { get; set; }
        public List<IFormFile> Photos { get; set; }
        public IFormFile RearPhoto { get; set; }
        public IFormFile FrontPhoto { get; set; }
        //user


        public int AdType { get; set; } //arenda, prodaja, zakaz


        //if rent:
        public int PeriodOfTime { get; set; } //1-30 days

        //if order:
        public Nullable<DateTime> ValidUntil { get; set; }
        public Nullable<DateTime> ValidFrom { get; set; }


        //price related
        public double Price { get; set; }
        public int Currency { get; set; } //enum

        //esli mashina privoznaya
        public double WinPriceMin { get; set; } //mashinin udush qiymeti OT
        public double WinPriceMax { get; set; } //mashinin udush qiymeti DO
        public double? TransportationPrice { get; set; } //dashinma xercleri


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
        public Nullable<DateTime> VipExpireDate { get; set; } //esli vip, nado obazatelno sdelat expire date
                                                              //mi pramo na stranicke dobavleniya budem govorit useru vistavit vip, premium, ili sade. vne zavisimosti ot togo shto on viberet,
                                                              //mi vse ravno sozdadim elan. no, esli on ne oplatit, vip/premium olmayacag, tipa bank sehifesinnen obratno qayidsa



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

    public class TransportCreateDTOValidator : AbstractValidator<TransportCreateDTO>
    {
        public TransportCreateDTOValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            RuleFor(x => x.MakeId)
                .NotEmpty().WithMessage(x => stringLocalizer["makeRequired"]);

            RuleFor(x => x.ModelId)
                .NotEmpty().WithMessage(x => stringLocalizer["modelRequired"]);

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage(x => stringLocalizer["cityRequired"]);

            RuleFor(x => x.ColorId)
                .NotEmpty().WithMessage(x => stringLocalizer["colorRequired"]);

            RuleFor(x => x.Photos)
                .NotEmpty().WithMessage(x => stringLocalizer["photosRequired"]);

            RuleFor(x => x.RearPhoto)
                .NotEmpty().WithMessage(x => stringLocalizer["rearPhotoRequired"]);

            RuleFor(x => x.FrontPhoto)
                .NotEmpty().WithMessage(x => stringLocalizer["frontPhotoRequired"]);

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage(x => stringLocalizer["currencyRequired"]);

            RuleFor(x => x.AdType)
                .NotEmpty().WithMessage(x => stringLocalizer["adTypeRequired"])
                .LessThanOrEqualTo(3).WithMessage(x => stringLocalizer["adTypeRequired"]);

            RuleFor(x => x.Year)
                .NotEmpty().WithMessage(x => stringLocalizer["yearRequired"])
                .GreaterThan(1900).WithMessage(x => stringLocalizer["yearLess1900"])
                .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage(x => stringLocalizer["yearMoreToday"]);

            RuleFor(x => x.Odometer)
                .NotEmpty().WithMessage(x => stringLocalizer["odometerRequired"])
                .GreaterThanOrEqualTo(0).WithMessage(x => stringLocalizer["odometerMore0"]);

            RuleFor(x => x.PersonPlacesCount)
                .NotEmpty().WithMessage(x => stringLocalizer["personPlacesCountRequired"])
                .GreaterThanOrEqualTo(1).WithMessage(x => stringLocalizer["personPlacesCountMore0"]);

            RuleFor(x => x.Description)
                .MaximumLength(2048).WithMessage(x => stringLocalizer["descriptionMaxLength"]);

            RuleFor(x => x.EngineVolume)
                .NotEmpty().WithMessage(x => stringLocalizer["engineVolumeRequired"])
                .GreaterThanOrEqualTo(0).WithMessage(x => stringLocalizer["engineVolumeMore0"]);

            RuleFor(x => x.EnginePower)
                .NotEmpty().WithMessage(x => stringLocalizer["enginePowerRequired"])
                .GreaterThanOrEqualTo(0).WithMessage(x => stringLocalizer["enginePowerMore0"]);

            RuleFor(x => x.FuelType)
                .NotEmpty().WithMessage(x => stringLocalizer["fuelTypeRequired"])
                .GreaterThan(0).WithMessage(x => stringLocalizer["fuelTypeMore0"]);

            RuleFor(x => x.DrivingWheels)
                .NotEmpty().WithMessage(x => stringLocalizer["drivingWheelsRequired"])
                .GreaterThan(0).WithMessage(x => stringLocalizer["drivingWheelsMore0"]);

            RuleFor(x => x.TransmissionType)
                .NotEmpty().WithMessage(x => stringLocalizer["transmissionTypeRequired"])
                .GreaterThan(0).WithMessage(x => stringLocalizer["transmissionTypeMore0"]);

            RuleFor(x => x.BodyType)
             .NotEmpty().WithMessage(x => stringLocalizer["bodyTypeRequired"])
                .GreaterThan(0).WithMessage(x => stringLocalizer["bodyTypeMore0"]);

            RuleFor(x => x.PersonPlacesCount)
            .LessThanOrEqualTo(3)
            .When(x => x.BodyType == (int)BodyTypes.Motorcycle)
            .WithMessage(x => stringLocalizer["placesForMoto"]);

            RuleFor(t => t.HasGasCylinder)
            .Equal(true)
            .When(t => t.FuelType == (int)FuelTypes.gas)
            .WithMessage(x => stringLocalizer["gasHasCylinder"]);

            When(x => (AdTypes)x.AdType == AdTypes.ForRent, () =>
            {
                RuleFor(x => x.Price)
                .NotEmpty().WithMessage(x => stringLocalizer["priceRequired"])
                .GreaterThan(0).WithMessage(x => stringLocalizer["priceRequired"]);

                RuleFor(x => x.PeriodOfTime)
                .NotEmpty().WithMessage(x => stringLocalizer["periodOfTimeRequired"])
                .GreaterThanOrEqualTo(1).WithMessage(x => stringLocalizer["periodOfTimeMore1"])
                .LessThanOrEqualTo(30).WithMessage(x => stringLocalizer["periodOfTimeLess30"]);
            });

            When(x => (AdTypes)x.AdType == AdTypes.ForSale, () =>
            {
                When(x => x.IsDamaged, () =>
                {
                    RuleFor(x => x.PriceForRepair)
                    .NotEmpty().WithMessage(x => stringLocalizer["priceForRepairRequired"])
                    .GreaterThan(0).WithMessage(x => stringLocalizer["priceForRepairMore0"]);
                });

                RuleFor(x => x.Price)
                  .NotEmpty().WithMessage(x => stringLocalizer["priceRequired"])
                  .GreaterThan(0).WithMessage(x => stringLocalizer["priceRequired"]);

                RuleFor(x => x.ExporterCountry)
                .NotEmpty().WithMessage(x => stringLocalizer["exporterCountryRequired"])
                .GreaterThanOrEqualTo(1).WithMessage(x => stringLocalizer["exporterCountryMore0"]);

                RuleFor(x => x.Vin)
                .NotEmpty().WithMessage(x => stringLocalizer["vinRequired"])
                .Length(17).WithMessage(x => stringLocalizer["vinLength"])
                .Matches(@"\b[A-HJ-NPR-Z0-9]{17}\b").WithMessage(x => stringLocalizer["vinMatches"]);
            });

            When(x => (AdTypes)x.AdType == AdTypes.ForOrder, () =>
            {
                RuleFor(x => x.ValidUntil)
                .NotEmpty().WithMessage(x => stringLocalizer["validityRequired"]);

                RuleFor(x => x.ValidFrom)
                .NotEmpty().WithMessage(x => stringLocalizer["validityRequired"]);

                RuleFor(x => x.WinPriceMax)
                .NotEmpty().WithMessage(x => stringLocalizer["winPriceMaxRequired"])
                .GreaterThanOrEqualTo(1).WithMessage(x => stringLocalizer["winPricesMore0"]);

                RuleFor(x => x.WinPriceMin)
                .NotEmpty().WithMessage(x => stringLocalizer["winPriceMinRequired"])
                .GreaterThanOrEqualTo(0).WithMessage(x => stringLocalizer["winPricesMore0"]);

                RuleFor(x => x.Price)
                 .NotEmpty().WithMessage(x => stringLocalizer["priceRequired"])
                 .GreaterThan(0).WithMessage(x => stringLocalizer["priceRequired"]);

                RuleFor(x => x.TransportationPrice)
                 .NotEmpty().WithMessage(x => stringLocalizer["transportationPriceRequired"])
                .GreaterThanOrEqualTo(0).WithMessage(x => stringLocalizer["transportationPriceRequired"]);

                RuleFor(x => x.ExporterCountry)
                 .NotEmpty().WithMessage(x => stringLocalizer["exporterCountryRequired"])
                 .GreaterThanOrEqualTo(1).WithMessage(x => stringLocalizer["exporterCountryMore0"]);

                RuleFor(x => x.Vin)
                .NotEmpty().WithMessage(x => stringLocalizer["vinRequired"])
                .Length(17).WithMessage(x => stringLocalizer["vinLength"])
                .Matches(@"\b[A-HJ-NPR-Z0-9]{17}\b").WithMessage(x => stringLocalizer["vinMatches"]);

                RuleFor(x => x.WinPriceMax)
                   .Must((dto, winPriceMax) => winPriceMax > dto.WinPriceMin)
                   .WithMessage(x => stringLocalizer["maxMoreMin"]);

                When(x => x.IsDamaged, () =>
                {
                    RuleFor(x => x.PriceForRepair)
                    .NotEmpty().WithMessage(x => stringLocalizer["priceForRepairRequired"])
                    .GreaterThan(0).WithMessage(x => stringLocalizer["priceForRepairMore0"]);
                });
            });
        }
    }
}
