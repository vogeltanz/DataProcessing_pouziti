using System;
using System.Collections.Generic;
using DataProcessing;   //namespace vytvořené knihovny (.NET standard) pro zpracování dat v CSV souboru

namespace DataProcessing_pouziti
{
    class Program
    {
        static void Main(string[] args)
        {
            //test zda byly zadány argumenty
            if (args != null && args.Length == 2 && args[0] != null && args[0] == "-s" && args[1] != null)
            {
                //třídu CSV můžeme již jednoduše použít z naimportovaného DLL souboru, není nutné dělat cokoliv jiného. (Pozor! pokud by se jednalo o binární DLL, tak to tímto způsobem samozřejmě nepůjde! Knihovna musí být zkompilována pomocí .NET!)
                CSV csvSoubor = new CSV();
                csvSoubor.NactiCSV(args[1]);



                Console.WriteLine("Výpis všech dat v CSV souboru");
                int nejdelsiDelka = csvSoubor.VratNejdelsiDelkuSeznamu(csvSoubor.Data);
                for (int i = 0; i < nejdelsiDelka; i++)
                {
                    foreach (List<String> seznamStringu in csvSoubor.Data)
                    {
                        if (seznamStringu != null && i < seznamStringu.Count)
                        {
                            Console.Write(seznamStringu[i] + "\t");
                        }
                    }
                    Console.WriteLine(String.Empty);
                }
                Console.WriteLine(String.Empty);



                Console.WriteLine("Výpis jen věku, který byl předtím převeden na double");
                List<double?> seznamVeku = csvSoubor.PrevedSeznamDat(csvSoubor.Data[1]);
                foreach(double? cislo in seznamVeku)
                {
                    Console.WriteLine(cislo.GetValueOrDefault());
                }
                Console.WriteLine(String.Empty);


                Console.WriteLine("Výpis nově přidaného jména v aplikaci:");
                csvSoubor.VratSloupecDat("jméno").Add("Nové jméno");
                csvSoubor.VratSloupecDat("věk").Add("1000");
                csvSoubor.VratSloupecDat("výška").Add("200");
                csvSoubor.VratSloupecDat("váha").Add("90");
                Console.WriteLine(csvSoubor.Data[0][csvSoubor.Data[0].Count - 1] + " " + csvSoubor.Data[1][csvSoubor.Data[1].Count - 1] + " " + csvSoubor.Data[2][csvSoubor.Data[2].Count - 1] + " " + csvSoubor.Data[3][csvSoubor.Data[3].Count - 1]);
                Console.WriteLine(String.Empty);


                Console.WriteLine("Výpis dalšího nově přidaného jména v aplikaci pomocí metody ve třídě CSV:");
                csvSoubor.PřidatŘádek(new CSVpolozka("jméno", "Další nové jméno"), new CSVpolozka("věk", "240"), new CSVpolozka("výška", "20"), new CSVpolozka("váha", "50"));
                Console.WriteLine(csvSoubor.Data[0][csvSoubor.Data[0].Count - 1] + " " + csvSoubor.Data[1][csvSoubor.Data[1].Count - 1] + " " + csvSoubor.Data[2][csvSoubor.Data[2].Count - 1] + " " + csvSoubor.Data[3][csvSoubor.Data[3].Count - 1]);
                Console.WriteLine(String.Empty);

                Console.WriteLine("A dalšího bez zadání některých sloupců (pomocí metody ve třídě CSV):");
                csvSoubor.PřidatŘádek(new CSVpolozka("jméno", "Bezejmenný"), new CSVpolozka("výška", "70"));
                Console.WriteLine(csvSoubor.Data[0][csvSoubor.Data[0].Count - 1] + " " + csvSoubor.Data[1][csvSoubor.Data[1].Count - 1] + " " + csvSoubor.Data[2][csvSoubor.Data[2].Count - 1] + " " + csvSoubor.Data[3][csvSoubor.Data[3].Count - 1]);
                Console.WriteLine(String.Empty);


                Console.WriteLine("Znovu, tentokrát bez zadání jmen sloupců (pomocí metody ve třídě CSV):");
                csvSoubor.PřidatŘádek("Stařík", "70", "150", "75");
                Console.WriteLine(csvSoubor.Data[0][csvSoubor.Data[0].Count - 1] + " " + csvSoubor.Data[1][csvSoubor.Data[1].Count - 1] + " " + csvSoubor.Data[2][csvSoubor.Data[2].Count - 1] + " " + csvSoubor.Data[3][csvSoubor.Data[3].Count - 1]);
                Console.WriteLine(String.Empty);

                Console.WriteLine("A znovu, tentokrát s vynecháním posledních sloupců (pomocí metody ve třídě CSV):");
                csvSoubor.PřidatŘádek("Neúplný profil", "75");
                Console.WriteLine(csvSoubor.Data[0][csvSoubor.Data[0].Count - 1] + " " + csvSoubor.Data[1][csvSoubor.Data[1].Count - 1] + " " + csvSoubor.Data[2][csvSoubor.Data[2].Count - 1] + " " + csvSoubor.Data[3][csvSoubor.Data[3].Count - 1]);
                Console.WriteLine(String.Empty);


                Console.WriteLine(String.Empty);
                Console.WriteLine("Zápis CSV souboru...");
                csvSoubor.UlozCSV("csv_výstup.csv");
                Console.WriteLine("Úspěšně dokončeno!");
            }
            else
            {
                Console.WriteLine("Argumenty nebyly při spuštění aplikace zadány, nebo byly zadány nesprávně! (příklad argumentů: \"-s csv.csv\")");
            }

            Console.WriteLine("\n\nPokračujte stisknutím libovolné klávesy...");
            Console.ReadKey(true);
        }
    }
}
