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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;

namespace DAL
{
    //clase que contiene los metodos para administrar los productos, calibraciones, cajones ,stock
    public class DALProducto
    {

        //objeto conexion 
        DALConexion conn = new DALConexion();

        //constructor de clase 
        public DALProducto()
        {

        }


        #region Producto ABM

        public void ProductoAtlta(Producto producto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Alta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@codinventario", producto.CodInventario);
            cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            cmd.Parameters.AddWithValue("@modelo", producto.Modelo);
            cmd.Parameters.AddWithValue("@numserie", producto.NumSerie);
            cmd.Parameters.AddWithValue("@estado", producto.EstadoItem);
            cmd.Parameters.AddWithValue("@altaf", producto.AltaF);
            cmd.Parameters.AddWithValue("@preciounit", producto.PrecioUnitario);
            cmd.Parameters.AddWithValue("@costorepo", producto.CostoReposicion);
            cmd.Parameters.AddWithValue("@idmarca", producto.IdMarcaP);
            cmd.Parameters.AddWithValue("@idsf", producto.Idsf);
            cmd.Parameters.AddWithValue("@idtipo", producto.IdTipoP);
            cmd.Parameters.AddWithValue("@idcatep", producto.IdCateP);
            cmd.Parameters.AddWithValue("@idseg", producto.IdSegP);
            cmd.Parameters.AddWithValue("@eskit", producto.EsKit);
            cmd.Parameters.AddWithValue("@numkit", producto.NumeroKit);
            cmd.Parameters.AddWithValue("@condicionstk", producto.CondicionStk);
            cmd.Parameters.AddWithValue("@accesorios", producto.Accesorios);
            cmd.Parameters.AddWithValue("@dimensiones", producto.Dimensiones);
            cmd.Parameters.AddWithValue("@color", producto.Color);
           


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

        public void ProductoModificar(Producto producto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Modificar";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@cod_inventario", producto.CodInventario);
            cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            cmd.Parameters.AddWithValue("@modelo", producto.Modelo);
            cmd.Parameters.AddWithValue("@numserie", producto.NumSerie);
            cmd.Parameters.AddWithValue("@preciounit", producto.PrecioUnitario);
            cmd.Parameters.AddWithValue("@costorepo", producto.CostoReposicion);
            cmd.Parameters.AddWithValue("@idmarca", producto.IdMarcaP);
            cmd.Parameters.AddWithValue("@idtipo", producto.IdTipoP);
            cmd.Parameters.AddWithValue("@idcate", producto.IdCateP);
            cmd.Parameters.AddWithValue("@idseg", producto.IdSegP);
            cmd.Parameters.AddWithValue("@eskit", producto.EsKit);
            cmd.Parameters.AddWithValue("@numerokit", producto.NumeroKit);
            cmd.Parameters.AddWithValue("@accesorios", producto.Accesorios);
            cmd.Parameters.AddWithValue("@dimensiones", producto.Dimensiones);
            cmd.Parameters.AddWithValue("@color", producto.Color);
            cmd.Parameters.AddWithValue("@idproducto", producto.IdProducto);
       

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

        public ObservableCollection<Producto> ListarTodos()
        {
            ObservableCollection<Producto> productos = new ObservableCollection<Producto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Todos";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                productos = ArmarListaProductos(reader);

                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return productos;
        }

        public ObservableCollection<Producto> ListarTodosActivos()
        {
            ObservableCollection<Producto> productos = new ObservableCollection<Producto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandTimeout = 50;
            cmd.CommandText = "Sahm_Usuario.Productos_Activos";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Producto producto = new Producto();
                    producto.IdProducto = (int)reader["idproducto"];
                    producto.Nombre = (string)reader["nombre"];
                    producto.CodInventario = (string)reader["cod_inventario"];
                    producto.EstadoItem = (int)reader["idestado"];
                    producto.AltaF = (DateTime)reader["altaf"];
              
                    producto.Modelo = (string)reader["modelo"];
                   
                    producto.NumSerie = (string)reader["numserie"];
                  
                    producto.Marca = (string)reader["nombre_marca"];
                    producto.Tipo = (string)reader["nomtipo"];
                    producto.Categoria = (string)reader["nomcate_p"];
                    producto.Segmento = (string)reader["nombre_seg"];
                    producto.Situacion = (string)reader["nomsf"];
                    producto.StkDisponible = (decimal)reader["stkdisponible"];
                    producto.CondicionStk = (int)reader["condicion_stk"];
                    if (producto.CondicionStk == 1)
                    {
                        producto.TipoSituacionStk = "En Uso";
                    }
                    else
                    {
                        if (producto.CondicionStk == 2)
                        {
                            producto.TipoSituacionStk = "Discontinuado";
                        }
                        else
                        {
                            producto.TipoSituacionStk = "Obsoleto";
                        }
                    }

                    productos.Add(producto);

                }

                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return productos;
        }

        public ObservableCollection<Producto> ListarTodosBajas()
        {
            ObservableCollection<Producto> productos = new ObservableCollection<Producto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Productos_Todos_Bajas";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                productos = ArmarListaProductos(reader);

                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return productos;
        }



        private ObservableCollection<Producto> ArmarListaProductos(SqlDataReader reader)
        {
            ObservableCollection<Producto> productos = new ObservableCollection<Producto>();
            while (reader.Read())
            {
                Producto producto = new Producto();
                producto.IdProducto = (int)reader["idproducto"];
                producto.Nombre = (string)reader["nombre"];
                producto.CodInventario = (string)reader["cod_inventario"];
                producto.Accesorios = (string)reader["accesorios"];
                producto.EstadoItem = (int)reader["idestado"];
               
                producto.AltaF = (DateTime)reader["altaf"];
                if (reader["bajaf"] != DBNull.Value)
                {
                    producto.BajaF = (DateTime)reader["bajaf"];
                }
                else
                {
                    producto.BajaF = null;
                }

                producto.CodigoBarra = (int)reader["codigo_barra"];
                producto.Color = (string)reader["color"];
                producto.CondicionStk = (int)reader["condicion_stk"];
                if (producto.CondicionStk == 1)
                {
                    producto.TipoSituacionStk = "En Uso";
                }
                else
                {
                    if (producto.CondicionStk == 2)
                    {
                        producto.TipoSituacionStk = "Discontinuado";
                    }
                    else
                    {
                        producto.TipoSituacionStk = "Obsoleto";
                    }
                }
                producto.ControlSf = (int)reader["control_sf"];
                producto.CostoReposicion = (decimal)reader["costo_reposicion"];
                producto.Descripcion = (string)reader["descripcion"];
                producto.Dimensiones = (string)reader["dimensiones"];
                producto.EsKit = (int)reader["es_kit"];
                producto.IdTipoP = (int)reader["idtipo_p"];
                producto.IdCateP = (int)reader["idcate_p"];
                producto.IdSegP = (int)reader["idseg_p"];
                producto.IdMarcaP = (int)reader["idmarca_p"];
                producto.Idsf = (int)reader["idsf"];
                producto.Modelo = (string)reader["modelo"];
                producto.NumeroKit = (int)reader["numero_kit"];
                producto.NumSerie = (string)reader["numserie"];
                producto.PrecioUnitario = (decimal)reader["precio_unitario"];
                producto.Marca = (string)reader["nombre_marca"];
                producto.Tipo = (string)reader["nomtipo"];
                producto.Categoria = (string)reader["nomcate_p"];
                producto.Segmento = (string)reader["nombre_seg"];
                producto.Situacion = (string)reader["nomsf"];
                producto.StkDisponible = (decimal)reader["stkdisponible"];
                producto.Patron = (int)reader["patron"];
                if (producto.Patron==1)
                {
                    producto.EsPatron = "Si";
                }
                else
                {
                    producto.EsPatron = "No";
                }
                producto.Constrate = (int)reader["contraste"];
                if (producto.Constrate == 1)
                {
                    producto.NomConstraste = "Interno";
                }
                else
                {
                    if (producto.Constrate == 2)
                    {
                        producto.NomConstraste = "Externo";
                    }
                    else
                    {
                        producto.NomConstraste = "No corresponde";
                    }
                }
                producto.Escala = (string)reader["escala"];
                producto.RangoMedicion = (string)reader["rango_medicion"];
                producto.Exactitud = (string)reader["exactitud"];

                productos.Add(producto);

            }
            return productos;
        }


        public ObservableCollection<Producto> ListarTodosTipoCategoriaSegmento(int _idtipo, int _idcate, int _idseg)
        {
            ObservableCollection<Producto> productos = new ObservableCollection<Producto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Todos_Tipo_Categoria_Segmento";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idtipo", _idtipo);
            cmd.Parameters.AddWithValue("@idcate", _idcate);
            cmd.Parameters.AddWithValue("@idseg", _idseg);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                productos = ArmarListaProductos(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return productos;
        }

        public Producto BuscarDatosUno(int _idproducto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Buscar_Uno";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            Producto producto = new Producto();
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                producto = ArmarProducto(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return producto;
        }

        public void ProductoCambiarEstado(int _idproducto, int _idestado)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Cambiar_Estado";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@idestado", _idestado);

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

