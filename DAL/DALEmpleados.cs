using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using ENTIDADES;

namespace DAL
{
    public   class DALEmpleados
    {
        
        //creamos un obejeto conexion desde la clase DALConexion
        DALConexion conn = new DALConexion();
        //variables usadas como parametros  por las instrucciones sql
       


        //CONSTRUCTOR DE CLASE
       public DALEmpleados()
        {
            
        }

        #region[AMB]
        // METODO PARA INSERTAR UN NUEVO REGISTRO EN LA TABLA EMPLEADO
        public void CrearEmpleado(Empleado nuevo)
        {
            //procedimiento con datos basicos del empleado           

         
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Empleado_Alta";
            cmd.Connection = conn.AbriConexion();
            cmd.Parameters.AddWithValue("@nom", nuevo.Nombre);
            cmd.Parameters.AddWithValue("@dni", nuevo.Dni);
            cmd.Parameters.AddWithValue("@direccion", nuevo.Direccion);
            cmd.Parameters.AddWithValue("@te", nuevo.TeParticular);
            cmd.Parameters.AddWithValue("@celular", nuevo.TeCelular);
            cmd.Parameters.AddWithValue("@idlocalidad", nuevo.IdLocalidad);
            cmd.Parameters.AddWithValue("@idprovincia", nuevo.IdProvincia);
            cmd.Parameters.AddWithValue("@idsector", nuevo.IdSector);
            cmd.Parameters.AddWithValue("@idcate", nuevo.IdCatEmpleado);
            cmd.Parameters.AddWithValue("@altaf", nuevo.AltaF);
            cmd.Parameters.AddWithValue("@gremio", nuevo.Gremio);


            //ejecutamos el comando
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

        //METODO PARA MODIFICAR / ACTUALIZAR DATOS DE UN EMPLEADO
        public void ActualizarEmpleado(Empleado empleado)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Empleado_Actualiza_Datos_Uno";
            cmd.Connection = conn.AbriConexion();
            cmd.Parameters.AddWithValue("@idempleado", empleado.IdEmpleado);
            cmd.Parameters.AddWithValue("@nom", empleado.Nombre);
            cmd.Parameters.AddWithValue("@dni", empleado.Dni);
            cmd.Parameters.AddWithValue("@direccion", empleado.Direccion);
            cmd.Parameters.AddWithValue("@te", empleado.TeParticular);
            cmd.Parameters.AddWithValue("@celular", empleado.TeCelular);
            cmd.Parameters.AddWithValue("@idlocalidad", empleado.IdLocalidad);
            cmd.Parameters.AddWithValue("@idprovincia", empleado.IdProvincia);
            cmd.Parameters.AddWithValue("@idsector", empleado.IdSector);
            cmd.Parameters.AddWithValue("@idcate",empleado.IdCatEmpleado);
            cmd.Parameters.AddWithValue("@gremio", empleado.Gremio);
            cmd.Parameters.AddWithValue("@tcamisa", empleado.TCamisa);
            cmd.Parameters.AddWithValue("@tpantalon", empleado.TPantalon);
            cmd.Parameters.AddWithValue("@tcalzado", empleado.TCalzado);


            //ejecutamos el comando
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

        public void ActualizarFotoEmpleado (Empleado empleado)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Empleado_Actualizar_Foto";
            cmd.Connection = conn.AbriConexion();
            cmd.Parameters.AddWithValue("@idempleado", empleado.IdEmpleado);
            cmd.Parameters.AddWithValue("@foto", empleado.Foto);
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

        //METODO PARA INSERTAR UN NUEVO REGISTRO EN LA TABLA LICENCIA
        public void CrearLicenciaConducir(Licencia nueva_licencia)
        {

            //campos locales
            string strInstruccionSQL;
            int _idem;
            string _numl, _tazul, _psico, _lespecial;
            DateTime _vtol, _vtoa, _vtop, _vtoes;


            strInstruccionSQL = "insert into licencia(idempleado,numlicencia,tarjetazul,psicofisico,licespecial,vtolicencia,vtoazul,vtopsico,vtoespecial)";
            strInstruccionSQL += "values(@idem, @numl, @tazul, @psico, @lespecial, @vtol, @vtoa, @vtop, @vtoes)";
            // asignamos valores a las variables
            _idem = nueva_licencia.IdEmpleado;
            _numl = nueva_licencia.NumLicencia;
            _tazul = nueva_licencia.Tarjetazul;
            _psico = nueva_licencia.Psicofisico;
            _lespecial = nueva_licencia.Licespecial;
            _vtol = nueva_licencia.VtoLicencia;
            _vtoa = nueva_licencia.VtoAzul;
            _vtop = nueva_licencia.VtoPsico;
            _vtoes = nueva_licencia.VtoEspecial;
            // creamos el comado y lo parametrizamos
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = strInstruccionSQL;
            //asignamos valores a los parametros
            cmd.Parameters.AddWithValue("@idem", _idem);
            cmd.Parameters.AddWithValue("@numl", _numl);
            cmd.Parameters.AddWithValue("@tazul", _tazul);
            cmd.Parameters.AddWithValue("@psico", _psico);
            cmd.Parameters.AddWithValue("@lespecial", _lespecial);
            cmd.Parameters.AddWithValue("@vtol", _vtol);
            cmd.Parameters.AddWithValue("@vtoa", _vtoa);
            cmd.Parameters.AddWithValue("@vtop", _vtop);
            cmd.Parameters.AddWithValue("@vtoes", _vtoes);

            //abrimos la conexion 
            conn.AbriConexion();
            //ejecutamos el comando
            cmd.ExecuteNonQuery();
            //cerramos la conexion
            conn.CerrarConexion();

        }

        //METODO PARA MODIFICAR /ACTUALIZAR UN REGISTRO DE LA TABLA LICENCIA
        public void ModificarLicenciaConducir(Licencia nueva_licencia)
        {
            //campos locales


            string strInstruccionSQL;
            int _idem, _idlicencia;
            string _numl, _tazul, _psico, _lespecial;
            DateTime _vtol, _vtoa, _vtop, _vtoes;

            //creamos  y abrimos la conexion

            strInstruccionSQL = "insert into licencia(idempleado,numlicencia,tarjetazul,psicofisico,licespecial,vtolicencia,vtoazul,vtopsico,vtoespecial)";
            strInstruccionSQL += "values(@idem, @numl, @tazul, @psico, @lespecial, @vtol, @vtoa, @vtop, @vtoes)";
            // asignamos valores a las variables
            _idlicencia = nueva_licencia.IdLicencia;
            _idem = nueva_licencia.IdEmpleado;
            _numl = nueva_licencia.NumLicencia;
            _tazul = nueva_licencia.Tarjetazul;
            _psico = nueva_licencia.Psicofisico;
            _lespecial = nueva_licencia.Licespecial;
            _vtol = nueva_licencia.VtoLicencia;
            _vtoa = nueva_licencia.VtoAzul;
            _vtop = nueva_licencia.VtoPsico;
            _vtoes = nueva_licencia.VtoEspecial;

            // creamos el comando y lo parametrizamos
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = strInstruccionSQL;

            //asignamos valores a los parametros
            cmd.Parameters.AddWithValue("@idem", _idem);
            cmd.Parameters.AddWithValue("@numl", _numl);
            cmd.Parameters.AddWithValue("@tazul", _tazul);
            cmd.Parameters.AddWithValue("@psico", _psico);
            cmd.Parameters.AddWithValue("@lespecial", _lespecial);
            cmd.Parameters.AddWithValue("@vtol", _vtol);
            cmd.Parameters.AddWithValue("@vtoa", _vtoa);
            cmd.Parameters.AddWithValue("@vtop", _vtop);
            cmd.Parameters.AddWithValue("@vtoes", _vtoes);

            //abrimos la conexion 
            conn.AbriConexion();
            //ejecutamos el comando
            cmd.ExecuteNonQuery();
            //cerramos la conexion
            conn.CerrarConexion();
        }

        #endregion

        #region[consultas base]


        //BUSCAR UN EMPLEADO POR ID
        public Empleado BuscarPorId(int idempleado)
        {
            //campos locales
      
            SqlDataReader lectura;

            Empleado emp = new Empleado();
            //creamos el comando
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Empleado_Buscar_Id";
            //agregamos el parametro del id del empleado
            cmd.Parameters.AddWithValue("@idem",idempleado);
            

            //abrimos la coneccion
            try
            {
                conn.AbriConexion();
                //ejecutamos el comando
                lectura = cmd.ExecuteReader(CommandBehavior.SingleRow);
                //creamos el objeto que vamos a devolver
                 emp = ArmarEmpleado(lectura);
                conn.CerrarConexion();
              
            }
            catch (Exception)
            {

                throw;
            }

            return emp;
        }

        //BUSCAR UN EMPLEADO POR DNI
        public Empleado BuscarPorDni(int dni)
        {
                //creamos el comando y damos valores a sus propiedades
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn.AbriConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pa_Empleado_Buscar_Dni";
            cmd.Parameters.AddWithValue("dni", dni);
                
                try
                {
                    
                    SqlDataReader lectura = cmd.ExecuteReader(CommandBehavior.SingleRow);
                    Empleado emp = ArmarEmpleado(lectura);

                    conn.CerrarConexion();
                    return emp;
                }
                catch (Exception)
                {

                    throw;
                }

            

        }

        //BUSCAR UN EMPLEADO POR NOMBRE 
        public Empleado BuscarPorNombre(string nom_empleado)
        {
            
            
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn.AbriConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pa_empleado_buscar_nombre";
                
                cmd.Parameters.Add("@nom", SqlDbType.VarChar, 100);
                cmd.Parameters["@nom"].Value = nom_empleado;
                try
                {
                    conn.AbriConexion();
                    SqlDataReader lectura = cmd.ExecuteReader(CommandBehavior.SingleRow);
                    Empleado emp = ArmarEmpleado(lectura);

                    conn.CerrarConexion();
                    return emp;

                }
                catch (Exception)
                {

                    throw;
                }
            

        }

        //LISTAR TODOS LOS EMPLEADOS QUE PERTENECEN A UNA CATEGORIA
        public ObservableCollection<Empleado> EmpleadosPorCategoria(int _idcate)
        {
            
            
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn.AbriConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pa_Empleado_Por_Categoria";
                cmd.Parameters.Add("@idcate", SqlDbType.Int);
                cmd.Parameters["@idcate"].Value = _idcate;

                ObservableCollection<Empleado> lista_empleados = new ObservableCollection<Empleado>(); // contiene la lista de empleados devueltos por la consulta
                try
                {
                    conn.AbriConexion();
                    SqlDataReader lectura_reg = cmd.ExecuteReader();
                    lista_empleados = ArmarLista(lectura_reg);
                    conn.CerrarConexion();
                    
                }
                catch (Exception)
                {

                    throw;
                }
                return lista_empleados;
            
        }

        //LISTAR TODOS LOS EMPLEADOS QUE PERTENECEN A UN SECTOR 
        public ObservableCollection<Empleado> EmpleadosPorSector(int _idsector)
        {
            
            
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn.AbriConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pa_Empleado_Por_Sector";
                cmd.Parameters.Add("@idsector", SqlDbType.Int);
                cmd.Parameters["@idsector"].Value = _idsector;
                ObservableCollection<Empleado> lista_empleados = new ObservableCollection<Empleado>();

                try
                {
                    conn.AbriConexion();
                    SqlDataReader lectura_reg = cmd.ExecuteReader();
                    lista_empleados = ArmarLista(lectura_reg);
                    conn.CerrarConexion();
                 }
                catch (Exception)
                {

                    throw;
                }
                return lista_empleados;
            

        }

        //LISTAR TODOS LOS EMPLEADOS QUE TIENEN UN TALLE DE CAMISA DETERMINADO
        public ObservableCollection<Empleado>EmpleadosPorCamisa(int _tallecamisa)
        {
            
            
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn.AbriConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pa_Empleado_Por_Camisa";
                cmd.Parameters.Add("@tallecamisa", SqlDbType.Int);
                cmd.Parameters["@tallecamisa"].Value = _tallecamisa;
                ObservableCollection<Empleado> lista_empleados = new ObservableCollection<Empleado>();
                try
                {
                    conn.AbriConexion();
                    SqlDataReader lectura_reg = cmd.ExecuteReader();
                    lista_empleados = ArmarLista(lectura_reg);
                    conn.CerrarConexion();
                }
                catch (Exception)
                {

                    throw;
                }
                return lista_empleados;
            
        }

        //LISTAR TODOS LOS EMPLEADOS QUE TIENEN UN TALLE DE CALZADO DETERMINADO
        public ObservableCollection<Empleado>EmpleadosPorCalzado(int _tallecalzado)
        {
           
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn.AbriConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pa_Empleado_Por_Calzado";
                cmd.Parameters.Add("@tallecalzado", SqlDbType.Int);
                cmd.Parameters["@tallecalzado"].Value = _tallecalzado;
                ObservableCollection<Empleado> lista_empleados = new ObservableCollection<Empleado>();
                try
                {
                    conn.AbriConexion();
                    SqlDataReader lectura_reg = cmd.ExecuteReader();
                    lista_empleados = ArmarLista(lectura_reg);
                    conn.CerrarConexion();
                }
                catch (Exception)
                {

                    throw;
                }

                return lista_empleados;
            
        }

        //LISTAR TODOS LOS EMPLEADO QUE TIENEN UN TALLE DE PANTALON DETERMINADO
        public ObservableCollection<Empleado>EmpleadosPorPantalon(int _tallepantalon)
        {
            
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn.AbriConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pa_Empleado_Por_Pantalon";
                cmd.Parameters.Add("@tallepantalon", SqlDbType.Int);
                cmd.Parameters["@tallepantalon"].Value = _tallepantalon;
                ObservableCollection<Empleado> lista_empleados = new ObservableCollection<Empleado>();
                try
                {
                    conn.AbriConexion();
                    SqlDataReader lectura_reg = cmd.ExecuteReader();
                    lista_empleados = ArmarLista(lectura_reg);
                    conn.CerrarConexion();
                }
                catch (Exception)
                {

                    throw;
                }

                return lista_empleados;
            
        }

        //LISTAR TODOS LOS EMPLEADOS QUE PERTENECEN A UN GREMIO DETERMINADO
        public ObservableCollection<Empleado>EmpleadosPorGremio(string _gremio)
        {
           
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn.AbriConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pa_Empleado_Por_Gremio";
                cmd.Parameters.Add("@gremio", SqlDbType.VarChar, 50);
                cmd.Parameters["@gremio"].Value = _gremio;
                ObservableCollection<Empleado> lista_empleados = new ObservableCollection<Empleado>();
                try
                {
                    conn.AbriConexion();
                    SqlDataReader lectura_reg = cmd.ExecuteReader();
                    lista_empleados = ArmarLista(lectura_reg);
                    conn.CerrarConexion();
                }
                catch (Exception)
                {

                    throw;
                }
                return lista_empleados;
            
        }

        //LISTAR TODOS LOS EMPLEADOS ACTIVOS
        public ObservableCollection<Empleado> EmpleadosActivos()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Empleado_Activos";
           
            ObservableCollection<Empleado> lista_empleados = new ObservableCollection<Empleado>();
            try
            {
                conn.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                lista_empleados = ArmarLista(lectura_reg);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_empleados;

        }

        public List<Empleado> EmpleadosPorNombre(string _nombre)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Empleado_Combobox";
            cmd.Parameters.AddWithValue("@nombre", _nombre);

            List<Empleado> lista_empleados = new List<Empleado>();
            try
            {
                conn.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                while (lectura_reg.Read())
                {
                    Empleado empleado = new Empleado();
                    empleado.IdEmpleado = (int)lectura_reg["idempleado"];
                    empleado.Nombre = (string)lectura_reg["nombre"];
                    lista_empleados.Add(empleado);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_empleados;
        }

        //LISTAR TODOS LOS EMPLEADOS DADOS DE BAJA
        public ObservableCollection<Empleado>EmpleadosBaja()
        {
           
            /// 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Empleado_En_Baja";

            ObservableCollection<Empleado> lista_empleados = new ObservableCollection<Empleado>();
            try
            {
                conn.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                lista_empleados = ArmarLista(lectura_reg);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_empleados;
        }

        //METODO QUE PERMITE VALIDAD EL DNI INGRESADO 
        public int EmpleadoValidarDni(int dni_a_buscar)
        {
            int idmpleado = 0;
            
   
            //asiganmos valores a las variables
           int _dni = dni_a_buscar;
            //creamos el comando
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_EmpleadoValida_dni";
            //agregamos el parametro del id del empleado
            cmd.Parameters.Add("@dni", SqlDbType.Int);
            cmd.Parameters["@dni"].Value = _dni;
            conn.AbriConexion();
            idmpleado = Convert.ToInt32(cmd.ExecuteScalar());//devolvemos la primer columan de la primer fila
                                                               // afectada por la consulta
                                     //es decir, si es distinto de cero, existe el empleado.
            conn.CerrarConexion();
            return idmpleado;

        }

       

        //METODO QUE MUESTRA TODAS LAS LICENCIAS DE CONDUCIR DE UN EMPLEADO
        public Licencia MostrarLicenciaConducir(int idempleado)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Empleado_Licencias";
            cmd.Parameters.Add("@idempleado", SqlDbType.Int);
            cmd.Parameters["@idempleado"].Value = idempleado;

            try
            {

                SqlDataReader lectura = cmd.ExecuteReader();
                Licencia licencia = ArmarLicenciaConducir(lectura);

                conn.CerrarConexion();
                return licencia;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SectorEmpleado>ListarSectorEmpleado()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Empleado_Listar_Sectores";
            List<SectorEmpleado> lista_sectores = new List<SectorEmpleado>();
            try
            {
                conn.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                while (lectura_reg.Read())
                {
                    SectorEmpleado sectorEmpleado = new SectorEmpleado();
                    sectorEmpleado.IdSector = (int)lectura_reg["idsector"];
                    sectorEmpleado.NomSector = (string)lectura_reg["nomsector"];
                    sectorEmpleado.CodSector = (string)lectura_reg["codsector"];
                    lista_sectores.Add(sectorEmpleado);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_sectores;

        }

        
        //  METODO QUE MUESTRA TODAS LAS CATEGORIAS DE  EMPLEADOS
        public List<CategoriaEmpleado> ListaCateEmpleado()
        {
            List<CategoriaEmpleado> categoriaEmpleados = new List<CategoriaEmpleado>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Categoria_Empleado_Todos";
            try
            {
                conn.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                while (lectura_reg.Read())
                {
                    CategoriaEmpleado categoria = new CategoriaEmpleado();
                    categoria.IdCategoria = (int)lectura_reg["idcatempleado"];
                    categoria.NombreCategoria = (string)lectura_reg["nombre_categoria"];
                    categoriaEmpleados.Add(categoria);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return categoriaEmpleados;
        }

    

        public ObservableCollection<Autorizacion_vh>AutorizacionesDeVehiculo(int _idempleado)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Empleado_Autorizaciones_Vehiculo";
            cmd.Parameters.AddWithValue("@idempleado", _idempleado);
            ObservableCollection<Autorizacion_vh> autorizacion_s = new ObservableCollection<Autorizacion_vh>();
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                autorizacion_s = ArmarAutorizaciones(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return autorizacion_s;
        }

        private ObservableCollection<Autorizacion_vh> ArmarAutorizaciones(SqlDataReader reader)
        {
            ObservableCollection<Autorizacion_vh> _Vhs = new ObservableCollection<Autorizacion_vh>();
            while (reader.Read())
            {
                Autorizacion_vh autorizacion_ = new Autorizacion_vh();
                autorizacion_.IdAut = (int)reader["idaut"];
                autorizacion_.IdVh = (int)reader["idvh"];
                autorizacion_.IdEmpleado = (int)reader["idempleado"];
                autorizacion_.AltaF = (DateTime)reader["altaf"];
                autorizacion_.Estado = (string)reader["estado_autorizacion"];
                autorizacion_.NumAutorizacion = (string)reader["numautorizacion"];
                autorizacion_.Valor = (int)reader["valor_autorizacion"];
                autorizacion_.DominioVh = (string)reader["dominio"];
                autorizacion_.Modelo = (string)reader["modelo"];
                autorizacion_.NombreEmpleado = (string)reader["nombre"];
                autorizacion_.Marca = (string)reader["nombre_marca"];
                autorizacion_.DNI = (int)reader["dni"];
                autorizacion_.Finicio = (DateTime)reader["finicio"];
               
                if (reader["ffinal"] != DBNull.Value)
                {
                    autorizacion_.Ffinal = (DateTime)reader["ffinal"];
                }
                else
                {
                    autorizacion_.Ffinal = null;
                }
                _Vhs.Add(autorizacion_);
            }
            return _Vhs;
        }

        #endregion

        #region Balances

        //listar el balance de productos para un empleado determinado
        public  ObservableCollection<BalanceEmpleado>BalanceEmpleado(int _idempleado)
        {
            ObservableCollection<BalanceEmpleado> _balance = new ObservableCollection<BalanceEmpleado>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "pa_Empleado_Balance";
            cmd.Parameters.AddWithValue("@idempleado", _idempleado);
           
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                _balance = ArmarBalanceEmpleado(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return _balance;

        }

        private ObservableCollection<BalanceEmpleado> ArmarBalanceEmpleado(SqlDataReader reader)
        {
            ObservableCollection<BalanceEmpleado> _balance = new ObservableCollection<BalanceEmpleado>();
            while (reader.Read())
            {
                BalanceEmpleado bem = new BalanceEmpleado();
                bem.IdBe = (int)reader["idbe"];
                bem.IdProducto = (int)reader["idproducto"];
                bem.PrecioUnitario = (decimal)reader["preciounitario"];
                bem.CantidadEntregada = (int)reader["cantientregada"];
                bem.CantidadDevolucion= (int)reader["cantidevolucion"];
                bem.CantidadDescuento= (int)reader["cantidescuento"];
                bem.DifCantidad = (int)reader["difcantidad"];
                //bem.CostoExistencia = (decimal)reader["costoexistencia"];
                //bem.CostoDescuento = (decimal)reader["costodescuento"];
                bem.CostoExistencia = bem.CantidadEntregada * bem.PrecioUnitario;
                bem.CostoDevolucion = bem.CantidadDevolucion * bem.PrecioUnitario;
                bem.CostoDescuento = bem.CantidadDescuento * bem.PrecioUnitario;
                bem.EstadoItem = (int)reader["estado_item"];
                bem.DiasAcumulado = (int)reader["dias_acumulados"];
                bem.NombreProducto = (string)reader["nombre"];
                 bem.ModeloProducto = (string)reader["modelo"];
                bem.MarcaProducto = (string)reader["nombre_marca"];
               bem.CodInventario = (string)reader["cod_inventario"];
		       bem.SerieProducto = (string)reader["numserie"];
                bem.IdTipoP = (int)reader["idtipo_p"];
                bem.IdCateP = (int)reader["idcate_p"];
                bem.IdLinea = (int)reader["idseg_p"];
                bem.Imputacion = (int)reader["imputacion"];
                bem.ClienteObra = (string)reader["cliente"];
                bem.NomEstado = (string)reader["significado"];
                _balance.Add(bem);

            }

            return _balance ;
        }

        public void ActualizarBalancePorBajaDSO(int _imputacion, int _idempleado, int _idproducto, int _cantidad, decimal _precioItem)
        {
            // este metodo da marcha atras con los productos imputados a un empleado u obra
            //
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Empleado_Actualiza_Balance_BajaDoc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idempleado", _idempleado);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@cantidad", _cantidad);
            cmd.Parameters.AddWithValue("@precioitem", _precioItem);

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

        public void ActualizarBalancePorBajaDDO(int _imputacion, int _idempleado, int _idproducto, int _cantidad, decimal _precioItem)
        {
            // este metodo da marcha atras con los productos imputados a un empleado u obra
            //
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Empleado_Actualiza_Balance_BajaDoc_Devolucion";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idempleado", _idempleado);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@cantidad", _cantidad);
            cmd.Parameters.AddWithValue("@precioitem", _precioItem);

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

        //actualizar los productos existentes del balance del empleado ante nuevos envios de productos
        //el producto existe en el balance y debemos modificar la cantidad (UPDATE)
        public  void ActualizarBalance(int _idempleado, int _idproducto, int _cantidad, int _imputacion, decimal _preciop)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Empleado_Actualiza_Balance";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idempleado", _idempleado);
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@cantidad", _cantidad);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            cmd.Parameters.AddWithValue("@precioproducto", _preciop);
           

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

        public void ActualizarBalancePorDevolucion(int _idempleado, int _idproducto, int _cantidad, int _imputacion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Empleado_Actualiza_Balance_PorDevolucion";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idempleado", _idempleado);
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@cantidad", _cantidad);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
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


        public  void ActualizarBalancePorDescuento(int _idempleado, int _idproducto, int _cantidad, int _imputacion, decimal _preciop)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Empleado_Actualiza_Balance_Descuento";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idempleado", _idempleado);
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@cantidad", _cantidad);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            cmd.Parameters.AddWithValue("@precioitem", _preciop);


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

        public void CambiarEstadoProductoEnBalance(int idproducto, int imputacion, int idempleado , int idestado)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Balance_Cambiar_Estado_Producto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", idproducto);
            cmd.Parameters.AddWithValue("@imputacion", imputacion);
            cmd.Parameters.AddWithValue("@idempleado", idempleado);
            cmd.Parameters.AddWithValue("@idestado", idestado);
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

        public ObservableCollection<BalanceEmpleado>BalanceEmpleadoUnaImputacion(int _imputacion, int _idempleado)
        {
            ObservableCollection<BalanceEmpleado> _balance = new ObservableCollection<BalanceEmpleado>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Empleado_Balance_Imputacion";
            cmd.Parameters.AddWithValue("@idempleado", _idempleado);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                _balance = ArmarBalanceEmpleado(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return _balance;
        }

        public ObservableCollection<BalanceEmpleado>BalanceEmpleadoUnaImputacionTipoP(int _imputacion, int _idempleado, int _idtipo)
        {
            ObservableCollection<BalanceEmpleado> _balance = new ObservableCollection<BalanceEmpleado>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Empleado_Balance_Tipo_Producto_Imputacion";
            cmd.Parameters.AddWithValue("@idempleado", _idempleado);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            cmd.Parameters.AddWithValue("@idtipoproducto", _idtipo);
            
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                _balance = ArmarBalanceEmpleado(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return _balance;
        }

        public ObservableCollection<BalanceEmpleado> BalanceEmpleadoUnaImputacionTipoYCategoria(int _imputacion, int _idempleado, int _idtipo, int _idcate)
        {
            ObservableCollection<BalanceEmpleado> _balance = new ObservableCollection<BalanceEmpleado>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Empleado_Balance_TipoP_CateP_Imputacion";
            cmd.Parameters.AddWithValue("@idempleado", _idempleado);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            cmd.Parameters.AddWithValue("@idtipoproducto", _idtipo);
            cmd.Parameters.AddWithValue("@idcateproducto", _idcate);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                _balance = ArmarBalanceEmpleado(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return _balance;
        }

        public ObservableCollection<BalanceEmpleado>BalanceEmpleadoUnTipoP(int _idempleado, int _idtipop)
        {
            ObservableCollection<BalanceEmpleado> _balance = new ObservableCollection<BalanceEmpleado>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "pa_Empleado_Balance_Tipo_Producto";
            cmd.Parameters.AddWithValue("@idempleado", _idempleado);
           
            cmd.Parameters.AddWithValue("@idtipoproducto", _idtipop);
           
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                _balance = ArmarBalanceEmpleado(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return _balance;
        }

        public ObservableCollection<BalanceEmpleado>BalanceEmpleadoUnTipoYCategoria(int _idempleado, int _idtipo , int _idcate)
        {
            ObservableCollection<BalanceEmpleado> _balance = new ObservableCollection<BalanceEmpleado>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Empleado_Balance_TipoP_CateP";
            cmd.Parameters.AddWithValue("@idempleado", _idempleado);
            cmd.Parameters.AddWithValue("@idtipoproducto", _idtipo);
            cmd.Parameters.AddWithValue("@idcateproducto", _idcate);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                _balance = ArmarBalanceEmpleado(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return _balance;

        }

        public List<DetDescuentoEmpleado>ListaProductosDescuento(int idempleado)
        {
            List<DetDescuentoEmpleado> lista = new List<DetDescuentoEmpleado>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Empleado_Listar_Descuentos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idempleado", idempleado);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DetDescuentoEmpleado det = new DetDescuentoEmpleado();
                    det.FechaRemito = (DateTime)reader["altaf"];
                    det.CantidadItem = (int)reader["cantidad_item"];
                    det.Descripcion = (string)reader["nombre"];
                    det.CodIventario = (string)reader["cod_inventario"];
                    det.CodigoItem = (int)reader["codigo_item"];
                    det.CostoTotalItem = (decimal)reader["costo_docu"];
                    det.PrecioItem = (decimal)reader["precio_item"];
                    det.IdDocumento = (int)reader["iddocumento"];
                    lista.Add(det);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }


        public void BorrarItemDescuento(DetDescuentoEmpleado det, int idempleado, int imputacion)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Empleado_Borrar_Item_Descuento";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idempleado", idempleado);
            cmd.Parameters.AddWithValue("@idproducto", det.CodigoItem);
            cmd.Parameters.AddWithValue("@cantidad", det.CantidadItem);
            cmd.Parameters.AddWithValue("@imputacion", imputacion);
            cmd.Parameters.AddWithValue("@precioitem", det.PrecioItem);
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

        public void BorrarValeDescuento(int iddocumento)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Empleado_Borrar_Vale_Descuento";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddocumento", iddocumento);
        
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

        #endregion


       

        #region[metodos privados]
        // armar lista de empleados para metodos
        private ObservableCollection<Empleado> ArmarLista(SqlDataReader lectura)
        {
            ObservableCollection<Empleado> lista = new ObservableCollection<Empleado>();
            while (lectura.Read())
            {
                Empleado empleado = new Empleado();

                empleado.IdEmpleado = (int)lectura["idempleado"];
                empleado.Nombre = (string)lectura["nombre"];
                empleado.Dni = (int)lectura["dni"];
                empleado.Direccion = (string)lectura["direccion"];
                
                empleado.Email = (string)lectura["email"];
              
                empleado.TeCelular = (string)lectura["teparticular"];
               
                empleado.TeParticular = (string)lectura["tecelular"];
                empleado.IdLocalidad = (int)lectura["idlocalidad"];
                empleado.IdProvincia = (int)lectura["idprovincia"];
                empleado.IdSector = (int)lectura["idsector"];
                empleado.NomSector = (string)lectura["nomsector"];
                empleado.IdEstado = (int)lectura["idestado"];
                if (empleado.IdEstado==1)
                {
                    empleado.Estado = "Activo";
                }
                else
                {
                    empleado.Estado = "Inactivo";
                }
                empleado.Gremio = (string)lectura["gremio"];
                empleado.IdCatEmpleado = (int)lectura["idcatempleado"];
                empleado.NomCategoria = (string)lectura["nombre_categoria"];
                empleado.AltaF = (DateTime)lectura["altaf"];
               
                if (lectura["bajaf"] != DBNull.Value)
                {
                    empleado.BajaF = (DateTime)lectura["bajaf"];
                }
                else
                {
                    empleado.BajaF = null;
                }
                
                empleado.TCamisa = (string)lectura["tcamisa"];
                empleado.TCalzado = (string)lectura["tcalzado"];
                empleado.TPantalon = (string)lectura["tpantalon"];
                empleado.Provincia = (string)lectura["nomprovincia"];
                empleado.Localidad = (string)lectura["nomlocalidad"];
                if (lectura["foto_empleado"]!= DBNull.Value)
                {
                    empleado.Foto = (byte[])lectura["foto_empleado"];
                }
                

                lista.Add(empleado);
            }
            return lista;

        }
        //armar objeto empleado para metodos
        private Empleado ArmarEmpleado(SqlDataReader lectura)
        {
            Empleado empleado = new Empleado();
            while (lectura.Read())
            {
                empleado.IdEmpleado = (int)lectura["idempleado"];
                empleado.Nombre = (string)lectura["nombre"];
                empleado.Dni = (int)lectura["dni"];
                empleado.Direccion = (string)lectura["direccion"];
                empleado.Email = (string)lectura["email"];
                empleado.TeCelular = (string)lectura["teparticular"];
                empleado.TeParticular = (string)lectura["tecelular"];
                empleado.IdLocalidad = (int)lectura["idlocalidad"];
                empleado.IdProvincia = (int)lectura["idprovincia"];
                empleado.IdSector = (int)lectura["idsector"];
                empleado.NomSector = (string)lectura["nomsector"];
                empleado.IdEstado = (int)lectura["idestado"];
                if (empleado.IdEstado == 1)
                {
                    empleado.Estado = "Activo";
                }
                else
                {
                    empleado.Estado = "Inactivo";
                }
                empleado.Gremio = (string)lectura["gremio"];
                empleado.IdCatEmpleado = (int)lectura["idcatempleado"];
                empleado.NomCategoria = (string)lectura["nombre_categoria"];
                empleado.AltaF = (DateTime)lectura["altaf"];

                if (lectura["bajaf"] != DBNull.Value)
                {
                    empleado.BajaF = (DateTime)lectura["bajaf"];
                }
                else
                {
                    empleado.BajaF = null;
                }

                empleado.TCamisa = (string)lectura["tcamisa"];
                empleado.TCalzado = (string)lectura["tcalzado"];
                empleado.TPantalon = (string)lectura["tpantalon"];
                empleado.Provincia = (string)lectura["nomprovincia"];
                empleado.Localidad = (string)lectura["nomlocalidad"];
                if (lectura["foto_empleado"] != DBNull.Value)
                {
                    empleado.Foto = (byte[])lectura["foto_empleado"];
                }

            }
            return empleado;

        }
        //armar lista de licencias para metodos
        private Licencia ArmarLicenciaConducir(SqlDataReader lectura)
        {
            Licencia licencia = new Licencia();

            licencia.NumLicencia = (string)lectura["numlicencia"];
            licencia.Tarjetazul = (string)lectura["tarjetazul"];
            licencia.Psicofisico = (string)lectura["psicofisico"];
            licencia.Licespecial = (string)lectura["licespecial"];
            licencia.VtoLicencia = (DateTime)lectura["vtolicencia"];
            licencia.VtoAzul = (DateTime)lectura["vtoazul"];
            licencia.VtoPsico = (DateTime)lectura["vtopsico"];
            licencia.VtoEspecial = (DateTime)lectura["vtoespecial"];

            return licencia;
        }
       
        #endregion
        
    }
}
