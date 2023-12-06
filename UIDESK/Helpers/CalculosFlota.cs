using DAL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;

namespace UIDESK.Helpers
{
    public class CalculosFlota
    {
        DALConexion conexion = new DALConexion();

        //metodo que devuelve el estado de la flota en funcion de porcentajes de km de los vehiculos de una categoria
        public string SituacionFlotaKilometraje(int idcate,List<VhKmRango> lista)
        {
            string resultado = string.Empty;
            int rangoMayor = lista[0].CantVh;// almacenamos los datos del primer elemento de la lista
            int idrangoMayor = lista[0].IdRango;

            for (int i = 1; i < lista.Count; i++) // en este bucle determinamos el mayor de esos elementos
            {
                
                if (rangoMayor < lista[i].CantVh)
                {
                    rangoMayor = lista[i].CantVh;
                    idrangoMayor = lista[i].IdRango;
                }
            }
            switch (idrangoMayor) // comparamos el idrango del objeto mayor cantidad de vehiculos  dentro de la lista
            {
                case 1: //0-50
                    resultado = "bueno";
                break;
                case 2: //50-100
                    resultado = "bueno";
                    break;
                case 3: //100-200
                    resultado = "bueno";
                    break; 
                case 4: //200-300
                    if (idcate == 1 || idcate == 3) // autos y minibuses
                    {
                        resultado = "regular";
                    }
                    else
                    {
                        if (idcate == 2) // pickup
                        {
                            resultado = "bueno";
                        }
                    }
                    break;
                case 5://300-400
                    if (idcate == 1 || idcate == 3) // autos y minibuses
                    {
                        resultado = "malo";
                    }
                    else
                    {
                        if (idcate == 2) // pickup
                        {
                            resultado = "regular";
                        }
                    }
                    break;
                case 6: //400-500
                    if (idcate == 1 || idcate == 3) // autos y minibuses
                    {
                        resultado = "malo";
                    }
                    else
                    {
                        if (idcate == 2) // pickup
                        {
                            resultado = "regular";
                        }
                    }
                    break;
                case 7: // mas de 500
                    if (idcate == 1 || idcate == 3) // autos y minibuses
                    {
                        resultado = "malo";
                    }
                    else
                    {
                        if (idcate == 2) // pickup
                        {
                            resultado = "malo";
                        }
                    }
                    break;
            }
            return resultado;
        }
        public List<VhKmAvgCate>PromedioKmCategoria(int idcate, int anios)
        {
            List<VhKmAvgCate> lista = new List<VhKmAvgCate> ();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculos_PromedioKm_Categoria";
            cmd.Parameters.AddWithValue ("@idcate", idcate);
            cmd.Parameters.AddWithValue("@anios", anios);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader ();
                while (reader.Read ()) { 
                    VhKmAvgCate vh = new VhKmAvgCate ();
                    vh.Idvh = (int)reader["idvh"];
                    vh.Modelo = (string)reader["modelo"];
                    vh.Dominio = (string)reader["dominio"];
                    vh.Marca = (string)reader["nombre_marca"];
                    vh.KmAcumulado = (decimal)reader["km_acumulado"];
                   
                    if (reader["costo_repo_dls"] != DBNull.Value)
                    {
                        vh.CostoRepoDls = (decimal)reader["costo_repo_dls"];
                    }
                    else{ vh.CostoRepoDls = 0;}
                   
                    if (reader["PromedioConsumo"] != DBNull.Value)
                    {
                        vh.PromedioConsumo = (decimal)reader["PromedioConsumo"];
                    }
                    else { vh.PromedioConsumo =0; }
                    if (reader["PromedioMante"] != DBNull.Value)
                    {
                        vh.PromedioMante = (decimal)reader["PromedioMante"];
                    }
                    else { vh.PromedioMante = 0; }
                    if (reader["PronosticoConsumo"] != DBNull.Value)
                    {
                        vh.PronosticoConsumo = (decimal)reader["PronosticoConsumo"];
                    }
                    else { vh.PronosticoConsumo = 0; }
                    if (reader["PronosticoMante"] != DBNull.Value)
                    {
                        vh.PronosticoMante = (decimal)reader["PronosticoMante"];
                    }
                    else { vh.PronosticoMante = 0; }
                   
                    
                    lista.Add (vh);
                }
                conexion.CerrarConexion();
               
            }
            catch (Exception)
            {

                throw;
            }
            return lista; // lista con los datos promedio de km realizado por mes y promedio de costo de mantenimiento por mes para los vehiculos de una categoria
        }
    }
}
