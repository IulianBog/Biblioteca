using Biblioteca.Model;
using Biblioteca.Functii;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    class Program
    {

        

        static void Main(string[] args)
        {
            string choice;
            double suma;
            bool ok = true;
            List<Carte> carte_list = new List<Carte>();
            List<Utilizator> utilizator_list = new List<Utilizator>();
            Functii.CRUD_Carti crud_Carti = new Functii.CRUD_Carti();
            Functii.CRUD_Utilizator crud_Utilizator = new CRUD_Utilizator();
            Meniu meniu = new Meniu();
            

            while(ok == true)
            {
                Thread.Sleep(1000);
                Console.Clear();
                meniu.meniuPrincipal();
                choice = Console.ReadLine();
                int id_utilizator;

                switch (choice)
                {
                    case "1":
                        crud_Utilizator.Adaugare_Utilizator(utilizator_list);
                        Thread.Sleep(1000);
                        break;
                    case "2":

                        Console.Clear();
                        meniu.meniuBiblioteca();
                        choice = Console.ReadLine();
                       
                        switch (choice)
                        {
                            case "1":
                                crud_Carti.Adaugare_Carte(carte_list);
                                break;
                            case "2":
                                Console.WriteLine("Carte aleasa apare de " + crud_Carti.Aparitii_Carte(carte_list) + " ori");
                                break;
                            case "3":
                                crud_Carti.Vizualizare_Biblioteca(carte_list);
                                Thread.Sleep(1000);
                                break;
                            case "4":
                                break;
                            default:
                                Console.WriteLine("Comanda incorecta! Incercati din nou!\n");
                                break;
                        }
                        break;

                    case "3":
                        Console.Clear();
                        meniu.meniuReturnare();
                        choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "1":
                                crud_Utilizator.Imprumut_Carte(carte_list, utilizator_list);
                                break;
                            case "2":
                                crud_Utilizator.Retur_Carte(carte_list,utilizator_list);
                                break;
                            case "3":
                                suma = crud_Utilizator.Calculare_Pret(utilizator_list, carte_list);
                                if (suma != -1)
                                {
                                    Console.WriteLine($"Utilizatorul are de platit " +
                                        $"{suma} LEI");
                                }
                                else
                                    Console.WriteLine("Nu este posibila calcularea!");
                                break;
                            case "4":
                                break;
                            default:
                                Console.WriteLine("Comanda incorecta! Incercati din nou!\n");
                                break;
                        }
                        break;

                    case "4":
                        ok = false;
                        break;

                    default:
                        Console.WriteLine("Comanda incorecta! Incercati din nou!\n");
                        break;
                }
            }

        }
    }
    
}

