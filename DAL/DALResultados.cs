using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTIDADES;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace DAL
{
    public class DALResultados
    {
        DALConexion conexion = new DALConexion();
        public DALResultados()
        {

        }

        public ObservableCollection<RelacionManteKM> RelManteKM()
        {
            
            ObservableCollection<RelacionManteKM> relacions = new ObservableCollection<RelacionManteKM>();
           
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculo_Costo_Mante_Por_Kilometro";

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RelacionManteKM manteKM = new RelacionManteKM();
                    manteKM.Idvh = (int)reader["idvh"];
                    manteKM.Dominio = (string)reader["dominio"];
                    manteKM.Modelo = (string)reader["modelo"];
                    manteKM.Marca = (string)reader["nombre_marca"];
                    manteKM.KmAcumulado = (decimal)reader["KMAcumulado"];
                    manteKM.CostoManteAcu = (decimal)reader["MAcumulado"];
                    manteKM.Relacion = (decimal)reader["Relacion"];
                    manteKM.Categoria = (string)reader["nomcate"];
                    manteKM.AnioModelo = (string)reader["aniomodelo"];
                    
                    relacions.Add(manteKM);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return relacions;
        }

        public ObservableCollection<RelacionManteHs> RelManteHs()
        {
            ObservableCollection<RelacionManteHs> relacions = new ObservableCollection<RelacionManteHs>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculo_Costo_Mante_Por_Hora";

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RelacionManteHs manteKM = new RelacionManteHs();
                    manteKM.Idvh = (int)reader["idvh"];
                    manteKM.Dominio = (string)reader["dominio"];
                    manteKM.Modelo = (string)reader["modelo"];
                    manteKM.Marca = (string)reader["nombre_marca"];
                    manteKM.HsAcumuladas = (int)reader["HSAcumulado"];
                    manteKM.CostoManteAcu = (decimal)reader["MAcumulado"];
                    manteKM.Relacion = (decimal)reader["Relacion"];
                    manteKM.Categoria = (string)reader["nomcate"];
                    manteKM.AnioModelo = (string)reader["aniomodelo"];
                    relacions.Add(manteKM);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return relacions;
        }

        public ObservableCollection<SituacionOperativaVh>SituacionOpListarTodas()
        {
            ObservableCollection<SituacionOperativaVh> relacions = new ObservableCollection<SituacionOperativaVh>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculos_Situacion_Operativa";

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SituacionOperativaVh situacion = new SituacionOperativaVh();
                    situacion.Idvh = (int)reader["idvh"];
                    situacion.IdCate = (int)reader["idcate"];
                    situacion.Dominio = (string)reader["dominio"];
                    situacion.Modelo = (string)reader["modelo"];
                    situacion.Categoria = (string)reader["nomcate"];
                    situacion.DiasAcumulados = (int)reader["diasacu"];
                    if (reader["diasparo"] == DBNull.Value)
                    {
                        situacion.DiasParo = 0;
                    }
                    else
                    {
                        situacion.DiasParo = (int)reader["diasparo"];
                    }
                    if (reader["diasmante"] == DBNull.Value)
                    {
                        situacion.DiasMante = 0;
                    }
                    else
                    {
                        situacion.DiasMante = (int)reader["diasmante"];
                    }
                    situacion.CostoDiarioParo = (decimal)reader["costo_diario_parada"];
                    situacion.CostoDiarioUso = (decimal)reader["costo_diario_uso"];

                    relacions.Add(situacion);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return relacions;
        }


        public List<VhKmRango>VehiculosPorRangoKm(int idcatevh)
        {
            List<VhKmRango> lista = new List<VhKmRango>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculos_Cantidad_por_Kilometraje";
            cmd.Parameters.AddWithValue("@idcate", idcatevh);
            cmd.Parameters.Add("@cant050",SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@cant50100", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@cant100200", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@cant200300", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@cant300400", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@cant400500", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@cant500", SqlDbType.Int).Direction = ParameterDirection.Output;

            try
            {
                conexion.AbriConexion();
                int i = cmd.ExecuteNonQuery();
                //creamos la lista
                VhKmRango vh1 = new VhKmRango() { IdRango=1,NombreRango="Entre 0y 50k", CantVh = Convert.ToInt16(cmd.Parameters["@cant050"].Value)};
                VhKmRango vh2 = new VhKmRango() { IdRango = 2, NombreRango = "Entre 50 y 100k", CantVh = Convert.ToInt16(cmd.Parameters["@cant50100"].Value) };
                VhKmRango vh3 = new VhKmRango() { IdRango = 3, NombreRango = "Entre 100 y 200k", CantVh = Convert.ToInt16(cmd.Parameters["@cant100200"].Value) };
                VhKmRango vh4 = new VhKmRango() { IdRango = 4, NombreRango = "Entre 200 y 3000k", CantVh = Convert.ToInt16(cmd.Parameters["@cant200300"].Value) };
                VhKmRango vh5 = new VhKmRango() { IdRango = 5, NombreRango = "Entre 300 y 400k", CantVh = Convert.ToInt16(cmd.Parameters["@cant300400"].Value) };
                VhKmRango vh6 = new VhKmRango() { IdRango = 6, NombreRango = "Entre 400 y 500k", CantVh = Convert.ToInt16(cmd.Parameters["@cant400500"].Value) };
                VhKmRango vh7 = new VhKmRango() { IdRango = 7, NombreRango = "Mas 500k", CantVh = Convert.ToInt16(cmd.Parameters["@cant500"].Value) };
                lista.Add(vh1);
                lista.Add(vh2);
                lista.Add(vh3);
                lista.Add(vh4);
                lista.Add(vh5);
                lista.Add(vh6);
                lista.Add(vh7);

                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }
        public VhKmAvg VehiculosKmPromedio(int idcatevh)
        {
            VhKmAvg kmAvg = new VhKmAvg();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculos_Kilometraje_promedio";
            cmd.Parameters.AddWithValue("@idcate", idcatevh);
            cmd.Parameters.Add("@cantidad",SqlDbType.Int).Direction= ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@kmtotal",SqlDbType.Decimal).Direction= ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@average",SqlDbType.Decimal).Direction= ParameterDirection.Output;
            try
            {
                conexion.AbriConexion();
              
                int i = cmd.ExecuteNonQuery();
                kmAvg.CantVh = Convert.ToInt32(cmd.Parameters["@cantidad"].Value);
                kmAvg.Promedio = Convert.ToDecimal(cmd.Parameters["@average"].Value);
                kmAvg.TotalKM = Convert.ToDecimal(cmd.Parameters["@kmtotal"].Value);



                conexion.CerrarConexion();
            }
            catch(Exception)
            {
                throw;
            }
            return kmAvg;
        }

        public Vehiculo VehiculoMasnuevo( int idcatevh)
        {
            Vehiculo km = new Vehiculo();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Resultado_Vehiculo_mas_nuevo";
            cmd.Parameters.AddWithValue("@categoria", idcatevh);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    km.IdVh = (int)reader["idvh"];
                    km.AnioModelo = (string)reader["aniomodelo"];
                    km.Descripcion = (string)reader["descripcion"];
                    km.Modelo = (string)reader["modelo"];
                    km.Dominio = (string)reader["dominio"];
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return km;
        }

        public Vehiculo VehiculoMasViejo(int idcatevh)
        {
            Vehiculo k = new Vehiculo();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Resultado_Vehiculo_mas_viejo";
            cmd.Parameters.AddWithValue("@categoria", idcatevh);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    k.IdVh = (int)reader["idvh"];
                    k.AnioModelo = (string)reader["aniomodelo"];
                    k.Descripcion = (string)reader["descripcion"];
                    k.Modelo = (string)reader["modelo"];
                    k.Dominio = (string)reader["dominio"];
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return k;
        }

        public List<VhAnioModelo>VehiculosPorAnioModelo(int idcatevh) 
        { 
            List<VhAnioModelo> lista = new List<VhAnioModelo>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculos_AnioModelo";
            cmd.Parameters.AddWithValue("@idcate", idcatevh);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader(); 
                while (reader.Read())
                {
                    VhAnioModelo vh = new VhAnioModelo();
                    vh.AnioModelo = (string)reader["aniomodelo"];
                    vh.CantAnioModelo = (int)reader["Cantidad"];
                    lista.Add(vh);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }
       

        public List<Vehiculo>VehiculosInversiones(int idcatevh, int aniodesde, int aniohasta)
        {
            List<Vehiculo> lista = new List<Vehiculo>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculos_Inversiones_Realizadas";
            cmd.Parameters.AddWithValue("@idcate", idcatevh);
            cmd.Parameters.AddWithValue("@aniodesde", aniodesde);
            cmd.Parameters.AddWithValue("@aniohasta", aniohasta);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Vehiculo vh = new Vehiculo();
                    vh.IdVh = (int)reader["idvh"];
                    vh.Modelo = (string)reader["modelo"];
                    vh.Dominio = (string)reader["dominio"];
                    vh.AnioModelo = (string)reader["aniomodelo"];
                    vh.KmInicial = (decimal)reader["kminicial"];
                    vh.ValorCompra = (decimal)reader["valorcompra"];
                    vh.FechaCompra = (DateTime)reader["fechacompra"];
                    if (reader["fechacompra"]!= DBNull.Value)
                    {
                        vh.FechaCompra = (DateTime)reader["fechacompra"];
                    }
                    else
                    {
                        vh.FechaCompra = null;
                    }
                    lista.Add(vh);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

       
    }
}
