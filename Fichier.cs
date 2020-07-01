using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGestion_v1._0
{
    class Fichier
    {
        int idFichier;
        String nom;
        int fatherId;

        public Fichier(int idFichier, String name, int fatherId)
        {
            this.idFichier = idFichier;
            nom = name;
            this.fatherId = fatherId;
        }
        public int getIdFichier()
        {
            return idFichier;
        }
        public int getFatherId()
        {
            return fatherId;
        }
        public String getName()
        {
            return nom;
        }
    }
}
