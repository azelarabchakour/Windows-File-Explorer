﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGestion_v1._0
{
    class Metier
    {
        public List<Dossier> listerDossier(int fatherId)
        {
            List<Dossier> list = new List<Dossier>();
            DataBase D = new DataBase();
            list = D.SelectDossier(fatherId);
            return list;
        }
        public List<Fichier> listerFichier(int fatherId)
        {
            List<Fichier> list = new List<Fichier>();
            DataBase D = new DataBase();
            list = D.SelectFichier(fatherId);
            return list;
        }
        public void createDossier(String nom, int fatherId)
        {
            DataBase D = new DataBase();
            D.insertDossier(nom, fatherId);
         
        }
        public bool createFichier(String nom, int fatherId)
        {
            DataBase D = new DataBase();
            if (D.insertFichier(nom, fatherId))
                return true;
            else
                return false;
        }
        public void deleteDossier(int idDossier)
        {
            DataBase db = new DataBase();
            db.deleteDossier(idDossier);
        }
        public void deleteFichier(int idFichier)
        {
            DataBase db = new DataBase();
            db.deleteFichier(idFichier);
        }        
        public bool renameDossier(int idDossier, String newName, int fatherId)
        {
            DataBase db = new DataBase();
            return db.renameDossier(idDossier, newName, fatherId);
                
        }
        public bool renameFichier(int idFichier ,String newName,int fatherId)
        {
            DataBase db = new DataBase();
            return db.renameFichier(idFichier, newName, fatherId);
        }
        public void copyDossier(int idSource, int fatherIdDestionation)
        {
            DataBase db = new DataBase();
            db.copyDossier("dossier", idSource, fatherIdDestionation);
        }
        public void copyFichier(int idSource, int fatherIdDestionation)
        {
            DataBase db = new DataBase();
            db.copyFichier("fichier", idSource, fatherIdDestionation);
        }
        public void cutDossier(int idSource, int fatherIdDestination)
        {
            DataBase db = new DataBase();
            db.cutDossier("dossier", idSource, fatherIdDestination);
        }
        public void cutFichier(int idSource, int fatherIdDestination)
        {
            DataBase db = new DataBase();
            db.cutFichier("fichier", idSource, fatherIdDestination);
        }
        public int returnId(int fatherId, String nom)
        {
            DataBase db = new DataBase();
            return db.returnId(nom, fatherId);
        }
        public int returnIdFichier(int fatherId, String nom)
        {
            DataBase db = new DataBase();
            return db.returnIdFichier(nom, fatherId);
            
        }
        public int returnFatherId(int id)
        {
            DataBase db = new DataBase();
            return db.returnFatherId(id);
        }
        public bool find(String name, int fatherId)
        {
            DataBase d = new DataBase();
            return d.find("dossier", name, fatherId);
        }
        public bool findFichier(String name, int fatherId)
        {
            DataBase d = new DataBase();
            return d.find("fichier", name, fatherId);
        }
        
        public List<Dossier> search(int id, String name)
        {
            DataBase db = new DataBase();
            return db.search(id, name);
        }
        public String searchWithId(int id)
        {
            DataBase db = new DataBase();
            return db.searchWithId(id);
        }

        public void copyDemo(int idSource, int idDestionation)
        {
            DataBase db = new DataBase();
            List<Dossier> d = new List<Dossier>();
            d = listerDossier(idSource);
            foreach(Dossier dd in d)
            {
                db.copyDossier("dossier",dd.getIdDossier(),returnId(idDestionation,dd.getNom()));
            }
            foreach(Dossier dd in d)
            {
                copyDemo(dd.getIdDossier(),returnId(idDestionation,dd.getNom()));
            }
        }
    }
}