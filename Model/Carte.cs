using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Functii;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Model
{
    public class Carte
    {

        public string ISBN { get; set; }
        public string Nume_Carte { get; set; }
        public float Pret { get; set; }

        /// <summary>
        /// True - Imprumutata
        /// False - Neimprumutata
        /// </summary>
        public bool Status { get; set; }


        public Carte()
        {
            Status = false;
        }      
    }
}
