using CPDMS.Entities;
using CPDMS.Models.Request;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CPDMS.Utility
{
    public class CommonUtility
    {
        public DataTable GetData()
        {
            using (var context = new AdminDcpcContext())
            {

                var ss = context.MStates.ToList();

                var dt = new DataTable();
                var conn = context.Database.GetDbConnection();
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open) conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "TEST";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("Name", "Sandeep"));
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (connectionState != ConnectionState.Closed) conn.Close();
                }
                return dt;
            }
        }

        public DataTable Registration(string NameOfUnit)
        {
            using (var context = new AdminDcpcContext())
            {

                var ss = context.MStates.ToList();

                var dt = new DataTable();
                var conn = context.Database.GetDbConnection();
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open) conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "PROC_Registration";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@NameofUnit",NameOfUnit));
                        //cmd.Parameters.Add(new SqlParameter("@CPCIICCode", tRegistrationDTO.CpciicCode));
                        //cmd.Parameters.Add(new SqlParameter("@UserID", tRegistrationDTO.UserId));
                        //cmd.Parameters.Add(new SqlParameter("@Password", tRegistrationDTO.Password));
                        //cmd.Parameters.Add(new SqlParameter("@Role", tRegistrationDTO.Role));
                        //cmd.Parameters.Add(new SqlParameter("@Mobileno", tRegistrationDTO.MobileNo));
                        //cmd.Parameters.Add(new SqlParameter("@EmailId", tRegistrationDTO.EmailId));
                        //cmd.Parameters.Add(new SqlParameter("@MobileOTP", tRegistrationDTO.MobileOtp));
                        //cmd.Parameters.Add(new SqlParameter("@EmailOTP", tRegistrationDTO.EmailOtp));
                        //cmd.Parameters.Add(new SqlParameter("@IPAddress", tRegistrationDTO.IpAddress));
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (connectionState != ConnectionState.Closed) conn.Close();
                }
                return dt;
            }
        }
    }
}
