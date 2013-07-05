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

namespace App_Code.Login
{


    #region Users Object

    public class UserLogin
    {
        #region Constructer

        public UserLogin()
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

        private string userName = "";
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string password = "";
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string fullName="";
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        private string address = "";
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string mobile = "";
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }




        #endregion

    }


    #endregion

    public static class UserLogin_S
    {

        #region Feilds

        private static string connString = ConfigurationManager.ConnectionStrings["BillingAppConnectionString"].ConnectionString;

        private static DbProviderFactory provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["BillingAppConnectionString"].ProviderName);
        private static string parmPrefix = "@";

        #endregion

        #region Methods

        public static UserLogin Loging(string UserName)
        {
            UserLogin userDetail = new UserLogin();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT UserID,UserName,Password,FullName" +
                                         " FROM Login_Info WHERE UserName = @UserName";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpID = provider.CreateParameter();
                        dpID.ParameterName = parmPrefix + "UserName";
                        dpID.Value = UserName;
                        cmd.Parameters.Add(dpID);


                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    userDetail = new UserLogin();

                                    if (!rdr.IsDBNull(0))
                                    {
                                        userDetail.UserId = rdr.GetGuid(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        userDetail.UserName = rdr.GetString(1);
                                    }
                                   
                                    if (!rdr.IsDBNull(2))
                                    {
                                        userDetail.Password = rdr.GetString(2);
                                    }
                                    if (!rdr.IsDBNull(3))
                                    {
                                        userDetail.FullName = rdr.GetString(3);
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

        public static UserLogin NewLoging(string UserName, string password)
        {
            UserLogin userDetail = new UserLogin();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT UserID,UserName,Password,FullName" +
                                         " FROM Login_Info WHERE UserName = @UserName AND Password=@password";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpID = provider.CreateParameter();
                        dpID.ParameterName = parmPrefix + "UserName";
                        dpID.Value = UserName;
                        cmd.Parameters.Add(dpID);

                        DbParameter dpass = provider.CreateParameter();
                        dpass.ParameterName = parmPrefix + "password";
                        dpass.Value = password;
                        cmd.Parameters.Add(dpass);


                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    userDetail = new UserLogin();

                                    if (!rdr.IsDBNull(0))
                                    {
                                        userDetail.UserId = rdr.GetGuid(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        userDetail.UserName = rdr.GetString(1);
                                    }

                                    if (!rdr.IsDBNull(2))
                                    {
                                        userDetail.Password = rdr.GetString(2);
                                    }
                                    if (!rdr.IsDBNull(3))
                                    {
                                        userDetail.FullName = rdr.GetString(3);
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


        public static UserLogin GetUserDetailsById(string UserId)
        {
            UserLogin userDetail = new UserLogin();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT UserID,UserName,Password,FullName" +
                                         " FROM Login_Info WHERE UserID = @UserId";

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
                                    userDetail = new UserLogin();

                                    if (!rdr.IsDBNull(0))
                                    {
                                        userDetail.UserId = rdr.GetGuid(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        userDetail.UserName = rdr.GetString(1);
                                    }
                                   
                                    if (!rdr.IsDBNull(2))
                                    {
                                        userDetail.Password = rdr.GetString(2);
                                    }
                                    if (!rdr.IsDBNull(3))
                                    {
                                        userDetail.FullName = rdr.GetString(3);
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

        public static List<UserLogin> ListOfAllUsers()
        {
            List<UserLogin> AllUsers = new List<UserLogin>();           

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT UserID,UserName,Password,FullName" +
                                         " FROM Login_Info";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;                

                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                   UserLogin userDetail = new UserLogin();

                                    if (!rdr.IsDBNull(0))
                                    {
                                        userDetail.UserId = rdr.GetGuid(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        userDetail.UserName = rdr.GetString(1);
                                    }

                                    if (!rdr.IsDBNull(2))
                                    {
                                        userDetail.Password = rdr.GetString(2);
                                    }
                                    if (!rdr.IsDBNull(3))
                                    {
                                        userDetail.FullName = rdr.GetString(3);
                                    }
                                    AllUsers.Add(userDetail);
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                        conn.Close();
                        return AllUsers;
                    }
                }
            }
            catch (Exception exp)
            {
                return null;
            }        

        }

        public static bool InsertUser(UserLogin insertUser)
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
                        sqlQuery = "INSERT INTO Login_Info" +
                               "(UserID,UserName,Password,FullName,mobile,address) " +
                               "VALUES(@id,@userName,@password,@fullName,@mobile,@address)";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter sNewId = provider.CreateParameter();
                        sNewId.ParameterName = parmPrefix + "id";
                        sNewId.Value = insertUser.UserId;
                        cmd.Parameters.Add(sNewId);

                        DbParameter sUserName = provider.CreateParameter();
                        sUserName.ParameterName = parmPrefix + "userName";
                        sUserName.Value = insertUser.UserName;
                        cmd.Parameters.Add(sUserName);

                       
                        DbParameter sPassword = provider.CreateParameter();
                        sPassword.ParameterName = parmPrefix + "password";
                        sPassword.Value = insertUser.Password;
                        cmd.Parameters.Add(sPassword);

                        DbParameter sFullName = provider.CreateParameter();
                        sFullName.ParameterName = parmPrefix + "fullName";
                        sFullName.Value = insertUser.FullName;
                        cmd.Parameters.Add(sFullName);

                        DbParameter sMobile = provider.CreateParameter();
                        sMobile.ParameterName = parmPrefix + "mobile";
                        sMobile.Value = insertUser.Mobile;
                        cmd.Parameters.Add(sMobile);

                        DbParameter sAddress = provider.CreateParameter();
                        sAddress.ParameterName = parmPrefix + "address";
                        sAddress.Value = insertUser.Address;
                        cmd.Parameters.Add(sAddress);


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

        public static UserLogin SeeUserInfo(string UserName)
        {
            UserLogin userDetail = new UserLogin();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT UserID,UserName,FullName,address,mobile" +
                                         " FROM Login_Info WHERE UserName = @UserName";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpID = provider.CreateParameter();
                        dpID.ParameterName = parmPrefix + "UserName";
                        dpID.Value = UserName;
                        cmd.Parameters.Add(dpID);


                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    userDetail = new UserLogin();

                                    if (!rdr.IsDBNull(0))
                                    {
                                        userDetail.UserId = rdr.GetGuid(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        userDetail.UserName = rdr.GetString(1);
                                    }                                   
                                    if (!rdr.IsDBNull(2))
                                    {
                                        userDetail.FullName = rdr.GetString(2);
                                    }
                                    if (!rdr.IsDBNull(3))
                                    {
                                        userDetail.Address = rdr.GetString(3);
                                    }
                                    if (!rdr.IsDBNull(4))
                                    {
                                        userDetail.Mobile = rdr.GetString(4);
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

        public static bool ResetPassword(UserLogin newPwdUser)
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
                        sqlQuery = "UPDATE login_Info " +
                               "SET Password = @password WHERE userID = @userId";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;



                        DbParameter sUserId = provider.CreateParameter();
                        sUserId.ParameterName = parmPrefix + "userId";
                        sUserId.Value = newPwdUser.UserId;
                        cmd.Parameters.Add(sUserId);


                        DbParameter sPassword = provider.CreateParameter();
                        sPassword.ParameterName = parmPrefix + "password";
                        sPassword.Value = newPwdUser.Password;
                        cmd.Parameters.Add(sPassword);

                     

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