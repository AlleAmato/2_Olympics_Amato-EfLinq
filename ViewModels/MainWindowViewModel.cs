using EtityFrameworkLinq_esempio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EtityFrameworkLinq_esempio.ViewModels
{
    internal class MainWindowViewModel
    {
        private List<athlete> _dati;

        public List<athlete> Dati
        {
            get { return _dati; }
            set { _dati = value; }
        }

        private List<CittaOspitante> _dati2;

        public List<CittaOspitante> Dati2
        {
            get { return _dati2; }
            set { _dati2 = value; }
        }

        public MainWindowViewModel()
        {
            using(OlympicsContext context=new OlympicsContext())
            {/*
                //query costruita, tutti i metodi vanno messi prima del ToList()
                //Dati = context.athletes
                //    .Where(q => q.IdAthlete >= 7 )
                  //  .Where(q => q.Year == 1992)
                    //.OrderBy(ob => ob.IdAthlete).ThenBy(ob => ob.Name).ThenByDescending(ob => ob.NOC)
                    //.Skip(10)//tolgo le prime 10 righe
                    //.Take(10)//top (10) di quelle rimaste dopo lo skip
                    //.ToList();

                //oppure lo metto dentro una variabile anonima ma sono da usare solo se necessarie
                var v = context.athletes
                    .Where(q => q.IdAthlete >= 7)
                    .Where(q => q.Year == 1992)
                    .OrderBy(ob => ob.IdAthlete).ThenBy(ob => ob.Name).ThenByDescending(ob => ob.NOC)
                    .Skip(10)
                    .Take(10)
                    .ToList();

                athlete primo = context.athletes
                    .OrderBy(ob => ob.Name)
                    .First();//prendo il primo, ma first mette in esecuzione la query un po come il ToList,
                             //posso anche mettere dentro al first ciò che metterei nel where, Last funziona allo stesso modo
                             //se pero nella first o nella last non ritorno nulla allora va in errore quindi la query deve essere con almeno 1 elemento
                             //in questo caso posso usare FirstOrDefault/Last... restituiscono il default del tipo che sto tratando (es. oggetti=null)
                             //quindi di solito si usa FirstOrDefault

                //se non voglio scegliere tutte le colonne nel select, es. eta media dei medagliati uso le funzioni di aggregazione
                double? avg= (short) context.athletes //può essere null s etutti i campi sono null o se nessuna riga viene selezionata
                    .Where(q => q.Medal != null) //posso anche fare dei cast espliciti
                    .Average(age => age.Age); // stessa cosa per i metodi max e min,
                                              // count invece di solito si usa sezza parametri oppure con le codizioni tipo where e restituisce int
                                              //il distinct non ha bisogno di parametri toglie solo i duplicati

                //i metodi piu complessi da eseguire (3):select, group by

                //Elenco di tutti i NOC
                var a =context.athletes.Select( //dentro la select devo creare una nuova classe al volo dove ho solo le colonne che scelgo io
                    s => new
                    {
                        //creo una classe anonima e la definisco qua
                        Nazione = s.NOC, //nazione =s.noc è come: NOC as Nazione
                    }
                    ).Distinct().ToList();//dato che la lista che mi restituisce è di un oggetto di classe inesistente allora creo una variabile anonima a

                //se devo fare un binding devo creare una classe per forza quindi creo la classe nazioni
                var nazioni = context.athletes.Select( //dentro la select devo creare una nuova classe al volo dove ho solo le colonne che scelgo io
                    s => new Nazione
                    {
                        //creo una classe anonima e la definisco qua
                        NOC= s.NOC, //nazione =s.noc è come: NOC as Nazione
                    }
                    ).Distinct().ToList().OrderBy(ob=> ob.NOC);

                //group by, group by noc di tutte le nazioni con almeno 100 partecipanti, ordine alfabetico
                context.athletes.Where(q => q.Year == 2012)
                    .GroupBy(gb => new//nel group by ho bisogno di una select con oggetto anonimo/classe se lo devo bindare
                    {
                        gb.NOC
                    })
                    .Select(s => new
                    {
                        s.Key.NOC, //qui uso key perchè è il nome sotto quale sono racchiusi i campi del group by univoci (in questo caso il NOC)
                        Partecipations=s.Count()//devo creare pre forza un nuovo oggetto per il campo calcolato sennò da errore
                    })
                    .Where(q=>q.Partecipations>100).OrderBy(ob=>ob.NOC).ToList();
                    
                
                //medaglie di tutti gli atleti che hanno vinto qualcosa
                var query1 = context.AthletesNfs.Include(i => i.Medals)
                    .Where(q=>q.Medals.Count()!=0)
                    .ToList();
                //nel caso io non volessi nel select un'altra tabella ma mi servisse solo nel where non ho bisogno di fare la join
                //lo capisce da solo tramite la chiave esterna, però non restituisce i risultati

                //tutti i medagliati d'oro
                var query2 = context.AthletesNfs.Include(i => i.Medals)//medals in questo caso è una collezione non il campo quindi avrei bisogno di un foreach
                                                                       //che non posso fare qui dentro e uso una sorta di having
                    .Where(q => q.Medals.Any(a => a.Medal1 == "Gold")).ToList();//con any posso dare una lambda expression
                                                                                //che posso usare sulle righe risultate simile a un having

                //voglio che chi ha vinto piu medaglie compaia piu volte per ogni medaglia
                List<Medal> query3 = context.Medals.Include(i => i.AthletesNf).ToList();
                //Dati = query3;
                */

                //query di fine lezione

                //1)
               /* Dati = context.athletes
                .Where(q => q.Medal=="Gold" )
                .Where(q=>q.Age >=1)
                .OrderBy(ob => ob.Age).ThenBy(ob => ob.Name)
                .Take(10)//top (10) di quelle rimaste dopo lo skip
                .ToList();*/

                //2)
                Dati2= context.athletes.Select( 
                     s => new CittaOspitante
                     {
                        //creo una classe anonima e la definisco qua
                        City = s.City //nazione =s.noc è come: NOC as Nazione
                    }
                     ).Distinct().OrderBy(ob=>ob.City).ToList();

            }
        }
    }
}
