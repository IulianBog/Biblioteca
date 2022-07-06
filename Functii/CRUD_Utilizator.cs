using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Model;
namespace Biblioteca.Functii
{
    public class CRUD_Utilizator
    {
        

        public void Adaugare_Utilizator(List<Utilizator> utilizator_list)
        {
            string Nume;

            Console.WriteLine("Introduceti numele utilizatorului: ");
            Nume = Console.ReadLine();
         
            utilizator_list.Add(new Utilizator {Utilizator_ID = utilizator_list.Count, Nume_Utilizator = Nume });
            Console.WriteLine("\n");

            utilizator_list.ForEach(delegate (Utilizator utilizator)
            {
                Console.WriteLine($"{utilizator.Utilizator_ID} ----- {utilizator.Nume_Utilizator}");
                
                if (utilizator.Carti_imprumutate == null)
                    Console.WriteLine("Nu a imprumutat nici o carte");
                else
                    foreach(var carte in utilizator.Carti_imprumutate)
                    {
                        Console.WriteLine($"{carte.Key} --> {carte.Value}");
                    }
            });
        }

        /// <summary>
        /// Alegem utilizator inainte sa alegem cartea, prin intermediul Id-ului.
        /// Daca Id-ul introdus nu exista se reapeleaza functia
        /// </summary>
        /// <returns> Id utilizator </returns>
        public int Alegere_utilizator(List<Utilizator> utilizator_list)
        {
            List<Carte> carte_list = new List<Carte>();
            int Id_utilizator;

            
            if (utilizator_list.Count == 0)
            {
                Console.WriteLine("Nu exista nici un utilizator");
                return -1;
            }

            Console.WriteLine("Alegeti utilizatorul dorit: \n");

            utilizator_list.ForEach(delegate (Utilizator utilizator)
            {

                Console.WriteLine(utilizator.Utilizator_ID + " --> " + utilizator.Nume_Utilizator);

            });

            Console.WriteLine('\n');

            Id_utilizator = Convert.ToInt32(Console.ReadLine());

            if (utilizator_list.Any(Utilizator => Utilizator.Utilizator_ID == Id_utilizator) != null)
            {
                return Id_utilizator;
            }
            else
            {
                Console.WriteLine("Ati ales un utilizator inexistent! Alegeti din nou! \n");
                return Alegere_utilizator(utilizator_list);
            }
        }


        /// <summary>
        /// Functia verifica daca string-ul introdus poate sa fie 
        /// </summary>
        /// <returns> Data sub format dd.MM.yyyy 00:00 </returns>
        public DateTime Verificare_Input_Data()
        {
            string data_string;
            DateTime data;

            Console.WriteLine("Introduceti data imprumutului: \n");
            data_string = Console.ReadLine();

            DateTime.TryParseExact(data_string, "ddMMyyyy",
                           CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out data);

          
            if (data != null && data <= DateTime.Today)
            {
                return data;    //formatul de return al datei este determinata si de setarea ceasului calculatorului
            }
            else
            {
                Console.WriteLine("Ati introdus o data invalida! Data trebuie sa aiba forma ddMMyyyy " +
                    "si nu poate sa fie mai mare decat data de azi. \n");
                return Verificare_Input_Data();
            }
        }
        
        public void Imprumut_Carte(List<Carte> carte_list, List<Utilizator> utilizator_list)
        {
            
            string Cod_ISBN;
            DateTime data_imprumut;
            bool ok = false;
            int Id_utilizator = Alegere_utilizator(utilizator_list);

            if (Id_utilizator == -1)
            {
                return;
            }

            Console.WriteLine("Alegeti cartea dorita: \n");

            carte_list.ForEach(delegate (Carte carte) {

                if (carte.Status == false)
                {
                    ok = true;
                    Console.WriteLine(carte.ISBN + ' ' + carte.Nume_Carte);
                }

            });

            Console.WriteLine("\n");
            Cod_ISBN = Console.ReadLine();


            if (ok == false)
            {
                Console.WriteLine("Toate cartile din biblioteca sunt imprumutate.");
                return;
            }



            var obj_carte = carte_list.FirstOrDefault(Carte => Carte.ISBN == Cod_ISBN);

            if (obj_carte != null)
            {
                var obj_utilizator = utilizator_list.FirstOrDefault(Utilizator => Utilizator.Utilizator_ID == Id_utilizator);
                obj_carte.Status = true;


                data_imprumut = Verificare_Input_Data();

                obj_utilizator.Carti_imprumutate = new Dictionary<string, DateTime>();
                obj_utilizator.Carti_imprumutate.Add(Cod_ISBN, data_imprumut);
 
            }

            else
            {
                Console.WriteLine("Ati ales o carte inexistenta! Alegeti din nou! \n");
                Imprumut_Carte(carte_list, utilizator_list);
            }

        }

