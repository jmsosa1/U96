using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ENTIDADES;
using System.Collections.ObjectModel;

namespace DAL
{
    //clase con los metodos necesarios para administrar los proveedores , depositos 
    public class DALProveedor
    {
        DALConexion conn= new DALConexion();

      

        public DALProveedor()
        {
            
        }

        public void CrearProveedor(Proveedor proveedor)
        {
           SqlCommand cmd = new SqlCommand();
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.CommandText = "pa_Proveedor_Alta";
           cmd.Connection = conn.AbriConexion();

            //asiganmos valores a los parametros
            cmd.Parameters.AddWithValue("@nombre", proveedor.Nombre);
            cmd.Parameters.AddWithValue("@razonsocial", proveedor.RazonSocial);
            cmd.Parameters.AddWithValue("@dir1", proveedor.Dir1);
            cmd.Parameters.AddWithValue("@dir2", proveedor.Dir2);
            cmd.Parameters.AddWithValue("@tel1", proveedor.Tel1);
            cmd.Parameters.AddWithValue("@tel2", proveedor.Tel2);
            cmd.Parameters.AddWithValue("@email", proveedor.Email);
            cmd.Parameters.AddWithValue("@sitioweb", proveedor.Web);
            cmd.Parameters.AddWithValue("@contacto", proveedor.Contacto);
            cmd.Parameters.AddWithValue("@cuit", proveedor.Cuit);
            cmd.Parameters.AddWithValue("@altaf", proveedor.Altaf);
            cmd.Parameters.AddWithValue("@idrubro", proveedor.IdRubro);
            cmd.Parameters.AddWithValue("@idlocalidad", proveedor.IdLocalidad);
            cmd.Parameters.AddWithValue("@idprovincia", proveedor.IdProvincia);
            cmd.Parameters.AddWithValue("@cuittexto", proveedor.CuitTexto);

            try
            {
                conn.AbriConexion();
                cmd.ExecuteNonQuery();
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            
            

        }

        public void ActualizarProveedor(Proveedor proveedor)
        {
                //creamos el comando
                SqlCommand   cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn.AbriConexion();
                cmd.CommandText = "Proveedor_Modifica";
            //asiganmos valores a los parametros
            cmd.Parameters.AddWithValue("@idproveedor", proveedor.IdProve);
            cmd.Parameters.AddWithValue("@nombre", proveedor.Nombre);
            cmd.Parameters.AddWithValue("@razonsocial", proveedor.RazonSocial);
            cmd.Parameters.AddWithValue("@dir1", proveedor.Dir1);
            cmd.Parameters.AddWithValue("@dir2", proveedor.Dir2);
            cmd.Parameters.AddWithValue("@tel1", proveedor.Tel1);
            cmd.Parameters.AddWithValue("@tel2", proveedor.Tel2);
            cmd.Parameters.AddWithValue("@email", proveedor.Email);
            cmd.Parameters.AddWithValue("@sitioweb", proveedor.Web);
            cmd.Parameters.AddWithValue("@contacto", proveedor.Contacto);
            cmd.Parameters.AddWithValue("@cuit", proveedor.Cuit);
            cmd.Parameters.AddWithValue("@idrubro", proveedor.IdRubro);
            cmd.Parameters.AddWithValue("@idlocalidad", proveedor.IdLocalidad);
            cmd.Parameters.AddWithValue("@idprovincia", proveedor.IdProvincia);



            try
            {
                conn.AbriConexion();
                
                 cmd.ExecuteNonQuery();
                conn.CerrarConexion();
               
            }
            catch (Exception)
            {

                throw;
            }
               
            
        }

        public void BajaProveedor(int idproveedor)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Proveedor_Baja";
            cmd.Connection = conn.AbriConexion();
            cmd.Parameters.AddWithValue("@idproveedor", idproveedor);
            try
            {
                conn.AbriConexion();
                cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

        }

        //TO DO
        // BuscarPorNombre
        public ObservableCollection<Proveedor> BuscarPorNombre(string nomprove)
        {
            //campos locales
         

            ObservableCollection<Proveedor> lista_proveedores = new ObservableCollection<Proveedor>();

               
               SqlCommand  cmd = new SqlCommand();
                cmd.Connection = conn.AbriConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pa_Proveedor_Buscar_Nombre";
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 100);
                cmd.Parameters["@nombre"].Value = nomprove;
                try
                {
                    conn.AbriConexion();
                    SqlDataReader lectura_reg = cmd.ExecuteReader();
                    //le asignamos valores a sus propiedades desde el objeto sqldatareader creado
                    //nota : en los campos que no son obligatorios, debera usarse un operador ternario "?" para evaluar si es null
                    // de lo contrario dara error al hacer la conversion
                    //actualizacion : esta verificacion debe hacerse en la interfaz ,llenando los campos que esten vacios 
                    //con valores por defecto
                    lista_proveedores = ArmarLista(lectura_reg);
                    conn.CerrarConexion();
                }
                catch (Exception)
                {

                    throw;
                }

            
            return lista_proveedores;

        }
      
