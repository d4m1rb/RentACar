using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.MobileUI.Helpers
{
    public class Recommender
    {
        private readonly APIService _vozilaService = new APIService("Automobil");
        private readonly APIService _ocjeneService = new APIService("Ocjena");


        Dictionary<int, List<Ocjena>> vozila = new Dictionary<int, List<Ocjena>>();
        List<Ocjena> ocjenePosmatranogVozila;
        List<Automobil> _vozila;

        public  List<RentACarApp.Model.Models.Automobil> GetSlicnaVozila(int VoziloID)
        {
            UcitajVozila(VoziloID);
            OcjenaSearchRequest ocjenaSearch = new OcjenaSearchRequest()
            {
                VoziloId = VoziloID
            };
            
            List<Ocjena> zajednickeOcjene1 = new List<Ocjena>();
            List<Ocjena> zajednickeOcjene2 = new List<Ocjena>();


            List<RentACarApp.Model.Models.Automobil> preporucenaVozila = new List<Automobil>();

            foreach (var item in vozila)
            {
                bool prvi = true;

                foreach (var o in ocjenePosmatranogVozila)
                {
                    foreach (var ocjeneVozila in item.Value.ToArray())
                    {
                        if(ocjeneVozila.KlijentId==o.KlijentId)
                        {

                            zajednickeOcjene1.Add(o);
                            if (prvi)
                            {
                                zajednickeOcjene2.Add(ocjeneVozila);
                                prvi = false;
                            }
                        }
                    }

                }
                double slicnost = GetSlicnost(zajednickeOcjene1, zajednickeOcjene2);
                if (slicnost > 0.5)
                {
                    bool ima = true;
                    foreach (var v in _vozila)
                    {
                        if (v.AutomobilId == VoziloID && ima)
                        {
                            preporucenaVozila.Add(v);
                            ima = false;
                        }
                    }
                  
                }
                zajednickeOcjene1.Clear();
                zajednickeOcjene2.Clear();
            }

            return preporucenaVozila;
        }

        private double GetSlicnost(List<Ocjena> zajednickeOcjene1, List<Ocjena> zajednickeOcjene2)
        {
            if (zajednickeOcjene1.Count != zajednickeOcjene2.Count)
                return 0;

            double brojnik = 0, nazivnik1 = 0, nazivnik2 = 0;

            for (int i = 0; i < zajednickeOcjene1.Count; i++)
            {
                brojnik = zajednickeOcjene1[i].Ocjena1 * zajednickeOcjene2[i].Ocjena1;
                nazivnik1 = zajednickeOcjene1[i].Ocjena1 * zajednickeOcjene1[i].Ocjena1;
                nazivnik2 = zajednickeOcjene2[i].Ocjena1 * zajednickeOcjene2[i].Ocjena1;
            }
            nazivnik1 = Math.Sqrt(nazivnik1);
            nazivnik2 = Math.Sqrt(nazivnik2);

            double nazivnik = nazivnik1 * nazivnik2;
            if (nazivnik == 0)
                return 0;
            else
                return brojnik / nazivnik;
        }



        /*Ucitava vozila koja su: - dostupna
                                 - iste kategorije kao i posmatrano vozilo
                                 - imaju ocjenu
                                 - cijena iznajmljivanja u rangu +-20%
        */
        private async void UcitajVozila(int voziloID)
        {
            OcjenaSearchRequest ocjenaSearch = new OcjenaSearchRequest()
            {
                VoziloId = voziloID
            };

            ocjenePosmatranogVozila = await _ocjeneService.Get<List<RentACarApp.Model.Models.Ocjena>>(ocjenaSearch);


            var vozilo = await _vozilaService.GetById<RentACarApp.Model.Models.Automobil>(voziloID);

            AutomobilSearchRequest request = new AutomobilSearchRequest();
            request.Dostupan = true;

            _vozila = await _vozilaService.Get<List<RentACarApp.Model.Models.Automobil>>(request);

            request.KategorijaId = vozilo.KategorijaId;
            List<RentACarApp.Model.Models.Automobil> aktivnaVozila = await _vozilaService.Get<List<RentACarApp.Model.Models.Automobil>>(request);
            List<RentACarApp.Model.Models.Ocjena> ocjene;


            ////Samo vozila sa cijenom iznajmljivanja u rangu +-20%
            //foreach (var item in aktivnaVozila)
            //{
            //    decimal posmatraniSaUmanjenomCijenom=item.CijenaIznajmljivanja*((decimal)(0.2));
            //    decimal donjaGranica =  vozilo.CijenaIznajmljivanja - posmatraniSaUmanjenomCijenom;
            //    decimal gornjaGranica = vozilo.CijenaIznajmljivanja + posmatraniSaUmanjenomCijenom;

            //    if (!(item.CijenaIznajmljivanja > donjaGranica && item.CijenaIznajmljivanja < gornjaGranica))
            //        aktivnaVozila.Remove(item);
            //}
            Dictionary<int, List<Ocjena>> vv = new Dictionary<int, List<Ocjena>>();

            foreach (var v in aktivnaVozila)
            {
                ocjenaSearch.VoziloId = v.AutomobilId;
                ocjene = await _ocjeneService.Get<List<RentACarApp.Model.Models.Ocjena>>(ocjenaSearch);
                if (ocjene.Count > 0)
                    vozila.Add(v.AutomobilId, ocjene);
            }
          
        }
    }
}