        private Producto ArmarProducto(SqlDataReader reader)
        {
            Producto producto = new Producto();
            while (reader.Read())
            {
                producto.IdProducto = (int)reader["idproducto"];
                producto.Nombre = (string)reader["nombre"];
                producto.CodInventario = (string)reader["cod_inventario"];
                producto.Accesorios = (string)reader["accesorios"];
                producto.EstadoItem = (int)reader["idestado"];
                producto.AltaF = (DateTime)reader["altaf"];
                if (reader["bajaf"] != DBNull.Value)
                {
                    producto.BajaF = (DateTime)reader["bajaf"];
                }
                else
                {
                    producto.BajaF = null;
                }

                producto.CodigoBarra = (int)reader["codigo_barra"];
                producto.Color = (string)reader["color"];
                producto.CondicionStk = (int)reader["condicion_stk"];
                producto.ControlSf = (int)reader["control_sf"];
                producto.CostoReposicion = (decimal)reader["costo_reposicion"];
                producto.Descripcion = (string)reader["descripcion"];
                producto.Dimensiones = (string)reader["dimensiones"];
                producto.EsKit = (int)reader["es_kit"];
                producto.IdTipoP = (int)reader["idtipo_p"];
                producto.IdCateP = (int)reader["idcate_p"];
                producto.IdSegP = (int)reader["idseg_p"];
                producto.IdMarcaP = (int)reader["idmarca_p"];
                producto.Idsf = (int)reader["idsf"];
                producto.Modelo = (string)reader["modelo"];
                producto.NumeroKit = (int)reader["numero_kit"];
                producto.NumSerie = (string)reader["numserie"];
                producto.PrecioUnitario = (decimal)reader["precio_unitario"];
                producto.Marca = (string)reader["nombre_marca"];
                producto.Tipo = (string)reader["nomtipo"];
                producto.Categoria = (string)reader["nomcate_p"];
                producto.Segmento = (string)reader["nombre_seg"];
                producto.Situacion = (string)reader["nomsf"];
                producto.TipoSituacionStk = (string)reader["nom_stk"];
                producto.Escala = (string)reader["escala"];
                producto.RangoMedicion = (string)reader["rango_medicion"];
                producto.Exactitud = (string)reader["exactitud"];
                producto.Patron = (int)reader["patron"];
                if (producto.Patron == 1)
                {
                    producto.EsPatron = "Si";
                }
                else
                {
                    producto.EsPatron = "No";
                }
                producto.Constrate = (int)reader["contraste"];
                if (producto.Constrate == 1)
                {
                    producto.NomConstraste = "Interno";
                }
                else
                {
                    if (producto.Constrate == 2)
                    {
                        producto.NomConstraste = "Externo";
                    }
                    else
                    {
                        producto.NomConstraste = "No corresponde";
                    }
                }
            }
            return producto;
        }

        public void ActualizarSituacionFisica(int _idproducto, int _idsf)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Actualizar_SF";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@idsf", _idsf);

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

        public Producto ObtenerUltimoId()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Obtener_Ultimo_Id";
            cmd.CommandType = CommandType.StoredProcedure;

