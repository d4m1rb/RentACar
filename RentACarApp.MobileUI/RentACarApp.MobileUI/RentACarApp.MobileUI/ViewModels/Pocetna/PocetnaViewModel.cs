using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.MobileUI.Models.Dashboard;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RentACarApp.MobileUI.ViewModels.Pocetna
{
    /// <summary>
    /// ViewModel for stock overview page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class PocetnaViewModel : BaseViewModel
    {
        private readonly APIService _rezervacijeService = new APIService("RezervacijaRentanja");
        private readonly APIService _vozilaService = new APIService("Automobil");
        private readonly APIService _kategorijaVozilaService = new APIService("KategorijaVozila");
        private readonly APIService _klijentService = new APIService("Klijent");
        private readonly APIService _korisniciUlogeService = new APIService("KorisniciUloga");

        #region Field

        /// <summary>
        /// To store the health care data collection.
        /// </summary>
        private ObservableCollection<HealthCare> rezervacijeItems;
        private ObservableCollection<HealthCare> zaradaItems;
        private ObservableCollection<HealthCare> vozilaItems;
        private ObservableCollection<HealthCare> klijentiItems;

        /// <summary>
        /// To store the health care data collection.
        /// </summary>
        private ObservableCollection<HealthCare> listItems;

        /// <summary>
        /// To store the heart rate data collection.
        /// </summary>
        private ObservableCollection<ChartDataPoint> heartRateData;

        /// <summary>
        /// To store the calories burned data collection.
        /// </summary>
        private ObservableCollection<ChartDataPoint> caloriesBurnedData;

        /// <summary>
        /// To store the sleep time data collection.
        /// </summary>
        private ObservableCollection<ChartDataPoint> sleepTimeData;

        /// <summary>
        /// To store the water consumed data collection.
        /// </summary>
        private ObservableCollection<ChartDataPoint> waterConsumedData;

        private byte[] klijentSlika;
        private int klijentId;
        private int brojvozila;
        private int brojkategorijavozila;
        private int brojrezervacija;
        private int rezervacijeOvogMjeseca;
        private int rezervacijeOveGodine;   
        private int brojKlijenata;
        private int brojKlijenataM;
        private int brojKlijenataG;
        private Image img;


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="PocetnaViewModel" /> class.
        /// </summary>
        public PocetnaViewModel()
        {
            InitCommand = new Command(async () => await Init());
            
            GetChartData();

            RezervacijeItems = new ObservableCollection<HealthCare>()
            {
                new HealthCare()
                {
                    Category = "UKUPNO REZERVACIJA",
                    CategoryValue = BrojRezervacija.ToString(),
                    
                    BackgroundGradientStart = "#e0005e",
                    BackgroundGradientEnd = "#8a033b",
                },
                new HealthCare()
                {
                    Category = "REZERVACIJE OVAJ MJESEC",
                    CategoryValue = RezervacijeOvogMjeseca.ToString(),

                    BackgroundGradientStart = "#e0005e",
                    BackgroundGradientEnd = "#8a033b",
                    //BackgroundGradientStart = "#ff7272",
                    //BackgroundGradientEnd = "#f650c5"
                },
                new HealthCare()
                {
                    Category = "REZERVACIJE OVE GODINE",
                    CategoryValue = RezervacijeOveGodine.ToString(),
                   BackgroundGradientStart = "#e0005e",
                    BackgroundGradientEnd = "#8a033b",
                    //BackgroundGradientStart = "#5e7cea",
                    //BackgroundGradientEnd = "#1dcce3"
                },
                
            };
            VozilaItems = new ObservableCollection<HealthCare>()
            {
                new HealthCare()
                {
                    Category = "UKUPNO VOZILA",
                    CategoryValue = BrojVozila.ToString(),

                    BackgroundGradientStart = "#ff7272",
                    BackgroundGradientEnd = "#f650c5"
                },
                new HealthCare()
                {
                    Category = "BR. KATEGORIJA VOZILA",
                    CategoryValue = BrojKategorijaVozila.ToString(),

                   BackgroundGradientStart = "#ff7272",
                    BackgroundGradientEnd = "#f650c5"
                    //BackgroundGradientStart = "#ff7272",
                    //BackgroundGradientEnd = "#f650c5"
                },                

            };   

            ListItems = new ObservableCollection<HealthCare>()
            {
                new HealthCare()
                {
                    Category = "Blood Pressure",
                    CategoryValue = "141/90 mmgh",
                    CategoryPercentage = "30%",
                    BackgroundGradientStart = "#cf86ff"
                },
                new HealthCare()
                {
                    Category = "Body Weight",
                    CategoryValue = "80kg",
                    CategoryPercentage = "50%",
                    BackgroundGradientStart = "#8691ff"
                },
                new HealthCare()
                {
                    Category = "Steps",
                    CategoryValue = "3463",
                    CategoryPercentage = "60%",
                    BackgroundGradientStart = "#ff9686"
                }
            };

            this.ProfileImage = App.BaseImageUrl + "ProfileImage1.png";
            this.MenuCommand = new Command(this.MenuClicked);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the profile image path.
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Gets or sets the health care items collection.
        /// </summary>
        public ObservableCollection<HealthCare> RezervacijeItems
        {
            get
            {
                return this.rezervacijeItems;
            }

            set
            {
                this.rezervacijeItems = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<HealthCare> ZaradaItems
        {
            get
            {
                return this.zaradaItems;
            }

            set
            {
                this.zaradaItems = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<HealthCare> KlijentiItems
        {
            get
            {
                return this.klijentiItems;
            }

            set
            {
                this.klijentiItems = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<HealthCare> VozilaItems
        {
            get
            {
                return this.vozilaItems;
            }

            set
            {
                this.vozilaItems = value;
                this.NotifyPropertyChanged();
            }
        }



        public Image Img
        {
            get
            {
                return this.img;
            }

            set
            {
                this.img = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the health care items collection.
        /// </summary>
        public ObservableCollection<HealthCare> ListItems
        {
            get
            {
                return this.listItems;
            }

            set
            {
                this.listItems = value;
                this.NotifyPropertyChanged();
            }
        }



        public byte[] KlijentSlika
        {
            get
            {
                return this.klijentSlika;
            }

            set
            {
                this.klijentSlika = value;
                this.NotifyPropertyChanged();
            }
        }

        public int KlijentId
        {
            get
            {
                return this.klijentId;
            }

            set
            {
                this.klijentId = value;
                this.NotifyPropertyChanged();
            }
        }
        public int BrojKlijenataM
        {
            get
            {
                return this.brojKlijenataM;
            }

            set
            {
                this.brojKlijenataM = value;
                this.NotifyPropertyChanged();
            }
        }

        public int BrojKlijenataG
        {
            get
            {
                return this.brojKlijenataG;
            }

            set
            {
                this.brojKlijenataG = value;
                this.NotifyPropertyChanged();
            }
        }
        public int BrojRezervacija
        {
            get
            {
                return this.brojrezervacija;
            }

            set
            {
                this.brojrezervacija = value;
                this.NotifyPropertyChanged();
            }
        }

        public int RezervacijeOvogMjeseca
        {
            get
            {
                return this.rezervacijeOvogMjeseca;
            }

            set
            {
                this.rezervacijeOvogMjeseca = value;
                this.NotifyPropertyChanged();
            }
        }

        public int RezervacijeOveGodine
        {
            get
            {
                return this.rezervacijeOveGodine;
            }

            set
            {
                this.rezervacijeOveGodine = value;
                this.NotifyPropertyChanged();
            }
        }

        public int BrojKlijenata
        {
            get
            {
                return this.brojKlijenata;
            }

            set
            {
                this.brojKlijenata = value;
                this.NotifyPropertyChanged();
            }
        }

        public int BrojVozila
        {
            get
            {
                return this.brojvozila;
            }

            set
            {
                this.brojvozila = value;
                this.NotifyPropertyChanged();
            }
        }

        public int BrojKategorijaVozila
        {
            get
            {
                return this.brojkategorijavozila;
            }

            set
            {
                this.brojkategorijavozila = value;
                this.NotifyPropertyChanged();
            }
        }        

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that will be executed when the menu button is clicked.
        /// </summary>
        public Command MenuCommand { get; set; }
        public Command InitCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Chart Data Collection
        /// </summary>
        /// 


        public async Task Init()
        {
            var logiraniKlijent = await _klijentService.GetById<Klijent>(KlijentId);
            KlijentSlika=logiraniKlijent.Slika;

            //REZERVACIJE
            RezervacijaRentanjaSearchRequest rezervacijeSearch = new RezervacijaRentanjaSearchRequest()
            {                
                KlijentId = logiraniKlijent.KlijentId
            };
            var rezervacijeAll = await _rezervacijeService.Get<List<RezervacijaRentanja>>(rezervacijeSearch);
           
            int rezervacijaOvajMjesec = 0, rezervacijaOveGodine = 0;

            foreach (var r in rezervacijeAll)
            {
                //Mjesecna zarada i broj rezervacija
                if (r.RezervacijaOd.Month == DateTime.Now.Month)
                {                       
                    rezervacijaOvajMjesec += 1;
                }

                //Godisnja zarada i broj rezervacija
                if (r.RezervacijaOd.Year == DateTime.Now.Year)
                {                       
                    rezervacijaOveGodine += 1;
                }                    
            }                               


            var vozilaAll= await _vozilaService.Get<List<Automobil>>(new AutomobilSearchRequest());
            BrojVozila = vozilaAll.Count;
            var kategorijeAll = await _kategorijaVozilaService.Get<List<KategorijaVozila>>(new KategorijaSearchRequest());
            BrojKategorijaVozila = kategorijeAll.Count;

            BrojRezervacija = rezervacijeAll.Count;
            RezervacijeOvogMjeseca = rezervacijaOvajMjesec;
            RezervacijeOveGodine = rezervacijaOveGodine;           


            RezervacijeItems[0].CategoryValue = BrojRezervacija.ToString();
            RezervacijeItems[1].CategoryValue = RezervacijeOvogMjeseca.ToString();
            RezervacijeItems[2].CategoryValue = RezervacijeOveGodine.ToString();          

            VozilaItems[0].CategoryValue = BrojVozila.ToString();
            VozilaItems[1].CategoryValue = BrojKategorijaVozila.ToString();           
        }

        private void GetChartData()
        {
            DateTime dateTime = new DateTime(2019, 5, 1);

            heartRateData = new ObservableCollection<ChartDataPoint>()
            {
                new ChartDataPoint(dateTime, 15),
                new ChartDataPoint(dateTime.AddMonths(1), 20),
                new ChartDataPoint(dateTime.AddMonths(2), 17),
                new ChartDataPoint(dateTime.AddMonths(3), 23),
                new ChartDataPoint(dateTime.AddMonths(4), 18),
                new ChartDataPoint(dateTime.AddMonths(5), 25),
                new ChartDataPoint(dateTime.AddMonths(6), 19),
                new ChartDataPoint(dateTime.AddMonths(7), 21),
            };

            caloriesBurnedData = new ObservableCollection<ChartDataPoint>()
            {
                new ChartDataPoint(dateTime, 940),
                new ChartDataPoint(dateTime.AddMonths(1), 960),
                new ChartDataPoint(dateTime.AddMonths(2), 942),
                new ChartDataPoint(dateTime.AddMonths(3), 957),
                new ChartDataPoint(dateTime.AddMonths(4), 940),
                new ChartDataPoint(dateTime.AddMonths(5), 942),
            };

            sleepTimeData = new ObservableCollection<ChartDataPoint>()
            {
                new ChartDataPoint(dateTime, 7.8),
                new ChartDataPoint(dateTime.AddMonths(1), 7.2),
                new ChartDataPoint(dateTime.AddMonths(2), 8.0),
                new ChartDataPoint(dateTime.AddMonths(3), 6.8),
                new ChartDataPoint(dateTime.AddMonths(4), 7.6),
                new ChartDataPoint(dateTime.AddMonths(5), 7.0),
                new ChartDataPoint(dateTime.AddMonths(6), 7.5),
            };

            waterConsumedData = new ObservableCollection<ChartDataPoint>()
            {
                new ChartDataPoint(dateTime, 36),
                new ChartDataPoint(dateTime.AddMonths(1), 41),
                new ChartDataPoint(dateTime.AddMonths(2), 38),
                new ChartDataPoint(dateTime.AddMonths(3), 41),
                new ChartDataPoint(dateTime.AddMonths(4), 35),
                new ChartDataPoint(dateTime.AddMonths(5), 37),
                new ChartDataPoint(dateTime.AddMonths(6), 38),
                new ChartDataPoint(dateTime.AddMonths(7), 36),
            };
        }

        /// <summary>
        /// Invoked when the menu button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void MenuClicked(object obj)
        {
            // Do something
            HomePage.HomeStranicaInstanca.IsPresented = true;
        }

        #endregion
    }
}