        //BuscarPorRazonSocial
        public ObservableCollection<Proveedor> BuscarPorRazonSocial(string razonsocial)
        {
            //campos locales
            string _razon = razonsocial + "%";
            ObservableCollection<Proveedor> lista_proveedores = new ObservableCollection<Proveedor>();

     
            
                SqlCommand cmd = new SqlCommand(); // declaramos el comando y lo seteamos
                cmd.Connection = conn.AbriConexion();                        //
                cmd.CommandType = CommandType.StoredProcedure; //
                cmd.CommandText = "pa_Proveedor_Buscar_Razon"; // 
                cmd.Parameters.AddWithValue("@razon",_razon);//

                try
                {
                    conn.AbriConexion(); // abrimos la conexion
                    SqlDataReader lectura_reg = cmd.ExecuteReader(); // creamos el objeto reader y le pasamos el resultado del comando

                    lista_proveedores = ArmarLista(lectura_reg); // llamamos la metodo que devuelve la lista de registros y lo asignamos
                                                                 // al la lista de retorno
                    conn.CerrarConexion(); // cerramos la conexion
                }
                catch (Exception)
                {

                    throw;
                }

            
            return lista_proveedores; // devolvemos la lista

        }

        //BuscarPorCuit
        public Proveedor BuscarPorCuit(int numcuit)
        {
            //creamos el comando y lo parametrizamos

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn.AbriConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pa_Proveedor_Buscar_Cuit";
                cmd.Parameters.Add("@cuit", SqlDbType.Int);
                cmd.Parameters["@cuit"].Value = numcuit;
                Proveedor p = new Proveedor();

                try
                {
                    conn.AbriConexion();
                    SqlDataReader lectura = cmd.ExecuteReader(CommandBehavior.SingleRow);
                    p = ArmarProveedor(lectura); //llamamos a la funcion armadora del objeto
                    conn.CerrarConexion();
                   
                }
                catch (Exception)
                {

                    throw;
                }
                return p;

        }

        //Buscar por Id
        public Proveedor BuscarPorId(int _idprove)
        {
            //creamos el comando y lo parametrizamos
            Proveedor p = new Proveedor();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Proveedor_Buscar_Id";
            cmd.Parameters.AddWithValue("@idprove", _idprove);
            
            try
            {
                conn.AbriConexion();
                SqlDataReader lectura = cmd.ExecuteReader();
                p = ArmarProveedor(lectura);
                conn.CerrarConexion();
               
            }
            catch (Exception)
            {

                throw;
            }
            return p;
            
        }

        // Listar ProveedoresPorRubro
        public ObservableCollection<Proveedor>ProveedorPorRubro(int numrubro)
        {
            //campos locales

            ObservableCollection<Proveedor> lista_proveedores = new ObservableCollection<Proveedor>();

                SqlCommand cmd = new SqlCommand(); // declaramos el comando y lo seteamos
                cmd.Connection = conn.AbriConexion();                        //
                cmd.CommandType = CommandType.StoredProcedure; //
                cmd.CommandText = "pa_Proveedor_Por_Rubro"; // 
                cmd.Parameters.Add("@idrubro", SqlDbType.VarChar, 100);//
                cmd.Parameters["@idrubro"].Value = numrubro;//

                try
                {
                    conn.AbriConexion(); // abrimos la conexion
                    SqlDataReader lectura_reg = cmd.ExecuteReader(); // creamos el objeto reader y le pasamos el resultado del comando

                    lista_proveedores = ArmarLista(lectura_reg); // llamamos la metodo que devuelve la lista de registros y lo asignamos
                                                                 // al la lista de retorno
                    conn.CerrarConexion(); // cerramos la conexion
                }
                catch (Exception)
                {

                    throw;
                }

            
            return lista_proveedores; // devolvemos la lista

        }
        
