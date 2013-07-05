using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Configuration.Provider;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace App_Code.User_Mapping
{


    #region UsersMapping Object

    public class UserMapping
    {
        #region Constructer

        public UserMapping()
        {
            this.UserId = Guid.NewGuid();
        }
        #endregion

        #region Fields & Properties


        private Guid userId;
        public Guid UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private string deviceId = "";
        public string DeviceId
        {
            get { return deviceId; }
            set { deviceId = value; }
        }

        private int meterId = 0;
        public int MeterId
        {
            get { return meterId; }
            set { meterId = value; }
        }

     

        #endregion

    }


    #endregion

    public static class UserMapping_S
    {

        #region Feilds

        private static string connString = ConfigurationManager.ConnectionStrings["BillingAppConnectionString"].ConnectionString;

        private static DbProviderFactory provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["BillingAppConnectionString"].ProviderName);
        private static string parmPrefix = "@";

        #endregion

        #region Methods

  

        public static UserMapping MapUser(Guid UserId)
        {
            UserMapping userDetail = new UserMapping();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT UserID,DeviceID,MeterID" +
                                         " FROM Faculty_Mapping WHERE UserID = @UserId";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpID = provider.CreateParameter();
                        dpID.ParameterName = parmPrefix + "UserId";
                        dpID.Value = UserId;
                        cmd.Parameters.Add(dpID);


                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    userDetail = new UserMapping();

                                    if (!rdr.IsDBNull(0))
                                    {
                                        userDetail.UserId = rdr.GetGuid(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        userDetail.DeviceId = rdr.GetString(1);
                                    }

                                    if (!rdr.IsDBNull(2))
                                    {
                                        userDetail.MeterId = rdr.GetInt32(2);
                                    }
                                   

                                }
                            }
                            else
                            {
                                return null;
                            }
                        }



                    }
                    conn.Close();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
            return userDetail;

        }

        public static List<UserMapping> ListAllMeters(string deviceId)
        {
            List<UserMapping> allMeters = new List<UserMapping>();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT UserID,DeviceID,MeterID" +
                                         " FROM Faculty_Mapping WHERE DeviceID = @DeviceId";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpID = provider.CreateParameter();
                        dpID.ParameterName = parmPrefix + "DeviceId";
                        dpID.Value = deviceId;
                        cmd.Parameters.Add(dpID);


                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                  UserMapping  meter = new UserMapping();

                                    if (!rdr.IsDBNull(0))
                                    {
                                        meter.UserId = rdr.GetGuid(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        meter.DeviceId = rdr.GetString(1);
                                    }

                                    if (!rdr.IsDBNull(2))
                                    {
                                        meter.MeterId = rdr.GetInt32(2);
                                    }
                                    allMeters.Add(meter);

                                }
                            }
                            else
                            {
                                return null;
                            }
                        }



                    }
                    conn.Close();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
            return allMeters;

        }


        public static UserMapping UserMapWithMeterDevice(string deviceId, int meterId)
        {
            UserMapping meter = new UserMapping();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT UserID" +
                                         " FROM Faculty_Mapping WHERE DeviceID = @DeviceId AND MeterID = @meterId";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpID = provider.CreateParameter();
                        dpID.ParameterName = parmPrefix + "DeviceId";
                        dpID.Value = deviceId;
                        cmd.Parameters.Add(dpID);

                        DbParameter dpmID = provider.CreateParameter();
                        dpmID.ParameterName = parmPrefix + "MeterId";
                        dpmID.Value = meterId;
                        cmd.Parameters.Add(dpmID);


                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    meter = new UserMapping();

                                    if (!rdr.IsDBNull(0))
                                    {
                                        meter.UserId = rdr.GetGuid(0);
                                    }
                                    

                                }
                            }
                            else
                            {
                                return null;
                            }
                        }



                    }
                    conn.Close();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
            return meter;

        }

        public static string InsertMap(UserMapping insertMap)
        {
            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;
                    conn.Open();

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery;
                        sqlQuery = "INSERT INTO Faculty_Mapping" +
                               "(DeviceID,UserID,MeterID) " +
                               "VALUES(@deviceID,@userID,@meterID)";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter sNewId = provider.CreateParameter();
                        sNewId.ParameterName = parmPrefix + "userID";
                        sNewId.Value = insertMap.UserId;
                        cmd.Parameters.Add(sNewId);

                        DbParameter sDeviceId = provider.CreateParameter();
                        sDeviceId.ParameterName = parmPrefix + "deviceID";
                        sDeviceId.Value = insertMap.DeviceId;
                        cmd.Parameters.Add(sDeviceId);


                        DbParameter sMeterID = provider.CreateParameter();
                        sMeterID.ParameterName = parmPrefix + "meterID";
                        sMeterID.Value = insertMap.MeterId;
                        cmd.Parameters.Add(sMeterID);

                     

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    conn.Close();
                }
                return true.ToString();
            }
            catch (Exception exp)
            {
                return null;
            }

        }

        public static bool UpdateMap(UserMapping insertMap)
        {
            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;
                    conn.Open();

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery;
                        sqlQuery = "Update Faculty_Mapping " +
                               "SET DeviceID = @deviceID, MeterID = @meterID WHERE UserID = @userID ";
                             

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter sNewId = provider.CreateParameter();
                        sNewId.ParameterName = parmPrefix + "userID";
                        sNewId.Value = insertMap.UserId;
                        cmd.Parameters.Add(sNewId);

                        DbParameter sDeviceId = provider.CreateParameter();
                        sDeviceId.ParameterName = parmPrefix + "deviceID";
                        sDeviceId.Value = insertMap.DeviceId;
                        cmd.Parameters.Add(sDeviceId);


                        DbParameter sMeterID = provider.CreateParameter();
                        sMeterID.ParameterName = parmPrefix + "meterID";
                        sMeterID.Value = insertMap.MeterId;
                        cmd.Parameters.Add(sMeterID);



                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    conn.Close();
                }
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }

        }



        #endregion

    }
}