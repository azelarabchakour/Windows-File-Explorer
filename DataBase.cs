using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace FileGestion_v1._0
{
    class DataBase
    {
        string server;
        string database;
        string uid;
        string password;
        string connectionString;
        MySqlConnection connection;  


        public bool openConnection()
        {

            server = "localhost";
            database = "csharpdatabase";
            uid = "root";
            password = "";
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        public bool closeConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public void insertDossier(string nom, int fatherId)
        {
            
            
                string query = "INSERT INTO dossier (nom, fatherId) VALUES('" + nom + "', '" + fatherId + "')";
                //open connection
                if (this.openConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Execute command
                    cmd.ExecuteNonQuery();
                    //Console.WriteLine("done");
                    //close connection
                    this.closeConnection();
                   
                }
            
            
           
            
        }
        public bool insertFichier(string nom, int fatherId)
        {
            int count = -1;
            List<Fichier> d = new List<Fichier>();
            d = SelectFichier(fatherId);
            foreach (Fichier dd in d)
            {
                if (dd.getName() == nom)
                    count++;
            }

            if (count == -1)
            {
                string query = "INSERT INTO fichier (nom, fatherId) VALUES('" + nom + "', '" + fatherId + "')";
                //open connection
                if (this.openConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Execute command
                    cmd.ExecuteNonQuery();
                    //Console.WriteLine("done");
                    //close connection
                    this.closeConnection();
                    return true;
                }
            }

            return false;
        }

        public List<Dossier> SelectDossier(int currentDirectory)
        {
            string query = "SELECT * FROM dossier WHERE fatherId=" + currentDirectory;
            //string query = "SELECT nom FROM dossier WHERE fatherId=33";

            //Create a list to store the result
            List<Dossier> list = new List<Dossier>();

            //Open connection
            if (this.openConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    Dossier d = new Dossier(
                        int.Parse(dataReader["idDossier"] + ""),
                        dataReader["nom"] + "",
                        int.Parse(dataReader["fatherId"] + "")
                        );
                    list.Add(d);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.closeConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }
        public List<Fichier> SelectFichier(int currentDirectory)
        {
            string query = "SELECT * FROM fichier WHERE fatherId=" + currentDirectory;
            //string query = "SELECT nom FROM dossier WHERE fatherId=33";

            //Create a list to store the result
            List<Fichier> list = new List<Fichier>();

            //Open connection
            if (this.openConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    Fichier d = new Fichier(
                        int.Parse(dataReader["idFichier"] + ""),
                        dataReader["nom"] + "",
                        int.Parse(dataReader["fatherId"] + "")
                        );
                    list.Add(d);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.closeConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }
        public bool find(string ForD, string name, int fatherId)
        {
            int count = -1;
            string query = "SELECT nom FROM " + ForD + " WHERE nom ='" + name + "' and  fatherId ="+ fatherId ;
            //string query = "SELECT nom FROM dossier WHERE fatherId=33";

            //Create a list to store the result
            List<string> list = new List<string>();

            //Open connection
            this.openConnection();
            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                count++;
            }

            this.closeConnection();
            if (count > -1)
            {
                return true;
            }
            return false;



        }


        //public int returnFatherId(string ForD, string nom)
        //{
        //    string query = "SELECT fatherId FROM " + ForD + " WHERE nom='" + nom + "'";
        //    int fatherId = -1;

        //    //Open Connection
        //    if (this.openConnection() == true)
        //    {
        //        //Create Mysql Command
        //        MySqlCommand cmd = new MySqlCommand(query, connection);

        //        //ExecuteScalar will return one value
        //        fatherId = int.Parse(cmd.ExecuteScalar() + "");

        //        //close Connection
        //        this.closeConnection();

        //        return fatherId;
        //    }
        //    else
        //    {
        //        return fatherId;
        //    }
        //}
        public void deleteDossier(int idDossier)
        {
            String query = "DELETE FROM dossier WHERE idDossier =" + idDossier;
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.closeConnection();
            }
        }
        public void deleteFichier(int idFichier)
        {
            String query = "DELETE FROM fichier WHERE idfichier =" + idFichier;
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.closeConnection();
            }
        }
        public bool renameDossier(int idDossier, String newName,int fatherId)
        {
            int count = -1;
            List<Dossier> d = new List<Dossier>();
            d = SelectDossier(fatherId);
            foreach (Dossier dd in d)
            {
                if(dd.getIdDossier() != idDossier)
                    if (dd.getNom() == newName)
                        count++;
            }
            if(count == -1)
            {
                String query = "UPDATE  dossier SET nom = '" + newName + "' WHERE idDossier = " + idDossier;
                if (this.openConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.closeConnection();
                    return true;
                }

            }
            return false;
        }
        public bool renameFichier(int idFichier, String newName,int fatherId)
        {
            int count = -1;
            List<Fichier> d = new List<Fichier>();
            d = SelectFichier(fatherId);
            foreach (Fichier dd in d)
            {
                if (dd.getIdFichier() != idFichier)
                    if (dd.getName() == newName)
                        count++;
            }
            if (count == -1)
            {
                String query = "UPDATE  fichier SET nom = '" + newName + "' WHERE idFichier = " + idFichier;
                if (this.openConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.closeConnection();
                    return true;
                }
            }
            return false;
        }
        public void cutFichier(string ForD, int idSource, int fatherIdDestination)
        {
            String query = "UPDATE fichier set fatherId = " + fatherIdDestination + " WHERE idFichier = " + idSource;
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.closeConnection();
            }
        }
        public void cutDossier(string ForD, int idSource, int fatherIdDestination)
        {
            String query = "UPDATE dossier set fatherId = " + fatherIdDestination + " WHERE idDossier = " + idSource;
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.closeConnection();
            }
        }
        public Dossier getDossier(int idDossier)
        {
            Dossier d = null;
            string query = "SELECT * FROM dossier WHERE idDossier=" + idDossier;
            if(this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    d = new Dossier(
                        int.Parse(dataReader["idDossier"] + ""),
                        dataReader["nom"] + "",
                        int.Parse(dataReader["fatherId"] + "")
                        );
                }
                    
                this.closeConnection();
                return d;
            }
            return d;
        }
        public Fichier getFichier(int idFichier)
        {
            Fichier d = null;
            string query = "SELECT * FROM fichier WHERE idFichier=" + idFichier;
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    d = new Fichier(
                        int.Parse(dataReader["idFichier"] + ""),
                        dataReader["nom"] + "",
                        int.Parse(dataReader["fatherId"] + "")
                        );
                }
                    
                this.closeConnection();
                return d;
            }
            return d;
        }
        public void copyDossier(string ForD, int idSource, int fatherIdDestination)
        {
            Dossier d = getDossier(idSource);
            insertDossier(d.getNom(), fatherIdDestination);
            /*List<Dossier> list = new List<Dossier>();
            list = SelectDossier(idSource);
            foreach(Dossier dd in list)
            {
                insertDossier(dd.getNom(), fatherIdDestination);
            }*/
        }
        public void copyFichier(string ForD, int idSource, int fatherIdDestination)
        {
            Fichier f = getFichier(idSource);
            insertFichier(f.getName(), fatherIdDestination);
        }
        public int returnId(String nom, int fatherId)
        {
            String query = "SELECT * FROM dossier WHERE nom ='" + nom + "' AND fatherId =" + fatherId;
            if(this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if(dataReader.Read())
                {
                    int id = int.Parse(dataReader["idDossier"] + "");
                    this.closeConnection();
                    return id;
                }
                this.closeConnection();
            }
            return -1;
        }
        public int returnIdFichier(String nom, int fatherId)
        {
            String query = "SELECT * FROM fichier WHERE nom ='" + nom + "' AND fatherId =" + fatherId;
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    int id = int.Parse(dataReader["idFichier"] + "");
                    this.closeConnection();
                    return id;
                }
                this.closeConnection();
            }
            return -1;
        }
        public int returnFatherId(int Id)
        {
            
            String query = "SELECT fatherId FROM dossier WHERE idDossier=" + Id;
            if(this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    int id = int.Parse(dataReader["fatherId"] + "");
                    this.closeConnection();
                    return id;
                }
            }
            return -1;
        }
        public List<Dossier> search(int id, String name)
        {
            String query = "SELECT * FROM dossier WHERE nom = '" + name + "' and fatherId = " + id;
            List<Dossier> list = new List<Dossier>();

            if (this.openConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    Dossier d = new Dossier(
                        int.Parse(dataReader["idDossier"] + ""),
                        dataReader["nom"] + "",
                        int.Parse(dataReader["fatherId"] + "")
                        );
                    list.Add(d);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.closeConnection();

                //return list to be displayed
                return list;

            }
            return list;
        }

        public String searchWithId(int id)
        {
            String s = "";
            String query = "SELECT nom FROM dossier where idDossier =" + id;
            if(this.openConnection())
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if(dataReader.Read())
                {
                    s = dataReader["nom"].ToString();
                    this.closeConnection();
                    return s;
                }
            }
            return s;
        }
        public int getLastFolderCreated()
        {
            string query = "SELECT * FROM dossier";
            int max = 0;

            //Open connection
            if (this.openConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    if (int.Parse(dataReader["idDossier"] + "") > max)
                        max = int.Parse(dataReader["idDossier"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.closeConnection();

                //return list to be displayed
                return max;
            }
            else
                return max;
        }
        public int getLastFileCreated()
        {
            string query = "SELECT * FROM fichier";
            int max = 0;

            //Open connection
            if (this.openConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    if (int.Parse(dataReader["idFichier"] + "") > max)
                        max = int.Parse(dataReader["idFichier"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.closeConnection();

                //return list to be displayed
                return max;
            }
            else
                return max;
        }
    }
}