        //ProveedoresPorLocalidad
        public ObservableCollection<Proveedor>ProveedorPorLocalidad(int numlocalidad)
        {
            //campos locales

            ObservableCollection<Proveedor> lista_proveedores = new ObservableCollection<Proveedor>();

         
                SqlCommand cmd = new SqlCommand(); // declaramos el comando y lo seteamos
                cmd.Connection = conn.AbriConexion();                        //
                cmd.CommandType = CommandType.StoredProcedure; //
                cmd.CommandText = "pa_Proveedor_Por_Localidad"; // 
                cmd.Parameters.Add("@idlocalidad", SqlDbType.VarChar, 100);//
                cmd.Parameters["@idlocalidad"].Value = numlocalidad;//

                try
                {
                    conn.AbriConexion(); // abrimos la conexion
                    SqlDataReader lectura_reg = cmd.ExecuteReader(); // creamos el objeto reader y le pasamos el resultado del comando

                    lista_proveedores = ArmarLista(lectura_reg); // llamamos la metodo que devuelve la lista de registros y lo asignamos
                                                                 // al la lista de retorno
                    conn.CerrarConexion(); // cerramos la conexion
                }
                catch (Exception)
                {

                    throw;
                }

            
            return lista_proveedores; // devolvemos la lista

        }

        //ProveedoresPorProvincia
        public ObservableCollection<Proveedor>ProveedorPorProvincia(int numprovincia)
        {
            //campos locales

            ObservableCollection<Proveedor> lista_proveedores = new ObservableCollection<Proveedor>();

          
                SqlCommand cmd = new SqlCommand(); // declaramos el comando y lo seteamos
                cmd.Connection = conn.AbriConexion();                        //
                cmd.CommandType = CommandType.StoredProcedure; //
                cmd.CommandText = "pa_Proveedor_Por_Provincia"; // 
                cmd.Parameters.Add("@idlocalidad", SqlDbType.VarChar, 100);//
                cmd.Parameters["@idlocalidad"].Value = numprovincia;//

                try
                {
                    conn.AbriConexion(); // abrimos la conexion
                    SqlDataReader lectura_reg = cmd.ExecuteReader(); // creamos el objeto reader y le pasamos el resultado del comando

                    lista_proveedores = ArmarLista(lectura_reg); // llamamos la metodo que devuelve la lista de registros y lo asignamos
                                                                 // al la lista de retorno
                    conn.CerrarConexion(); // cerramos la conexion
                }
                catch (Exception)
                {

                    throw;
                }

            
            return lista_proveedores; // devolvemos la lista
        }
        
        //ProveedoresTodos
        public ObservableCollection<Proveedor> ProveedorTodos()
        {
            //campos locales

            ObservableCollection<Proveedor> lista_proveedores = new ObservableCollection<Proveedor>();

        
                SqlCommand cmd = new SqlCommand(); // declaramos el comando y lo seteamos
                cmd.Connection = conn.AbriConexion();                        //
                cmd.CommandType = CommandType.StoredProcedure; //
                cmd.CommandText = "pa_Proveedor_Todos"; // 
           

                try
                {
                    conn.AbriConexion(); // abrimos la conexion
                    SqlDataReader lectura_reg = cmd.ExecuteReader(); // creamos el objeto reader y le pasamos el resultado del comando

                    lista_proveedores = ArmarLista(lectura_reg); // llamamos la metodo que devuelve la lista de registros y lo asignamos
                                                                 // al la lista de retorno
                    conn.CerrarConexion(); // cerramos la conexion
                }
                catch (Exception)
                {

                    throw;
                }

            
            return lista_proveedores; // devolvemos la lista
        }

