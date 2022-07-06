using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Model
{
    public class Meniu
    {
            public void meniuPrincipal()
            {
                Console.WriteLine("~~~~~ Meniu principal ~~~~~\n\n");
                Console.WriteLine("1. Adauga utilizator nou\n2. Biblioteca\n3. Imprumut, retur si achitare");
                Console.WriteLine("4. Doriti sa iesiti din aplicatie?\n");
            }

            public void meniuBiblioteca()
            {
                Console.WriteLine("~~~~~ Biblioteca ~~~~~\n\n");
                Console.WriteLine("1. Adaugare carte\n2. Aparitii carte\n3. Vizualizarea bibliotecii");
                Console.WriteLine("4. Doriti sa va intoarceti la meniul anterior?\n");
            }

            public void meniuReturnare()
            {
                Console.WriteLine("~~~~~ Imprumut, retur si achitare ~~~~~\n\n");
                Console.WriteLine("1. Imprumuta carte\n2. Returneaza carte\n3. Calculare pret");
                Console.WriteLine("4. Doriti sa va intoarceti la meniul anterior?\n");
            }

    }
}
