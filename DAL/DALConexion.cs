using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
   public class DALConexion
    {

        
        SqlConnection conexion = new SqlConnection();
        

     
        public SqlConnection AbriConexion()
        {
            
            if (conexion.State==ConnectionState.Closed)
            {
                conexion.Open();
            }
            return conexion;
        }

        public SqlConnection CerrarConexion()
        {
           
            if (conexion.State==ConnectionState.Open)
            {
                conexion.Close();
            }
            return conexion;
        }

      // constructor para conexion en pc de produccion

      /*
        public DALConexion()
        {
            conexion.ConnectionString= @"Data Source=DESKTOP-MP4BG10\SQLEXPRESS01; Initial Catalog=SAHMv6;User id=usersahm;Password=fear2007";
        }
        */
        // constructor para conexion en pc oficina avellaneda

      
        public DALConexion()
        {
            conexion.ConnectionString = @"Data Source=JMSOSA\SQLEXPRESS; Initial Catalog=SAHMv6;Trusted_Connection=True";
            
            // cadena de conexion para ambito de desarrolllo
           //conexion.ConnectionString = @"Data Source=172.16.13.60,1433\SQLEXPRESS; Initial Catalog=SAHMv6-P;User id=jmsosa;Password=fear2007";


            // cadena de conexion para ambito de produccion
            //conexion.ConnectionString = @"Data Source=172.16.13.60,1433\SQLEXPRESS; Initial Catalog=SAHMv6;User id=jmsosa;Password=fear2007";

            // conexion para pruebas desde otras maquinas
            //conexion.ConnectionString = @"Data Source=172.16.17.117,1733\SQLEXPRESS01; Initial Catalog=SAHMv6;User id=jmsosa;Password=fear2007";
        }
    }
}