        public List<Proveedor>ProveedorCombobox(string _razon)
        {
            List<Proveedor> lista_proveedores = new List<Proveedor>();

            string razonsocial = _razon + '%';
            SqlCommand cmd = new SqlCommand(); // declaramos el comando y lo seteamos
            cmd.Connection = conn.AbriConexion();                        //
            cmd.CommandType = CommandType.StoredProcedure; //
            cmd.CommandText = "pa_Proveedor_Combobox";//
            cmd.Parameters.AddWithValue("@razon", razonsocial);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Proveedor proveedor = new Proveedor();
                    proveedor.IdProve = (int)reader["idprove"];
                    proveedor.RazonSocial = (string)reader["razonsocial"];
                    lista_proveedores.Add(proveedor);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_proveedores;
        }

        //ValidarCuit
        public int ProveedorValidarCuit(decimal cuit_validar)
        {
            int idproveedor = 0;

            decimal _cuit=0;
            //asiganmos valores a las variables
            _cuit = cuit_validar;
            //creamos el comando
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Proveedor_Valida_cuit";
            //agregamos el parametro del id del empleado
            cmd.Parameters.AddWithValue("@cuit", _cuit);
            conn.AbriConexion();
            idproveedor = cmd.ExecuteNonQuery();
            conn.CerrarConexion();
            return idproveedor;

        }

        public List<RubroProve> ListarRubros()
        {
            List<RubroProve> rubroProves = new List<RubroProve>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_RubroProve_Todos";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RubroProve rubro = new RubroProve();
                    rubro.IdRubro = (int)reader["idrubro"];
                    rubro.NombreRubro = (string)reader["nombrerubro"];
                    rubroProves.Add(rubro);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return rubroProves;
        }

