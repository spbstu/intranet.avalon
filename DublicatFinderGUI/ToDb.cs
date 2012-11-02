using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DublicatFinderGUI
{
    [Serializable]
    class ToDb
    {
//        private string myConnectionString = @"Server=GCOMPAQ\GLEB; Database=webdev_glekuz ;Trusted_Connection=Yes";
        private const string myConnectionString = @"Server=CHECKMATES; Database=webdev_glekuz ;Trusted_Connection=Yes";
        
        public string SelectDublicate { get { return @"Duplicates_GetDuplicate"; }}

        public string SelectPersonsByFIO { get { return @"Persons_SelectByFullName"; } }

        private string errors = string.Empty;
        
        private DataSet _personsDataSet ;

        #region properties
        public string Errors { get { return errors; } }
       

        public string ConnectionString
        {
            get { return myConnectionString; }
        }
        public int PersonsCount()
        {
            return _personsDataSet.Tables["APersons"].Rows.Count;
        }

        public DataSet GetPersonsDataSet()
        {
            return _personsDataSet;
        }

        #endregion

    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeProcedureName"> store Procedure Name or sql command text</param>
        /// <param name="storeProcedure"></param>
        public  void ExecStoredProcedure(string storeProcedureName,bool storeProcedure = true)
        {
            SqlConnection connection = new SqlConnection(myConnectionString);
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(storeProcedureName, connection)) 
                {
                    if(storeProcedure) command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                errors += exception.Message;
            }
            finally
            {
                connection.Close();
            }
            
        }

        public void ExecStoredProcedure(string storeProcedureName, int parametr, string parametrName)
        {
            SqlConnection connection = new SqlConnection(myConnectionString);
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(storeProcedureName, connection)) //@"Duplicates_BuildTable"  @"Duplicates_DeleteTable"
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(parametrName, parametr);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                errors += exception.Message;
            }
            finally
            {
                connection.Close();
            }

        }

        public string CreateSelectString(string tableName, List<string> personIDs, bool personId )
        {
            string commandText;
            if(personId)
                commandText =  @"Select * from "+ tableName + " where PersonID = " + personIDs[0];
            else
                commandText =  @"Select * from "+ tableName + " where StudentID = " + personIDs[0];
            for(var i =1 ; i<personIDs.Count ; i++)
            {
                if(personId)commandText += " OR PersonID = " + personIDs[i];
                else commandText += " OR StudentID = " + personIDs[i];
            }

            return commandText;
        }

        public void ExecUpdatePersonIdInOtherTables(int oldID, int newID)
        {
            SqlConnection connection = new SqlConnection(myConnectionString);
            try
            {
                connection.Open();
                using (SqlCommand comm = new SqlCommand("UpdatePersonIdInOtherTables", connection)) 
                {
                    
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("newID", newID);
                    comm.Parameters.AddWithValue("oldID", oldID);
                    comm.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                errors += exception.Message;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}