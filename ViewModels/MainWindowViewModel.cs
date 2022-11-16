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
        /*
        private List<CittaOspitante> _dati2;

        public List<CittaOspitante> Dati2
        {
            get { return _dati2; }
            set { _dati2 = value; }
        }

        private ContaAtleti _dati3;

        public ContaAtleti Dati3
        {
            get { return _dati3; }
            set { _dati3 = value; }
        }

        private List<CittaOspitante2> _dati4;

        public List<CittaOspitante2> Dati4
        {
            get { return _dati4; }
            set { _dati4 = value; }*/

    public MainWindowViewModel()
        {
        using (OlympicsContext context = new OlympicsContext())
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

                //1 giusta) Selezione i 10 atleti più giovani ad aver vinto una medaglia d'oro
                var query1 = context.athletes
            .Where(q => q.Medal == "Gold")
            .Where(q => q.Age != null)
            .Select(s => new
            {
                s.IdAthlete,
                s.Name,
                s.Age
            })
            .Distinct()
            .OrderBy(ob => ob.Age).ThenBy(ob => ob.Name)
            .Take(10)
            .ToList();

                //2 giusta) Recuperare l'elenco delle città che hanno ospitato i giochi, in ordine alfabetico
                var query2 = context.athletes.Select( 
                  s => new 
                    {
                      s.City 
                    }
                  ).Distinct().OrderBy(ob=>ob.City).ToList();

                //3 giusta) Quanti atleti hanno vinto la medaglia d'oro?
                var query3 = context.athletes
                     .Where(q => q.Medal == "Gold")
                     .Select(s => s.IdAthlete) //in questo modo non creo un nuovo oggetto ma una stringa
                                               //(che poi faccio a tolist così è anche bindabile se ho una lista in questo caso no)
                     .Distinct()
                     .Count();

                //4 giusta) Per ogni città calcolare quante edizioni ha ospitato. Ordinare in ordine decrescente.
                var query4 = context.athletes
                    .Select(s => new
                    {
                        s.City,
                        s.Games
                    }
                    )
                    .GroupBy(gb => new
                    {
                        gb.City,
                    })
                    .Select(s => new
                    {
                        s.Key.City,
                        Editions = s.Distinct().Count()
                    })
                    .OrderByDescending(obd => obd.Editions).ThenBy(ob=>ob.City)
                    .ToList();
                //5 giusta) Stilare la classifica degli sport che hanno assegnato più medaglie,Sulle tabelle normalizzate
                var query5 = context.Medals
                    .Select(s => new
                    {
                        s.Event.Sport
                    })
                    .GroupBy(gb => new
                    {
                        gb.Sport
                    })
                    .Select(s => new
                    {
                        s.Key.Sport,
                        Medals = s.Count()
                    })
                    .OrderByDescending(ob=>ob.Medals)
                    .ToList();

                //6 giusta) Per ogni edizione calcolare il numero totale di medaglie assegnate, Sulle tabelle normalizzate
                var query6 = context.Medals
                    .Select(s => s.Game.Games) //visto che abbiamo un solo parametro posso fare cosi e farlo diventare una stringa e non un oggetto
                    .GroupBy(gb => gb)//non devo creare l'oggetto per gb
                    .Select(s => new
                    {
                        s.Key, //qua l'oggetto per cui ho fatto il group by quin coì è una stringa, è tutto più semplice,
                               //se mi basta un campo evito di creare un nuovo oggetto e faccio solo una stringa
                        Medals = s.Count()
                    })
                    .OrderByDescending(ob => ob.Medals)
                    .ToList();
            }
        }
}
}