        //dar de baja un proveedor
        public void DarBajaUnProveedor(int idproveedor)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Proveedor_Baja";
            cmd.Connection = conn.AbriConexion();
            try
            {
                conn.AbriConexion();
                cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Armar Lista

        private ObservableCollection<Proveedor> ArmarLista(SqlDataReader lectura)
        {
            ObservableCollection<Proveedor> lista = new ObservableCollection<Proveedor>();

            while (lectura.Read())
            {
                //le asignamos valores a sus propiedades desde el objeto sqldatareader creado
                //nota : en los campos que no son obligatorios, debera usarse un operador ternario "?" para evaluar si es null
                // de lo contrario dara error al hacer la conversion
                //actualizacion : esta verificacion debe hacerse en la interfaz ,llenando los campos que esten vacios 
                //con valores por defecto
                Proveedor proveedor = new Proveedor();
                proveedor.IdProve = (int)lectura["idprove"];
                proveedor.Nombre = (string)lectura["nombre"];
                proveedor.RazonSocial = (string)lectura["razonsocial"];
                proveedor.Dir1 = (string)lectura["dir1"];
                proveedor.Dir2 = (string)lectura["dir2"];
                proveedor.Altaf = (DateTime)lectura["altaf"];
                if (lectura["bajaf"] != DBNull.Value)
                {
                    proveedor.BajaF = (DateTime)lectura["bajaf"];
                }
                else
                {
                    proveedor.BajaF = null;
                }
                proveedor.Tel1 = (string)lectura["tel1"];
                proveedor.Tel2 = (string)lectura["tel2"];
                proveedor.Cuit = (decimal)lectura["cuit"];
                proveedor.Email = (string)lectura["email"];
                proveedor.Web = (string)lectura["sitioweb"];
                proveedor.IdLocalidad = (int)lectura["idlocalidad"];
                proveedor.IdProvincia = (int)lectura["idprovincia"];
                proveedor.IdRubro = (int)lectura["idrubro"];
                proveedor.Contacto = (string)lectura["contacto"];
                proveedor.Estado = (int)lectura["estado"];
                proveedor.Observaciones = (string)lectura["observaciones"];
                proveedor.Localidad = (string)lectura["nomlocalidad"];
                proveedor.Provincia = (string)lectura["nomprovincia"];
                proveedor.Rubro = (string)lectura["nombrerubro"];
                proveedor.CuitTexto = (string)lectura["cuit_texto"];
                proveedor.Atencion = (int)lectura["atencion"];
                proveedor.Calidad = (int)lectura["calidad"];
                proveedor.Precio = (int)lectura["precio"];
                proveedor.Plazo = (int)lectura["plazo"];
                proveedor.Calificacion = (int)lectura["calificacion"];
                lista.Add(proveedor);
            }
            return lista;
        }
        
        //devolver un objeto proveedor
        private Proveedor ArmarProveedor(SqlDataReader lectura)
        {
            Proveedor proveedor = new Proveedor();
            while (lectura.Read())
            {
               
                //le asignamos valores a sus propiedades desde el objeto sqldatareader creado
                //nota : en los campos que no son obligatorios, debera usarse un operador ternario "?" para evaluar si es null
                // de lo contrario dara error al hacer la conversion
                //actualizacion : esta verificacion debe hacerse en la interfaz ,llenando los campos que esten vacios 
                //con valores por defecto
                proveedor.IdProve = (int)lectura["idprove"];
                proveedor.Nombre = (string)lectura["nombre"];
                proveedor.RazonSocial = (string)lectura["razonsocial"];
                proveedor.Dir1 = (string)lectura["dir1"];
                proveedor.Dir2 = (string)lectura["dir2"];
                proveedor.Altaf = (DateTime)lectura["altaf"];
                if (lectura["bajaf"] != DBNull.Value)
                {
                    proveedor.BajaF = (DateTime)lectura["bajaf"];
                }
                else
                {
                    proveedor.BajaF = null;
                }

                proveedor.Tel1 = (string)lectura["tel1"];
                proveedor.Tel2 = (string)lectura["tel2"];
                proveedor.Cuit = (decimal)lectura["cuit"];
                proveedor.Email = (string)lectura["email"];
                proveedor.Web = (string)lectura["sitioweb"];
                proveedor.IdLocalidad = (int)lectura["idlocalidad"];
                proveedor.IdProvincia = (int)lectura["idprovincia"];
                proveedor.IdRubro = (int)lectura["idrubro"];
                proveedor.Contacto = (string)lectura["contacto"];
                proveedor.Estado = (int)lectura["estado"];
                proveedor.Observaciones = (string)lectura["observaciones"];
                proveedor.Localidad = (string)lectura["nomlocalidad"];
                proveedor.Provincia = (string)lectura["nomprovincia"];
                proveedor.Rubro = (string)lectura["nombrerubro"];
            }
            return proveedor;

        }

        public void ActualizarCalificacion(int idprove ,int plazo, int calidad, int precio, int atencion)
        {
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Proveedor_Actualizar_Califacion";
            cmd.Connection = conn.AbriConexion();
            cmd.Parameters.AddWithValue("@idprove", idprove);
            cmd.Parameters.AddWithValue("@plazo", plazo);
            cmd.Parameters.AddWithValue("@calidad", calidad);
            cmd.Parameters.AddWithValue("@precio", precio);
            cmd.Parameters.AddWithValue("@atencion", atencion);
          
            try
            {
                conn.AbriConexion();
                cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public List<CompraP>ProveedorListaCompras(int idprove)
        {
            List<CompraP> lista = new List<CompraP>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Proveedor_Lista_Compras";
            cmd.Parameters.AddWithValue("@idprove", idprove);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CompraP compra = new CompraP();
                    compra.IdCompra = (int)reader["idcompra"];
                    if (reader["altaf"] != DBNull.Value)
                    {
                        compra.Alta = (DateTime)reader["altaf"];
                    }
                    if (reader["fecha_compra"]!= DBNull.Value)
                    {
                        compra.FechaCompra = (DateTime)reader["fecha_compra"];
                    }
                    else
                    {
                        compra.FechaCompra = null;
                    }
                    if (reader["fecha_factura"]!= DBNull.Value)
                    {
                        compra.FechaFactura = (DateTime)reader["fecha_factura"];
                    }
                    else
                    {
                        compra.FechaFactura = null;
                    }
                    if (reader["importe_compra"]!=null)
                    {
                        compra.ImporteCompra = (decimal)reader["importe_compra"];
                    }
                    else
                    {
                        compra.ImporteCompra = 0;
                    }
                    if (reader["num_factura"]!= null)
                    {
                        compra.NumFactura = (string)reader["num_factura"];
                    }
                    else
                    {
                        compra.NumFactura = "";
                    }
                    if (reader["orden_compra"]!= null)
                    {
                        compra.OC = (string)reader["orden_compra"];
                    }
                    else
                    {
                        compra.OC = "";
                    }
                    
                    
                    
                    lista.Add(compra);                               
                }
                    conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }
        
    }
}
