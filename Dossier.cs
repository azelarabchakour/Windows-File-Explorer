using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGestion_v1._0
{
    class Dossier
    {
        int idDossier;
        String nom;
        int fatherId;

        public Dossier(String name, int fatherId)
        {
            nom = name;
            this.fatherId = fatherId;
        }
        public Dossier(int id, String name, int fatherId)
        {
            idDossier = id;
            nom = name;
            this.fatherId = fatherId;
        }
        public int getIdDossier()
        {
            return idDossier;
        }
        public String getNom()
        {
            return nom;
        }
        public void setNom(String name)
        {
            this.nom = name;
        }
        public int getFatherId()
        {
            return fatherId;
        }
    }
}
