using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebMvcExam.Controllers;
using WebMvcExam.Models;

namespace WebMvcExam.Models
{
    public class DataComponent
    {
        static string CONNECTIONSTRING = @"Data Source=RILPT180;Initial Catalog=varsha;Persist Security Info=True;User ID=sa;Password=sa123";
        public List<Showcase> GetAllDresses()
        {
            var list = new List<Showcase>();
            using (SqlConnection con = new SqlConnection(CONNECTIONSTRING))
            {
                try
                {
                    con.Open();
                    SqlCommand sqlCmd = con.CreateCommand();
                    sqlCmd.CommandText = "SELECT * FROM SHOP";
                    var reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var shop = new Showcase();
                        shop.shop_id = Convert.ToInt32(reader[0]);
                        shop.shop_name = reader[1].ToString();
                        shop.dress_type = reader[2].ToString();
                        shop.dress_price = Convert.ToInt32(reader[3]);
                        shop.dress_size = Convert.ToInt32(reader[4]);
                        list.Add(shop);
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            return list;
        }
    }
    public Showcase FindDress(int id)
    {
        Showcase dr = new Showcase();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM SHOP WHERE SHOP_ID = " + id;
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    dr.shop_id = Convert.ToInt32(reader[0]);
                    dr.shop_name = reader[1].ToString();
                    dr.dress_type = reader[2].ToString();
                    dr.dress_price = Convert.ToInt32(reader[3]);
                    dr.dress_size = Convert.ToInt32(reader[4]);
                }
                else
                    throw new Exception("No Dress found");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }


    }

}
public void UpdateShowcase(Showcase dr)
{
    using (SqlConnection con = new SqlConnection(CONNECTIONSTRING))
    {
        var query = $"UPDATE ShopTable set ShopName = '{ dr.shop_name }', drDress_type = '{dr.dress_type}', Dres_size = {dr.dress_size} WHERE Shop_id = {dr.shop_id}";
        SqlCommand cmd = new SqlCommand(query, con);
        try
        {
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected == 0)
                throw new Exception("No Dresses were updated");
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
    }
}

public void AddNewDress(Showcase dr)
{
    using (SqlConnection con = new SqlConnection(CONNECTIONSTRING))
    {
        var query = "Insert into ShopTable values(@shop_id, @shop_name, @dress_type, @dress_price, @dress_size)";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@shop_id", dr.shop_id);
        cmd.Parameters.AddWithValue("@shop_name", dr.shop_name);
        cmd.Parameters.AddWithValue("@dress_type", dr.dress_type);
        cmd.Parameters.AddWithValue("@dress_price", dr.dress_price);
        cmd.Parameters.AddWithValue("@dress_size", dr.dress_size);
        try
        {
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected == 0)
                throw new Exception("No Dresses were added");
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
    }
}
public void DeleteDress(int shopId)
{
    using (SqlConnection con = new SqlConnection(CONNECTIONSTRING))
    {
        var query = $"Delete * from  shop where Shop_Id={shopId}";
        SqlCommand Command = new SqlCommand(query, con);
        try
        {
            con.Open();
            int dress = Command.ExecuteNonQuery();
            if (dress == 0)
                throw new Exception("Dress does not exists");
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }

    }
}



