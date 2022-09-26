using Varastonhallinta;

//Console.WriteLine($"yhteyden tarjoaa {Projectconstants.DatabaseProvider}.");

//AddItems(id, tuotteennimi, hinta, saldo);

ReadItems();

//ChangeItems("abc", 11);

DeleteTuote(11);

Console.WriteLine("");

ReadItems();


static void ReadItems()
{
    using (Varastonhallintaa varastonhallinta = new())
    {
        Console.WriteLine("Kaikki Tuotteet");

        //Haetaan kaikki Tuotteet taulun tietueet
        IQueryable<Tuote>? Tuotteet = varastonhallinta.Tuotteet;

        if (Tuotteet is null)
        {
            Console.WriteLine("Ei ole yhtäkään tuotetta varastossa.");
            return;
        }
        //Käydään kaikki tietueet läpi ja tulostetaan ne näytölle
        foreach (Tuote tuote in Tuotteet)
        {
            Console.WriteLine("Id: " + tuote.id);
            Console.WriteLine("Saldo: " + tuote.Varastosaldo);
            Console.WriteLine("Hinta: " + tuote.Tuotenhinta);
            Console.WriteLine("Nimi: " + tuote.Tuotenimi);
        }
    }
}



static bool AddItems(int newId, string newTuotenimi, string newTuotenhinta, string newVarastosaldo)
{
    using (Varastonhallintaa varastonhallinta = new())
    {
        Tuote tuote = new()
        {
            id = newId,
            Tuotenimi = newTuotenimi,
            Tuotenhinta = newTuotenhinta,
            Varastosaldo = newVarastosaldo

        };

        varastonhallinta.Tuotteet?.Add(tuote);

        int affected = varastonhallinta.SaveChanges();
        return (affected == 1);
    }
}



static int DeleteTuote(int deletable)
{
    using(Varastonhallintaa varastonhallinta = new())
    {
        Tuote tuotedelete = varastonhallinta.Tuotteet.Find(deletable);

        if (tuotedelete is null)
        {
            Console.WriteLine("Ei löydy!");
            return 0;
        }
        else
        {
            varastonhallinta.Remove(tuotedelete);
            int affected = varastonhallinta.SaveChanges();
            return affected;
        }
    }
}


static bool ChangeItems(string newTuotename, int id)
{
    using(Varastonhallintaa varastonhallinta = new())
    {
        Tuote tuoteupdate = varastonhallinta.Tuotteet.Find(id);
        
        if(tuoteupdate is null)
        {
            return false;
        }
        else
        {
            tuoteupdate.Tuotenimi = newTuotename;
            int affected = varastonhallinta.SaveChanges();
            return(affected == 1);
        }
    }
}

