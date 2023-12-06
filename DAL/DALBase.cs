using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using ENTIDADES;
using System.Collections.ObjectModel;

namespace DAL
{
    //clase que contiene esa informacion de base que se usa en varios formularios
    //como ser localidades, provincias, sectores
    //calculos varios 
    //otros

    public class DALBase
    {
        //creamos un obejeto conexion desde la clase DALConexion
        DALConexion conn = new DALConexion();

        //contructor
        public DALBase()
        {

        }

        #region Depositos

        public void DepositoAlta(Deposito deposito) // metodo para dar de alta o modificar un deposito.
        { }

        public Deposito DepositoBuscarUno(int _iddeposito) // busca un deposito por su ID
        {
            Deposito deposito = new Deposito();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Deposito_Buscar_Uno";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddepo", _iddeposito);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                deposito = ArmarDeposito(reader);
                conn.CerrarConexion();
            }
            catch (Exception ex)
            {

                throw;
            }
            return deposito;

        }

        private Deposito ArmarDeposito(SqlDataReader reader) // metodo que arma al objeto deposito resultante de un SP
        {
            Deposito depo = new Deposito();
            while (reader.Read())
            {
                depo.IdDeposito = (int)reader["iddeposito"];
                depo.IdLocalidad = (int)reader["idlocalidad"];
                depo.IdProvincia = (int)reader["idprovincia"];
                depo.NomDepo = (string)reader["nomdepo"];
                depo.Responsable = (string)reader["responsable"];
                depo.Telefono = (string)reader["tel1"];
                depo.AltaF = (DateTime)reader["altaf"];
                if (reader["bajaf"] == DBNull.Value)
                {
                    depo.BajaF = null;
                }
                else
                {
                    depo.BajaF = (DateTime)reader["bajaf"];
                }
                depo.Email = (string)reader["email"];
                depo.Estado = (int)reader["estado"];

            }
            return depo;
        }
        #endregion

        #region[localidades/provincias]
        //listar todas las provincias
        public List<Provincia> ListaProvincia()
        {
            List<Provincia> lista_provincia = new List<Provincia>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Provincia_Todos"; 
            try
            {
                conn.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                while (lectura_reg.Read())
                {
                    Provincia provincia = new Provincia();
                    provincia.IdProvincia = (int)lectura_reg["idprovincia"];
                    provincia.Nombre = (string)lectura_reg["nomprovincia"];
                    lista_provincia.Add(provincia);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_provincia;
        }

        //listar todas las localidades de una provincia
        public List<Localidad> ListaLocalidad(int idprovincia)
        {
            List<Localidad> lista_localidad = new List<Localidad>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Localidad_Todos"; 
            cmd.Parameters.AddWithValue("@idprovincia", idprovincia);
            try
            {
                conn.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                while (lectura_reg.Read())
                {
                    Localidad localidad = new Localidad();
                    localidad.IdLocalidad = (int)lectura_reg["idlocalidad"];
                    localidad.IdProvincia = (int)lectura_reg["idprovincia"];
                    localidad.Nombre = (string)lectura_reg["nomlocalidad"];

                    localidad.CP = (string)lectura_reg["cp"];

                    localidad.Provincia = (string)lectura_reg["nomprovincia"];
                    lista_localidad.Add(localidad);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_localidad;
        }

        //agregar un localidad
        public int AgregarLocalidad(Localidad localidad)
        {
            int fila = 0;
            string op = "Alta";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Localidad_abm";
            cmd.Parameters.AddWithValue("@nombre", localidad.Nombre); // nombre de la localidad
            cmd.Parameters.AddWithValue("@cp", localidad.CP); // codigo postal
            cmd.Parameters.AddWithValue("@idprovincia", localidad.IdProvincia); // id de provincia
            cmd.Parameters.AddWithValue("@operacion", op); // codigo de la operacion alta 

            try
            {
                conn.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int ModificarLocalidad(Localidad localidad)
        {
            int fila = 0;
            string op = "Modi";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Localidad_abm";
            cmd.Parameters.AddWithValue("@nombre", localidad.Nombre);
            cmd.Parameters.AddWithValue("@cp", localidad.CP);
            cmd.Parameters.AddWithValue("@idprovincia", localidad.IdProvincia);
            cmd.Parameters.AddWithValue("@operacion", op);

            try
            {
                conn.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }
        #endregion

        #region CalculosVarios


        //calcular la cantidad de dias que llevan las asignaciones de vehiculos en obra
        public int CalcularDiasAsignacionVehiculo(DateTime _fechadiahoy)
        {
            int filasafectadas;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Asignacion_Calcular_Dias";
            cmd.Parameters.AddWithValue("@fecha2", _fechadiahoy);
            try
            {
                conn.AbriConexion();
                filasafectadas = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return filasafectadas;
        }

        //devuelve la ultima fecha de control de asignaciones
        public DateTime UltimaFechaCAS()
        {
            DateTime filasafectadas;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_CAS_Ultima_Fecha";

            try
            {
                conn.AbriConexion();
                filasafectadas = Convert.ToDateTime(cmd.ExecuteScalar());
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return filasafectadas;
        }

        //actualiza la fecha de control de asignaciones con la fecha hoy
        public int ActualizarFechaCAS(DateTime _fecha)
        {
            int filaAfectada;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_CAS_Actualiza_Fecha";
            cmd.Parameters.AddWithValue("@ultimafecha", _fecha);

            try
            {
                conn.AbriConexion();
                filaAfectada = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return filaAfectada;
        }

        //registro de operaciones de un usuario
        public int UsuarioRegOp(LogOperaciones logOperaciones)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Usuario_Reg_Op";
            cmd.Parameters.AddWithValue("@iduser", logOperaciones.IdUsuario);
            cmd.Parameters.AddWithValue("@fecha", logOperaciones.Fecha);
            cmd.Parameters.AddWithValue("@tabla", logOperaciones.Tabla);
            cmd.Parameters.AddWithValue("@codigo", logOperaciones.CodigoOperacion);
            cmd.Parameters.AddWithValue("@idreg", logOperaciones.IdRegistro);
            try
            {
                conn.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        //calculo de dias de ejecucion que lleva una tarea determinada
        public void TareasCalcularDiasEjecucion(DateTime _fechadiahoy)
        {
            //calcular la cantidad de dias que pasaron desde que se dio de alta la tarea
            int filasafectadas;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Calcular_Dias_Ejecucion";
            cmd.Parameters.AddWithValue("@fecha2", _fechadiahoy);
            try
            {
                conn.AbriConexion();
                filasafectadas = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //calculo de la cantidad de tareas que tiene asignado un usuario como pendientes
        public int CantidadTareasUsuario(int idusuario)
        {
            //metodo que devuelve la cantidad de tareas que tiene seguimiento un usuario
            int cantidadtareas = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Cantidad_Seguimiento";

            cmd.Parameters.AddWithValue("@idusuario", idusuario);
            cmd.Parameters.Add("@cantidad", SqlDbType.Int).Direction = ParameterDirection.Output;

            try
            {
                conn.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                cantidadtareas = Convert.ToInt16(cmd.Parameters["@cantidad"].Value);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return cantidadtareas;
        }

        //devuelve la lista de vehiculos ocn documentacion vencida
        public List<VehiculoDocu> ChekeoDocumentacionVehiculos()
        {
            //procedimiento que chekea cuantos vehiculos tienen documentacion vencidad
            // y de que tipo de documentacion se trata
            List<VehiculoDocu> vehiculos_doc_v = new List<VehiculoDocu>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculos_Doc_Vencida";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    VehiculoDocu vehiculo = new VehiculoDocu();
                    vehiculo.IdVh = (int)reader["idvh"];
                    vehiculo.DominioVh = (string)reader["dominio"];
                    vehiculo.DescriVh = (string)reader["descripcion"];
                    vehiculo.ModeloVh = (string)reader["modelo"];
                    vehiculo.NombreDocu = (string)reader["nombredocu"];
                    vehiculo.FVencimiento = (DateTime)reader["fvencimiento"];
                    vehiculos_doc_v.Add(vehiculo);

                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return vehiculos_doc_v;

        }

        //devuelve la lista de vehiculos con documentacion a vencer dentro de los proximos 15 dias
        public List<VehiculoDocu> ChekeoDocAVencer()
        {
            //procedimiento que chekea cuantos vehiculos tienen documentacion vencida
            List<VehiculoDocu> vehiculos_doc_v = new List<VehiculoDocu>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculos_Doc_Por_Vencer";

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    VehiculoDocu vehiculo = new VehiculoDocu();
                    vehiculo.IdVh = (int)reader["idvh"];
                    vehiculo.DominioVh = (string)reader["dominio"];
                    vehiculo.DescriVh = (string)reader["descripcion"];
                    vehiculo.ModeloVh = (string)reader["modelo"];
                    vehiculo.NombreDocu = (string)reader["nombredocu"];
                    vehiculo.FVencimiento = (DateTime)reader["fvencimiento"];

                    vehiculos_doc_v.Add(vehiculo);

                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return vehiculos_doc_v;
        }

        //actualiza el estado de la documentacion del vehiculo (o sea si esta vencida o no)
        //en funcion de la fecha actual del sistema
        public void ActualizaEstadoDocuVehiculos(DateTime _factual)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Actualizar_Estado_Docu";
            cmd.Parameters.AddWithValue("@fecha_actual", _factual);
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

        #region Monedas
        public List<Monedas> ListarVariacionMoneda( int _anio) // lista todas las variaciones de moneda en un año mes a mes
        {
            List<Monedas> variaciones = new List<Monedas>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Monedas_Y_Variaciones";
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio", _anio);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Monedas moneda = new Monedas();
                    moneda.IdMoneda = (int)reader["idmoneda"];
                    moneda.NombreMoneda = (string)reader["nombre_moneda"];
                    moneda.Simbolo = (string)reader["simbolo"];
                    moneda.MesValor = (int)reader["mes"];
                    moneda.AnioValor = (int)reader["anio"];
                    moneda.Valor = (decimal)reader["valor"];
                    variaciones.Add(moneda);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return variaciones;
        }

        public List<Monedas>ListaMonedasSimbolos() // lista los simbolos de monedas usados
        {
            List<Monedas> lista= new List<Monedas>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "MonedasListarSimbolos";
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Monedas moneda = new Monedas();
                    moneda.IdMoneda = (int)reader["idmoneda"];
                    moneda.NombreMoneda = (string)reader["nombre_moneda"];
                    moneda.Simbolo = (string)reader["simbolo"];
                    
                    lista.Add(moneda);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }
        #endregion


        #region Depositos

        public List<Deposito>ListarDepositos() // lista todos los depositos activos y no activos en el sistema
        {
            List<Deposito> depositos = new List<Deposito>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Deposito_Listar_Todos";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Deposito deposito = new Deposito();
                    deposito.IdDeposito = (int)reader["iddeposito"];
                    deposito.NomDepo = (string)reader["nomdepo"];
                    deposito.AltaF = (DateTime)reader["altaf"];
                    deposito.Direccion = (string)reader["direccion"];
                    deposito.Email = (string)reader["email"];
                    deposito.Telefono = (string)reader["tel1"];
                    deposito.Responsable = (string)reader["responsable"];
                    deposito.IdLocalidad = (int)reader["idlocalidad"];
                    deposito.IdProvincia = (int)reader["idprovincia"];
                    deposito.NomLocalidad = (string)reader["nomprovincia"];
                    deposito.NomProvincia = (string)reader["nomlocalidad"];
                    depositos.Add(deposito);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return depositos;
        }
        #endregion

        #region Productos
        public List<CausaBaja>ListaCausasBaja() // lista las causa de bajas posibles para un producto
        {
            List<CausaBaja> causaBajas = new List<CausaBaja>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "CausaBajas_Listar";
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CausaBaja causa = new CausaBaja();
                    causa.IdCausaBaja = (int)reader["idcausabaja"];
                    causa.NomCausa = (string)reader["nomcausa"];
                    causaBajas.Add(causa);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return causaBajas;
        }
        #endregion

        #region Sistema
        public string UltimaVersion() // muestra la ultima version del sistema de base de datos
        {
            string ultima_version = "";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UltimaVersion";
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ultima_version = (string)reader["version_ac"];
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return ultima_version;
        }


        public ObservableCollection<Coeficiente>ListarCoeficientes() // listar coeficientes usados en varios calculos
        {
            ObservableCollection<Coeficiente> lista = new ObservableCollection<Coeficiente>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Coeficientes_Listar";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Coeficiente coeficiente = new Coeficiente();
                    coeficiente.IdCof = (int)reader["idcof"];
                    coeficiente.NomCof = (string)reader["nom_cof"];
                    coeficiente.ValorMin = (decimal)reader["valor_min"];
                    coeficiente.ValorMed = (decimal)reader["valor_medio"];
                    coeficiente.ValorMax = (decimal)reader["valor_max"];
                    lista.Add(coeficiente);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }


        public void CoeficienteSave(Coeficiente coeficiente)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Coeficiente_Save";
            cmd.Parameters.AddWithValue("@id", coeficiente.IdCof);
            cmd.Parameters.AddWithValue("@nom", coeficiente.NomCof);
            cmd.Parameters.AddWithValue("@min", coeficiente.ValorMin);
            cmd.Parameters.AddWithValue("@medio", coeficiente.ValorMed);
            cmd.Parameters.AddWithValue("@max", coeficiente.ValorMax);
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


        public void CoeficienteDelete( int idcof)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Coeficiente_Delete";
            cmd.Parameters.AddWithValue("@id", idcof);
           
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

        public Coeficiente BuscarUno(int id)
        {
            Coeficiente coeficiente = new Coeficiente();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Coeficiente_Buscar_Uno";
            cmd.Parameters.AddWithValue("@id", id);
           
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    coeficiente.IdCof = (int)reader["idcof"];
                    coeficiente.NomCof = (string)reader["nom_cof"];
                    coeficiente.ValorMin = (decimal)reader["valor_min"];
                    coeficiente.ValorMed = (decimal)reader["valor_medio"];
                    coeficiente.ValorMax = (decimal)reader["valor_max"];
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return coeficiente;
        }

       
        #endregion

        #region Usuarios

        public List<Usuario>ListarUsuariosSistema() // lista todos los usuarios del sistema 
        {
            List<Usuario> lista = new List<Usuario>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Usuarios_Todos";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Usuario user = new Usuario();
                    user.IdUsuario = (int)reader["idusuario"];
                    user.Iniciales = (string)reader["iniciales"];
                    lista.Add(user);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }
        #endregion

        #region MaquinasTaller

        public void RecalcularGapVencimientosDias(DateTime date)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Maquinas_actualizar_situacion_Tarea_Por_Vencimientos";
            cmd.Parameters.AddWithValue("@fecha", date);
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


    }
}