            Producto ultimo = new Producto();
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ultimo.IdProducto = (int)reader["idproducto"];

                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return ultimo;
        }


        #region instrumentos
       

       

        public void InstrumentoModificar( string escala, string rango, string exactitud, int patron, int contraste, int idproducto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Instrumento_Modificar";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@escala", escala);
            cmd.Parameters.AddWithValue("@rango", rango);
            cmd.Parameters.AddWithValue("@exactitud", exactitud);
            cmd.Parameters.AddWithValue("@patron", patron);
            cmd.Parameters.AddWithValue("@contraste", contraste);
            cmd.Parameters.AddWithValue("@idproducto", idproducto);
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

        #endregion

        #region repuestosvh

        public List<SegmentoP> RepuestosVhListarCategorias()
        {

            List<SegmentoP> cate_repuestos = new List<SegmentoP>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "RepuestosVH_Listar_Categorias";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SegmentoP segmento = new SegmentoP();
                    segmento.IdCateP = (int)reader["idcate_p"];
                    segmento.IdSegmentoP = (int)reader["idsegp"];
                    segmento.NombreSeg = (string)reader["nombre_seg"];
                    cate_repuestos.Add(segmento);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return cate_repuestos;
        }



        #endregion



        #region Tipo,Categoria,Segmento,Marca

        public void TipoProductoAlta(string _nombre)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Alta_Tipo";
            cmd.Parameters.AddWithValue("@nombre", _nombre);
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

        public void TipoProductoModi(int _id, string _nom)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Modi_Tipo";
            cmd.Parameters.AddWithValue("@idtipo", _id);
            cmd.Parameters.AddWithValue("@nombre", _nom);
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

        public VarPrecioTipoP BuscarUltimaVariacionPrecioUnTipoProducto(int idtipo_p)
        {
            VarPrecioTipoP varPrecio = new VarPrecioTipoP();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Tipo_Ultimo_Var_Precio";
            cmd.Parameters.AddWithValue("@idtipo", idtipo_p);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    varPrecio.Id = (int)reader["id"];
                    varPrecio.IdTipoP = (int)reader["idtipo"];
                    varPrecio.FechaVar = (DateTime)reader["fecha_var"];
                    varPrecio.Porcentaje = (decimal)reader["porcentaje"];
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return varPrecio;
        }

        public void CateProductoAlta(int _idtipo, string _nombre, decimal _costo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Alta_Categoria";
            cmd.Parameters.AddWithValue("@idtipo", _idtipo);
            cmd.Parameters.AddWithValue("@nombre", _nombre);
            cmd.Parameters.AddWithValue("@costocate", _costo);
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

        public void CateProductoModi(int _id, string _nom, decimal _costo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Modi_Categoria";
            cmd.Parameters.AddWithValue("@idcate", _id);
            cmd.Parameters.AddWithValue("@nombre", _nom);
            cmd.Parameters.AddWithValue("@costocate", _costo);
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

        public void CateProductoBaja(int _id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Baja_Categoria";
            cmd.Parameters.AddWithValue("@idcate", _id);
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

        public void SegProductoAlta(int _idcate, string _nombre, decimal precio)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Alta_Segmento";
            cmd.Parameters.AddWithValue("@idcate", _idcate);
            cmd.Parameters.AddWithValue("@nombre", _nombre);
            cmd.Parameters.AddWithValue("@precio", precio);
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

        public void SegProductoModi(SegmentoP segmento)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Modi_Segmento";
            cmd.Parameters.AddWithValue("@idseg", segmento.IdSegmentoP);
            cmd.Parameters.AddWithValue("@nombre", segmento.NombreSeg);
            cmd.Parameters.AddWithValue("@precio", segmento.CostoReposicion);
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

        public void SegProductoBaja(int _id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Baja_Segmento";
            cmd.Parameters.AddWithValue("@idseg", _id);
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

        public ObservableCollection<TipoProducto> ListarTipos()
        {
            ObservableCollection<TipoProducto> tipoProductos = new ObservableCollection<TipoProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Tipos";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TipoProducto tipo = new TipoProducto();
                    tipo.IdTipoP = (int)reader["idtipo_p"];
                    tipo.NomTipo = (string)reader["nomtipo"];
                    tipoProductos.Add(tipo);

                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return tipoProductos;
        }

        public ObservableCollection<CategoriaP> ListarCategoriasTipo(int idtipo)
        {
            ObservableCollection<CategoriaP> categoriaPs = new ObservableCollection<CategoriaP>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Categorias_Tipo";
            cmd.Parameters.AddWithValue("@idtipo", idtipo);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CategoriaP categoria = new CategoriaP();
                    categoria.IdCateP = (int)reader["idcate_p"];
                    categoria.IdTipoP = (int)reader["idtipo_P"];
                    categoria.NomCateP = (string)reader["nomcate_p"];
                    categoriaPs.Add(categoria);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return categoriaPs;
        }

        public ObservableCollection<CategoriaP> ListarCategorias()
        {
            ObservableCollection<CategoriaP> categoriaPs = new ObservableCollection<CategoriaP>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Categorias_Todas";

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CategoriaP categoria = new CategoriaP();
                    categoria.IdCateP = (int)reader["idcate_p"];
                    categoria.IdTipoP = (int)reader["idtipo_P"];
                    categoria.NomCateP = (string)reader["nomcate_p"];
                    categoriaPs.Add(categoria);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return categoriaPs;
        }


        public ObservableCollection<ConsultaExistencias> ConsultaExistenciaEnObra(int idcategoria)
        {
            ObservableCollection<ConsultaExistencias> categoriaPs = new ObservableCollection<ConsultaExistencias>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Consulta_Existencia_Categorias";
            cmd.Parameters.AddWithValue("@idcategoria", idcategoria);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ConsultaExistencias cta = new ConsultaExistencias();
                    cta.IdProducto = (int)reader["idproducto"];
                    cta.IdEmpleado = (int)reader["idempleado"];
                    cta.IdDeposito = 0;
                    cta.CantDisponible = 0;
                    cta.CodInventario = (string)reader["cod_inventario"];
                    cta.DifCantidad = (int)reader["difcantidad"];
                    cta.Imputacion = (int)reader["imputacion"];
                    cta.NombreDeposito = "";
                    cta.NombreEmpleado = (string)reader["nom_empleado"];
                    cta.NombreProducto = (string)reader["nombre"];
                    categoriaPs.Add(cta);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return categoriaPs;
        }


        public ObservableCollection<ConsultaExistencias> ConsultaExistenciaEnStock(int idcategoria)
        {
            ObservableCollection<ConsultaExistencias> categoriaPs = new ObservableCollection<ConsultaExistencias>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Consuta_Existencia_Categorias_Stock";
            cmd.Parameters.AddWithValue("@idcategoria", idcategoria);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ConsultaExistencias cta = new ConsultaExistencias();
                    cta.IdProducto = (int)reader["idproducto"];
                    cta.IdEmpleado = 0;
                    cta.IdDeposito = (int)reader["iddeposito"];
                    cta.CantDisponible = (decimal)reader["stkdisponible"];
                    cta.CodInventario = (string)reader["cod_inventario"];
                    cta.DifCantidad = 0;
                    cta.Imputacion = 0;
                    cta.NombreDeposito = (string)reader["nomdepo"];
                    cta.NombreEmpleado = "";
                    cta.NombreProducto = (string)reader["nombre"];
                    categoriaPs.Add(cta);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return categoriaPs;
        }

        public ObservableCollection<SegmentoP> ListarSegmentoCategoria(int idcategoria)
        {
            ObservableCollection<SegmentoP> segmentoPs = new ObservableCollection<SegmentoP>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Segmentos_Categoria";
            cmd.Parameters.AddWithValue("@idcategoria", idcategoria);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SegmentoP segmento = new SegmentoP();
                    segmento.IdSegmentoP = (int)reader["idsegp"];
                    segmento.IdCateP = (int)reader["idcate_p"];
                    segmento.NombreSeg = (string)reader["nombre_seg"];
                    segmento.CostoReposicion = (decimal)reader["pp_segmento"];
                    segmentoPs.Add(segmento);
                }

                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return segmentoPs;
        }

        public ObservableCollection<SegmentoP> ListarSegmentos()
        {
            ObservableCollection<SegmentoP> segmentoPs = new ObservableCollection<SegmentoP>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Segmentos_Todos";

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SegmentoP segmento = new SegmentoP();
                    segmento.IdSegmentoP = (int)reader["idsegp"];
                    segmento.IdCateP = (int)reader["idcate_p"];
                    segmento.NombreSeg = (string)reader["nombre_seg"];
                    segmentoPs.Add(segmento);
                }

                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return segmentoPs;
        }

        public SegmentoP BuscarUnSegmento(int idsegmento)
        {
            SegmentoP segmento = new SegmentoP();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Buscar_Un_Segmento";
            cmd.Parameters.AddWithValue("@id", idsegmento);
           
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    segmento.IdSegmentoP = (int)reader["idsegp"];
                    segmento.NombreSeg = (string)reader["nombre_seg"];
                    segmento.CostoReposicion = (decimal)reader["pp_segmento"];
                    segmento.IdCateP = (int)reader["idcate_p"];
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return segmento;
        }

        public ObservableCollection<MarcaProductos> ListarMarcas()
        {
            ObservableCollection<MarcaProductos> marcaProductos = new ObservableCollection<MarcaProductos>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Marcas";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MarcaProductos tipo = new MarcaProductos();
                    tipo.IdMarca = (int)reader["idmarca_p"];
                    tipo.NombreMarca = (string)reader["nombre_marca"];

                    marcaProductos.Add(tipo);

                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return marcaProductos;
        }

        public int MarcaProductoAlta(string _nombreMarca)
        {
            int _rows = 0; ;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Marca_Poducto_Alta";
            cmd.Parameters.AddWithValue("@nombremarca", _nombreMarca);
            try
            {
                conn.AbriConexion();
                _rows = cmd.ExecuteNonQuery();
                conn.CerrarConexion();


            }
            catch (Exception)
            {

                throw;
            }

            return _rows;
        }

        public int MarcaProductoModifica(int _idmarca, string _nombreMarca)
        {
            int _rows = 0; ;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Marca_Poducto_Modifica";
            cmd.Parameters.AddWithValue("@id", _idmarca);
            cmd.Parameters.AddWithValue("@nombremarca", _nombreMarca);
            try
            {
                conn.AbriConexion();
                _rows = cmd.ExecuteNonQuery();
                conn.CerrarConexion();


            }
            catch (Exception)
            {

                throw;
            }

            return _rows;
        }

        public int MarcaProductoBaja(int _idmarca)
        {
            int _rows = 0; ;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Marca_Producto_Baja";
            cmd.Parameters.AddWithValue("@id", _idmarca);
            try
            {
                conn.AbriConexion();
                _rows = cmd.ExecuteNonQuery();
                conn.CerrarConexion();


            }
            catch (Exception)
            {

                throw;
            }

            return _rows;
        }

        public List<CausaAjusteStock> ListaCausaAjusteStock()
        {
            List<CausaAjusteStock> lista_causa = new List<CausaAjusteStock>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Listar_Causas_Ajuste";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CausaAjusteStock ajusteStock = new CausaAjusteStock();
                    ajusteStock.IdCausaAjuste = (int)reader["idcausa"];
                    ajusteStock.NomCausa = (string)reader["nomcausa"];
                    lista_causa.Add(ajusteStock);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return lista_causa;
        }

        public ObservableCollection<Producto>ListarProductosDeUnTipo (int idtipo)
        {
            ObservableCollection<Producto> lista = new ObservableCollection<Producto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Listar_Todos_De_Un_Tipo";
            cmd.Parameters.AddWithValue("@idtipo", idtipo);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Producto producto = new Producto();
                    producto.IdProducto = (int)reader["idproducto"];
                    producto.Nombre = (string)reader["nombre"];
                    producto.CodInventario = (string)reader["cod_inventario"];
                    producto.Accesorios = (string)reader["accesorios"];
                    producto.EstadoItem = (int)reader["idestado"];
                    producto.AltaF = (DateTime)reader["altaf"];
                    if (reader["bajaf"] != DBNull.Value)
                    {
                        producto.BajaF = (DateTime)reader["bajaf"];
                    }
                    else
                    {
                        producto.BajaF = null;
                    }

                    producto.CodigoBarra = (int)reader["codigo_barra"];
                    producto.Color = (string)reader["color"];
                    producto.CondicionStk = (int)reader["condicion_stk"];
                    if (producto.CondicionStk == 1)
                    {
                        producto.TipoSituacionStk = "En Uso";
                    }
                    else
                    {
                        if (producto.CondicionStk == 2)
                        {
                            producto.TipoSituacionStk = "Discontinuado";
                        }
                        else
                        {
                            producto.TipoSituacionStk = "Obsoleto";
                        }
                    }
                    producto.ControlSf = (int)reader["control_sf"];
                    producto.CostoReposicion = (decimal)reader["costo_reposicion"];
                    producto.Descripcion = (string)reader["descripcion"];
                    producto.Dimensiones = (string)reader["dimensiones"];
                    producto.EsKit = (int)reader["es_kit"];
                    producto.IdTipoP = (int)reader["idtipo_p"];
                    producto.IdCateP = (int)reader["idcate_p"];
                    producto.IdSegP = (int)reader["idseg_p"];
                    producto.IdMarcaP = (int)reader["idmarca_p"];
                    producto.Idsf = (int)reader["idsf"];
                    producto.Modelo = (string)reader["modelo"];
                    producto.NumeroKit = (int)reader["numero_kit"];
                    producto.NumSerie = (string)reader["numserie"];
                    producto.PrecioUnitario = (decimal)reader["precio_unitario"];
                    producto.Marca = (string)reader["nombre_marca"];
                    producto.Tipo = (string)reader["nomtipo"];
                    producto.Categoria = (string)reader["nomcate_p"];
                    producto.Segmento = (string)reader["nombre_seg"];
                    producto.Situacion = (string)reader["nomsf"];
                    lista.Add(producto);
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

        #region Stock

        public ObservableCollection<StockProducto> SelectorProducto()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "StockProducto_Deposito";
            cmd.CommandType = CommandType.StoredProcedure;
            ObservableCollection<StockProducto> stockProductos = new ObservableCollection<StockProducto>();
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                stockProductos = ArmarStockProductos(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return stockProductos;
        }

        public ObservableCollection<StockProducto> SelectorProductoPorDeposito(int _iddeposito)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "StockProducto_Deposito_Uno";
            cmd.Parameters.AddWithValue("@iddeposito", _iddeposito);
            cmd.CommandType = CommandType.StoredProcedure;
            ObservableCollection<StockProducto> stockProductos = new ObservableCollection<StockProducto>();
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                stockProductos = ArmarStockProductos(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return stockProductos;
        }

        private ObservableCollection<StockProducto> ArmarStockProductos(SqlDataReader reader)
        {
            ObservableCollection<StockProducto> lista = new ObservableCollection<StockProducto>();
            while (reader.Read())
            {
                StockProducto stock = new StockProducto();
                stock.IdStk = (int)reader["idstk"];
                stock.IdProducto = (int)reader["idproducto"];
                stock.IdDeposito = (int)reader["iddeposito"];
                stock.StkActual = (decimal)reader["stkactual"];
                stock.StkDisponible = (decimal)reader["stkdisponible"];
                stock.StkReservado = (decimal)reader["stkreservado"];
                stock.PuntoPedido = (decimal)reader["puntopedido"];
                stock.ControlPP = (int)reader["controlpp"];
                stock.CostoStk = (decimal)reader["costostk"];
                stock.Estante = (string)reader["estante"];
                stock.Fila = (string)reader["fila"];
                stock.Frente = (string)reader["frente"];
                stock.Columna = (string)reader["columna"];
                stock.NombreProducto = (string)reader["nombre"];
                stock.ModeloProducto = (string)reader["modelo"];
                stock.CodInventario = (string)reader["cod_inventario"];
                stock.NumSerieProducto = (string)reader["numserie"];
                stock.Marca = (string)reader["nom_marca"];
                stock.Deposito = (string)reader["nomdepo"];
                stock.TipoProducto = (string)reader["nomtipo"];
                stock.Categoria = (string)reader["nomcate_p"];
                lista.Add(stock);

            }

            return lista;
        }

        public void ActualizarStock(int _idproducto, int _cantidad, int _iddeposito, string _operacion, decimal _preciop)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Stk_Producto_Actualizar";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@cantidad", _cantidad);
            cmd.Parameters.AddWithValue("@iddeposito", _iddeposito);
            cmd.Parameters.AddWithValue("@operacion", _operacion);
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

        public void AltaEnStock(int _idproducto, int _cantidad, int _iddeposito)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Stk_Producto_Alta_En_Stock";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@cantidad", _cantidad);
            cmd.Parameters.AddWithValue("@iddeposito", _iddeposito);
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

        public ObservableCollection<StockProducto> StockDeUnProducto(int _idproducto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Stock_Producto_Uno";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            ObservableCollection<StockProducto> stockProductos = new ObservableCollection<StockProducto>();
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                stockProductos = ArmarStockProductos(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return stockProductos;
        }

        public void ActualizarPuntoPedido(int _idstk, int _iddepo, decimal _nuevopp)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Cambiar_PP";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idstk", _idstk);
            cmd.Parameters.AddWithValue("@iddepo", _iddepo);
            cmd.Parameters.AddWithValue("@nuevopp", _nuevopp);
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

        public void ActualizarPosicionProducto(int _idstk, int _iddepo, string frente, string col, string estante, string fila)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Cambiar_Posicion";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idstock", _idstk);
            cmd.Parameters.AddWithValue("@iddepo", _iddepo);
            cmd.Parameters.AddWithValue("@frente", frente);
            cmd.Parameters.AddWithValue("@columna", col);
            cmd.Parameters.AddWithValue("@estante", estante);
            cmd.Parameters.AddWithValue("@fila", fila);
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

        public void AjustarStockUnProducto(int _idusuario, int _idproducto, int _cantidad, int _iddeposito, int _idcausa)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Ajuste_stock";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fecha_ajuste", DateTime.Today);
            cmd.Parameters.AddWithValue("@idusuario", _idusuario);
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@cantidad", _cantidad);
            cmd.Parameters.AddWithValue("@iddeposito", _iddeposito);
            cmd.Parameters.AddWithValue("@idcausa", _idcausa);
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


        public List<SituacionStk> ListarSituacionStk()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Situacion_stock_Listar_todos";
            cmd.CommandType = CommandType.StoredProcedure;
            List<SituacionStk> lista = new List<SituacionStk>();
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SituacionStk stk = new SituacionStk();
                    stk.IdSituacionStk = (int)reader["idsituacionstk"];
                    stk.NombreSituacion = (string)reader["nom_stk"];
                    lista.Add(stk);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public List<AjusteStockProducto> ListarAjustesUnProducto(int _idproducto)
        {
            List<AjusteStockProducto> list_ajustes = new List<AjusteStockProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Listar_Ajustes_Stock_Un_Producto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AjusteStockProducto aj = new AjusteStockProducto();
                    aj.Id = (int)reader["idjstk"];
                    aj.IdProducto = (int)reader["idproducto"];
                    aj.Cantidad = (int)reader["cantidad"];
                    aj.Fecha = (DateTime)reader["fecha_ajuste"];
                    aj.IdDeposito = (int)reader["iddeposito"];
                    aj.IdUsuario = (int)reader["idusuario"];
                    aj.IdCausa = (int)reader["idcausa_ajuste"];
                    aj.NomDeposito = (string)reader["nomdepo"];
                    aj.NomUsuario = (string)reader["nomuser"];
                    aj.NomCausa = (string)reader["nomcausa"];
                    list_ajustes.Add(aj);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return list_ajustes;
        }

        public void CambiarSituacionStockUnProducto(int _idpro, int _idsituacion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Actualizar_Situacion_Stk";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idpro);
            cmd.Parameters.AddWithValue("@nuevacondicion", _idsituacion);

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

        public void RecalcularCostoStock(int idproducto, int iddeposito, decimal precio_producto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "StockProducto_Recalcular_Costo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", idproducto);
            cmd.Parameters.AddWithValue("@precio_producto", precio_producto);
            cmd.Parameters.AddWithValue("@iddeposito", iddeposito);

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

        public decimal StockUnProductoUnDeposito(int _idproducto, int iddeposito)
        {
            decimal stockProducto = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "StockProducto_Uno_Un_Deposito";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@iddeposito", iddeposito);
            cmd.Parameters.Add("@resultado", SqlDbType.Decimal).Direction = ParameterDirection.Output;

            try
            {
                conn.AbriConexion();
                cmd.ExecuteNonQuery();
                stockProducto = Convert.ToDecimal(cmd.Parameters["@resultado"].Value);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return stockProducto;
        }

        public void BajaUnProducto(int idproducto, int idcausabaja, int idusuario, int imputacion, string descripcion, DateTime fecha)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Baja_Uno";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", idproducto);
            cmd.Parameters.AddWithValue("@idcausabaja", idcausabaja);
            cmd.Parameters.AddWithValue("@descripcion", descripcion);

            cmd.Parameters.AddWithValue("@imputacion", imputacion);
            cmd.Parameters.AddWithValue("@fechabaja", fecha);
            cmd.Parameters.AddWithValue("@idusuario", idusuario);
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

        public void LiquidarElStockPorBaja(int _idproducto, int iddepo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_LiquidarStock_Por_Baja";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@iddepo", iddepo);
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

        public ObservableCollection<CategoriaP> ListarStockActualCategoriasIndumentaria()
        {
            ObservableCollection<CategoriaP> lista_stock = new ObservableCollection<CategoriaP>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Indumentaria_Stock_Actual_Todos_Depositos";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CategoriaP ctp = new CategoriaP();
                    ctp.IdCateP = (int)reader["idcate_p"];
                    ctp.NomCateP = (string)reader["nomcate_p"];
                    ctp.StockActual = (decimal)reader["stockactual"];
                    lista_stock.Add(ctp);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;
        }

        public ObservableCollection<StockProducto> ListarStockActualIndumentariaUnaCategoria(int idcategoria, int iddeposito)
        {
            ObservableCollection<StockProducto> lista_stock = new ObservableCollection<StockProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Indumentaria_Detalle_Existencia_Una_Categoria_En_Deposito";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddeposito", iddeposito);
            cmd.Parameters.AddWithValue("@idcatep", idcategoria);

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StockProducto st = new StockProducto();
                    st.IdProducto = (int)reader["idproducto"];
                    st.NombreProducto = (string)reader["nombre"];
                    st.ModeloProducto = (string)reader["modelo"];
                    st.Marca = (string)reader["nombre_marca"];
                    st.StkActual = (decimal)reader["stockactual"];
                    st.CodInventario = (string)reader["cod_inventario"];
                    st.PuntoPedido = (decimal)reader["puntopedido"];
                    //acemos el calculo para indicar la situacion del stock del producto
                    if (st.PuntoPedido >= st.StkActual)
                    {
                        st.SituacionStock = "Comprar";
                    }
                    lista_stock.Add(st);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;
        }


        public ObservableCollection<CategoriaP> ListarEntregasIndumentariaAnio(int _anio, int _iddepo)
        {
            ObservableCollection<CategoriaP> lista_stock = new ObservableCollection<CategoriaP>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Indumentaria_Entregas_Categorias_Anio";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio", _anio);
            cmd.Parameters.AddWithValue("@iddepo", _iddepo);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CategoriaP ctp = new CategoriaP();
                    ctp.IdCateP = (int)reader["idcate_p"];
                    ctp.NomCateP = (string)reader["nomcate_p"];
                    ctp.StockActual = (int)reader["cantidad_cate"];
                    lista_stock.Add(ctp);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;
        }

        public ObservableCollection<StockProducto> ListarDetalleUnaCategoriaAnio(int _idcate, int _iddepo, int _anio)
        {
            ObservableCollection<StockProducto> lista_stock = new ObservableCollection<StockProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Indumentaria_Entrega_Detalle_Una_Categoria_Anio";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idcatep", _idcate);
            cmd.Parameters.AddWithValue("@iddepo", _iddepo);
            cmd.Parameters.AddWithValue("@anio", _anio);

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StockProducto st = new StockProducto();
                    st.IdProducto = (int)reader["codigo_item"];
                    st.NombreProducto = (string)reader["nombre"];
                    st.ModeloProducto = (string)reader["modelo"];
                    st.Marca = (string)reader["nombre_marca"];
                    st.StkActual = (int)reader["cantidad"];
                    st.IdSegmento = (int)reader["idseg_p"];

                    lista_stock.Add(st);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;

        }

        public ObservableCollection<CategoriaP> ListarEntregasIndumentarioF1F2(int _iddepo, DateTime _desde, DateTime _hasta)
        {
            ObservableCollection<CategoriaP> lista_stock = new ObservableCollection<CategoriaP>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Indumentaria_Entregas_Categorias_F1F2";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iddepo", _iddepo);
            cmd.Parameters.AddWithValue("@fdesde", _desde);
            cmd.Parameters.AddWithValue("@fhasta", _hasta);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CategoriaP ctp = new CategoriaP();
                    ctp.IdCateP = (int)reader["idcate_p"];
                    ctp.NomCateP = (string)reader["nomcate_p"];
                    ctp.StockActual = (int)reader["cantidad_cate"];
                    lista_stock.Add(ctp);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;
        }

        public ObservableCollection<StockProducto> ListarDetalleUnaCategoriaF1F2(int _idcate, int _iddepo, DateTime _desde, DateTime _hasta)
        {
            ObservableCollection<StockProducto> lista_stock = new ObservableCollection<StockProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Indumentaria_Entrega_Detalle_Una_Categoria_F1F2";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idcatep", _idcate);
            cmd.Parameters.AddWithValue("@iddepo", _iddepo);
            cmd.Parameters.AddWithValue("@fdesde", _desde);
            cmd.Parameters.AddWithValue("@fhasta", _hasta);

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StockProducto st = new StockProducto();
                    st.IdProducto = (int)reader["codigo_item"];
                    st.NombreProducto = (string)reader["nombre"];
                    st.ModeloProducto = (string)reader["modelo"];
                    st.Marca = (string)reader["nombre_marca"];
                    st.StkActual = (int)reader["cantidad"];

                    lista_stock.Add(st);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;
        }


        // stock activo
        public ObservableCollection<StockTipoProducto> ListarStockActualTipoProductos(int _iddeposito, int _idsf)
        {
            ObservableCollection<StockTipoProducto> lista_stock = new ObservableCollection<StockTipoProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "StockGestion_Costo_Cantidad_Tipo_Producto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddeposito", _iddeposito);
            cmd.Parameters.AddWithValue("@idsf", _idsf);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    StockTipoProducto st = new StockTipoProducto();
                    st.IdTipoP = (int)reader["idtipo_p"];
                    st.NombreTipo = (string)reader["nomtipo"];
                    st.StockActual = (decimal)reader["stockactual"];
                    st.CostoStockActual = (decimal)reader["costostk"];
                    st.IdDeposito = _iddeposito;
                    lista_stock.Add(st);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;
        }

        //stock punto de pedido
        public ObservableCollection<StockTipoProducto> ListarStockPPTipoProducto(int _iddeposito, int _idsf)
        {
            ObservableCollection<StockTipoProducto> lista_stock = new ObservableCollection<StockTipoProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "StockGestion_PP_Tipo_Producto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddeposito", _iddeposito);
            cmd.Parameters.AddWithValue("@idsf", _idsf);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    StockTipoProducto st = new StockTipoProducto();
                    st.IdTipoP = (int)reader["idtipo_p"];
                    st.NombreTipo = (string)reader["nomtipo"];
                    st.StockActual = (int)reader["cantProductos"];
                    st.CostoStockActual = (decimal)reader["costostk"];
                    st.IdDeposito = _iddeposito;
                    lista_stock.Add(st);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;
        }

        //stock mantenimiento
        public ObservableCollection<StockTipoProducto> ListarTiposProductosEnMantenimiento()
        {
            ObservableCollection<StockTipoProducto> lista_stock = new ObservableCollection<StockTipoProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "StockGestion_Cantidad_TipoProducto_Mante";
            cmd.CommandType = CommandType.StoredProcedure;
          
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    StockTipoProducto st = new StockTipoProducto();
                    st.IdTipoP = (int)reader["idtipo_p"];
                    st.NombreTipo = (string)reader["nomtipo"];
                    st.StockActual = (decimal)reader["CantProductos"];
                    st.CostoStockActual = (decimal)reader["costoMante"];
                    st.IdDeposito = 0;
                    lista_stock.Add(st);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;
        }

        public ObservableCollection<StockCategoriaProducto>ListarCantCateXTipoProMante(int idtipo_p)
        {
            ObservableCollection<StockCategoriaProducto> lista_stock = new ObservableCollection<StockCategoriaProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "StockGestion_Cantidad_Categoria_TipoProducto_Mante";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idtipo", idtipo_p);
           
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StockCategoriaProducto st = new StockCategoriaProducto();
                    
                    st.IdCategoria = (int)reader["idcate_p"];
                    st.NombreCate = (string)reader["nomcate_p"];
                    st.NombreTipo = (string)reader["nomtipo"];
                    st.StockActual = (decimal)reader["CantProductos"];
                    st.CostoStock = (decimal)reader["costoMante"];
                    st.IdTipoP = idtipo_p;
                    lista_stock.Add(st);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;
        }

        public ObservableCollection<StockProducto>ListarCantProXCateMante(int _idcate)
        {
            ObservableCollection<StockProducto> lista_stock = new ObservableCollection<StockProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "StockGestion_Cantidad_Producto_Categoria_Mante";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idcate", _idcate);

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StockProducto st = new StockProducto();

                    st.IdProducto = (int)reader["idproducto"];
                    st.StkDisponible = (decimal)reader["stkdisponible"];
                    st.IdDeposito = (int)reader["iddeposito"];
                    st.NombreProducto = (string)reader["nombre"];
                    st.CodInventario = (string)reader["cod_inventario"];
                    st.ModeloProducto = (string)reader["modelo"];
                    st.Marca = (string)reader["nombre_marca"];
                    
                    lista_stock.Add(st);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;

        }
        //
         //stock punto pedido   
        public ObservableCollection<StockCategoriaProducto>ListarStockPPCategoriasTipoProducto(int idtipo_p, int _iddeposito, int _idsf)
        {
            ObservableCollection<StockCategoriaProducto> lista_stock = new ObservableCollection<StockCategoriaProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "StockGestion_PP_Cantidad_Categoria_Un_Tipo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idtipo", idtipo_p);
            cmd.Parameters.AddWithValue("@iddeposito", _iddeposito);
            cmd.Parameters.AddWithValue("@idsf", _idsf);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StockCategoriaProducto st = new StockCategoriaProducto();
                    st.IdCategoria = (int)reader["idcate_p"];
                    st.NombreCate = (string)reader["nomcate_p"];
                    st.PuntoPedido = (decimal)reader["puntopedido"];
                    st.StockActual = (int)reader["cantProductos"];
                    st.CostoStock = (decimal)reader["costostk"];
                    st.IdTipoP = idtipo_p;
                    lista_stock.Add(st);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;
        }

        public ObservableCollection<StockProducto>ListarCantProXCatePP(int _idcate, int _iddeposito, int _idsf)
        {
            ObservableCollection<StockProducto> lista_stock = new ObservableCollection<StockProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "StockGestion_Cantidad_Producto_Categoria_PP";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idcate", _idcate);
            cmd.Parameters.AddWithValue("@iddeposito", _iddeposito);
            cmd.Parameters.AddWithValue("@idsf", _idsf);

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StockProducto st = new StockProducto();

                    st.IdProducto = (int)reader["idproducto"];
                    st.StkDisponible = (decimal)reader["stkdisponible"];
                    st.PuntoPedido = (decimal)reader["puntopedido"];
                    st.IdDeposito = (int)reader["iddeposito"];
                    st.NombreProducto = (string)reader["nombre"];
                    st.CodInventario = (string)reader["cod_inventario"];
                    st.ModeloProducto = (string)reader["modelo"];
                    st.Marca = (string)reader["nombre_marca"];

                    lista_stock.Add(st);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;
        }

        public List<StockProducto>ListarProductosEnPP( int iddeposito)
        {
            List<StockProducto> lista_stock = new List<StockProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "StockGestion_Productos_En_PP";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddeposito", iddeposito);

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StockProducto st = new StockProducto();

                    st.IdProducto = (int)reader["idproducto"];
                    st.StkActual = (decimal)reader["stkactual"];
                    st.PuntoPedido = (decimal)reader["puntopedido"];
                    st.CostoStk = (decimal)reader["precio_unitario"];
                    st.NombreProducto = (string)reader["nombre"];
                    st.CodInventario = (string)reader["cod_inventario"];
                    st.ModeloProducto = (string)reader["modelo"];
                   

                    lista_stock.Add(st);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;
        }

        //stock activo
        public ObservableCollection<StockCategoriaProducto>ListarStockActualCategoriasTipoProducto(int idtipo_p, int _iddeposito, int _idsf)
        {
            ObservableCollection<StockCategoriaProducto> lista_stock = new ObservableCollection<StockCategoriaProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "StockGestion_Costo_Cantidad_Categoria_Un_Tipo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idtipo_p", idtipo_p);
            cmd.Parameters.AddWithValue("@iddeposito", _iddeposito);
            cmd.Parameters.AddWithValue("@idsf", _idsf);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StockCategoriaProducto st = new StockCategoriaProducto();
                    st.IdCategoria = (int)reader["idcate_p"];
                    st.NombreCate = (string)reader["nomcate_p"];
                    st.NombreTipo = (string)reader["nomtipo"];
                    st.StockActual = (decimal)reader["stockactual"];
                    st.CostoStock = (decimal)reader["costostk"];
                    st.IdTipoP = idtipo_p;
                    lista_stock.Add(st);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;
        }
        public ObservableCollection<StockProducto>ListarCantProXCateActivo(int _idcate, int _iddeposito, int _idsf)
        {
            ObservableCollection<StockProducto> lista_stock = new ObservableCollection<StockProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "StockGestion_Cantidad_Producto_Categoria_Activo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idcate", _idcate);
            cmd.Parameters.AddWithValue("@iddeposito", _iddeposito);
            cmd.Parameters.AddWithValue("@idsf", _idsf);

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StockProducto st = new StockProducto();

                    st.IdProducto = (int)reader["idproducto"];
                    st.StkDisponible = (decimal)reader["stkdisponible"];
                    st.PuntoPedido = (decimal)reader["puntopedido"];
                    st.IdDeposito = (int)reader["iddeposito"];
                    st.NombreProducto = (string)reader["nombre"];
                    st.CodInventario = (string)reader["cod_inventario"];
                    st.ModeloProducto = (string)reader["modelo"];
                    st.Marca = (string)reader["nombre_marca"];

                    lista_stock.Add(st);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_stock;
        }

        

        #endregion

        #region Compras

        public void CompraEncabezadoAlta(CompraP compraP)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Compra_Encabezado_Alta";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idprove", compraP.IdProve);
            cmd.Parameters.AddWithValue("@idusuario", compraP.IdUsuario);
            cmd.Parameters.AddWithValue("@altaf", compraP.Alta);
            cmd.Parameters.AddWithValue("@fechaOC", compraP.FechaCompra);
            cmd.Parameters.AddWithValue("@fechafactura", compraP.FechaFactura);
            cmd.Parameters.AddWithValue("@orden_compra", compraP.OC);
            cmd.Parameters.AddWithValue("@numfactura", compraP.NumFactura);
            cmd.Parameters.AddWithValue("@importe_compra", compraP.ImporteCompra);
            cmd.Parameters.AddWithValue("@observaciones", compraP.Observaciones);
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
        public void CompraDetalleAlta(CompraPDetalle compraPDetalle)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Compra_Detalle_Alta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idcompra", compraPDetalle.IdCompra);
            cmd.Parameters.AddWithValue("@idremito", compraPDetalle.IdRemito);
            cmd.Parameters.AddWithValue("@idproducto", compraPDetalle.IdProducto);
            cmd.Parameters.AddWithValue("@cantidad", compraPDetalle.Cantidad);
            cmd.Parameters.AddWithValue("@precio_unit", compraPDetalle.PrecioItem);
            cmd.Parameters.AddWithValue("@total_item", compraPDetalle.TotalItem);
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

        public void CompraBorrarUna( int idcompra)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Compra_Borrar_Una";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idcompra",idcompra);
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
        public int ObtenerUltimoIdCompra()
        {
            int _ultimoIdDoc = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Compra_Obtener_UltimoId";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _ultimoIdDoc = (int)reader["idcompra"];

                }

                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }

            return _ultimoIdDoc;
        }
        public void ActualizarRegistradosDIP(int _idremito)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "DIP_Actualizar_Registrados";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddocumento", _idremito);
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

        public void ActualizarDetDocuARegistrado( int _idremito , int _idproducto)
        {
            //metodo que marca los item de un documento como registrado, usado en la registracio de una compra
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "DIP_Actualizar_Det_Documento";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddocumento", _idremito);
            cmd.Parameters.AddWithValue("@codigoitem", _idproducto);
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

        public void AnularRegistradosDIP(int _idremito)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "DIP_Anular_Registrados";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddocumento", _idremito);
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

        public void ActualizarPreciosProductos(int _idproducto, decimal _precio)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Productos_Actualizar_Precios_Compra";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@precioactual", _precio);
          
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
        public void ActualizarCostoStockUno(int _idproducto , decimal _precio)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Stock_Producto_Actualizar_Costo_Uno";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@precioactual", _precio);

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

        public ObservableCollection<CompraP>ComprasListarTodos()
        {
            ObservableCollection<CompraP> lista_compras = new ObservableCollection<CompraP>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Compra_Listar_Todos";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_compras = ArmarListaCompras(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_compras;
        }

        private ObservableCollection<CompraP> ArmarListaCompras(SqlDataReader reader)
        {
            ObservableCollection<CompraP> lista = new ObservableCollection<CompraP>();
            while (reader.Read())
            {
                CompraP compra = new CompraP();
                compra.IdCompra = (int)reader["idcompra"];
                compra.IdProve = (int)reader["idprove"];
                compra.IdUsuario = (int)reader["idusuario"];
                compra.Alta = (DateTime)reader["altaf"];
                if (reader["fecha_compra"] == DBNull.Value)
                {
                    compra.FechaCompra = null;
                }
                else
                {
                    compra.FechaCompra = (DateTime)reader["fecha_compra"];
                }
                compra.OC = (string)reader["orden_compra"];
                compra.NumFactura = (string)reader["num_factura"];
                compra.ImporteCompra = (decimal)reader["importe_compra"];
                compra.Observaciones = (string)reader["observaciones"];
                if (reader["fecha_factura"] == DBNull.Value)
                {
                    compra.FechaFactura = null;
                }
                else
                {
                    compra.FechaFactura = (DateTime)reader["fecha_factura"];
                }
                compra.NombreProveedor = (string)reader["razonsocial"];
                compra.NombreUsuario = (string)reader["nomuser"];
                lista.Add(compra);
            }
            return lista;
        }

        public ObservableCollection<CompraPDetalle>CompraListarDetalleUna(int _idcompra)
        {
            ObservableCollection<CompraPDetalle> lista_det_compras = new ObservableCollection<CompraPDetalle>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Compra_Listar_Detalle_Uno";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idcompra", _idcompra);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_det_compras = ArmarListaDetalleCompras(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_det_compras;
        }

        private ObservableCollection<CompraPDetalle> ArmarListaDetalleCompras(SqlDataReader reader)
        {
            ObservableCollection<CompraPDetalle> lista = new ObservableCollection<CompraPDetalle>();
            while (reader.Read())
            {
                CompraPDetalle detalle = new CompraPDetalle();
                detalle.IdDet = (int)reader["iddet_compra"];
                detalle.Cantidad = (int)reader["cantidad"];
                detalle.Deposito = (string)reader["nomdepo"];
                if (reader["fecha_rem_proveedor"] == DBNull.Value)
                {
                    detalle.FechaRemito = null;
                }
                else
                {
                    detalle.FechaRemito = (DateTime)reader["fecha_rem_proveedor"];
                }
                detalle.IdCompra = (int)reader["idcompra"];
                detalle.IdProducto = (int)reader["idproducto"];
                detalle.IdRemito = (int)reader["idremito"];
                detalle.MarcaProducto = (string)reader["nom_marca"];
                detalle.Modelo = (string)reader["modelo"];
                detalle.NomProducto = (string)reader["nombre"];
                detalle.PrecioItem = (decimal)reader["precio_unitario"];
                detalle.RemitoProveedor = (string)reader["remito_proveedor"];
                detalle.TotalItem = (decimal)reader["total_item"];
                detalle.FechaIngreso = (DateTime)reader["altaf"];
                lista.Add(detalle);
            }
            return lista;
        }

        public void ActualizarHistorialPrecios(int idproducto,decimal precio)
        {
            //genera una nueva entrada a la tabla de historial de precios del producto
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Alta_Historial_Precios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", idproducto);
            cmd.Parameters.AddWithValue("@nuevoprecio", precio);
            cmd.Parameters.AddWithValue("@alta", DateTime.Today);
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

        public ObservableCollection<CompraP>CompraListarUnProducto( int idproducto)
        {
            ObservableCollection<CompraP> lista_compras = new ObservableCollection<CompraP>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Compras_Listar_Un_Producto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", idproducto);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_compras = ArmarListaComprasUnProducto(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_compras;

        }

        private ObservableCollection<CompraP> ArmarListaComprasUnProducto(SqlDataReader reader)
        {
            ObservableCollection<CompraP> lista = new ObservableCollection<CompraP>();
            while (reader.Read())
            {
                CompraP compra = new CompraP();
                compra.IdCompra = (int)reader["idcompra"];
                compra.IdProve = (int)reader["idprove"];
                compra.IdUsuario = (int)reader["idusuario"];
                compra.Alta = (DateTime)reader["altaf"];
                if (reader["fecha_compra"] == DBNull.Value)
                {
                    compra.FechaCompra = null;
                }
                else
                {
                    compra.FechaCompra = (DateTime)reader["fecha_compra"];
                }
                compra.OC = (string)reader["orden_compra"];
                compra.NumFactura = (string)reader["num_factura"];
                compra.ImporteCompra = (decimal)reader["importe_compra"];
                compra.Observaciones = (string)reader["observaciones"];
                if (reader["fecha_factura"] == DBNull.Value)
                {
                    compra.FechaFactura = null;
                }
                else
                {
                    compra.FechaFactura = (DateTime)reader["fecha_factura"];
                }
                compra.NombreProveedor = (string)reader["razonsocial"];
                compra.NombreUsuario = (string)reader["nomuser"];
                compra.CantidadUno = (int)reader["cantidad"];
                compra.PrecioUniUno = (decimal)reader["precio_unitario"];
                compra.TotalItemUno = (decimal)reader["totalitem"];
                lista.Add(compra);
            }
            return lista;
        }

       
        #endregion

        #region Fotos
        public void GuardarFoto(FotoProducto foto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Foto_Grabar";
            cmd.Parameters.AddWithValue("@idproducto", foto.IdProducto);
            cmd.Parameters.AddWithValue("@foto", foto.Foto);
            cmd.Parameters.AddWithValue("@altaf", foto.AltaF);
            cmd.Parameters.AddWithValue("@descripcion", foto.DescriFoto);
            cmd.Parameters.AddWithValue("@titulo", foto.Titulo);

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

        public void BorrarFoto(int _idfoto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Foto_Borrar";
            cmd.Parameters.AddWithValue("@idfotop", _idfoto);
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

        public ObservableCollection<FotoProducto> ProductoListaFotos(int _idproducto)
        {
            ObservableCollection<FotoProducto> fotos = new ObservableCollection<FotoProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Listar_Fotos";
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FotoProducto f = new FotoProducto();
                    f.IdFotoP = (int)reader["idimgpro"];
                    f.AltaF = (DateTime)reader["altaf"];
                    f.DescriFoto = (string)reader["descripcion"];
                    f.IdProducto = (int)reader["idproducto"];
                    f.Titulo = (string)reader["titulo"];
                    f.Foto = (byte[])reader["foto"];
                    fotos.Add(f);

                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fotos;
        }

        #endregion

        #region Mantenimientos
         public void AltaMantenimiento(Mante_P mante_P)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Mante_Alta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", mante_P.IdProducto);
            cmd.Parameters.AddWithValue("@altaf", mante_P.AltaF);
            cmd.Parameters.AddWithValue("@idprove", mante_P.IdProve);
            cmd.Parameters.AddWithValue("@imputacion", mante_P.Imputacion);
            cmd.Parameters.AddWithValue("@numfactura", mante_P.NumFacturaProve);
            cmd.Parameters.AddWithValue("@numremito", mante_P.NumRemitoProve);
            cmd.Parameters.AddWithValue("@ocprove", mante_P.OCProve);
            cmd.Parameters.AddWithValue("@fecha_factura", mante_P.FechaFactura);
            //cmd.Parameters.AddWithValue("@idotm", mante_P.IdOtm);
            cmd.Parameters.AddWithValue("@importefactura", mante_P.ImporteFactura);
            cmd.Parameters.AddWithValue("@detalle", mante_P.DetalleMante);
            cmd.Parameters.AddWithValue("@idusuario", mante_P.IdUsuario);
            cmd.Parameters.AddWithValue("@idrma", mante_P.IdRma);
            

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

        public  void AnularMantenimiento( int _idmantep)
        {
            //este proc borra el registro del mantenimiento en la base de datos
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Mante_Alta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idmantep", _idmantep);
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

        public ObservableCollection<Mante_P>ListarManteUnProducto(int _idproducto)
        {
            //listar todos los mantenimientos de un producto
            ObservableCollection<Mante_P> mante_Ps = new ObservableCollection<Mante_P>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Mante_Listar_Todos_Uno";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Mante_P mante = new Mante_P();
                    mante.IdManteP = (int)reader["idmantep"];
                    mante.IdProducto = (int)reader["idproducto"];
                    mante.AltaF = (DateTime)reader["altaf"];
                    mante.NumFacturaProve = (string)reader["num_factura_prove"];
                    mante.NumRemitoProve = (string)reader["num_remito_prove"];
                    mante.OCProve = (string)reader["oc_prove"];
                    if (reader["fecha_factura"] == DBNull.Value)
                    {
                        mante.FechaFactura = null;
                    }
                    else {
                        mante.FechaFactura = (DateTime)reader["fecha_factura"];
                    };
                    if (reader["fecha_remito"] == DBNull.Value)
                    {
                        mante.FechaRemito = null;
                    }
                    else
                    {
                        mante.FechaRemito = (DateTime)reader["fecha_remito"];
                    };

                    mante.ImporteFactura = (decimal)reader["importe_factura"];
                    mante.Imputacion = (int)reader["imputacion"];
                    mante.IdOtm = (int)reader["idotm"];
                    mante.DetalleMante = (string)reader["detalle_mante"];
                    mante.ClienteObra = (string)reader["cliente"];
                    mante.RazonSocial = (string)reader["razonsocial"];
                    mante.IdUsuario = (int)reader["idusuario"];
                    mante.NomUsuario = (string)reader["nomuser"];
                    mante.NombreProducto = (string)reader["nombre"];
                    mante.CodInventario = (string)reader["cod_inventario"];
                    mante_Ps.Add(mante);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return mante_Ps;
        }

        public ObservableCollection<Mante_P> ListarTodosLosMantenimientos( DateTime desde, DateTime hasta)
        {
            //listar todos los mantenimientos de un producto
            ObservableCollection<Mante_P> mante_Ps = new ObservableCollection<Mante_P>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Mante_Listar_Todos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@f1", desde);
            cmd.Parameters.AddWithValue("@f2", hasta);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Mante_P mante = new Mante_P();
                    mante.IdManteP = (int)reader["idmantep"];
                    mante.IdProducto = (int)reader["idproducto"];
                    mante.AltaF = (DateTime)reader["altaf"];
                    mante.NumFacturaProve = (string)reader["num_factura_prove"];
                    mante.NumRemitoProve = (string)reader["num_remito_prove"];
                    mante.OCProve = (string)reader["oc_prove"];
                    if (reader["fecha_factura"] == DBNull.Value)
                    {
                        mante.FechaFactura = null;
                    }
                    else
                    {
                        mante.FechaFactura = (DateTime)reader["fecha_factura"];
                    };
                    if (reader["fecha_remito"] == DBNull.Value)
                    {
                        mante.FechaRemito = null;
                    }
                    else
                    {
                        mante.FechaRemito = (DateTime)reader["fecha_remito"];
                    };

                    mante.ImporteFactura = (decimal)reader["importe_factura"];
                    mante.Imputacion = (int)reader["imputacion"];
                    mante.IdOtm = (int)reader["idotm"];
                    mante.DetalleMante = (string)reader["detalle_mante"];
                    mante.ClienteObra = (string)reader["cliente"];
                    mante.RazonSocial = (string)reader["razonsocial"];
                    mante.IdUsuario = (int)reader["idusuario"];
                    mante.NomUsuario = (string)reader["nomuser"];
                    mante.NombreProducto = (string)reader["nombre"];
                    mante.CodInventario = (string)reader["cod_inventario"];
                    mante_Ps.Add(mante);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return mante_Ps;
        }

        public void NuevoRMAProducto(RMAProducto rMA)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "RMA_Alta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", rMA.IdProducto);
            cmd.Parameters.AddWithValue("@altaf", rMA.AltaF);
            cmd.Parameters.AddWithValue("@idproveedor", rMA.IdProveedor);
            cmd.Parameters.AddWithValue("@idtransporte", rMA.Idtransporte);
            cmd.Parameters.AddWithValue("@descricausa", rMA.CausaRma);
            cmd.Parameters.AddWithValue("@idusercrea", rMA.IdUserCrea);
            cmd.Parameters.AddWithValue("@imputacion", rMA.Imputacion);
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

        public void BajaRMAProducto(int idrma)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "RMA_Borrar_Una";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idrma",idrma);
          
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

        public void CambiarEstadoRMAProducto(int idrma, int idestado, DateTime _fecha, int _iduser)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "RMA_Cambiar_Estado_Una";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idrma", idrma);
            cmd.Parameters.AddWithValue("@idestado", idestado);
            cmd.Parameters.AddWithValue("@fecha", _fecha);
            cmd.Parameters.AddWithValue("@idusercumple", _iduser);

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

        public ObservableCollection<RMAProducto>ListarRMAUnProducto(int idproducto)
        {
            ObservableCollection<RMAProducto> lista_rma = new ObservableCollection<RMAProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "RMA_Listar_Todos_Un_Producto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", idproducto);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_rma = ArmarListaRMA(reader);
               
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_rma;
        }

        public int ObtenerUltimoIdRMA()
        {
            int _ultimoIdDoc = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RMA_Obtener_UltimoId";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _ultimoIdDoc = (int)reader["idrma"];

                }

                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }

            return _ultimoIdDoc;
        }

        public RMAProducto BuscarUnRMAPorId(int idrma)
        {
            RMAProducto rMA = new RMAProducto();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RMA_Buscar_Una";
            cmd.Parameters.AddWithValue("@idrma", idrma);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rMA.IdRma = (int)reader["idrma"];
                    rMA.AltaF = (DateTime)reader["altaf"];
                    if (reader["bajaf"] != DBNull.Value)
                    {
                        rMA.BajaF = (DateTime)reader["bajaf"];
                    }
                    else
                    {
                        rMA.BajaF = null;
                    }
                    rMA.CausaRma = (string)reader["descri_causa"];
                    rMA.idestadoRma = (int)reader["estado_rma"];
                    rMA.IdProducto = (int)reader["idproducto"];
                    rMA.IdProveedor = (int)reader["idproveedor"];
                    rMA.Idtransporte = (int)reader["idtransporte"];
                    rMA.NombreProducto = (string)reader["nombre"];
                    rMA.Modelo = (string)reader["modelo"];
                    rMA.CodInventario = (string)reader["cod_inventario"];
                    rMA.Serie = (string)reader["numserie"];
                    rMA.Marca = (string)reader["nombre_marca"];
                    rMA.NombrProveedor = (string)reader["razonsocial"];
                    rMA.NombreTransporte = (string)reader["transporte"];
                    rMA.NombreEstado = (string)reader["significado"];
                    rMA.IdUserCrea = (int)reader["idusercrea"];
                    rMA.IdUserCumple = (int)reader["idusercumple"];
                    rMA.UserCrea = (string)reader["creador"];
                    rMA.UserCumple = (string)reader["cumple"];
                    rMA.Imputacion = (int)reader["imputacion"];

                }

                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }

            return rMA;
        }

        public ObservableCollection<RMAProducto>RMAListarTodos()
        {
            ObservableCollection<RMAProducto> lista_rma = new ObservableCollection<RMAProducto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "RMA_Listar_Todos";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_rma = ArmarListaRMA(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_rma;
        }

        private ObservableCollection<RMAProducto> ArmarListaRMA(SqlDataReader reader)
        {
            ObservableCollection<RMAProducto> lista = new ObservableCollection<RMAProducto>();
            while (reader.Read())
            {
                RMAProducto rMA = new RMAProducto();
                rMA.IdRma = (int)reader["idrma"];
                rMA.AltaF = (DateTime)reader["altaf"];
                if (reader["bajaf"] != DBNull.Value)
                {
                    rMA.BajaF = (DateTime)reader["bajaf"];
                }
                else
                {
                    rMA.BajaF = null;
                }
                rMA.CausaRma = (string)reader["descri_causa"];
                rMA.idestadoRma = (int)reader["estado_rma"];
                rMA.IdProducto = (int)reader["idproducto"];
                rMA.IdProveedor = (int)reader["idproveedor"];
                rMA.Idtransporte = (int)reader["idtransporte"];
                rMA.NombreProducto = (string)reader["nombre"];
                rMA.Modelo = (string)reader["modelo"];
                rMA.CodInventario = (string)reader["cod_inventario"];
                rMA.Serie = (string)reader["numserie"];
                rMA.Marca = (string)reader["nombre_marca"];
                rMA.NombrProveedor = (string)reader["razonsocial"];
                rMA.NombreTransporte = (string)reader["transporte"];
                rMA.NombreEstado = (string)reader["significado"];
                rMA.IdUserCrea = (int)reader["idusercrea"];
                rMA.IdUserCumple = (int)reader["idusercumple"];
                rMA.UserCrea = (string)reader["creador"];
                rMA.UserCumple = (string)reader["cumple"];
                rMA.Imputacion = (int)reader["imputacion"];
                lista.Add(rMA);
            }
            return lista;
        }
        #endregion

        #region Movimientos
        public ObservableCollection<Documento>ListarMovObraUnProducto(int _idproducto)
        {
            ObservableCollection<Documento> lista_mov = new ObservableCollection<Documento>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_Listar_Movimientos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Documento dc = new Documento();
                    dc.Alta = (DateTime)reader["altaf"];
                    dc.FechaRemito = (DateTime)reader["fecharemito"];
                    dc.IdDocumento = (int)reader["iddocumento"];
                    dc.NumDocumento = (string)reader["num_documento"];
                    dc.Concepto = (string)reader["concepto"];
                    dc.Imputacion = (int)reader["imputacion"];
                    dc.ImputacionDestino = (int)reader["imputacion_destino"];
                    dc.IdEmpleado = (int)reader["idempleado"];
                    dc.IdEmpleadoDestino = (int)reader["idempleado_destino"];
                    dc.NombreObra = (string)reader["obra_origen"];
                    dc.ClienteObra = (string)reader["cliente_origen"];
                    dc.NOmbreObraDestino = (string)reader["obra_dest"];
                    dc.ClienteDestino = (string)reader["cliente_dest"];
                    dc.NombreEmpleado = (string)reader["emp_origen"];
                    dc.NombreEmpledoDestino = (string)reader["emp_destino"];
                    lista_mov.Add(dc);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_mov;
        }
        #endregion


        #region Costos

        public decimal CostoTotalComprasUnAnio(int _anio)
        {
            decimal _costo = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Productos_CostoTotalCompras_Por_Anio";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio",_anio);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _anio = (int)reader["Anio"];
                    _costo = (decimal)reader["IMPORTE"];
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return _costo;
        }

        public decimal CostoTotalMantenimientoUnAnio(int _anio)
        {
            decimal _costo = 0;
            //int anio = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Producto_CostoTotalMantenimiento_Por_Anio";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio", _anio);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _anio = (int)reader["Anio"];
                    _costo = (decimal)reader["IMPORTE"];
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return _costo;

        }


        //costo mensuales 
        public List<CostosGenericos> CostoMantenimientoAnioMes(int _anio)
        {
            List<CostosGenericos> lista = new List<CostosGenericos>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Costo_Mantenimiento_Mensual_Por_Anio";
            cmd.Parameters.AddWithValue("@anio", _anio);

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CostosGenericos detalleConsumo = new CostosGenericos();
                    detalleConsumo.Anio = _anio;
                    detalleConsumo.Mes = (int)reader["MES"];
                    detalleConsumo.Importe = (decimal)reader["IMPORTE"];
                    detalleConsumo.Moneda = "$";
                    detalleConsumo.TipoCosto = 2;
                    lista.Add(detalleConsumo);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        // costo de las compras mes a mes anual
        public List<CostosGenericos> CostoComprasAnioMes(int _anio)
        {
            List<CostosGenericos> lista = new List<CostosGenericos>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Compras_Mes_Mes_Por_Anio";
            cmd.Parameters.AddWithValue("@anio", _anio);

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CostosGenericos detalleConsumo = new CostosGenericos();
                    detalleConsumo.Anio = _anio;
                    detalleConsumo.Mes = (int)reader["MES"];
                    detalleConsumo.Importe = (decimal)reader["IMPORTE"];
                    detalleConsumo.Moneda = "$";
                    detalleConsumo.TipoCosto = 3;
                    lista.Add(detalleConsumo);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public List<CostosGenericos>CostosComprasTipoProductoAnio(int anio)
        {
            List<CostosGenericos> lista = new List<CostosGenericos>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Producto_Compras_TipoProducto_PorMEs_PorAnio";
            cmd.Parameters.AddWithValue("@anio", anio);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CostosGenericos detalleConsumo = new CostosGenericos();
                    detalleConsumo.Anio = anio;
                    detalleConsumo.Mes = (int)reader["Mes"];
                    detalleConsumo.Importe = (decimal)reader["IMPORTE"];
                    detalleConsumo.Moneda = "$";
                    detalleConsumo.TipoCosto = (int)reader["idtipo_p"];
                    detalleConsumo.NombreTipo = (string)reader["nomtipo"];
                    lista.Add(detalleConsumo);
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

    }
}
