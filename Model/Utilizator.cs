using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Model
{
   

    public class Utilizator 
    {
        
        public int Utilizator_ID { get; set; }
        public string Nume_Utilizator { get; set; }
        public Dictionary<string, DateTime> Carti_imprumutate { get; set; }
      
    }
}
