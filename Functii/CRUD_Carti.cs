using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Biblioteca.Model;

namespace Biblioteca.Functii
{
    public class CRUD_Carti
    {
        /// <summary>
        /// 
        /// E o validare prin intermediul unui regex (regular expression)
        /// 
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public bool Validare_ISBN(string isbn)
        {
            var regex = @"^(?:ISBN(?:-10)?:?●)?(?=[0-9X]{10}$|(?=(?:[0-9]+[-●]){3})[-●0-9X]{13}$)";
            var match = Regex.Match(isbn, regex, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
               return false;
        }

        /// <summary>
        /// 
        /// Aceasta e o validare de pe care am gasit-o pe
        /// https://www.geeksforgeeks.org/program-check-isbn/
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public bool isValidISBN(string isbn)
        {

            // length must be 10
            int n = isbn.Length;
            if (n != 10)
                return false;

            // Computing weighted sum of
            // first 9 digits
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                int digit = isbn[i] - '0';

                if (0 > digit || 9 < digit)
                    return false;

                sum += (digit * (10 - i));
            }

            // Checking last digit.
            char last = isbn[9];
            if (last != 'X' && (last < '0'
                             || last > '9'))
                return false;

            // If last digit is 'X', add 10
            // to sum, else add its value.
            sum += ((last == 'X') ? 10 :
                              (last - '0'));

            // Return true if weighted sum
            // of digits is divisible by 11.
            return (sum % 11 == 0);
        }

        public void Adaugare_Carte(List<Carte> carte_list)
        {
          
            string ISBN;
            string Nume;
            float pret;
           
            Console.WriteLine("Introduceti cod ISBN: ");
            ISBN = Console.ReadLine();

            ///pentru testare pune && in loc de ||
            if (isValidISBN(ISBN) == false || carte_list.Any(Carte => Carte.ISBN == ISBN))
            {
                Console.WriteLine("Codul ISBN este gresit sau a mai fost folosit! \n");
                Adaugare_Carte(carte_list);
            }


            Console.WriteLine("Introduceti numele cartii: ");
            Nume = Console.ReadLine();


            Console.WriteLine("Introduceti pretul: ");
            pret = float.Parse(Console.ReadLine());


            carte_list.Add(new Carte { ISBN = ISBN, 
                                       Nume_Carte = Nume, 
                                       Pret = pret });

        }

        /// <summary>
        /// Cauta o carte dupa codul numele ei si returneaza numarul de aparitii
        /// </summary>
        /// <param name="carte_list"></param>
        /// <returns>numarul de aparitii</returns>
        public int Aparitii_Carte(List<Carte> carte_list)
        {
            string Nume;
            int aparitie = 0;
            
            Console.WriteLine("Introduceti numele cartii: ");
            Nume = Console.ReadLine();

            carte_list.ForEach(delegate (Carte carte)
                {
                    if (carte.Nume_Carte == Nume)
                        aparitie++;
                });

            return aparitie;
        }

        public void Vizualizare_Biblioteca(List<Carte> carte_list)
        {

            if (carte_list.Count == 0)
                Console.WriteLine("Biblioteca e goala\n");
            /// daca e goala biblioteca sa zici
            
            carte_list.ForEach(delegate (Carte carte)
            {
                if (carte.Status != true)
                    Console.WriteLine(carte.Nume_Carte + ' ' +
                        carte.ISBN + ' ' +
                        carte.Pret + ' ' +
                        "Carte disponibila \n");
                else
                    Console.WriteLine(carte.Nume_Carte + ' ' + 
                        carte.ISBN + ' ' + 
                        carte.Pret + ' ' + 
                        "Carte imprumutata \n");
            });
        }
    }
}
