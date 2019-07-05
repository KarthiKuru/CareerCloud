using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        public void Add(params SecurityRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);

            using (conn)
            {
                foreach (SecurityRolePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Security_Roles ([Id],[Role],[Is_Inactive])
                                                VALUES(@Id,@Role,@Is_Inactive)", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Role", poco.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection
            (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand(@"SELECT [Id], [Role], 
                                                [Is_Inactive]
                                                FROM Security_Roles", conn);

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SecurityRolePoco[] SecRolePocos = new SecurityRolePoco[2000];
                while (rdr.Read())
                {
                    SecurityRolePoco poco = new SecurityRolePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Role = rdr.GetString(1);
                    poco.IsInactive = rdr.GetBoolean(2);

                    SecRolePocos[x] = poco;
                    x++;

                }
                conn.Close();
                return SecRolePocos.Where(a => a != null).ToList();

            }
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection
                 (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            using (conn)
            {

                foreach (SecurityRolePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Security_Roles WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection
                (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            using (conn)
            {
                foreach (SecurityRolePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE Security_Roles 
                                                    SET[Role] =@Role ,[Is_Inactive]=@Is_Inactive
                                                    WHERE [Id]= @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Role", poco.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
    }