        public void Retur_Carte(List<Carte> carte_list, List<Utilizator> utilizator_list)
        {
            string Cod_ISBN;
            bool ok = false;
            int Id_utilizator = Alegere_utilizator(utilizator_list);

            if (Id_utilizator == null)
                return;

            Console.WriteLine("Alegeti cartea dorita: \n");

            carte_list.ForEach(delegate (Carte carte) {

                if (carte.Status == true)
                {
                    ok = true;
                    Console.WriteLine(carte.ISBN + ' ' + carte.Nume_Carte);
                }
            });

            if (ok == false)
            {
                Console.WriteLine("Nu exista nici o carte imprumutata.");
                return;
            }

            Cod_ISBN = Console.ReadLine();

            var obj_carte = carte_list.FirstOrDefault(Carte => Carte.ISBN == Cod_ISBN);
            

            if (obj_carte != null)
            {
                var obj_utilizator = utilizator_list.FirstOrDefault(Utilizator => Utilizator.Utilizator_ID == Id_utilizator);
                obj_carte.Status = false;


                obj_utilizator.Carti_imprumutate = new Dictionary<string, DateTime>();
                obj_utilizator.Carti_imprumutate.Remove(Cod_ISBN);

            }

            else
            {
                Console.WriteLine("Ati ales o carte inexistenta! Alegeti din nou! \n");
                Retur_Carte(carte_list, utilizator_list);
            }

        }

        /// <summary>
        /// Calculez pretul in raport cu ziua de azi
        /// </summary>
        /// <param name="utilizator_list"></param>
        /// <param name="carte_list"></param>
        /// <returns></returns>
        public double Calculare_Pret(List<Utilizator> utilizator_list, List<Carte> carte_list)
        {
           
            float pret, suma = 0;
            string Cod_ISBN;
            DateTime data_imprumut;
            int Id_utilizator;

            if (utilizator_list.Count == 0)
            {
                Console.WriteLine("Nu exista nici un utilizator");
                return -1;
            }

            Id_utilizator = Alegere_utilizator(utilizator_list);

            var obj_utilizator = utilizator_list.FirstOrDefault(Utilizator =>
                                                 Utilizator.Utilizator_ID == Id_utilizator);

            if (obj_utilizator.Carti_imprumutate == null)
            {
                Console.WriteLine("Utilizatorul nu are nici o carte imprumutata");
                return -1;
            }

            Console.WriteLine("Alegeti cartea dorita: \n");

            foreach (var carte in obj_utilizator.Carti_imprumutate)
            {
                Console.WriteLine($"{carte.Key} ---> {carte.Value}\n");
            }

            Cod_ISBN = Console.ReadLine();

            if ((carte_list.Any(Carte => Carte.ISBN == Cod_ISBN)) == false)
            {
                Console.WriteLine("Ati introdus codul ISBN gresit sau nu exista in lista de carti imprumutate! \n");
                return Calculare_Pret(utilizator_list, carte_list);
            }

            var obj_carte = carte_list.FirstOrDefault(Carte => Carte.ISBN == Cod_ISBN);
            pret = obj_carte.Pret;
            obj_utilizator.Carti_imprumutate.TryGetValue(Cod_ISBN, out data_imprumut);
            data_imprumut = data_imprumut.AddDays(14);

            if (DateTime.Now > data_imprumut)
            {
                suma = (float)(pret + (DateTime.Today - data_imprumut.AddDays(14)).TotalDays * pret / 100);
            }
            else
            {
                suma = (float)pret;
            }


            return suma;
        }

    }
}
